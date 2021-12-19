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

		public static readonly List<string> BuiltInGameVariantNames = new List<string>()
		{
			"slayer",
			"team slayer",
			"rockets",
			"elimination",
			"duel",
			"oddball",
			"team oddball",
			"lowball",
			"ninjaball",
			"rocketball",
			"multi flag",
			"one flag",
			"tank flag",
			"attrition ctf",
			"assault",
			"neutral assault",
			"one bomb",
			"attrition assault",
			"infection",
			"save one bullet",
			"alpha zombie",
			"hide and seek",
			"crazy king",
			"team king",
			"mosh pit",
			"juggernaut",
			"mad dash",
			"ninjanaut",
			"vip",
			"one sided vip",
			"escort",
			"influential vip",
		};
		public static readonly List<string> BuiltInGameVariantDisplayNames = new List<string>()
		{
			"Slayer",
			"Team Slayer",
			"Rockets",
			"Elimination",
			"Duel",
			"Oddball",
			"Team Oddball",
			"Lowball",
			"Ninjaball",
			"Rocketball",
			"Multi Flag",
			"One Flag",
			"Tank Flag",
			"Attrition CTF",
			"Assault",
			"Neutral Bomb",
			"One Bomb",
			"Attrition Bomb",
			"Infection",
			"Save One Bullet",
			"Alpha Zombie",
			"Hide and Seek",
			"Crazy King",
			"Team King",
			"Mosh Pit",
			"Juggernaut",
			"Mad Dash",
			"Ninjanaut",
			"VIP",
			"One Sided VIP",
			"Escort",
			"Influential VIP",
		};
		public static readonly List<string> BuiltInGameVariantDescriptions = new List<string>()
		{
			/*"Slayer"*/		"Free for all. 25 kills to win.",
			/*"Team Slayer"*/	"Form teams for greater glory. 50 kills to win.",
			/*"Rockets"*/		"Free for all, with nothing but rocket launchers. 25 kills to win.",
			/*"Elimination"*/	"Highly tactical Team Slayer. 5 rounds, each to 5 kills. Be careful out there.",
			/*"Duel"*/			"No camping! Free for all, but the leader cannot hide. 10 kills to win.",
			/*"Oddball"*/		"Free for all. Hold the skull to earn points. 50 points to win.",
			/*"Team Oddball"*/	"Form teams, protect your carrier. 100 points to win.",
			/*"Lowball"*/		"Teamwork counts! Every member of your team must earn 25 points to win.",
			/*"Ninjaball"*/		"The ball carrier is fast and agile, but vulnerable. 100 points to win.",
			/*"Rocketball"*/	"You have no allies but your Rocket Launcher. 50 points to win.",
			/*"Multi Flag"*/	"Every team has a flag, and may capture when it is away. 3 captures to win.",
			/*"One Flag"*/		"Only one team has a flag. 4 rounds, and teams take turns defending the flag.",
			/*"Tank Flag"*/		"A variation of Multi Flag where the carrier is very tough, but very slow.",
			/*"Attrition CTF"*/	"Highly tactical CTF. Very long respawns, but teams respawn on captures.",
			/*"Assault"*/		"Every team has a bomb, and a base to defend. 3 detonations to win.",
			/*"Neutral Bomb"*/	"All teams have a base to defend, but share a single bomb. 3 detonations to win.",
			/*"One Bomb"*/		"Only one team has a base to defend. 4 rounds, and teams take turns defending.",
			/*"Attrition Bomb"*/"Highly tactical Assault. Neutral bomb, long respawns, but teams respawn on detonations.",
			/*"Infection"*/		"Some players start off as zombies and seek to devour humans. 3 rounds, most points wins.",
			/*"Save One Bullet"*/"The humans start off well armed, but ammunition is limited.",
			/*"Alpha Zombie"*/	"Players who start the round as zombies are more powerful than those they infect.",
			/*"Hide and Seek"*/	"Your only defense is stealth. Don't let the zombies find you!",
			/*"Crazy King"*/	"Free for all. Gain points by controlling the hill. 100 points to win.",
			/*"Team King"*/		"Hold the Hill as a team. The hill must be uncontested. 150 points to win.",
			/*"Mosh Pit"*/		"There is a single, unmoving hill. Everyone inside is tougher. 100 points to win.",
			/*"Juggernaut"*/	"Kill the Juggernaut to become the Juggernaut and earn points. 10 points to win.",
			/*"Mad Dash"*/		"Juggernaut scores points by reaching the destination zones. 5 points to win.",
			/*"Ninjanaut"*/		"The Juggernaut is fast, stealthy, and deadly. 10 points to win.",
			/*"VIP"*/			"Every team has a VIP. When your VIP dies, the next player to die becomes the new one. 10 points to win.",
			/*"One Sided VIP"*/	"Only one team has a VIP. 4 rounds, and teams take turns defending their VIP.",
			/*"Escort"*/		"Only one team has a VIP, who scores points by reaching a destination. If he dies, the round ends.",
			/*"Influential VIP"*/"Staying near your VIP makes you stronger, so move as a group. 10 points to win.",
		};
		public static readonly Dictionary<string, string> BuiltInGameVariantDescriptionsByName = new Dictionary<string, string>()
		{
			{"slayer",              "Free for all. 25 kills to win."},
			{"team slayer",         "Form teams for greater glory. 50 kills to win."},
			{"rockets",             "Free for all, with nothing but rocket launchers. 25 kills to win."},
			{"elimination",         "Highly tactical Team Slayer. 5 rounds, each to 5 kills. Be careful out there."},
			{"duel",                "No camping! Free for all, but the leader cannot hide. 10 kills to win."},
			{"oddball",             "Free for all. Hold the skull to earn points. 50 points to win."},
			{"team oddball",        "Form teams, protect your carrier. 100 points to win."},
			{"lowball",             "Teamwork counts! Every member of your team must earn 25 points to win."},
			{"ninjaball",           "The ball carrier is fast and agile, but vulnerable. 100 points to win."},
			{"rocketball",          "You have no allies but your Rocket Launcher. 50 points to win."},
			{"multi flag",          "Every team has a flag, and may capture when it is away. 3 captures to win."},
			{"one flag",            "Only one team has a flag. 4 rounds, and teams take turns defending the flag."},
			{"tank flag",           "A variation of Multi Flag where the carrier is very tough, but very slow."},
			{"attrition ctf",       "Highly tactical CTF. Very long respawns, but teams respawn on captures."},
			{"assault",             "Every team has a bomb, and a base to defend. 3 detonations to win."},
			{"neutral assault",     "All teams have a base to defend, but share a single bomb. 3 detonations to win."},
			{"one bomb",            "Only one team has a base to defend. 4 rounds, and teams take turns defending."},
			{"attrition assault",   "Highly tactical Assault. Neutral bomb, long respawns, but teams respawn on detonations."},
			{"infection",           "Some players start off as zombies and seek to devour humans. 3 rounds, most points wins."},
			{"save one bullet",     "The humans start off well armed, but ammunition is limited."},
			{"alpha zombie",        "Players who start the round as zombies are more powerful than those they infect."},
			{"hide and seek",       "Your only defense is stealth. Don't let the zombies find you!"},
			{"crazy king",          "Free for all. Gain points by controlling the hill. 100 points to win."},
			{"team king",           "Hold the Hill as a team. The hill must be uncontested. 150 points to win."},
			{"mosh pit",            "There is a single, unmoving hill. Everyone inside is tougher. 100 points to win."},
			{"juggernaut",          "Kill the Juggernaut to become the Juggernaut and earn points. 10 points to win."},
			{"mad dash",            "Juggernaut scores points by reaching the destination zones. 5 points to win."},
			{"ninjanaut",           "The Juggernaut is fast, stealthy, and deadly. 10 points to win."},
			{"vip",                 "Every team has a VIP. When your VIP dies, the next player to die becomes the new one. 10 points to win."},
			{"one sided vip",       "Only one team has a VIP. 4 rounds, and teams take turns defending their VIP."},
			{"escort",              "Only one team has a VIP, who scores points by reaching a destination. If he dies, the round ends."},
			{"influential vip",     "Staying near your VIP makes you stronger, so move as a group. 10 points to win."},
		};
		public static readonly List<string> BuiltInGameVariantDescriptionsWithNames = new List<string>()
		{
			"Slayer: Free for all. 25 kills to win.",
			"Team Slayer: Form teams for greater glory. 50 kills to win.",
			"Rockets: Free for all, with nothing but rocket launchers. 25 kills to win.",
			"Elimination: Highly tactical Team Slayer. 5 rounds, each to 5 kills. Be careful out there.",
			"Duel: No camping! Free for all, but the leader cannot hide. 10 kills to win.",
			"Oddball: Free for all. Hold the skull to earn points. 50 points to win.",
			"Team Oddball: Form teams, protect your carrier. 100 points to win.",
			"Lowball: Teamwork counts! Every member of your team must earn 25 points to win.",
			"Ninjaball: The ball carrier is fast and agile, but vulnerable. 100 points to win.",
			"Rocketball: You have no allies but your Rocket Launcher. 50 points to win.",
			"Multi Flag: Every team has a flag, and may capture when it is away. 3 captures to win.",
			"One Flag: Only one team has a flag. 4 rounds, and teams take turns defending the flag.",
			"Tank Flag: A variation of Multi Flag where the carrier is very tough, but very slow.",
			"Attrition CTF: Highly tactical CTF. Very long respawns, but teams respawn on captures.",
			"Assault: Every team has a bomb, and a base to defend. 3 detonations to win.",
			"Neutral Bomb: All teams have a base to defend, but share a single bomb. 3 detonations to win.",
			"One Bomb: Only one team has a base to defend. 4 rounds, and teams take turns defending.",
			"Attrition Bomb: Highly tactical Assault. Neutral bomb, long respawns, but teams respawn on detonations.",
			"Infection: Some players start off as zombies and seek to devour humans. 3 rounds, most points wins.",
			"Save One Bullet: The humans start off well armed, but ammunition is limited.",
			"Alpha Zombie: Players who start the round as zombies are more powerful than those they infect.",
			"Hide and Seek: Your only defense is stealth. Don't let the zombies find you!",
			"Crazy King: Free for all. Gain points by controlling the hill. 100 points to win.",
			"Team King: Hold the Hill as a team. The hill must be uncontested. 150 points to win.",
			"Mosh Pit: There is a single, unmoving hill. Everyone inside is tougher. 100 points to win.",
			"Juggernaut: Kill the Juggernaut to become the Juggernaut and earn points. 10 points to win.",
			"Mad Dash: Juggernaut scores points by reaching the destination zones. 5 points to win.",
			"Ninjanaut: The Juggernaut is fast, stealthy, and deadly. 10 points to win.",
			"VIP: Every team has a VIP. When your VIP dies, the next player to die becomes the new one. 10 points to win.",
			"One Sided VIP: Only one team has a VIP. 4 rounds, and teams take turns defending their VIP.",
			"Escort: Only one team has a VIP, who scores points by reaching a destination. If he dies, the round ends.",
			"Influential VIP: Staying near your VIP makes you stronger, so move as a group. 10 points to win.",
		};
		public static readonly Dictionary<BaseGame, List<BuiltInVariant>> BuiltInVariantsByBaseGame = new Dictionary<BaseGame, List<BuiltInVariant>>()
		{
			{ BaseGame.Slayer,			new List<BuiltInVariant>() { BuiltInVariant.Slayer, BuiltInVariant.TeamSlayer, BuiltInVariant.Rockets, BuiltInVariant.Elimination, BuiltInVariant.Duel, } },
			{ BaseGame.Oddball,			new List<BuiltInVariant>() { BuiltInVariant.Oddball, BuiltInVariant.TeamOddball, BuiltInVariant.Lowball, BuiltInVariant.Ninjaball, BuiltInVariant.Rocketball,} },
			{ BaseGame.CaptureTheFlag,	new List<BuiltInVariant>() { BuiltInVariant.MultiFlag, BuiltInVariant.OneFlag, BuiltInVariant.TankFlag, BuiltInVariant.AttritionCTF,} },
			{ BaseGame.Assault,			new List<BuiltInVariant>() { BuiltInVariant.Assault, BuiltInVariant.NeutralBomb, BuiltInVariant.OneBomb, BuiltInVariant.AttritionBomb,} },
			{ BaseGame.Infection,		new List<BuiltInVariant>() { BuiltInVariant.Infection, BuiltInVariant.SaveOneBullet, BuiltInVariant.AlphaZombie, BuiltInVariant.HideandSeek,} },
			{ BaseGame.KingOfTheHill,	new List<BuiltInVariant>() { BuiltInVariant.CrazyKing, BuiltInVariant.TeamKing, BuiltInVariant.MoshPit,} },
			{ BaseGame.Juggernaut,		new List<BuiltInVariant>() { BuiltInVariant.Juggernaut, BuiltInVariant.MadDash, BuiltInVariant.Ninjanaut,} },
			{ BaseGame.VIP,				new List<BuiltInVariant>() { BuiltInVariant.VIP, BuiltInVariant.OneSidedVIP, BuiltInVariant.Escort, BuiltInVariant.InfluentialVIP,} },
		};
		public static readonly Dictionary<BuiltInVariant, string> BuiltInVariantNameStringsByBuiltInVariant = new Dictionary<BuiltInVariant, string>()
		{
			{ BuiltInVariant.Slayer         , "slayer"             },
			{ BuiltInVariant.TeamSlayer     , "team slayer"        },
			{ BuiltInVariant.Rockets        , "rockets"            },
			{ BuiltInVariant.Elimination    , "elimination"        },
			{ BuiltInVariant.Duel           , "duel"               },
			{ BuiltInVariant.Oddball        , "oddball"            },
			{ BuiltInVariant.TeamOddball    , "team oddball"       },
			{ BuiltInVariant.Lowball        , "lowball"            },
			{ BuiltInVariant.Ninjaball      , "ninjaball"          },
			{ BuiltInVariant.Rocketball     , "rocketball"         },
			{ BuiltInVariant.MultiFlag      , "multi flag"         },
			{ BuiltInVariant.OneFlag        , "one flag"           },
			{ BuiltInVariant.TankFlag       , "tank flag"          },
			{ BuiltInVariant.AttritionCTF   , "attrition ctf"      },
			{ BuiltInVariant.Assault        , "assault"            },
			{ BuiltInVariant.NeutralBomb    , "neutral assault"    },
			{ BuiltInVariant.OneBomb        , "one bomb"           },
			{ BuiltInVariant.AttritionBomb  , "attrition assault"  },
			{ BuiltInVariant.Infection      , "infection"          },
			{ BuiltInVariant.SaveOneBullet  , "save one bullet"    },
			{ BuiltInVariant.AlphaZombie    , "alpha zombie"       },
			{ BuiltInVariant.HideandSeek    , "hide and seek"      },
			{ BuiltInVariant.CrazyKing      , "crazy king"         },
			{ BuiltInVariant.TeamKing       , "team king"          },
			{ BuiltInVariant.MoshPit        , "mosh pit"           },
			{ BuiltInVariant.Juggernaut     , "juggernaut"         },
			{ BuiltInVariant.MadDash        , "mad dash"           },
			{ BuiltInVariant.Ninjanaut      , "ninjanaut"          },
			{ BuiltInVariant.VIP            , "vip"                },
			{ BuiltInVariant.OneSidedVIP    , "one sided vip"      },
			{ BuiltInVariant.Escort         , "escort"             },
			{ BuiltInVariant.InfluentialVIP , "influential vip"    },
		};
		public static readonly Dictionary<BuiltInVariant, string> BuiltInVariantDisplayNamesByBuiltInVariant = new Dictionary<BuiltInVariant, string>()
		{
			{ BuiltInVariant.Slayer         , "Slayer"             },
			{ BuiltInVariant.TeamSlayer     , "Team Slayer"        },
			{ BuiltInVariant.Rockets        , "Rockets"            },
			{ BuiltInVariant.Elimination    , "Elimination"        },
			{ BuiltInVariant.Duel           , "Duel"               },
			{ BuiltInVariant.Oddball        , "Oddball"            },
			{ BuiltInVariant.TeamOddball    , "Team Oddball"       },
			{ BuiltInVariant.Lowball        , "Lowball"            },
			{ BuiltInVariant.Ninjaball      , "Ninjaball"          },
			{ BuiltInVariant.Rocketball     , "Rocketball"         },
			{ BuiltInVariant.MultiFlag      , "Multi Flag"         },
			{ BuiltInVariant.OneFlag        , "One Flag"           },
			{ BuiltInVariant.TankFlag       , "Tank Flag"          },
			{ BuiltInVariant.AttritionCTF   , "Attrition CTF"      },
			{ BuiltInVariant.Assault        , "Assault"            },
			{ BuiltInVariant.NeutralBomb    , "Neutral Bomb"       },
			{ BuiltInVariant.OneBomb        , "One Bomb"           },
			{ BuiltInVariant.AttritionBomb  , "Attrition Bomb"     },
			{ BuiltInVariant.Infection      , "Infection"          },
			{ BuiltInVariant.SaveOneBullet  , "Save One Bullet"    },
			{ BuiltInVariant.AlphaZombie    , "Alpha Zombie"       },
			{ BuiltInVariant.HideandSeek    , "Hide and Seek"      },
			{ BuiltInVariant.CrazyKing      , "Crazy King"         },
			{ BuiltInVariant.TeamKing       , "Team King"          },
			{ BuiltInVariant.MoshPit        , "Mosh Pit"           },
			{ BuiltInVariant.Juggernaut     , "Juggernaut"         },
			{ BuiltInVariant.MadDash        , "Mad Dash"           },
			{ BuiltInVariant.Ninjanaut      , "Ninjanaut"          },
			{ BuiltInVariant.VIP            , "VIP"                },
			{ BuiltInVariant.OneSidedVIP    , "One Sided VIP"      },
			{ BuiltInVariant.Escort         , "Escort"             },
			{ BuiltInVariant.InfluentialVIP , "Influential VIP"    },
		};
		public static readonly Dictionary<BuiltInVariant, string> BuiltInVariantDescriptionsByBuiltInVariant = new Dictionary<BuiltInVariant, string>()
		{
			{ BuiltInVariant.Slayer,           "Free for all. 25 kills to win." },
			{ BuiltInVariant.TeamSlayer,       "Form teams for greater glory. 50 kills to win." },
			{ BuiltInVariant.Rockets,          "Free for all, with nothing but rocket launchers. 25 kills to win." },
			{ BuiltInVariant.Elimination,      "Highly tactical Team Slayer. 5 rounds, each to 5 kills. Be careful out there." },
			{ BuiltInVariant.Duel,             "No camping! Free for all, but the leader cannot hide. 10 kills to win." },
			{ BuiltInVariant.Oddball,          "Free for all. Hold the skull to earn points. 50 points to win." },
			{ BuiltInVariant.TeamOddball,      "Form teams, protect your carrier. 100 points to win." },
			{ BuiltInVariant.Lowball,          "Teamwork counts! Every member of your team must earn 25 points to win." },
			{ BuiltInVariant.Ninjaball,        "The ball carrier is fast and agile, but vulnerable. 100 points to win." },
			{ BuiltInVariant.Rocketball,       "You have no allies but your Rocket Launcher. 50 points to win." },
			{ BuiltInVariant.MultiFlag,        "Every team has a flag, and may capture when it is away. 3 captures to win." },
			{ BuiltInVariant.OneFlag,          "Only one team has a flag. 4 rounds, and teams take turns defending the flag." },
			{ BuiltInVariant.TankFlag,         "A variation of Multi Flag where the carrier is very tough, but very slow." },
			{ BuiltInVariant.AttritionCTF,     "Highly tactical CTF. Very long respawns, but teams respawn on captures." },
			{ BuiltInVariant.Assault,          "Every team has a bomb, and a base to defend. 3 detonations to win." },
			{ BuiltInVariant.NeutralBomb,      "All teams have a base to defend, but share a single bomb. 3 detonations to win." },
			{ BuiltInVariant.OneBomb,          "Only one team has a base to defend. 4 rounds, and teams take turns defending." },
			{ BuiltInVariant.AttritionBomb,    "Highly tactical Assault. Neutral bomb, long respawns, but teams respawn on detonations." },
			{ BuiltInVariant.Infection,        "Some players start off as zombies and seek to devour humans. 3 rounds, most points wins." },
			{ BuiltInVariant.SaveOneBullet,    "The humans start off well armed, but ammunition is limited." },
			{ BuiltInVariant.AlphaZombie,      "Players who start the round as zombies are more powerful than those they infect." },
			{ BuiltInVariant.HideandSeek,      "Your only defense is stealth. Don't let the zombies find you!" },
			{ BuiltInVariant.CrazyKing,        "Free for all. Gain points by controlling the hill. 100 points to win." },
			{ BuiltInVariant.TeamKing,         "Hold the Hill as a team. The hill must be uncontested. 150 points to win." },
			{ BuiltInVariant.MoshPit,          "There is a single, unmoving hill. Everyone inside is tougher. 100 points to win." },
			{ BuiltInVariant.Juggernaut,       "Kill the Juggernaut to become the Juggernaut and earn points. 10 points to win." },
			{ BuiltInVariant.MadDash,          "Juggernaut scores points by reaching the destination zones. 5 points to win." },
			{ BuiltInVariant.Ninjanaut,        "The Juggernaut is fast, stealthy, and deadly. 10 points to win." },
			{ BuiltInVariant.VIP,              "Every team has a VIP. When your VIP dies, the next player to die becomes the new one. 10 points to win." },
			{ BuiltInVariant.OneSidedVIP,      "Only one team has a VIP. 4 rounds, and teams take turns defending their VIP." },
			{ BuiltInVariant.Escort,           "Only one team has a VIP, who scores points by reaching a destination. If he dies, the round ends." },
			{ BuiltInVariant.InfluentialVIP,   "Staying near your VIP makes you stronger, so move as a group. 10 points to win." },
		};
		public static readonly Dictionary<BuiltInVariant, string> BuiltInVariantDescriptionsWithNamesByBuiltInVariant = new Dictionary<BuiltInVariant, string>()
		{
			{ BuiltInVariant.Slayer,         "Slayer: Free for all. 25 kills to win." },
			{ BuiltInVariant.TeamSlayer,     "Team Slayer: Form teams for greater glory. 50 kills to win." },
			{ BuiltInVariant.Rockets,        "Rockets: Free for all, with nothing but rocket launchers. 25 kills to win." },
			{ BuiltInVariant.Elimination,    "Elimination: Highly tactical Team Slayer. 5 rounds, each to 5 kills. Be careful out there." },
			{ BuiltInVariant.Duel,           "Duel: No camping! Free for all, but the leader cannot hide. 10 kills to win." },
			{ BuiltInVariant.Oddball,        "Oddball: Free for all. Hold the skull to earn points. 50 points to win." },
			{ BuiltInVariant.TeamOddball,    "Team Oddball: Form teams, protect your carrier. 100 points to win." },
			{ BuiltInVariant.Lowball,        "Lowball: Teamwork counts! Every member of your team must earn 25 points to win." },
			{ BuiltInVariant.Ninjaball,      "Ninjaball: The ball carrier is fast and agile, but vulnerable. 100 points to win." },
			{ BuiltInVariant.Rocketball,     "Rocketball: You have no allies but your Rocket Launcher. 50 points to win." },
			{ BuiltInVariant.MultiFlag,      "Multi Flag: Every team has a flag, and may capture when it is away. 3 captures to win." },
			{ BuiltInVariant.OneFlag,        "One Flag: Only one team has a flag. 4 rounds, and teams take turns defending the flag." },
			{ BuiltInVariant.TankFlag,       "Tank Flag: A variation of Multi Flag where the carrier is very tough, but very slow." },
			{ BuiltInVariant.AttritionCTF,   "Attrition CTF: Highly tactical CTF. Very long respawns, but teams respawn on captures." },
			{ BuiltInVariant.Assault,        "Assault: Every team has a bomb, and a base to defend. 3 detonations to win." },
			{ BuiltInVariant.NeutralBomb,    "Neutral Bomb: All teams have a base to defend, but share a single bomb. 3 detonations to win." },
			{ BuiltInVariant.OneBomb,        "One Bomb: Only one team has a base to defend. 4 rounds, and teams take turns defending." },
			{ BuiltInVariant.AttritionBomb,  "Attrition Bomb: Highly tactical Assault. Neutral bomb, long respawns, but teams respawn on detonations." },
			{ BuiltInVariant.Infection,      "Infection: Some players start off as zombies and seek to devour humans. 3 rounds, most points wins." },
			{ BuiltInVariant.SaveOneBullet,  "Save One Bullet: The humans start off well armed, but ammunition is limited." },
			{ BuiltInVariant.AlphaZombie,    "Alpha Zombie: Players who start the round as zombies are more powerful than those they infect." },
			{ BuiltInVariant.HideandSeek,    "Hide and Seek: Your only defense is stealth. Don't let the zombies find you!" },
			{ BuiltInVariant.CrazyKing,      "Crazy King: Free for all. Gain points by controlling the hill. 100 points to win." },
			{ BuiltInVariant.TeamKing,       "Team King: Hold the Hill as a team. The hill must be uncontested. 150 points to win." },
			{ BuiltInVariant.MoshPit,        "Mosh Pit: There is a single, unmoving hill. Everyone inside is tougher. 100 points to win." },
			{ BuiltInVariant.Juggernaut,     "Juggernaut: Kill the Juggernaut to become the Juggernaut and earn points. 10 points to win." },
			{ BuiltInVariant.MadDash,        "Mad Dash: Juggernaut scores points by reaching the destination zones. 5 points to win." },
			{ BuiltInVariant.Ninjanaut,      "Ninjanaut: The Juggernaut is fast, stealthy, and deadly. 10 points to win." },
			{ BuiltInVariant.VIP,            "VIP: Every team has a VIP. When your VIP dies, the next player to die becomes the new one. 10 points to win." },
			{ BuiltInVariant.OneSidedVIP,    "One Sided VIP: Only one team has a VIP. 4 rounds, and teams take turns defending their VIP." },
			{ BuiltInVariant.Escort,         "Escort: Only one team has a VIP, who scores points by reaching a destination. If he dies, the round ends." },
			{ BuiltInVariant.InfluentialVIP, "Influential VIP: Staying near your VIP makes you stronger, so move as a group. 10 points to win." },
		};
		public static readonly Dictionary<string, BuiltInVariant> BuiltInVariantsByNameLower = new Dictionary<string, BuiltInVariant>()
		{
			{"slayer"           , BuiltInVariant.Slayer         },
			{"team slayer"      , BuiltInVariant.TeamSlayer     },
			{"rockets"          , BuiltInVariant.Rockets        },
			{"elimination"      , BuiltInVariant.Elimination    },
			{"duel"             , BuiltInVariant.Duel           },
			{"oddball"          , BuiltInVariant.Oddball        },
			{"team oddball"     , BuiltInVariant.TeamOddball    },
			{"lowball"          , BuiltInVariant.Lowball        },
			{"ninjaball"        , BuiltInVariant.Ninjaball      },
			{"rocketball"       , BuiltInVariant.Rocketball     },
			{"multi flag"       , BuiltInVariant.MultiFlag      },
			{"one flag"         , BuiltInVariant.OneFlag        },
			{"tank flag"        , BuiltInVariant.TankFlag       },
			{"attrition ctf"    , BuiltInVariant.AttritionCTF   },
			{"assault"          , BuiltInVariant.Assault        },
			{"neutral assault"  , BuiltInVariant.NeutralBomb    },
			{"one bomb"         , BuiltInVariant.OneBomb        },
			{"attrition assault", BuiltInVariant.AttritionBomb  },
			{"infection"        , BuiltInVariant.Infection      },
			{"save one bullet"  , BuiltInVariant.SaveOneBullet  },
			{"alpha zombie"     , BuiltInVariant.AlphaZombie    },
			{"hide and seek"    , BuiltInVariant.HideandSeek    },
			{"crazy king"       , BuiltInVariant.CrazyKing      },
			{"team king"        , BuiltInVariant.TeamKing       },
			{"mosh pit"         , BuiltInVariant.MoshPit        },
			{"juggernaut"       , BuiltInVariant.Juggernaut     },
			{"mad dash"         , BuiltInVariant.MadDash        },
			{"ninjanaut"        , BuiltInVariant.Ninjanaut      },
			{"vip"              , BuiltInVariant.VIP            },
			{"one sided vip"    , BuiltInVariant.OneSidedVIP    },
			{"escort"           , BuiltInVariant.Escort         },
			{"influential vip"  , BuiltInVariant.InfluentialVIP },
			//{ "slayer",             BuiltInVariant.Slayer         }, //{ BuiltInVariant.Slayer         , "slayer"             },
			//{ "teamslayer",         BuiltInVariant.TeamSlayer     }, //{ BuiltInVariant.TeamSlayer     , "team slayer"        },
			//{ "rockets",            BuiltInVariant.Rockets        }, //{ BuiltInVariant.Rockets        , "rockets"            },
			//{ "rocketslayer",       BuiltInVariant.Rockets        }, //{ BuiltInVariant.Elimination    , "elimination"        },
			//{ "elimination",        BuiltInVariant.Elimination    }, //{ BuiltInVariant.Duel           , "duel"               },
			//{ "eliminationslayer",  BuiltInVariant.Elimination    }, //{ BuiltInVariant.Oddball        , "oddball"            },
			//{ "duel",               BuiltInVariant.Duel           }, //{ BuiltInVariant.TeamOddball    , "team oddball"       },
			//{ "oddball",            BuiltInVariant.Oddball        }, //{ BuiltInVariant.Lowball        , "lowball"            },
			//{ "teamoddball",        BuiltInVariant.TeamOddball    }, //{ BuiltInVariant.Ninjaball      , "ninjaball"          },
			//{ "lowball",            BuiltInVariant.Lowball        }, //{ BuiltInVariant.Rocketball     , "rocketball"         },
			//{ "lowoddball",         BuiltInVariant.Lowball        }, //{ BuiltInVariant.MultiFlag      , "multi flag"         },
			//{ "ninjaball",          BuiltInVariant.Ninjaball      }, //{ BuiltInVariant.OneFlag        , "one flag"           },
			//{ "ninjaoddball",       BuiltInVariant.Ninjaball      }, //{ BuiltInVariant.TankFlag       , "tank flag"          },
			//{ "rocketball",         BuiltInVariant.Rocketball     }, //{ BuiltInVariant.AttritionCTF   , "attrition ctf"      },
			//{ "rocketoddball",      BuiltInVariant.Rocketball     }, //{ BuiltInVariant.Assault        , "assault"            },
			//{ "ctf",                BuiltInVariant.MultiFlag      }, //{ BuiltInVariant.NeutralBomb    , "neutral assault"    },
			//{ "multiflag",          BuiltInVariant.MultiFlag      }, //{ BuiltInVariant.OneBomb        , "one bomb"           },
			//{ "multiflagctf",       BuiltInVariant.MultiFlag      }, //{ BuiltInVariant.AttritionBomb  , "attrition assault"  },
			//{ "teammultiflag",      BuiltInVariant.MultiFlag      }, //{ BuiltInVariant.Infection      , "infection"          },
			//{ "teammultiflagctf",   BuiltInVariant.MultiFlag      }, //{ BuiltInVariant.SaveOneBullet  , "save one bullet"    },
			//{ "teamctf",            BuiltInVariant.MultiFlag      }, //{ BuiltInVariant.AlphaZombie    , "alpha zombie"       },
			//{ "capturetheflag",     BuiltInVariant.MultiFlag      }, //{ BuiltInVariant.HideandSeek    , "hide and seek"      },
			//{ "oneflag",            BuiltInVariant.OneFlag        }, //{ BuiltInVariant.CrazyKing      , "crazy king"         },
			//{ "oneflagctf",         BuiltInVariant.OneFlag        }, //{ BuiltInVariant.TeamKing       , "team king"          },
			//{ "singleflag",         BuiltInVariant.OneFlag        }, //{ BuiltInVariant.MoshPit        , "mosh pit"           },
			//{ "singleflatctf",      BuiltInVariant.OneFlag        }, //{ BuiltInVariant.Juggernaut     , "juggernaut"         },
			//{ "tankflag",           BuiltInVariant.TankFlag       }, //{ BuiltInVariant.MadDash        , "mad dash"           },
			//{ "attritionctf",       BuiltInVariant.AttritionCTF   }, //{ BuiltInVariant.Ninjanaut      , "ninjanaut"          },
			//{ "assault",            BuiltInVariant.Assault        }, //{ BuiltInVariant.VIP            , "vip"                },
			//{ "teamassault",        BuiltInVariant.Assault        }, //{ BuiltInVariant.OneSidedVIP    , "one sided vip"      },
			//{ "multibomb",          BuiltInVariant.Assault        }, //{ BuiltInVariant.Escort         , "escort"             },
			//{ "multibombassault",   BuiltInVariant.Assault        }, //{ BuiltInVariant.InfluentialVIP , "influential vip"    },
			//{ "neutralassault",     BuiltInVariant.NeutralBomb    },
			//{ "neutralbomb",        BuiltInVariant.NeutralBomb    },
			//{ "neutralbombassault", BuiltInVariant.NeutralBomb    },
			//{ "onebomb",            BuiltInVariant.OneBomb        },
			//{ "onebombassault",     BuiltInVariant.OneBomb        },
			//{ "singlebomb",         BuiltInVariant.OneBomb        },
			//{ "singlebombassault",  BuiltInVariant.OneBomb        },
			//{ "attritionassault",   BuiltInVariant.AttritionBomb  },
			//{ "infection",          BuiltInVariant.Infection      },
			//{ "zombies",            BuiltInVariant.Infection      },
			//{ "saveonebullet",      BuiltInVariant.SaveOneBullet  },
			//{ "alphazombie",        BuiltInVariant.AlphaZombie    },
			//{ "hideandseek",        BuiltInVariant.HideandSeek    },
			//{ "crazyking",          BuiltInVariant.CrazyKing      },
			//{ "crazykingofthehill", BuiltInVariant.CrazyKing      },
			//{ "teamking",           BuiltInVariant.TeamKing       },
			//{ "kingofthehill",      BuiltInVariant.TeamKing       },
			//{ "moshpit",            BuiltInVariant.MoshPit        },
			//{ "juggernaut",         BuiltInVariant.Juggernaut     },
			//{ "maddash",            BuiltInVariant.MadDash        },
			//{ "ninjanaut",          BuiltInVariant.Ninjanaut      },
			//{ "vip",                BuiltInVariant.VIP            },
			//{ "teamvip",            BuiltInVariant.VIP            },
			//{ "onesidedvip",        BuiltInVariant.OneSidedVIP    },
			//{ "onevip",             BuiltInVariant.OneSidedVIP    },
			//{ "singlevip",          BuiltInVariant.OneSidedVIP    },
			//{ "escort",             BuiltInVariant.Escort         },
			//{ "escortvip",          BuiltInVariant.Escort         },
			//{ "vipescort",          BuiltInVariant.Escort         },
			//{ "influentialvip",     BuiltInVariant.InfluentialVIP },
		};

		public static readonly Dictionary<string, string> VariantGametypeExtensionAssociations = new Dictionary<string, string>() {
			{ ".slayer", "Slayer" },
			{".oddball", "Oddball"},
			{".koth", "King of the Hill"},
			{".ctf", "Capture the Flag"},
			{".assault", "Assault"},
			{".jugg", "Juggernaut"},
			{".zombiez", "Infection"},
			{".vip", "VIP"},
		};
		public static readonly Dictionary<string, BaseGame> BaseGamesByName = new Dictionary<string, BaseGame>()
		{
			{ "Slayer", BaseGame.Slayer },
			{ "TeamSlayer", BaseGame.Slayer },
			{ "Team Slayer", BaseGame.Slayer },
			{ "Oddball", BaseGame.Oddball },
			{ "Assault", BaseGame.Assault },
			{ "Bomb", BaseGame.Assault },
			{ "CaptureTheFlag", BaseGame.CaptureTheFlag },
			{ "CTF", BaseGame.CaptureTheFlag },
			{ "Capture The Flag", BaseGame.CaptureTheFlag },
			{ "Infection", BaseGame.Infection },
			{ "Infected", BaseGame.Infection },
			{ "Zombies", BaseGame.Infection },
			{ "Zombie", BaseGame.Infection },
			{ "VIP", BaseGame.VIP },
			{ "KingOfTheHill", BaseGame.KingOfTheHill},
			{ "King Of The Hill", BaseGame.KingOfTheHill},
			{ "KOTH", BaseGame.KingOfTheHill},
			{ "King", BaseGame.KingOfTheHill},
			{ "Juggernaut", BaseGame.Juggernaut }
		};
		public static readonly Dictionary<string, BaseGame> BaseGamesByNameLower = new Dictionary<string, BaseGame>()
		{
			{ "slayer", BaseGame.Slayer },
			{ "teamslayer", BaseGame.Slayer },
			{ "team slayer", BaseGame.Slayer },
			{ "oddball", BaseGame.Oddball },
			{ "assault", BaseGame.Assault },
			{ "bomb", BaseGame.Assault },
			{ "capturetheflag", BaseGame.CaptureTheFlag },
			{ "ctf", BaseGame.CaptureTheFlag },
			{ "capture the flag", BaseGame.CaptureTheFlag },
			{ "infection", BaseGame.Infection },
			{ "infected", BaseGame.Infection },
			{ "zombies", BaseGame.Infection },
			{ "zombie", BaseGame.Infection },
			{ "vip", BaseGame.VIP },
			{ "kingofthehill", BaseGame.KingOfTheHill},
			{ "king of the hill", BaseGame.KingOfTheHill},
			{ "koth", BaseGame.KingOfTheHill},
			{ "king", BaseGame.KingOfTheHill},
			{ "juggernaut", BaseGame.Juggernaut }
		};
		public static readonly Dictionary<BaseGame, string> BaseGameInternalNamesByBaseGame = new Dictionary<BaseGame, string>()
		{
			{BaseGame.Slayer, "slayer"},
			{BaseGame.Oddball, "oddball"},
			{BaseGame.KingOfTheHill, "koth"},
			{BaseGame.CaptureTheFlag, "ctf"},
			{BaseGame.Assault, "assault"},
			{BaseGame.Juggernaut, "jugg"},
			{BaseGame.Infection, "zombiez"},
			{BaseGame.VIP, "vip"},
		};
		public static readonly Dictionary<string, BaseGame> BaseGamesByInternalBaseGameNames = new Dictionary<string, BaseGame>()
		{
			{"slayer", BaseGame.Slayer},
			{"oddball", BaseGame.Oddball},
			{"koth", BaseGame.KingOfTheHill},
			{"ctf", BaseGame.CaptureTheFlag},
			{"assault", BaseGame.Assault},
			{"jugg", BaseGame.Juggernaut},
			{"zombiez", BaseGame.Infection},
			{"vip", BaseGame.VIP},
		};
		public static readonly Dictionary<BaseGame, string> BaseGameDisplayNamesByBaseGame = new Dictionary<BaseGame, string>()
		{
			{BaseGame.Slayer, "Slayer"},
			{BaseGame.Oddball, "Oddball"},
			{BaseGame.KingOfTheHill, "King of the Hill"},
			{BaseGame.CaptureTheFlag, "Capture the Flag"},
			{BaseGame.Assault, "Assault"},
			{BaseGame.Juggernaut, "Juggernaut"},
			{BaseGame.Infection, "Infection"},
			{BaseGame.VIP, "VIP"},
			{BaseGame.Unknown, "Unknown" }
		};
		public static BaseGame GetBaseGame(string name)
		{
			if (BaseGamesByName.ContainsKey(name))
			{
				return BaseGamesByName[name];
			}
			else if (BaseGamesByNameLower.ContainsKey(name))
			{
				return BaseGamesByNameLower[name];
			}
			else
			{
				return BaseGame.Unknown;
			}
		}
		private const string UnknownVariantIdentifier = "Unknown";

		/// <summary>
		///		Returns the internal game name corresponding to the game whose "display name" was passed, or null if a match is not found.<br>
		///		Display names are 'Slayer', 'Juggernaut', 'Capture The Flag', etc. | Internal names are 'slayer', 'jugg', 'ctf', etc.</br>
		/// </summary>
		/// <param name="gameDisplayName">
		///		The display name of the game you would like to get the internal name for.<br>
		///		Game display names are the official game names found in-game -'Slayer', 'Juggernaut', 'Capture The Flag', etc.</br>
		/// </param>
		/// <returns>Returns the internal game name if a match was found, otherwise returns null.</returns>
		public static string TryGetBaseGameInternalNameFromDisplayName(string gameDisplayName)
		{
			if (BuiltInVariantsByNameLower.TryGetValue(gameDisplayName?.ToLowerInvariant() ?? "", out BuiltInVariant builtInVariant)) {
				return BuiltInVariantNameStringsByBuiltInVariant[builtInVariant];
			}
			else {
				return null;
			}
		}

		public static GameVariant TryGetBuiltInVariant(string game)
		{
			if (BuiltInVariantsByNameLower.ContainsKey(game?.ToLowerInvariant() ?? "")) {
				BuiltInVariant builtInVariant = BuiltInVariantsByNameLower[game.ToLowerInvariant()];
				return CreateBuiltInVariant(builtInVariant);
			}
			else {
				return null;
			}
		}
		private static GameVariant CreateBuiltInVariant(BuiltInVariant builtInVariant)
		{
			BaseGame bg = BaseGame.Unknown;
			foreach (KeyValuePair<BaseGame, List<BuiltInVariant>> item in BuiltInVariantsByBaseGame) {
				if (item.Value.Contains(builtInVariant)) { bg = item.Key; }
			}
			if (bg == BaseGame.Unknown) { return null; }
			return new GameVariant()
			{
				Author = "Bungie",
				BaseGameID = bg,
				BaseGameTypeString = BaseGameDisplayNamesByBaseGame[bg],
				Description = BuiltInVariantDescriptionsByBuiltInVariant[builtInVariant],
				Name = BuiltInVariantDisplayNamesByBuiltInVariant[builtInVariant],
				TypeNameForVotingFile = BuiltInVariantNameStringsByBuiltInVariant[builtInVariant],
				IsValid = true
			};
		}

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
		public static BaseGame GetBaseGameIDFromFilename(string filename)
		{
			string[] split = filename.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
			if (split.Length > 0) { return GetBaseGameID(split[split.Length - 1]); }
			return BaseGame.Unknown;
		}
		public static BaseGame GetBaseGameID(string game)
		{
			switch (game)
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

		public bool IsValid { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }		
		public string TypeNameForVotingFile { get; set; }
		public string BaseGameTypeString { get; set; }
		public BaseGame BaseGameID { get; set; }

		private GameVariant() { }
		public GameVariant(BaseGame baseGame, string variantName, string variantDisplayName, string variantDescription, string variantAuthor)
		{			
			BaseGameID = baseGame;
			BaseGameTypeString = BaseGameInternalNamesByBaseGame[baseGame];
			Name = variantDisplayName;
			TypeNameForVotingFile = variantDisplayName;
			Description = variantDescription;
			Author = variantAuthor;
		}
		public GameVariant(DirectoryInfo folder)
		{
			string baseGameType;
			foreach (FileInfo file in folder.GetFiles())
			{
				baseGameType = GetBaseGametype(file.Name);
				BaseGameID = GetBaseGameIDFromFilename(file.Name);
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

		/// <summary>
		/// The game's description, capped with an ellipses (...) so that it fits in one chat message.
		/// </summary>
		public string Description_OneLine {
			get {
				if (description_OneLine == null) {
					description_OneLine = $"{Name}({BaseGameTypeString}): {Description}";
					if (description_OneLine.Length > 122) {
						description_OneLine = description_OneLine.Substring(0, 119) + "...";
					}
				}
				return description_OneLine;
			}
		}
		private string description_OneLine;
		/// <summary>
		/// The game's full description, separated into multiple lines if it wouldn't fit entirely in one chat message.
		/// <br>If the full description does fit entirely in one chat message, the returned list will contain the full description as its only item.</br>
		/// </summary>
		public List<string> Description_Chunked {
			get {
				if (descriptions == null) {
					descriptions = new List<string>(
						$"{Name}({BaseGameTypeString}): {Description}".Split(122)
					);
				}
				return descriptions;
			}
		}
		private List<string> descriptions;


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

		public enum BuiltInVariant
		{
			Slayer,             // "Slayer",
			TeamSlayer,         // "Team Slayer",
			Rockets,            // "Rockets",
			Elimination,        // "Elimination",
			Duel,               // "Duel",
			Oddball,            // "Oddball",
			TeamOddball,        // "Team Oddball",
			Lowball,            // "Lowball",
			Ninjaball,          // "Ninjaball",
			Rocketball,         // "Rocketball",
			MultiFlag,          // "Multi Flag",
			OneFlag,            // "One Flag",
			TankFlag,           // "Tank Flag",
			AttritionCTF,       // "Attrition CTF",
			Assault,            // "Assault",
			NeutralBomb,        // "Neutral Bomb",
			OneBomb,            // "One Bomb",
			AttritionBomb,      // "Attrition Bomb",
			Infection,          // "Infection",
			SaveOneBullet,      // "Save One Bullet",
			AlphaZombie,        // "Alpha Zombie",
			HideandSeek,        // "Hide and Seek",
			CrazyKing,          // "Crazy King",
			TeamKing,           // "Team King",
			MoshPit,            // "Mosh Pit",
			Juggernaut,         // "Juggernaut",
			MadDash,            // "Mad Dash",
			Ninjanaut,          // "Ninjanaut",
			VIP,                // "VIP",
			OneSidedVIP,        // "One Sided VIP",
			Escort,             // "Escort",
			InfluentialVIP,     // "Influential VIP",
		}

	}
}
