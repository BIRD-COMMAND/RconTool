using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Linq;

namespace RconTool
{
    [DataContract]
    public class ServerSettings
    {

        [JsonIgnore]
        public const string RconProtocolString = "dew-rcon";

        [DataMember]
        public string Ip { get; set; }
        [DataMember]
        public string InfoPort { get; set; }
        [DataMember]
        public string ServerPassword { get; set; } = "";
        [DataMember]
        public string RconPassword { get; set; }
        [DataMember]
        public string RconPort { get; set; }
        [DataMember]
        public string Name { get; set; } = "";
        [DataMember]
        public List<string> sendOnConnect { get; set; } = new List<string>();
        [DataMember]
        public List<ToolCommand> Commands { get; set; } = new List<ToolCommand>();
        [DataMember]
        public List<string> AuthorizedUIDs { get; set; } = new List<string>();
        [DataMember]
        public bool DynamicVotingFileManagement { get; set; } = true;
        [DataMember]
        public string ServerExecutableDirectoryPath { get; set; }
        [DataMember]
        public string VoteFilesDirectoryPath { get; set; }
        [DataMember]
        public string RelativeVotingPath { get; set; }
        [DataMember]
        public string GameVariantsDirectoryPath { get; set; }
        [DataMember]
        public string MapVariantsDirectoryPath { get; set; }
        [DataMember]
        public List<VoteFile> voteFiles { get; set; } = new List<VoteFile>();
        
        [JsonIgnore]
        public DirectoryInfo ServerExecutableDirectory {
            get { return ConfirmAndRetrieveDirectoryInfo(serverExecutableDirectory, ServerExecutableDirectoryPath); }
        }
        private DirectoryInfo serverExecutableDirectory;
        
        [JsonIgnore]
        public DirectoryInfo VoteFilesDirectory {
            get { return ConfirmAndRetrieveDirectoryInfo(voteFilesDirectory, VoteFilesDirectoryPath); }
        }
        private DirectoryInfo voteFilesDirectory;
        
        [JsonIgnore]
        public DirectoryInfo GameVariantsDirectory {
            get { return ConfirmAndRetrieveDirectoryInfo(gameVariantsDirectory, GameVariantsDirectoryPath); }
        }
        private DirectoryInfo gameVariantsDirectory;

        [JsonIgnore]
        public DirectoryInfo MapVariantsDirectory {
            get { return ConfirmAndRetrieveDirectoryInfo(mapVariantsDirectory, MapVariantsDirectoryPath); }
        }
        private DirectoryInfo mapVariantsDirectory;

