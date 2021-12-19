using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RconTool
{
	public class ServerMessenger
	{

        //PLUS add an optional timeout parameter so the server messenger can disregard invalid messages and continue waiting for the correct response type
        //PLUS , feature, additionally, the servermessenger could save all the responses it receives before the correct one, and send them all back as a list so it would be a Task<List<string>> ServerMessenger function instead of the Task<string> functions currently implemented

        public ResponseType ExpectedResponseType { get; set; } = ResponseType.String;
        private bool ReceivedResponse { get; set; } = false;
        private string ResponseString { get; set; } = null;
        private Connection connection { get; set; } = null;

        public static async Task<string> GetNumericResponse(Connection connection, string message)
        {
            return await GetResponse(ResponseType.Numeric, connection, message);
        }
        public static async Task<string> GetJsonFileResponse(Connection connection, string message)
        {
            return await GetResponse(ResponseType.JsonFile, connection, message);
        }
        public static async Task<string> GetJsonResponse(Connection connection, string message)
        {
            return await GetResponse(ResponseType.JsonContent, connection, message);
        }
        public static async Task<string> GetChatResponse(Connection connection, string message)
        {
            return await GetResponse(ResponseType.Chat, connection, message);
        }
        public static async Task<string> GetStringResponse(Connection connection, string message)
        {
            return await GetResponse(ResponseType.String, connection, message);
        }
        private static async Task<string> GetResponse(ResponseType targetResponseType, Connection connection, string message)
        {
            ServerMessenger messenger = new ServerMessenger(targetResponseType, connection, message);
            return await messenger.GetResponse();
        }

        public ServerMessenger(ResponseType targetResponseType, Connection connection, string message, string hideResponseLike = null)
        {
            this.connection = connection;
            ExpectedResponseType = targetResponseType;
            connection.RconWebSocketMutex.WaitOne();
                connection.RconWebSocket.OnMessage += OnMessage;
            connection.RconWebSocketMutex.ReleaseMutex();
            connection.RconCommandQueue.Enqueue(
                RconCommand.ConsoleLogCommand(
                    message,
                    message,
                    "Server Messenger"
                )
            );
        }

        public async Task<string> GetResponse()
        {
            while (!ReceivedResponse)
            {
                await Task.Delay(50);
            }
            return ResponseString;
        }

        public void OnMessage(object sender, WebSocketSharp.MessageEventArgs e)
        {

            string message = e.Data;

            if (message == "accept") { return; }

            switch (ExpectedResponseType)
            {
                case ResponseType.Chat:
                    if (!string.IsNullOrEmpty(message))
                    {
                        ResponseString = message;
                        ReceivedResponse = true;
                    }
                    else { return; }
                    break;
                case ResponseType.String:
                    if (!string.IsNullOrEmpty(message))
                    {
                        ResponseString = message;
                        ReceivedResponse = true;
                    }
                    else { return; }
                    break;
                case ResponseType.Numeric:
                    if (!string.IsNullOrEmpty(message))
                    // && message.IsNumeric()
                    {
                        ResponseString = message;
                        ReceivedResponse = true;
                    }
                    else { return; }
                    break;
                case ResponseType.JsonContent:
                    if (!string.IsNullOrWhiteSpace(message)
                        && message.StartsWith("{")
                        && message.EndsWith("}"))
                    {
                        ResponseString = message;
                        ReceivedResponse = true;
                    }
                    else { return; }
                break;
                case ResponseType.JsonFile:
                    if (!string.IsNullOrEmpty(message))
                    // && message.EndsWith(".json")
                    {
                        ResponseString = message;
                        ReceivedResponse = true;
                    }
                    else { return; }
                    break;
            }
            
            ReceivedResponse = true;
            connection.RconWebSocketMutex.WaitOne();
                connection.RconWebSocket.OnMessage -= OnMessage;
            connection.RconWebSocketMutex.ReleaseMutex();

        }

        public enum ResponseType {
            Chat,
            String,
            Numeric,
            JsonContent,
            JsonFile
        }

    }
}
