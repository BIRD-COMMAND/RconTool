using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{
	
	[JsonObject (MemberSerialization.OptIn)]
	public class VoteFile
	{

		#region long string
		public const string BackupVotingJson1 =
			"{\"Maps\":[{\"displayName\":\"Standoff\",\"mapName\":\"Bunkerworld\"},{\"displayName\":\"Sandtrap\",\"mapName\":\"Shrine\"},{\"displayName\":\"Icebox\",\"mapName\":\"s3d_turf\"},{\"displayName\":\"Reactor\",\"mapName\":\"s3d_reactor\"},{\"displayName\":\"Narrows\",\"mapName\":\"Chill\"},{\"displayName\":\"High Ground\",\"mapName\":\"Deadlock\"},{\"displayName\":\"Last Resort\",\"mapName\":\"Zanzibar\"},{\"displayName\":\"Valhalla\",\"mapName\":\"riverworld\"},{\"displayName\":\"The Pit\",\"mapName\":\"Cyberdyne\"},{\"displayName\":\"Guardian\",\"mapName\":\"guardian\"},{\"displayName\":\"Diamondback\",\"mapName\":\"s3d_avalanche\"},{\"displayName\":\"Edge\",\"mapName\":\"s3d_edge\"}],\"Types\":[{\"displayName\":\"Team Oddball\",\"typeName\":\"TeamOddball\",\"commands\":[\"Server.SprintEnabled 0\",\"Server.AssassinationEnabled 0\"],\"SpecificMaps\":[{\"displayName\":\"Narrows\",\"mapName\":\"Chill\"},{\"displayName\":\"The Pit\",\"mapName\":\"Cyberdyne\"},{\"displayName\":\"Edge\",\"mapName\":\"s3d_edge\"}]},{\"displayName\":\"Team Multi Flag\",\"typeName\":\"TeamMultiFlag\",\"commands\":[\"Server.SprintEnabled 0\",\"Server.AssassinationEnabled 0\",\"Server.NumberOfTeams 2\"],\"SpecificMaps\":[{\"displayName\":\"Valhalla\",\"mapName\":\"riverworld\"},{\"displayName\":\"Sandtrap\",\"mapName\":\"Shrine\"},{\"displayName\":\"Standoff\",\"mapName\":\"Bunkerworld\"}]},{\"displayName\":\"Team 1-Bomb\",\"typeName\":\"TeamOneBomb\",\"commands\":[\"Server.SprintEnabled 0\",\"Server.AssassinationEnabled 0\",\"Server.NumberOfTeams 2\"],\"SpecificMaps\":[{\"displayName\":\"High Ground\",\"mapName\":\"Deadlock\"},{\"displayName\":\"Last Resort\",\"mapName\":\"Zanzibar\"}]},{\"displayName\":\"Team Assault\",\"typeName\":\"TeamAssault\",\"commands\":[\"Server.SprintEnabled 0\",\"Server.AssassinationEnabled 0\",\"Server.NumberOfTeams 2\"],\"SpecificMaps\":[{\"displayName\":\"Standoff\",\"mapName\":\"Bunkerworld\"}]},{\"displayName\":\"Team KotH\",\"typeName\":\"TeamCrazyKing\",\"commands\":[\"Server.SprintEnabled 0\",\"Server.AssassinationEnabled 0\",\"Server.NumberOfTeams 2\"],\"SpecificMaps\":[{\"displayName\":\"Standoff\",\"mapName\":\"Bunkerworld\"},{\"displayName\":\"The Pit\",\"mapName\":\"Cyberdyne\"},{\"displayName\":\"Narrows\",\"mapName\":\"Chill\"},{\"displayName\":\"High Ground\",\"mapName\":\"Deadlock\"},{\"displayName\":\"Edge\",\"mapName\":\"s3d_edge\"},{\"displayName\":\"Reactor\",\"mapName\":\"s3d_reactor\"}]}]}";
		#endregion

		public ServerSettings Settings { get; set; }

		public string Path { get; set; }
		
		public string Name { get {
				return App.GetFileInfo(Path)?.Name;
		}	}
		
		public bool HasValidPath { get {
			if (string.IsNullOrEmpty(Path)) { return true; }
			return File.Exists(Path);
		} }


		[JsonProperty]
		public List<MapVariant> Maps { get; set; } = new List<MapVariant>();


		[JsonProperty]
		public List<Type> Types { get; set; } = new List<Type>();

		//public List<GameVariant> Games { get; set; } = new List<GameVariant>();

		public bool IsValid	{ get {		
			if (Types.Count < 2 || Maps.Count < 1) { return false; }
			int validTypes = 0;
			foreach (Type type in Types)
			{
				if (type.SpecificMaps.Count > 0 && type.GameVariant != null)
				{
					validTypes++;
				}
				if (validTypes >= 2) { return true; }
			}
			return false;
		} }

		private static VoteFile LoadOrCreate(string path, ServerSettings settings)
		{

			//if file exists - load
			if (File.Exists(path))
			{
				try
				{

					string json = File.ReadAllText(path);
					VoteFile newVF = JsonConvert.DeserializeObject<VoteFile>(json);
					if (!newVF.IsValid)
					{
						try
						{
							newVF = JsonConvert.DeserializeObject<VoteFile>(BackupVotingJson1);
						}
						catch (Exception e)
						{
							App.Log("Failed to serialize default vote file from backup json | " + path + " | " + e.Message, settings);
						}
						newVF.Validate(settings);
						if (newVF != null && newVF.IsValid)
						{
							newVF.Path = path;
							return newVF;
						}
						else { return null; }
					}
					else
					{
						return newVF;
					}
				}
				catch (Exception e)
				{
					App.Log("Failed to load vote file \"" + path + "\" | " + e.Message, settings);
					return null;
				}
			}
			//if not, create
			else
			{
				if (Directory.Exists(System.IO.Path.GetDirectoryName(path)))
				{
					VoteFile newVF = new VoteFile()
					{
						Path = path
					};
					newVF.WriteToDisk();
					return newVF;
				}
				else
				{
					return null;
				}
			}

		}

		public static VoteFile Create(string voteFileDirectoryPath, string name, ServerSettings settings)
		{
			if (name.EndsWith(".json"))
			{
				return LoadOrCreate(voteFileDirectoryPath + "\\" + name, settings);
			}
			else
			{
				return LoadOrCreate(voteFileDirectoryPath + "\\" + name + ".json", settings);
			}			
		}

		public static VoteFile FromLoad(string path, ServerSettings settings)
		{
			return LoadOrCreate(path, settings);
		}

		/// <summary>
		/// Makes this option into a single-option voting file. Requires special handling of the server to make sure it doesn't crash from having too few voting options.
		/// </summary>
		/// <param name="game">The game that will be used. Is not validated here.</param>
		/// <param name="map">The map that will be used. Is not validated here.</param>
		/// <param name="writeImmediately">True by default. If true, writes/updates the vote file to disk immediately after updating.</param>
		public void MakeSingleOptionVotingFile(GameVariant game, MapVariant map, bool writeImmediately = true)
		{
			Maps = new List<MapVariant>() { map, map };
			//Games = new List<GameVariant>() { game };
			Types = new List<Type>() { Type.SingleOption(game, map), Type.SingleOption(game,map) };
			if (writeImmediately)
			{
				WriteToDisk();
			}
		}

		public void WriteToDisk()
		{
			if (Directory.Exists(System.IO.Path.GetDirectoryName(Path)))
			{
				try
				{
					File.WriteAllText(Path, JsonConvert.SerializeObject(this));
				}
				catch (Exception e)
				{
					throw new Exception("Failed to serialize vote file to disk | " + e.Message);
				}
			}
			else
			{
				throw new Exception("Failed to write vote file to disk, vote file directory could not be found.");
			}
		}
		public void DeleteFromDisk()
		{
			if (File.Exists(Path))
			{
				try
				{
					File.Delete(Path);
				}
				catch (Exception e)
				{
					throw new Exception("Failed to Delete a Dynamic Vote File: " + e.Message);
				}
			}
		}

		public bool Validate(ServerSettings settings)
		{

			Settings = settings;
			int mapArrayLosses = 0;
			int typeMapLosses = 0;
			int typeLosses = 0;

			ValidateMaps(Maps, Settings, out mapArrayLosses);

			foreach (Type type in Types)
			{
				foreach (DirectoryInfo directory in settings.GameVariantsDirectory.GetDirectories())
				{
					if (directory.Name == type.TypeName)
					{
						foreach (FileInfo file in directory.GetFiles())
						{
							if (file.Name.StartsWith("variant"))
							{
								if (!IsFileLocked(file))
								{
									try { type.GameVariant = new GameVariant(directory); }
									catch (Exception e) { App.Log(e.Message, settings); }
								}
								else {
									App.Log("Could not validate file \"" + file.FullName + "\"", settings);
									break; 
								}
							}
						}
					}
				}
			}

			Types.RemoveAll(x => x.GameVariant == null);

			//for (int i = 0; i < Types.Count; i++)
			//{
			//	//ValidateMaps(Types[i].SpecificMaps, Settings, out typeMapLosses);
			//	DirectoryInfo match;
			//	if (!Types[i].GameVariant.Exists(Settings.GameVariantsDirectory, Types[i].TypeName, Types[i].DisplayName, out match))
			//	{
			//		Types[i] = null;
			//		typeLosses++;
			//	}
			//	else
			//	{
			//		foreach (FileInfo file in match.GetFiles())
			//		{
			//			if (file.Name.StartsWith("variant"))
			//			{
			//				if (!IsFileLocked(file))
			//				{
			//					Types[i].GameVariant = new GameVariant(match);
			//				}
			//				else { break; }
			//			}
			//		}					
			//	}
			//}
			//Types.RemoveAll(x => x == null);

			if (Types.Count == 0) { return false; }
			else { return true; }
		}

		public bool ValidateMaps(List<MapVariant> maps, ServerSettings settings, out int losses)
		{
			losses = 0;
			bool mapExists;

			// Validate the maps referenced in the file
			for (int i = 0; i < maps.Count; i++)
			{
				if (MapVariant.BaseMapIDs.Contains(maps[i].TypeNameForVotingFile))
				{
					maps[i].IsBaseMap = true;
					continue;
				}

				DirectoryInfo match = null;

				try { mapExists = maps[i].Exists(settings.MapVariantsDirectory, out match); }
				catch (Exception e) { App.Log("Failed to validate map \"" + maps[i].Name + "\" | " + e.Message, settings); mapExists = false; }

				if (mapExists)
				{
					try { maps[i] = new MapVariant(match); }
					catch (Exception e) { App.Log("Failed to validate map \"" + maps[i].Name + "\" | " + e.Message, settings); }
				}
				else { losses++; }

			}

			if (maps.Count > 0) { return true; }
			else { return false; }

		}

		protected virtual bool IsFileLocked(FileInfo file)
		{
			try
			{
				using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
				{
					stream.Close();
				}
			}
			catch (IOException)
			{
				//the file is unavailable because it is:
				//still being written to
				//or being processed by another thread
				//or does not exist (has already been processed)
				return true;
			}

			//file is not locked
			return false;
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as VoteFile);
		}

		public bool Equals(VoteFile p)
		{
			// If parameter is null, return false.
			if (Object.ReferenceEquals(p, null))
			{
				return false;
			}

			// Optimization for a common success case.
			if (Object.ReferenceEquals(this, p))
			{
				return true;
			}

			// If run-time types are not exactly the same, return false.
			if (this.GetType() != p.GetType())
			{
				return false;
			}

			// Return true if the fields match.
			// Note that the base class is not invoked because it is
			// System.Object, which defines Equals as reference equality.
			return (this.Path == p.Path);
		}

		public override int GetHashCode()
		{
			return this.Path.GetHashCode();
		}

		public static bool operator ==(VoteFile lhs, VoteFile rhs)
		{
			// Check for null on left side.
			if (Object.ReferenceEquals(lhs, null))
			{
				if (Object.ReferenceEquals(rhs, null))
				{
					// null == null = true.
					return true;
				}

				// Only the left side is null.
				return false;
			}
			// Equals handles case of null on right side.
			return lhs.Equals(rhs);
		}

		public static bool operator !=(VoteFile lhs, VoteFile rhs)
		{
			return !(lhs == rhs);
		}

		[JsonObject (MemberSerialization.OptIn)]
		public class Type
		{

			/// <summary>
			/// This is the display name used in voting, which is copied from the game variant name.
			/// </summary>
			[JsonProperty (PropertyName = "displayName")]
			public string DisplayName { get; set; }

			/// <summary>
			/// This is name of the folder where the game variant file is located.
			/// </summary>
			[JsonProperty (PropertyName = "typeName")]
			public string TypeName { get; set; }

			[JsonProperty (PropertyName = "commands")]
			public List<string> Commands { get; set; } = new List<string>();

			public GameVariant GameVariant { 
				get 
				{
					return gameVariant;
				}
				set 
				{
					if (value != null)
					{
						TypeName = value.TypeNameForVotingFile;
						DisplayName = value.Name;
					}
					gameVariant = value;					
				}
			}
			private GameVariant gameVariant;

			[JsonProperty]
			public List<MapVariant> SpecificMaps { get; set; } = new List<MapVariant>();
			
			public static Type SingleOption(GameVariant game, MapVariant map, List<string> commands = null)
			{
				return new Type()
				{					
					GameVariant = game,
					Commands = commands ?? new List<string>(),
					SpecificMaps = new List<MapVariant>()
					{
						map,
						map
					}
				};
			}

		}

	}

}
