using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{
	public class MatchInfo
	{
		public GameVariant GameVariant { get; set; }
		public MapVariant MapVariant { get; set; }
		public bool IsValid { get; set; } = true;

		public MatchInfo(GameVariant game, MapVariant map)
		{
			GameVariant = game; MapVariant = map;
		}

		public static MatchInfo Invalid { get { return new MatchInfo(null, null) { IsValid = false }; } }

	}
}
