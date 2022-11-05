using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RconTool
{

	[JsonObject(MemberSerialization.OptIn)]
    public class ServerSettings
    {

        public const string AutoTranslateIgnoredPhrasesSeparator = "\n";
        public const string RconProtocolString = "dew-rcon";

        public ServerSettings() {}

        #region Properties

        #region Server Connection

        [JsonProperty]
        public string Id { get; set; }

        [JsonProperty]
        public string Ip { get; set; } = "";

        [JsonProperty]
        public string InfoPort { get; set; } = "";

        [JsonProperty]
        public string RconPort { get; set; } = "";

        [JsonProperty]
        public string RconPassword { get; set; } = "";

        [JsonProperty]
        public string ServerPassword { get; set; } = "";

        [JsonProperty]
        public string Name { get; set; } = "";

        [JsonProperty]
        public List<string> SendOnConnectCommands { get; set; } = new List<string>();

        [JsonProperty]
        public TitleOption TitleDisplayOption = TitleOption.Game;

        public string TitleDisplayStyle {
			get {
				switch (TitleDisplayOption)
				{
					case TitleOption.Name: return "Name";
					case TitleOption.Game: return "Game";
					case TitleOption.Ip: return "Ip";
					case TitleOption.None: return "None";
					default: return "Game";
				}
			}
            set {
                if (value == "Name") { TitleDisplayOption = TitleOption.Name; }
                else if (value == "Game") { TitleDisplayOption = TitleOption.Game; }
                else if (value == "Ip") { TitleDisplayOption = TitleOption.Ip; }
                else if (value == "None") { TitleDisplayOption = TitleOption.None; }
            }
        }

        #endregion

        #region Discord Integration

        [JsonProperty]
        public string Webhook { get; set; }

        [JsonProperty]
        public string WebhookTrigger { get; set; }

        [JsonProperty]
        public string WebhookRole { get; set; }

        #endregion

        #region Local File Management

        [JsonProperty]
        public bool UseServerHook { get; set; }

        [JsonProperty]
        public bool UseLocalFiles { get; set; } = true;

        [JsonProperty]
        public string ServerExecutableDirectoryPath { get; set; }

        [JsonProperty]
        public string VoteFilesDirectoryPath { get; set; }

        [JsonProperty]
        public string RelativeVotingPath { get; set; }

        [JsonProperty]
        public string GameVariantsDirectoryPath { get; set; }

        [JsonProperty]
        public string MapVariantsDirectoryPath { get; set; }

        //public string VoteFilesJson {
        //    get { if (VoteFiles == null) { VoteFiles = new List<VoteFile>(); } return JsonConvert.SerializeObject(VoteFiles); }
        //    set { if (!string.IsNullOrWhiteSpace(value))
        //        {
        //            try { VoteFiles = JsonConvert.DeserializeObject<List<VoteFile>>(value); }
        //            catch { VoteFiles = new List<VoteFile>(); }
        //        } }
        //}

        //[JsonProperty]
        public List<VoteFile> VoteFiles { get; set; } = new List<VoteFile>();
                
        public DateTime LastVoteFileLoadTime { get; set; }

        public bool VoteFilesLoaded { get; set; } = false;

        public bool CanQueryMapsAndGametypes { get { return CanQueryMaps && CanQueryGametypes; } }
        public bool CanQueryMaps { get { return !string.IsNullOrWhiteSpace(MapVariantsDirectoryPath); } }
        public bool CanQueryGametypes { get { return !string.IsNullOrWhiteSpace(GameVariantsDirectoryPath); } }

        #endregion

        #region Server Commands and Misc. Settings

        [JsonProperty]
        public bool ChatCommandKickPlayerEnabled { get; set; } = true;

        [JsonProperty]
        public bool ChatCommandShuffleTeamsEnabled { get; set; } = true;

        [JsonProperty]
        public bool ChatCommandEndGameEnabled { get; set; } = true;

        public string ToolCommandsJson {
            get { 
                if (Commands == null) { Commands = new List<ToolCommand>(); } 
                return JsonConvert.SerializeObject(Commands); 
            }
            set {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    try { Commands = JsonConvert.DeserializeObject<List<ToolCommand>>(value); }
                    catch { Commands = new List<ToolCommand>(); }
                }
            }
        }
        [JsonProperty]
        public List<ToolCommand> Commands { get; set; } = new List<ToolCommand>();

        public string AuthorizedUIDsJson {
            get { if (AuthorizedUIDs == null) { AuthorizedUIDs = new List<string>(); } return JsonConvert.SerializeObject(AuthorizedUIDs); }
            set {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    try { AuthorizedUIDs = JsonConvert.DeserializeObject<List<string>>(value); }
                    catch { AuthorizedUIDs = new List<string>(); }
                }
            }
        }
        [JsonProperty]
        public List<string> AuthorizedUIDs { get; set; } = new List<string>();

        public string AutoTranslateIgnoredPhrases { 
            get { 
                if ((AutoTranslateIgnoredPhrasesList?.Count ?? 0) == 0) {  return ""; }
                else { return string.Join(AutoTranslateIgnoredPhrasesSeparator, AutoTranslateIgnoredPhrasesList); }
            } 
            set {
                if (string.IsNullOrWhiteSpace(value)) { AutoTranslateIgnoredPhrasesList = new List<string>(); }
                else { AutoTranslateIgnoredPhrasesList = value.Split("\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList(); }
            } 
        }
        [JsonProperty]
        public List<string> AutoTranslateIgnoredPhrasesList { get; set; } = new List<string>();

        // By default, server language is set to english
        // me: https://open.spotify.com/track/29ufIwomYfLbWBxPMdaUZm?si=0JtiFlHwS4mfEVmqHlwECw
        [JsonProperty]
        public string ServerLanguage { get; set; } = "en";

        [JsonProperty]
        public bool TranslationEnabled { get; set; } = false;

        [JsonProperty]
        public bool AutoTranslateChatMessages { get; set; } = true;

        public MatchMode ServerMatchMode { get; set; } = MatchMode.Voting;

		#endregion
        		

		public DirectoryInfo ServerExecutableDirectory {
            get { return string.IsNullOrWhiteSpace(ServerExecutableDirectoryPath) 
                    ? null : new DirectoryInfo(ServerExecutableDirectoryPath); 
            }
        }        
        
        public DirectoryInfo VoteFilesDirectory {
            get { return string.IsNullOrWhiteSpace(VoteFilesDirectoryPath)
                    ? null : new DirectoryInfo(VoteFilesDirectoryPath); 
            }
        }        
        
        public DirectoryInfo GameVariantsDirectory {
            get { return string.IsNullOrWhiteSpace(GameVariantsDirectoryPath) 
                    ? null : new DirectoryInfo(GameVariantsDirectoryPath); 
            }
        }
        
        public DirectoryInfo MapVariantsDirectory {
            get { return string.IsNullOrWhiteSpace(MapVariantsDirectoryPath)
                    ? null : new DirectoryInfo(MapVariantsDirectoryPath); 
            }
        }

        private static DirectoryInfo ConfirmAndRetrieveDirectoryInfo(DirectoryInfo directory, string path)
        {
            if (directory == null)
            {
                if (!string.IsNullOrWhiteSpace(path))
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
                if (!string.IsNullOrWhiteSpace(path))
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
                
        public string DisplayName { 
            get { 
                return !string.IsNullOrWhiteSpace(Name)
                    ? Name 
                    : $"{Ip ?? "Unknown IP"}:{(RconPort ?? InfoPort) ?? "Unknown Port"}"
                ;
            } 
        }
                
        public bool RconWebSocketAddressIsValid {
            get {
                return !(string.IsNullOrEmpty(Ip) || string.IsNullOrEmpty(RconPort));
            }
        }
        
        public string RconWebSocketAddress {
            get { return "ws://" + Ip + ":" + RconPort; }
        }
        
        public bool ServerInfoAddressIsValid { get {
                return !(string.IsNullOrEmpty(Ip) || string.IsNullOrEmpty(InfoPort));
            } }
        
        public string ServerInfoAddress { get {
                return "http://" + Ip + ":" + InfoPort + "/";
        } }

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

        #endregion

        public string VoteFilePath(string name)
        {
            return VoteFilesDirectory?.FullName + "\\" + name + ".json";
        }
        public VoteFile GetDynamicVoteFileObject()
        {

            if (!UseLocalFiles) {
                throw new Exception("Attempted to retrieve dynamic voting file, but dynamic voting file management is disabled.");
            }
            if (VoteFilesDirectory == null) {
                throw new Exception("Attempted to retrieve dynamic voting file, but voting files directory could not be located.");
            }

            VoteFile voteFile = null;
            if (VoteFiles.Count != 0)
            {
                voteFile = VoteFiles.DefaultIfEmpty(null)?.FirstOrDefault(x => x.Name == "dynamic.json");
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
                    VoteFiles.Add(voteFile);
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
            catch (Exception e) { App.Log(e.Message, App.GetConnection(this)); }
            VoteFiles.Remove(file);
        }
        public void WriteAllVoteFilesToDisk()
        {
            foreach (VoteFile voteFile in VoteFiles)
            {
                try { voteFile.WriteToDisk(); }
                catch (Exception e) { App.Log(e.Message, App.GetConnection(this)); }
            }
        }
        public void LoadAllVoteFilesFromDisk()
        {
            if (UseLocalFiles && VoteFilesDirectory != null)
            {
                VoteFiles.Clear();
                foreach (FileInfo item in VoteFilesDirectory.GetFiles())
                {
                    if (item.Name.EndsWith(".json"))
                    {
                        
                        VoteFile newVF = null;
                        try { newVF = VoteFile.FromLoad(item.FullName, this); }
                        catch (Exception e) { App.Log(e.Message, App.GetConnection(this)); }

                        if (newVF != null)
                        {
                            if (newVF.Validate(this))
                            {
                                VoteFiles.Add(newVF);
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
            if (VoteFiles != null) { VoteFiles.Clear(); }
            else { VoteFiles = new List<VoteFile>(); }
            RelativeVotingPath = "mods\\server";
            UseLocalFiles = false;
            VoteFilesDirectoryPath = null;
        }

        public enum MatchMode
        {
            Voting,
            SetList,
            Queue
        }

        public enum TitleOption
		{
            Name,
            Game,
            Ip,
            None
		}

    }

}
