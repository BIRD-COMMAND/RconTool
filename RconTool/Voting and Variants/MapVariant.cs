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

		public static readonly List<string> BaseMapStringIDs = new List<string>()
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
		public static readonly List<string> BaseMapNames = new List<string>()
		{
			"Standoff",
			"Sandtrap",
			"Icebox",
			"Reactor",
			"Narrows",
			"High Ground",
			"HighGround",
			"Last Resort",
			"LastResort",
			"Valhalla",
			"The Pit",
			"ThePit",
			"Guardian",
			"Diamondback",
			"Edge",
		};
		public static readonly Dictionary<string, BaseMap> BaseMapsByStringID = new Dictionary<string, BaseMap>()
		{
			{"guardian", BaseMap.Guardian },
			{"riverworld", BaseMap.Valhalla },
			{"s3d_avalanche", BaseMap.Diamondback },
			{"s3d_edge", BaseMap.Edge },
			{"s3d_reactor", BaseMap.Reactor },
			{"s3d_turf", BaseMap.Icebox },
			{"cyberdyne", BaseMap.ThePit },
			{"chill", BaseMap.Narrows },
			{"deadlock", BaseMap.HighGround },
			{"bunkerworld", BaseMap.Standoff },
			{"shrine", BaseMap.Sandtrap },
			{"zanzibar", BaseMap.LastResort },
		};
		public static readonly Dictionary<BaseMap, string> InternalMapStringsByBaseMap = new Dictionary<BaseMap, string>()
		{
			{BaseMap.Guardian, "guardian" },
			{BaseMap.Valhalla, "riverworld" },
			{BaseMap.Diamondback, "s3d_avalanche" },
			{BaseMap.Edge, "s3d_edge" },
			{BaseMap.Reactor, "s3d_reactor" },
			{BaseMap.Icebox, "s3d_turf" },
			{BaseMap.ThePit, "cyberdyne" },
			{BaseMap.Narrows, "chill" },
			{BaseMap.HighGround, "deadlock" },
			{BaseMap.Standoff, "bunkerworld" },
			{BaseMap.Sandtrap, "shrine" },
			{BaseMap.LastResort, "zanzibar" },
		};
		public static readonly Dictionary<BaseMap, string> BaseMapDisplayNamesByBaseMap = new Dictionary<BaseMap, string>()
		{
			{BaseMap.Guardian, "Guardian" },
			{BaseMap.Valhalla, "Valhalla" },
			{BaseMap.Diamondback, "Diamondback" },
			{BaseMap.Edge, "Edge" },
			{BaseMap.Reactor, "Reactor" },
			{BaseMap.Icebox, "Icebox" },
			{BaseMap.ThePit, "The Pit" },
			{BaseMap.Narrows, "Narrows" },
			{BaseMap.HighGround, "High Ground" },
			{BaseMap.Standoff, "Standoff" },
			{BaseMap.Sandtrap, "Sandtrap" },
			{BaseMap.LastResort, "Last Resort" },
			{BaseMap.Unknown, "Unknown" },
		};
		/// <summary>
		/// Contains base map names indexed by the hexadecimal IDs used in the map variant files.
		/// </summary>
		public static readonly Dictionary<int, string> BaseMapDisplayNamesByBasemapNumber = new Dictionary<int, string>()
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
		public static readonly Dictionary<int, string> BaseMapVoteFileNamesByBasemapNumber = new Dictionary<int, string>()
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
		public static readonly Dictionary<string, BaseMap> BaseMapIDsByName = new Dictionary<string, BaseMap>()
		{
			{"Standoff",BaseMap.Standoff},
			{"Sandtrap",BaseMap.Sandtrap},
			{"Shrine",BaseMap.Sandtrap},
			{"Icebox",BaseMap.Icebox},
			{"Turf",BaseMap.Icebox},
			{"Reactor",BaseMap.Reactor},
			{"Narrows",BaseMap.Narrows},
			{"High Ground",BaseMap.HighGround},
			{"HighGround", BaseMap.HighGround},
			{"Last Resort",BaseMap.LastResort},
			{"LastResort", BaseMap.LastResort},
			{"Zanzibar", BaseMap.LastResort},
			{"Valhalla",BaseMap.Valhalla},
			{"The Pit",BaseMap.ThePit},
			{"ThePit", BaseMap.ThePit},
			{"Guardian",BaseMap.Guardian},
			{"Diamondback",BaseMap.Diamondback},
			{"Sidewinder",BaseMap.Diamondback},
			{"Edge",BaseMap.Edge}
		};
		public static readonly Dictionary<string, BaseMap> BaseMapIDsByNameLower = new Dictionary<string, BaseMap>()
		{
			{"standoff",BaseMap.Standoff},
			{"sandtrap",BaseMap.Sandtrap},
			{"shrine",BaseMap.Sandtrap},
			{"icebox",BaseMap.Icebox},
			{"turf",BaseMap.Icebox},
			{"reactor",BaseMap.Reactor},
			{"narrows",BaseMap.Narrows},
			{"high ground",BaseMap.HighGround},
			{"highground", BaseMap.HighGround},
			{"last resort",BaseMap.LastResort},
			{"lastresort", BaseMap.LastResort},
			{"zanzibar", BaseMap.LastResort},
			{"valhalla",BaseMap.Valhalla},
			{"the pit",BaseMap.ThePit},
			{"thepit", BaseMap.ThePit},
			{"guardian",BaseMap.Guardian},
			{"diamondback",BaseMap.Diamondback},
			{"sidewinder",BaseMap.Diamondback},
			{"edge",BaseMap.Edge}
		};
		public static readonly Dictionary<string, BaseMap> BaseMapIDsByArgName = new Dictionary<string, BaseMap>()
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
		public static readonly Dictionary<BaseMap, string> BaseMapDescriptionsByBaseMap = new Dictionary<BaseMap, string>()
		{
			{BaseMap.Diamondback,"Hot winds blow over what should be a dead moon. A reminder of the power Forerunners once wielded. 6-16 players"},
			{BaseMap.Edge,		 "The remote frontier world of Partition has provided this ancient databank with the safety of seclusion. 6-16 players"},
			{BaseMap.Guardian,	 "Millennia of tending has produced trees as ancient as the Forerunner structures they have grown around. 2-6 players"},
			{BaseMap.HighGround, "A relic of older conflicts, this base was reactivated after the New Mombasa Slipspace Event. 4-12 players"},
			{BaseMap.Icebox,	 "Downtown Tyumen's Precinct 13 offers an ideal context for urban combat training. 4-10 players"},
			{BaseMap.LastResort, "Remote industrial sites like this one are routinely requisitioned and razed as part of Spartan training exercises. 4-12 players"},
			{BaseMap.Narrows,	 "Without cooling systems such as these, excess heat from The Ark's forges would render the construct uninhabitable. 2-8 players"},
			{BaseMap.Reactor,	 "Being constructed just prior to the Invasion, its builders had to evacuate before it was completed. 6-16 players"},
			{BaseMap.Sandtrap,	 "Although the Brute occupiers have been driven from this ancient structure, they left plenty to remember them by. 6-16 players"},
			{BaseMap.Standoff,	 "Once, nearby telescopes listened for a message from the stars. Now, these silos contain our prepared response. 4-12 players"},
			{BaseMap.ThePit,	 "Software simulations are held in contempt by the veteran instructors who run these training facilities. 4-10 players"},
			{BaseMap.Valhalla,	 "The crew of V-398 barely survived their unplanned landing in this gorge... this curious gorge. 6-16 players"},
		};

		public static readonly Dictionary<BaseMap, MapVariant> BaseMapVariantsByBaseMap = new Dictionary<BaseMap, MapVariant>()
		{
			{BaseMap.All, null }, 
			{BaseMap.Unknown, null },
			{BaseMap.Diamondback,	new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.Diamondback], Description = BaseMapDescriptionsByBaseMap[BaseMap.Diamondback], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.Diamondback], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.Diamondback], BaseMapID = BaseMap.Diamondback } },
			{BaseMap.Edge,			new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.Edge], Description = BaseMapDescriptionsByBaseMap[BaseMap.Edge], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.Edge], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.Edge], BaseMapID = BaseMap.Edge } },
			{BaseMap.Guardian,		new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.Guardian], Description = BaseMapDescriptionsByBaseMap[BaseMap.Guardian], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.Guardian], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.Guardian], BaseMapID = BaseMap.Guardian } },
			{BaseMap.HighGround,	new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.HighGround], Description = BaseMapDescriptionsByBaseMap[BaseMap.HighGround], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.HighGround], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.HighGround], BaseMapID = BaseMap.HighGround } },
			{BaseMap.Icebox,		new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.Icebox], Description = BaseMapDescriptionsByBaseMap[BaseMap.Icebox], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.Icebox], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.Icebox], BaseMapID = BaseMap.Icebox } },
			{BaseMap.LastResort,	new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.LastResort], Description = BaseMapDescriptionsByBaseMap[BaseMap.LastResort], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.LastResort], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.LastResort], BaseMapID = BaseMap.LastResort } },
			{BaseMap.Narrows,		new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.Narrows], Description = BaseMapDescriptionsByBaseMap[BaseMap.Narrows], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.Narrows], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.Narrows], BaseMapID = BaseMap.Narrows } },
			{BaseMap.Reactor,		new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.Reactor], Description = BaseMapDescriptionsByBaseMap[BaseMap.Reactor], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.Reactor], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.Reactor], BaseMapID = BaseMap.Reactor } },
			{BaseMap.Sandtrap,		new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.Sandtrap], Description = BaseMapDescriptionsByBaseMap[BaseMap.Sandtrap], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.Sandtrap], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.Sandtrap], BaseMapID = BaseMap.Sandtrap } },
			{BaseMap.Standoff,		new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.Standoff], Description = BaseMapDescriptionsByBaseMap[BaseMap.Standoff], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.Standoff], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.Standoff], BaseMapID = BaseMap.Standoff } },
			{BaseMap.ThePit,		new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.ThePit], Description = BaseMapDescriptionsByBaseMap[BaseMap.ThePit], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.ThePit], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.ThePit], BaseMapID = BaseMap.ThePit } },
			{BaseMap.Valhalla,		new MapVariant() { Author = "Bungie", Name = BaseMapDisplayNamesByBaseMap[BaseMap.Valhalla], Description = BaseMapDescriptionsByBaseMap[BaseMap.Valhalla], TypeNameForVotingFile = InternalMapStringsByBaseMap[BaseMap.Valhalla], BaseMapString = BaseMapDisplayNamesByBaseMap[BaseMap.Valhalla], BaseMapID = BaseMap.Valhalla } },
		};

		/// <summary>
		///		Returns the internal map name corresponding to the map whose "display name" was passed, or null if a match is not found.<br>
		///		Display names are 'Valhalla', 'Standoff', 'High Ground', etc. | Internal names are 'riverworld', 'bunkerworld', 'deadlock', etc.</br>
		/// </summary>
		/// <param name="mapDisplayName">
		///		The display name of the map you would like to get the internal name for.<br>
		///		Map display names are the official map names found in-game -'Valhalla', 'Standoff', 'High Ground', etc.</br>
		/// </param>
		/// <returns>Returns the internal map name if a match was found, otherwise returns null.</returns>
		public static string TryGetBaseMapInternalNameFromDisplayName(string mapDisplayName)
		{
			if (BaseMapIDsByNameLower.TryGetValue(mapDisplayName?.Replace(" ", "").ToLowerInvariant() ?? "", out BaseMap baseMap)) {
				return InternalMapStringsByBaseMap[baseMap];
			}
			else { 
				return null; 
			}
		}

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
				Name = BaseMapDisplayNamesByBasemapNumber[(int)map],
				Author = "Bungie",
				Description = "DEFAULT",
				TypeNameForVotingFile = BaseMapVoteFileNamesByBasemapNumber[(int)map],
				BaseMapString = BaseMapDisplayNamesByBasemapNumber[(int)map],
				BaseMapID = map
			};
		}
		public static bool TryGetBuiltInMapVariant(string name, out MapVariant mapVariant)
		{
			if (!string.IsNullOrWhiteSpace(name) && BaseMapIDsByName.ContainsKey(name)) {
				mapVariant = BaseMapVariantsByBaseMap[BaseMapIDsByName[name]];
				return true;
			}
			else {
				mapVariant = null;
				return false; 
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
			else if (BaseMapIDsByNameLower.ContainsKey(name))
			{
				return BaseMapIDsByNameLower[name];
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
					if (BaseMapDisplayNamesByBasemapNumber.ContainsKey(id)) { BaseMapString = BaseMapDisplayNamesByBasemapNumber[id]; BaseMapID = (BaseMap)id; }
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

		/// <summary>
		/// The map's description, capped with an ellipses (...) so that it fits in one chat message.
		/// </summary>
		public string Description_OneLine { 
			get { 
				if (description_OneLine == null) {
					description_OneLine = $"{Name}({BaseMapString}): {Description}";
					if (description_OneLine.Length > 122) { 
						description_OneLine = description_OneLine.Substring(0, 119) + "...";
					}
				}
				return description_OneLine;
			} 
		}
		private string description_OneLine;
		/// <summary>
		/// The map's full description, separated into multiple lines if it wouldn't fit entirely in one chat message.
		/// <br>If the full description does fit entirely in one chat message, the returned list will contain the full description as its only item.</br>
		/// </summary>
		public List<string> Description_Chunked {
			get {
				if (descriptions == null) {
					descriptions = new List<string>(
						$"{Name}({BaseMapString}): {Description}".Split(122)
					);
				}				
				return descriptions;
			}
		}
		private List<string> descriptions;

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
