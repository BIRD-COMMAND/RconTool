using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{
	[JsonObject(MemberSerialization.OptIn)]
	public class MapVariant
	{

		public static List<string> BaseMapIDs = new List<string>()
		{
			"guardian",
			"riverworld",
			"s3d_avalanche",
			"s3d_edge",
			"s3d_reactor",
			"s3d_turf",
			"cyberdyne",
			"chill",
			"deadlock",
			"bunkerworld",
			"shrine",
			"zanzibar",
		};
		/// <summary>
		/// Contains base map names indexed by the hexadecimal IDs used in the map variant files.
		/// </summary>
		public static Dictionary<int, string> BaseMapNamesByBasemapNumber = new Dictionary<int, string>()
		{
			{ 320, "Guardian" },
			{ 340, "Valhalla" },
			{ 705, "Diamondback" },
			{ 703, "Edge" },
			{ 700, "Reactor" },
			{ 31,  "Icebox" },
			{ 390, "The Pit" },
			{ 380, "Narrows" },
			{ 310, "High Ground" },
			{ 410, "Standoff" },
			{ 400, "Sandtrap" },
			{ 30,  "Last Resort" }
		};
		public static Dictionary<int, string> BaseMapVoteFileNamesByBasemapNumber = new Dictionary<int, string>()
		{
			{ 320, "guardian" },
			{ 340, "riverworld" },
			{ 705, "s3d_avalanche" },
			{ 703, "s3d_edge" },
			{ 700, "s3d_reactor" },
			{ 31,  "s3d_turf" },
			{ 390, "cyberdyne" },
			{ 380, "chill" },
			{ 310, "deadlock" },
			{ 410, "bunkerworld" },
			{ 400, "shrine" },
			{ 30,  "zanzibar" }
		};
		public static Dictionary<string, BaseMap> BaseMapIDsByName = new Dictionary<string, BaseMap>()
		{
			{"Standoff",BaseMap.Standoff},
			{"Sandtrap",BaseMap.Sandtrap},
			{"Icebox",BaseMap.Icebox},
			{"Reactor",BaseMap.Reactor},
			{"Narrows",BaseMap.Narrows},
			{"High Ground",BaseMap.HighGround},
			{"Last Resort",BaseMap.LastResort},
			{"Valhalla",BaseMap.Valhalla},
			{"The Pit",BaseMap.ThePit},
			{"Guardian",BaseMap.Guardian},
			{"Diamondback",BaseMap.Diamondback},
			{"Edge",BaseMap.Edge}
		};
		public static Dictionary<string, BaseMap> BaseMapIDsByArgName = new Dictionary<string, BaseMap>()
		{
			{"Standoff",BaseMap.Standoff},
			{"Sandtrap",BaseMap.Sandtrap},
			{"Icebox",BaseMap.Icebox},
			{"Reactor",BaseMap.Reactor},
			{"Narrows",BaseMap.Narrows},
			{"HighGround",BaseMap.HighGround},
			{"LastResort",BaseMap.LastResort},
			{"Valhalla",BaseMap.Valhalla},
			{"ThePit",BaseMap.ThePit},
			{"Guardian",BaseMap.Guardian},
			{"Diamondback",BaseMap.Diamondback},
			{"Edge",BaseMap.Edge}
		};
		//public static Dictionary<int, string> BaseMapDescriptionsByID = new Dictionary<int, string>()
		private const string UnknownVariantIdentifier = "Unknown";

		public bool IsBaseMap { get; set; }
		public bool IsValid { get; set; } = true;
		[JsonProperty(PropertyName = "displayName")]
		public string Name { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }
		[JsonProperty(PropertyName = "mapName")]
		public string TypeNameForVotingFile { get; set; }
		public string BaseMapString { get; set; }
		public BaseMap BaseMapID { get; set; }

		public bool Exists(DirectoryInfo mapsDirectory, out DirectoryInfo match)
		{
			match = null;
			foreach (DirectoryInfo folder in mapsDirectory.GetDirectories())
			{
				if (folder.Name == TypeNameForVotingFile)
				{
					foreach (FileInfo file in folder.GetFiles())
					{
						if (file.Name.EndsWith(".map"))
						{

							#region Check Internal Map Name
							FileStream fs = new FileStream(file.FullName, FileMode.Open);
							//@position 72 - read next 32 (72 - 103) - UTF - 16 Encoded Variant Name
							fs.Seek(72, SeekOrigin.Begin);
							byte[] nameBytes = new byte[32];
							fs.Read(nameBytes, 0, 32);
							string name;
							try { name = new UnicodeEncoding(false, false, true).GetString(nameBytes); }
							catch { fs.Close(); fs.Dispose(); throw new Exception("Error Decoding MapVariant Name"); }
							name = name.Replace("\0", "");
							#endregion

							if (name == Name) { match = file.Directory; return true; }

						}
					}
					return false;
				}
			}
			return false;
		}

		public static MapVariant GetBaseMapVariant(BaseMap map)
		{
			return new MapVariant()
			{
				Name = BaseMapNamesByBasemapNumber[(int)map],
				Author = "DEFAULT",
				Description = "DEFAULT",
				TypeNameForVotingFile = BaseMapVoteFileNamesByBasemapNumber[(int)map],
				BaseMapString = BaseMapNamesByBasemapNumber[(int)map],
				BaseMapID = map
			};
		}
		public static MapVariant DetermineBaseMap(string name)
		{
			if (BaseMapIDsByName.ContainsKey(name))
			{
				return GetBaseMapVariant(BaseMapIDsByName[name]);
			}
			else if (BaseMapIDsByArgName.ContainsKey(name))
			{
				return GetBaseMapVariant(BaseMapIDsByArgName[name]);
			}
			else
			{
				return null;
			}
		}
		public static BaseMap GetBaseMap(string name)
		{
			if (BaseMapIDsByName.ContainsKey(name))
			{
				return BaseMapIDsByName[name];
			}
			else if (BaseMapIDsByArgName.ContainsKey(name))
			{
				return BaseMapIDsByArgName[name];
			}
			else { return BaseMap.Unknown; }
		}

		public MapVariant() {}
		public MapVariant(DirectoryInfo folder)
		{

			foreach (FileInfo file in folder.GetFiles())
			{
				if (file.Name.EndsWith(".map"))
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

					//@position 288 - read next 4 (288 - 291) - UINT32 - Map Base Type ID
					fs.Seek(288, SeekOrigin.Begin);
					byte[] mapBaseTypeIdBytes = new byte[4];
					fs.Read(mapBaseTypeIdBytes, 0, 4);

					int id;
					try { id = (int)BitConverter.ToUInt32(mapBaseTypeIdBytes, 0); }
					catch { fs.Close(); fs.Dispose(); throw new Exception("Error Decoding Base Map Type ID"); }
					if (BaseMapNamesByBasemapNumber.ContainsKey(id)) { BaseMapString = BaseMapNamesByBasemapNumber[id]; BaseMapID = (BaseMap)id; }
					else { throw new Exception("Base Map Type Not Found For ID "+ id); }
					

					TypeNameForVotingFile = folder.Name;

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
		
		

		public enum BaseMap
		{
			All = 0,
			HighGround = 310,
			Guardian = 320,
			Valhalla = 340,
			Narrows = 380,
			ThePit = 390,
			Sandtrap = 400,
			Standoff = 410,
			LastResort = 30,
			Icebox = 31,
			Reactor = 700,
			Edge = 703,
			Diamondback = 705,
			Unknown = 1
		}

		public override bool Equals(object obj)
		{
			return this.Equals(obj as MapVariant);
		}

		public bool Equals(MapVariant p)
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

		public static bool operator ==(MapVariant lhs, MapVariant rhs)
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

		public static bool operator !=(MapVariant lhs, MapVariant rhs)
		{
			return !(lhs == rhs);
		}


	}

}
