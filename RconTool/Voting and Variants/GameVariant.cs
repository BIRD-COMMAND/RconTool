using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{
	
	
	public class GameVariant
	{

		public static Dictionary<string, string> VariantGametypeExtensionAssociations = new Dictionary<string, string>() {
			{ ".slayer", "Slayer" },
			{".oddball", "Oddball"},
			{".koth", "King of the Hill"},
			{".ctf", "Capture the Flag"},
			{".assault", "Assault"},
			{".jugg", "Juggernaut"},
			{".zombiez", "Infection"},
			{".vip", "VIP"},
		};
		public static Dictionary<string, BaseGame> BaseGamesByName = new Dictionary<string, BaseGame>()
		{
			{ "Slayer", BaseGame.Slayer },
			{ "Oddball", BaseGame.Oddball },
			{ "Assault", BaseGame.Assault },
			{ "CaptureTheFlag", BaseGame.CaptureTheFlag },
			{ "Infection", BaseGame.Infection },
			{ "VIP", BaseGame.VIP },
			{ "KingOfTheHill", BaseGame.KingOfTheHill},
			{ "Juggernaut", BaseGame.Juggernaut }
		};
		public static BaseGame GetBaseGame(string name)
		{
			if (BaseGamesByName.ContainsKey(name))
			{
				return BaseGamesByName[name];
			}
			else
			{
				return BaseGame.Unknown;
			}
		}
		private const string UnknownVariantIdentifier = "Unknown";

		public static string GetBaseGametype(string filename)
		{
			foreach (string value in VariantGametypeExtensionAssociations.Keys)
			{
				if (filename.EndsWith(value))
				{
					return VariantGametypeExtensionAssociations[value];
				}
			}
			return UnknownVariantIdentifier;
		}
		public static BaseGame GetBaseGameID(string filename)
		{
			string[] split = filename.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			if (split.Length > 0)
			{
				switch (split[split.Length - 1])
				{
					case "slayer": return BaseGame.Slayer;
					case "oddball": return BaseGame.Oddball;
					case "koth": return BaseGame.KingOfTheHill;
					case "ctf": return BaseGame.CaptureTheFlag;
					case "assault": return BaseGame.Assault;
					case "jugg": return BaseGame.Juggernaut;
					case "zombiez": return BaseGame.Infection;
					case "vip": return BaseGame.VIP;
					default: return BaseGame.Unknown;
				}
			}
			return BaseGame.Unknown;
		}

		public bool IsValid { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }		
		public string TypeNameForVotingFile { get; set; }
		public string BaseGameTypeString { get; set; }
		public BaseGame BaseGameID { get; set; }


		public GameVariant(DirectoryInfo folder)
		{
			string baseGameType;
			foreach (FileInfo file in folder.GetFiles())
			{
				baseGameType = GetBaseGametype(file.Name);
				BaseGameID = GetBaseGameID(file.Name);
				if (baseGameType != UnknownVariantIdentifier && file.Name.StartsWith("variant."))
				{

					FileStream fs = new FileStream(file.FullName, FileMode.Open);

					// Return Invalid Variant if file length is too short
					if (fs.Length < 250) { fs.Close(); fs.Dispose(); throw new Exception("Error Reading GameVariant File, Too Short"); }

					UnicodeEncoding utf16Encoder = new UnicodeEncoding(false, false, true);

					//@position 72 - read next 32 (72 - 103) - UTF - 16 Encoded Variant Name
					fs.Seek(72, SeekOrigin.Begin);
					byte[] nameBytes = new byte[32];
					fs.Read(nameBytes, 0, 32);
					
					try { Name = utf16Encoder.GetString(nameBytes); }
					catch { fs.Close(); fs.Dispose(); throw new Exception("Error Decoding GameVariant Name"); }
					Name = Name.Replace("\0", "");

					//@position 104 - read next 128 (104 - 231) - UTF - 8 Encoded Variant Description
					fs.Seek(104, SeekOrigin.Begin);
					byte[] descriptionBytes = new byte[128];
					fs.Read(descriptionBytes, 0, 128);

					try { Description = Encoding.UTF8.GetString(descriptionBytes); }
					catch { fs.Close(); fs.Dispose(); throw new Exception("Error Decoding GameVariant Description"); }
					Description = Description.Replace("\0", "");

					//@position 232 - read next 16 (232 - 247) - UTF - 8 Encoded Variant Author
					fs.Seek(232, SeekOrigin.Begin);
					byte[] authorBytes = new byte[16];
					fs.Read(authorBytes, 0, 16);

					try { Author = Encoding.UTF8.GetString(authorBytes); }
					catch { fs.Close(); fs.Dispose(); throw new Exception("Error Decoding GameVariant Author"); }
					Author = Author.Replace("\0", "");

					TypeNameForVotingFile = folder.Name;
					BaseGameTypeString = baseGameType;

					fs.Close();
					fs.Dispose();

					this.IsValid = true;
					return;
				}
			}

			this.Name = UnknownVariantIdentifier;
			this.Author = UnknownVariantIdentifier;
			this.Description = UnknownVariantIdentifier;
			this.IsValid = false;

		}


		private bool Exists(DirectoryInfo gameVariantsDirectory, out DirectoryInfo match)
		{
			match = null;
			foreach (DirectoryInfo folder in gameVariantsDirectory.GetDirectories())
			{
				if (folder.Name == TypeNameForVotingFile)
				{
					foreach (FileInfo file in folder.GetFiles())
					{
						if (file.Name.StartsWith("variant."))
						{

							#region Check Internal Game Name
							FileStream fs = new FileStream(file.FullName, FileMode.Open);
							//@position 72 - read next 32 (72 - 103) - UTF - 16 Encoded Variant Name
							fs.Seek(72, SeekOrigin.Begin);
							byte[] nameBytes = new byte[32];
							fs.Read(nameBytes, 0, 32);
							string name;
							try { name = new UnicodeEncoding(false, false, true).GetString(nameBytes); }
							catch { fs.Close(); fs.Dispose(); throw new Exception("Error Decoding GameVariant Name"); }
							name = name.Replace("\0", "");
							#endregion

							if (name == Name)
							{
								match = folder;
								return true;
							}

						}
					}
					return false;
				}
			}
			return false;

		}
		public bool Exists(DirectoryInfo gameVariantsDirectory)
		{
			return Exists(gameVariantsDirectory, out _);
		}
		public bool Exists(DirectoryInfo gameVariantsDirectory, string typeName, string displayName, out DirectoryInfo match)
		{
			match = null;
			this.TypeNameForVotingFile = typeName;
			this.Name = displayName;
			return Exists(gameVariantsDirectory, out match);
		}


		public override bool Equals(object obj)
		{
			return this.Equals(obj as GameVariant);
		}

		public bool Equals(GameVariant p)
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
			return (this.TypeNameForVotingFile == p.TypeNameForVotingFile);
		}

		public override int GetHashCode()
		{
			return this.TypeNameForVotingFile.GetHashCode();
		}

		public static bool operator ==(GameVariant lhs, GameVariant rhs)
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

		public static bool operator !=(GameVariant lhs, GameVariant rhs)
		{
			return !(lhs == rhs);
		}

		public enum BaseGame
		{
			Slayer = 0,
			Oddball = 1,
			KingOfTheHill = 2,
			CaptureTheFlag = 3,
			Assault = 4,
			Juggernaut = 5,
			Infection = 6,
			VIP = 7,
			Unknown = 8,
			All = 9
		}

	}
}
