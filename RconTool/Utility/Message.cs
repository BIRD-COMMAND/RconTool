using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RconTool
{

    [JsonObject(MemberSerialization.OptIn)]
    public class Message
    {

        public Message() { }

        [JsonProperty]
        public bool IsValidChatMessage { get; set; } = true;

        //[JsonProperty]
        //public int Id { get; set; }

        // DateTime examples from database '12/02/20 17:04:11' '12/03/20 03:08:58'
        // MM/dd/yy hh:mm:ss

        [JsonProperty]
        public string DateTimeString { get; set; } = "";

        [JsonProperty]
        public DateTime DateTime { get; set; } = DateTime.UtcNow;

        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public string UID { get; set; } = "";

        [JsonProperty]
        public string IP { get; set; } = "";

        [JsonProperty]
        public string Text { get; set; } = "";

        [JsonProperty]
        public string DetectedLanguage { get; set; } = "";

        [JsonProperty]
        public bool IsServerLanguage { get; set; } = true;

        [JsonProperty]
        public string ServerLanguageTranslation { get; set; } = "";

        [JsonProperty]
        public bool HasServerLanguageTranslation { get; set; } = false;

        //[JsonProperty]
        //public string TranslationsJson { 
        //    get { if (Translation == null) { Translation = new Translation(); } return JsonConvert.SerializeObject(Translation); } 
        //    set { if (!string.IsNullOrWhiteSpace(value)) {
        //            try { Translation = JsonConvert.DeserializeObject<Translation>(value); }
        //            catch { Translation = new Translation(); }
        //    } }
        //}

        [JsonProperty]
        public Translation Translation { get; set; } = new Translation();

   //     [SqliteColumn]
   //     public string ServerResponsesJson { 
   //         get {
   //             if (ServerResponses == null) { ServerResponses = new List<string>(); }
   //             if (ServerResponses.Count > 0) { return JsonConvert.SerializeObject(ServerResponses); }
   //             else { return "[]"; }
			//}
   //         set {
   //             if (!string.IsNullOrWhiteSpace(value)) {
   //                 try { ServerResponses = JsonConvert.DeserializeObject<List<string>>(value); }
   //                 catch { ServerResponses = new List<string>(); }
   //             }
   //         }
   //     }

        [JsonProperty]
        public List<string> ServerResponses { get; set; } = new List<string>();

        //public enum _Type {
        //    Console = 0,
        //    Chat = 1,
        //    Player = 2,
        //    App = 3
        //}

    }

}