        private static DirectoryInfo ConfirmAndRetrieveDirectoryInfo(DirectoryInfo directory, string path)
        {
            if (directory == null)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    try
                    {
                        directory = new DirectoryInfo(path);
                        return directory;
                    }
                    catch { return null; }

                }
                else { return null; }
            }
            else
            {
                if (!string.IsNullOrEmpty(path))
                {
                    if (directory.FullName == path)
                    {
                        return directory;
                    }
                    else
                    {
                        try
                        {
                            directory = new DirectoryInfo(path);
                            return directory;
                        }
                        catch { return null; }

                    }
                }
                else { directory = null; return null; }
            }
        }

        [JsonIgnore]
        public string Identifier { 
            get { 
                return !string.IsNullOrWhiteSpace(Name)
                    ? Name 
                    : Ip + ":" + ((RconPort ?? InfoPort) ?? "Unknown")
                ;
            } 
        }
        
        [JsonIgnore]
        public bool RconWebSocketAddressIsValid {
            get {
                return !(string.IsNullOrEmpty(Ip) || string.IsNullOrEmpty(RconPort));
            }
        }
        [JsonIgnore]
        public string RconWebSocketAddress {
            get { return "ws://" + Ip + ":" + RconPort; }
        }
        [JsonIgnore]
        public bool ServerInfoAddressIsValid { get {
                return !(string.IsNullOrEmpty(Ip) || string.IsNullOrEmpty(InfoPort));
            } }
        [JsonIgnore]
        public string ServerInfoAddress { get {
                return "http://" + Ip + ":" + InfoPort + "/";
        } }
        [JsonIgnore]
        public bool VoteFilesLoaded { get; set; } = false;
        [JsonIgnore]
        public DateTime LastVoteFileLoadTime { get; set; }
        [JsonIgnore]
        public MatchMode ServerMatchMode { get; set; } = MatchMode.Voting;

        [OnDeserialized]
        private void Validate(StreamingContext sc)
        {
            foreach (ToolCommand command in Commands)
            {
                command.AssociatedServer = this;
            }
        }

        public void InitializeCommands(Connection connection)
        {
            foreach (ToolCommand command in Commands)
            {
                command.Initialize(connection);
            }
        }

        public bool IsSameServerReference(ServerSettings other)
        {
            return (
                    Ip == other.Ip
                &&  InfoPort == other.InfoPort
                &&  RconPassword == other.RconPassword
                &&  RconPort == other.RconPort
            );
        }

        public ServerSettings(string ip, string infoPort, string serverPassword, string rconPassword, string rconPort, string name, List<string> sendOnConnect)
        {
            this.Ip = ip;
            this.InfoPort = infoPort;
            this.ServerPassword = serverPassword;
            this.RconPassword = rconPassword;
            this.RconPort = rconPort;
            this.Name = name;
            this.sendOnConnect = sendOnConnect;
        }

        public string ToBase64()
        {
            string s = JsonConvert.SerializeObject(this);
            return Base64Encode(s);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string VoteFilePath(string name)
        {
            return VoteFilesDirectory?.FullName + "\\" + name + ".json";
        }
        public VoteFile GetDynamicVoteFileObject()
        {

            if (!DynamicVotingFileManagement) {
                throw new Exception("Attempted to retrieve dynamic voting file, but dynamic voting file management is disabled.");
            }
            if (VoteFilesDirectory == null) {
                throw new Exception("Attempted to retrieve dynamic voting file, but voting files directory could not be located.");
            }

            VoteFile voteFile = null;
            if (voteFiles.Count != 0)
            {
                voteFile = voteFiles.DefaultIfEmpty(null)?.FirstOrDefault(x => x.Name == "dynamic.json");
            }
            if (voteFile == null) 
            { 
                // Not validating this one because it's intentionally blank at this point
                voteFile = VoteFile.Create(VoteFilesDirectory.FullName, "dynamic", this);
                if (voteFile == null)
                {
                    throw new Exception("Failed to write or retrieve dynamic voting file.");
                }
                else {
                    voteFiles.Add(voteFile);
                    return voteFile; 
                }

            }
            else 
            { 
                return voteFile; 
            }

        }
        
        public void DeleteVoteFile(VoteFile file)
        {
            try { file.DeleteFromDisk(); }
            catch (Exception e) { App.Log(e.Message, this); }
            voteFiles.Remove(file);
        }
        public void WriteAllVoteFilesToDisk()
        {
            foreach (VoteFile voteFile in voteFiles)
            {
                try { voteFile.WriteToDisk(); }
                catch (Exception e) { App.Log(e.Message, this); }
            }
        }
        public void LoadAllVoteFilesFromDisk()
        {
            if (VoteFilesDirectory != null)
            {
                voteFiles.Clear();
                foreach (FileInfo item in VoteFilesDirectory.GetFiles())
                {
                    if (item.Name.EndsWith(".json"))
                    {
                        
                        VoteFile newVF = null;
                        try { newVF = VoteFile.FromLoad(item.FullName, this); }
                        catch (Exception e) { App.Log(e.Message, this); }

                        if (newVF != null)
                        {
                            if (newVF.Validate(this))
                            {
                                voteFiles.Add(newVF);
                            }
                        }

                    }
                }
                VoteFilesLoaded = true;
                LastVoteFileLoadTime = DateTime.Now;
            }
        }
        public List<string> GetVoteFileNames()
        {
            if (VoteFilesDirectory != null)
            {
                List<string> names = new List<string>();
                foreach (FileInfo item in VoteFilesDirectory.GetFiles())
                {
                    if (item.Name.EndsWith(".json"))
                    {
                        names.Add(item.Name);
                    }
                }
                return names;
            }
            else
            {
                return null;
            }
        }
        
        public void ResetVotingSettings()
        {
            if (voteFiles != null) { voteFiles.Clear(); }
            else { voteFiles = new List<VoteFile>(); }
            RelativeVotingPath = "mods\\server";
            DynamicVotingFileManagement = false;
            VoteFilesDirectoryPath = null;
        }

        public void SetCommandEnabledState(string name, bool state, string tag = null)
        {
            if (tag == null)
            {
                foreach (ToolCommand item in Commands)
                {
                    if (item.Name == name)
                    {
                        if (item.Enabled != state)
                        {
                            item.ToggleEnabled();
                        }
                    }
                }
            }
            else
            {
                foreach (ToolCommand item in Commands)
                {
                    if (item.Tag.Contains(tag))
                    {
                        if (item.Enabled != state)
                        {
                            item.ToggleEnabled();
                        }
                    }
                }
            }
        }

        public enum MatchMode
        {
            Voting,
            SetList,
            Queue
        }

    }
}
