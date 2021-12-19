using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace RconTool
{

	[JsonObject(MemberSerialization.OptIn)]
	public class Translation
	{

		public static bool IsValidLanguageCode(string code)
		{
			if (string.IsNullOrWhiteSpace(code)) { return false; }
			return LanguageCodes.Contains(code);
		}

		public static readonly List<string> LanguageCodes = new List<string>()
		{
			"af",
			"sq",
			"am",
			"ar",
			"hy",
			"az",
			"eu",
			"be",
			"bn",
			"bs",
			"bg",
			"ca",
			"ceb",
			"ny",
			"zh-CN",
			"zh",
			"zh-TW",
			"co",
			"hr",
			"cs",
			"da",
			"nl",
			"en",
			"eo",
			"et",
			"tl",
			"fi",
			"fr",
			"fy",
			"gl",
			"ka",
			"de",
			"el",
			"gu",
			"ht",
			"ha",
			"haw",
			"iw",
			"he",
			"hi",
			"hmn",
			"hu",
			"is",
			"ig",
			"id",
			"ga",
			"it",
			"ja",
			"jw",
			"kn",
			"kk",
			"km",
			"rw",
			"ko",
			"ku",
			"ky",
			"lo",
			"la",
			"lv",
			"lt",
			"lb",
			"mk",
			"mg",
			"ms",
			"ml",
			"mt",
			"mi",
			"mr",
			"mn",
			"my",
			"ne",
			"no",
			"or",
			"ps",
			"fa",
			"pl",
			"pt",
			"pa",
			"ro",
			"ru",
			"sm",
			"gd",
			"sr",
			"st",
			"sn",
			"sd",
			"si",
			"sk",
			"sl",
			"so",
			"es",
			"su",
			"sw",
			"sv",
			"tg",
			"ta",
			"tt",
			"te",
			"th",
			"tr",
			"tk",
			"uk",
			"ur",
			"ug",
			"uz",
			"vi",
			"cy",
			"xh",
			"yi",
			"yo",
			"zu"
		};

		public Translation() {}
		public Translation(string translationCode, string translationText)
		{
			SetTranslation(translationCode, translationText);
		}

		public void SetTranslation(string code, string translation)
		{
			if (string.IsNullOrWhiteSpace(code)) { return; }
			switch (code)
			{
				case "af": af = translation; return;
				case "am": am = translation; return;
				case "ar": ar = translation; return;
				case "az": az = translation; return;
				case "be": be = translation; return;
				case "bg": bg = translation; return;
				case "bn": bn = translation; return;
				case "bs": bs = translation; return;
				case "ca": ca = translation; return;
				case "ceb": ceb = translation; return;
				case "co": co = translation; return;
				case "cs": cs = translation; return;
				case "cy": cy = translation; return;
				case "da": da = translation; return;
				case "de": de = translation; return;
				case "el": el = translation; return;
				case "en": en = translation; return;
				case "eo": eo = translation; return;
				case "es": es = translation; return;
				case "et": et = translation; return;
				case "eu": eu = translation; return;
				case "fa": fa = translation; return;
				case "fi": fi = translation; return;
				case "fr": fr = translation; return;
				case "fy": fy = translation; return;
				case "ga": ga = translation; return;
				case "gd": gd = translation; return;
				case "gl": gl = translation; return;
				case "gu": gu = translation; return;
				case "ha": ha = translation; return;
				case "haw": haw = translation; return;
				case "he": he = translation; return;
				case "hi": hi = translation; return;
				case "hmn": hmn = translation; return;
				case "hr": hr = translation; return;
				case "ht": ht = translation; return;
				case "hu": hu = translation; return;
				case "hy": hy = translation; return;
				case "id": id = translation; return;
				case "ig": ig = translation; return;
				case "is": ic = translation; return;
				case "it": it = translation; return;
				case "iw": iw = translation; return;
				case "ja": ja = translation; return;
				case "jw": jw = translation; return;
				case "ka": ka = translation; return;
				case "kk": kk = translation; return;
				case "km": km = translation; return;
				case "kn": kn = translation; return;
				case "ko": ko = translation; return;
				case "ku": ku = translation; return;
				case "ky": ky = translation; return;
				case "la": la = translation; return;
				case "lb": lb = translation; return;
				case "lo": lo = translation; return;
				case "lt": lt = translation; return;
				case "lv": lv = translation; return;
				case "mg": mg = translation; return;
				case "mi": mi = translation; return;
				case "mk": mk = translation; return;
				case "ml": ml = translation; return;
				case "mn": mn = translation; return;
				case "mr": mr = translation; return;
				case "ms": ms = translation; return;
				case "mt": mt = translation; return;
				case "my": my = translation; return;
				case "ne": ne = translation; return;
				case "nl": nl = translation; return;
				case "no": no = translation; return;
				case "ny": ny = translation; return;
				case "or": or = translation; return;
				case "pa": pa = translation; return;
				case "pl": pl = translation; return;
				case "ps": ps = translation; return;
				case "pt": pt = translation; return;
				case "ro": ro = translation; return;
				case "ru": ru = translation; return;
				case "rw": rw = translation; return;
				case "sd": sd = translation; return;
				case "si": si = translation; return;
				case "sk": sk = translation; return;
				case "sl": sl = translation; return;
				case "sm": sm = translation; return;
				case "sn": sn = translation; return;
				case "so": so = translation; return;
				case "sq": sq = translation; return;
				case "sr": sr = translation; return;
				case "st": st = translation; return;
				case "su": su = translation; return;
				case "sv": sv = translation; return;
				case "sw": sw = translation; return;
				case "ta": ta = translation; return;
				case "te": te = translation; return;
				case "tg": tg = translation; return;
				case "th": th = translation; return;
				case "tk": tk = translation; return;
				case "tl": tl = translation; return;
				case "tr": tr = translation; return;
				case "tt": tt = translation; return;
				case "ug": ug = translation; return;
				case "uk": uk = translation; return;
				case "ur": ur = translation; return;
				case "uz": uz = translation; return;
				case "vi": vi = translation; return;
				case "xh": xh = translation; return;
				case "yi": yi = translation; return;
				case "yo": yo = translation; return;
				case "zh-CN": zhCN = translation; return;
				case "zh-TW": zhTW = translation; return;
				case "zh": zh = translation; return;
				case "zu": zu = translation; return;
				default: return;
			}
			#region old hash-based switch
			//switch (code?.GetHashCode() ?? 0)
			//{
			//	case 0:				return;
			//	case -840714306:	bg = translation; return;
			//	case -840714301:	mg = translation; return;
			//	case -840714297:	ig = translation; return;
			//	case -840714293:	ug = translation; return;
			//	case -840714292:	tg = translation; return;
			//	case -840648769:	af = translation; return;
			//	case -840583236:	de = translation; return;
			//	case -840583234:	be = translation; return;
			//	case -840583230:	ne = translation; return;
			//	case -840583224:	he = translation; return;
			//	case -840583220:	te = translation; return;
			//	case -840517703:	gd = translation; return;
			//	case -840517689:	id = translation; return;
			//	case -840517683:	sd = translation; return;
			//	case -840386620:	lb = translation; return;
			//	case -840321095:	ga = translation; return;
			//	case -840321094:	fa = translation; return;
			//	case -840321092:	da = translation; return;
			//	case -840321091:	ca = translation; return;
			//	case -840321084:	la = translation; return;
			//	case -840321083:	ka = translation; return;
			//	case -840321082:	ja = translation; return;
			//	case -840321080:	ha = translation; return;
			//	case -840321076:	ta = translation; return;
			//	case -840321072:	pa = translation; return;
			//	case -840190021:	eo = translation; return;
			//	case -840190019:	co = translation; return;
			//	case -840190014:	no = translation; return;
			//	case -840190012:	lo = translation; return;
			//	case -840190011:	ko = translation; return;
			//	case -840190003:	so = translation; return;
			//	case -840190002:	ro = translation; return;
			//	case -840189993:	yo = translation; return;
			//	case -840124485:	en = translation; return;
			//	case -840124482:	bn = translation; return;
			//	case -840124477:	mn = translation; return;
			//	case -840124475:	kn = translation; return;
			//	case -840124467:	sn = translation; return;
			//	case -840058945:	am = translation; return;
			//	case -840058939:	km = translation; return;
			//	case -840058931:	sm = translation; return;
			//	case -839993415:	gl = translation; return;
			//	case -839993413:	el = translation; return;
			//	case -839993406:	nl = translation; return;
			//	case -839993405:	ml = translation; return;
			//	case -839993396:	tl = translation; return;
			//	case -839993395:	sl = translation; return;
			//	case -839993392:	pl = translation; return;
			//	case -839927869:	mk = translation; return;
			//	case -839927867:	kk = translation; return;
			//	case -839927861:	uk = translation; return;
			//	case -839927860:	tk = translation; return;
			//	case -839927859:	sk = translation; return;
			//	case -839796806:	fi = translation; return;
			//	case -839796797:	mi = translation; return;
			//	case -839796792:	hi = translation; return;
			//	case -839796790:	vi = translation; return;
			//	case -839796787:	si = translation; return;
			//	case -839796777:	yi = translation; return;
			//	case -839731252:	th = translation; return;
			//	case -839731242:	zh = translation; return;
			//	case -839731240:	xh = translation; return;
			//	case -839665722:	jw = translation; return;
			//	case -839665721:	iw = translation; return;
			//	case -839665715:	sw = translation; return;
			//	case -839665714:	rw = translation; return;
			//	case -839600188:	lv = translation; return;
			//	case -839600179:	sv = translation; return;
			//	case -839534663:	gu = translation; return;
			//	case -839534661:	eu = translation; return;
			//	case -839534651:	ku = translation; return;
			//	case -839534648:	hu = translation; return;
			//	case -839534643:	su = translation; return;
			//	case -839534642:	ru = translation; return;
			//	case -839534634:	zu = translation; return;
			//	case -839469125:	et = translation; return;
			//	case -839469117:	mt = translation; return;
			//	case -839469116:	lt = translation; return;
			//	case -839469113:	it = translation; return;
			//	case -839469112:	ht = translation; return;
			//	case -839469108:	tt = translation; return;
			//	case -839469107:	st = translation; return;
			//	case -839469104:	pt = translation; return;
			//	case -839403589:	es = translation; return;
			//	case -839403587:	cs = translation; return;
			//	case -839403586:	bs = translation; return;
			//	case -839403581:	ms = translation; return;
			//	case -839403577:	ic = translation; return;
			//	case -839403568:	ps = translation; return;
			//	case -839338054:	fr = translation; return;
			//	case -839338049:	ar = translation; return;
			//	case -839338047:	or = translation; return;
			//	case -839338045:	mr = translation; return;
			//	case -839338040:	hr = translation; return;
			//	case -839338037:	ur = translation; return;
			//	case -839338036:	tr = translation; return;
			//	case -839338035:	sr = translation; return;
			//	case -839272499:	sq = translation; return;
			//	case -838813761:	az = translation; return;
			//	case -838813749:	uz = translation; return;
			//	case -838748230:	fy = translation; return;
			//	case -838748227:	cy = translation; return;
			//	case -838748222:	ny = translation; return;
			//	case -838748221:	my = translation; return;
			//	case -838748219:	ky = translation; return;
			//	case -838748216:	hy = translation; return;
			//	case -352059729:	zhCN = translation; return;
			//	case -222195428:	hmn = translation; return;
			//	case -2144771873:	haw = translation; return;
			//	case 367984278:		zhTW = translation; return;
			//	case 2102879101:	ceb = translation; return;
			//	default: return;
			//}
			#endregion
		}
		public string GetTranslation(string code)
		{

			if (string.IsNullOrWhiteSpace(code)) { return null; }
			switch (code) {
				case "af": return af;
				case "am": return am;
				case "ar": return ar;
				case "az": return az;
				case "be": return be;
				case "bg": return bg;
				case "bn": return bn;
				case "bs": return bs;
				case "ca": return ca;
				case "ceb": return ceb;
				case "co": return co;
				case "cs": return cs;
				case "cy": return cy;
				case "da": return da;
				case "de": return de;
				case "el": return el;
				case "en": return en;
				case "eo": return eo;
				case "es": return es;
				case "et": return et;
				case "eu": return eu;
				case "fa": return fa;
				case "fi": return fi;
				case "fr": return fr;
				case "fy": return fy;
				case "ga": return ga;
				case "gd": return gd;
				case "gl": return gl;
				case "gu": return gu;
				case "ha": return ha;
				case "haw": return haw;
				case "he": return he;
				case "hi": return hi;
				case "hmn": return hmn;
				case "hr": return hr;
				case "ht": return ht;
				case "hu": return hu;
				case "hy": return hy;
				case "id": return id;
				case "ig": return ig;
				case "is": return ic; // 'is' is a reserved keyword - changed to ic for icelandic
				case "it": return it;
				case "iw": return iw;
				case "ja": return ja;
				case "jw": return jw;
				case "ka": return ka;
				case "kk": return kk;
				case "km": return km;
				case "kn": return kn;
				case "ko": return ko;
				case "ku": return ku;
				case "ky": return ky;
				case "la": return la;
				case "lb": return lb;
				case "lo": return lo;
				case "lt": return lt;
				case "lv": return lv;
				case "mg": return mg;
				case "mi": return mi;
				case "mk": return mk;
				case "ml": return ml;
				case "mn": return mn;
				case "mr": return mr;
				case "ms": return ms;
				case "mt": return mt;
				case "my": return my;
				case "ne": return ne;
				case "nl": return nl;
				case "no": return no;
				case "ny": return ny;
				case "or": return or;
				case "pa": return pa;
				case "pl": return pl;
				case "ps": return ps;
				case "pt": return pt;
				case "ro": return ro;
				case "ru": return ru;
				case "rw": return rw;
				case "sd": return sd;
				case "si": return si;
				case "sk": return sk;
				case "sl": return sl;
				case "sm": return sm;
				case "sn": return sn;
				case "so": return so;
				case "sq": return sq;
				case "sr": return sr;
				case "st": return st;
				case "su": return su;
				case "sv": return sv;
				case "sw": return sw;
				case "ta": return ta;
				case "te": return te;
				case "tg": return tg;
				case "th": return th;
				case "tk": return tk;
				case "tl": return tl;
				case "tr": return tr;
				case "tt": return tt;
				case "ug": return ug;
				case "uk": return uk;
				case "ur": return ur;
				case "uz": return uz;
				case "vi": return vi;
				case "xh": return xh;
				case "yi": return yi;
				case "yo": return yo;
				case "zh-CN": return zhCN;
				case "zh-TW": return zhTW;
				case "zh": return zh;
				case "zu": return zu;
				default: return null;
			}

			#region old hash-based switch
			//switch (code?.GetHashCode() ?? 0) {
			//case 0:				return null;
			//case -840714306:	return bg;
			//case -840714301:	return mg;
			//case -840714297:	return ig;
			//case -840714293:	return ug;
			//case -840714292:	return tg;
			//case -840648769:	return af;
			//case -840583236:	return de;
			//case -840583234:	return be;
			//case -840583230:	return ne;
			//case -840583224:	return he;
			//case -840583220:	return te;
			//case -840517703:	return gd;
			//case -840517689:	return id;
			//case -840517683:	return sd;
			//case -840386620:	return lb;
			//case -840321095:	return ga;
			//case -840321094:	return fa;
			//case -840321092:	return da;
			//case -840321091:	return ca;
			//case -840321084:	return la;
			//case -840321083:	return ka;
			//case -840321082:	return ja;
			//case -840321080:	return ha;
			//case -840321076:	return ta;
			//case -840321072:	return pa;
			//case -840190021:	return eo;
			//case -840190019:	return co;
			//case -840190014:	return no;
			//case -840190012:	return lo;
			//case -840190011:	return ko;
			//case -840190003:	return so;
			//case -840190002:	return ro;
			//case -840189993:	return yo;
			//case -840124485:	return en;
			//case -840124482:	return bn;
			//case -840124477:	return mn;
			//case -840124475:	return kn;
			//case -840124467:	return sn;
			//case -840058945:	return am;
			//case -840058939:	return km;
			//case -840058931:	return sm;
			//case -839993415:	return gl;
			//case -839993413:	return el;
			//case -839993406:	return nl;
			//case -839993405:	return ml;
			//case -839993396:	return tl;
			//case -839993395:	return sl;
			//case -839993392:	return pl;
			//case -839927869:	return mk;
			//case -839927867:	return kk;
			//case -839927861:	return uk;
			//case -839927860:	return tk;
			//case -839927859:	return sk;
			//case -839796806:	return fi;
			//case -839796797:	return mi;
			//case -839796792:	return hi;
			//case -839796790:	return vi;
			//case -839796787:	return si;
			//case -839796777:	return yi;
			//case -839731252:	return th;
			//case -839731242:	return zh;
			//case -839731240:	return xh;
			//case -839665722:	return jw;
			//case -839665721:	return iw;
			//case -839665715:	return sw;
			//case -839665714:	return rw;
			//case -839600188:	return lv;
			//case -839600179:	return sv;
			//case -839534663:	return gu;
			//case -839534661:	return eu;
			//case -839534651:	return ku;
			//case -839534648:	return hu;
			//case -839534643:	return su;
			//case -839534642:	return ru;
			//case -839534634:	return zu;
			//case -839469125:	return et;
			//case -839469117:	return mt;
			//case -839469116:	return lt;
			//case -839469113:	return it;
			//case -839469112:	return ht;
			//case -839469108:	return tt;
			//case -839469107:	return st;
			//case -839469104:	return pt;
			//case -839403589:	return es;
			//case -839403587:	return cs;
			//case -839403586:	return bs;
			//case -839403581:	return ms;
			//case -839403577:	return ic;
			//case -839403568:	return ps;
			//case -839338054:	return fr;
			//case -839338049:	return ar;
			//case -839338047:	return or;
			//case -839338045:	return mr;
			//case -839338040:	return hr;
			//case -839338037:	return ur;
			//case -839338036:	return tr;
			//case -839338035:	return sr;
			//case -839272499:	return sq;
			//case -838813761:	return az;
			//case -838813749:	return uz;
			//case -838748230:	return fy;
			//case -838748227:	return cy;
			//case -838748222:	return ny;
			//case -838748221:	return my;
			//case -838748219:	return ky;
			//case -838748216:	return hy;
			//case -352059729:	return zhCN;
			//case -222195428:	return hmn;
			//case -2144771873:	return haw;
			//case 367984278:		return zhTW;
			//case 2102879101:	return ceb;
			//default: return null;
			//}
			#endregion

		}

		public static readonly List<string> DefaultFilteredEnglishPhrases = new List<string>()
		{
			"gg",
			"ez",
			"ggez",
			"gg ez",
			"lmao",
			"omg",
			"lmfao",
			"rofl",
			"lol",
			"lel",
			"kek",
			"lool",
			"loll",
			"llol",
			"lolol",
			"lololol",
			"noob",
			"nub",
			"wut",
			"wat",
			"wot",
			"get em",
			"go",
			"gogo",
			"salutations",
			"yo",
			"uwu",
			"owo",
			"nani",
			"brb",
			"c u later",
			"see u latr",
			"cya",
			"see ya",
			"bye",
			"l8r",
			"later",
			"noice",
		};

		#region Translations

		[JsonProperty("af", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string af { get; set; } // Afrikaans

		[JsonProperty("sq", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string sq { get; set; } // Albanian

		[JsonProperty("am", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string am { get; set; } // Amharic

		[JsonProperty("ar", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ar { get; set; } // Arabic

		[JsonProperty("hy", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string hy { get; set; } // Armenian

		[JsonProperty("az", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string az { get; set; } // Azerbaijani

		[JsonProperty("eu", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string eu { get; set; } // Basque

		[JsonProperty("be", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string be { get; set; } // Belarusian

		[JsonProperty("bn", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string bn { get; set; } // Bengali

		[JsonProperty("bs", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string bs { get; set; } // Bosnian

		[JsonProperty("bg", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string bg { get; set; } // Bulgarian

		[JsonProperty("ca", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ca { get; set; } // Catalan

		[JsonProperty("ceb", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ceb { get; set; } // Cebuano

		[JsonProperty("ny", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ny { get; set; } // Chichewa

		[JsonProperty("zh-CN", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string zhCN { get; set; } // Chinese (Simplified)

		[JsonProperty("zh", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string zh { get; set; } // Chinese (Simplified)

		[JsonProperty("zh-TW", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string zhTW { get; set; } // Chinese (Traditional)

		[JsonProperty("co", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string co { get; set; } // Corsican

		[JsonProperty("hr", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string hr { get; set; } // Croatian

		[JsonProperty("cs", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string cs { get; set; } // Czech

		[JsonProperty("da", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string da { get; set; } // Danish

		[JsonProperty("nl", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string nl { get; set; } // Dutch

		[JsonProperty("en", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string en { get; set; } // English

		[JsonProperty("eo", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string eo { get; set; } // Esperanto

		[JsonProperty("et", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string et { get; set; } // Estonian

		[JsonProperty("tl", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string tl { get; set; } // Filipino

		[JsonProperty("fi", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string fi { get; set; } // Finnish

		[JsonProperty("fr", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string fr { get; set; } // French

		[JsonProperty("fy", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string fy { get; set; } // Frisian

		[JsonProperty("gl", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string gl { get; set; } // Galician

		[JsonProperty("ka", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ka { get; set; } // Georgian

		[JsonProperty("de", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string de { get; set; } // German

		[JsonProperty("el", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string el { get; set; } // Greek

		[JsonProperty("gu", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string gu { get; set; } // Gujarati

		[JsonProperty("ht", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ht { get; set; } // Haitian Creole

		[JsonProperty("ha", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ha { get; set; } // Hausa

		[JsonProperty("haw", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string haw { get; set; } // Hawaiian

		[JsonProperty("iw", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string iw { get; set; } // Hebrew

		[JsonProperty("he", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string he { get; set; } // Hebrew

		[JsonProperty("hi", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string hi { get; set; } // Hindi

		[JsonProperty("hmn", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string hmn { get; set; } // Hmong

		[JsonProperty("hu", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string hu { get; set; } // Hungarian

		[JsonProperty("is", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ic { get; set; } // Icelandic

		[JsonProperty("ig", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ig { get; set; } // Igbo

		[JsonProperty("id", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string id { get; set; } // Indonesian

		[JsonProperty("ga", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ga { get; set; } // Irish

		[JsonProperty("it", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string it { get; set; } // Italian

		[JsonProperty("ja", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ja { get; set; } // Japanese

		[JsonProperty("jw", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string jw { get; set; } // Javanese

		[JsonProperty("kn", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string kn { get; set; } // Kannada

		[JsonProperty("kk", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string kk { get; set; } // Kazakh

		[JsonProperty("km", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string km { get; set; } // Khmer

		[JsonProperty("rw", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string rw { get; set; } // Kinyarwanda

		[JsonProperty("ko", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ko { get; set; } // Korean

		[JsonProperty("ku", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ku { get; set; } // Kurdish (Kurmanji)

		[JsonProperty("ky", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ky { get; set; } // Kyrgyz

		[JsonProperty("lo", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string lo { get; set; } // Lao

		[JsonProperty("la", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string la { get; set; } // Latin

		[JsonProperty("lv", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string lv { get; set; } // Latvian

		[JsonProperty("lt", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string lt { get; set; } // Lithuanian

		[JsonProperty("lb", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string lb { get; set; } // Luxembourgish

		[JsonProperty("mk", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string mk { get; set; } // Macedonian

		[JsonProperty("mg", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string mg { get; set; } // Malagasy

		[JsonProperty("ms", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ms { get; set; } // Malay

		[JsonProperty("ml", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ml { get; set; } // Malayalam

		[JsonProperty("mt", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string mt { get; set; } // Maltese

		[JsonProperty("mi", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string mi { get; set; } // Maori

		[JsonProperty("mr", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string mr { get; set; } // Marathi

		[JsonProperty("mn", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string mn { get; set; } // Mongolian

		[JsonProperty("my", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string my { get; set; } // Myanmar (Burmese)

		[JsonProperty("ne", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ne { get; set; } // Nepali

		[JsonProperty("no", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string no { get; set; } // Norwegian

		[JsonProperty("or", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string or { get; set; } // Odia (Oriya)

		[JsonProperty("ps", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ps { get; set; } // Pashto

		[JsonProperty("fa", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string fa { get; set; } // Persian

		[JsonProperty("pl", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string pl { get; set; } // Polish

		[JsonProperty("pt", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string pt { get; set; } // Portuguese

		[JsonProperty("pa", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string pa { get; set; } // Punjabi

		[JsonProperty("ro", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ro { get; set; } // Romanian

		[JsonProperty("ru", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ru { get; set; } // Russian

		[JsonProperty("sm", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string sm { get; set; } // Samoan

		[JsonProperty("gd", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string gd { get; set; } // Scots Gaelic

		[JsonProperty("sr", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string sr { get; set; } // Serbian

		[JsonProperty("st", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string st { get; set; } // Sesotho

		[JsonProperty("sn", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string sn { get; set; } // Shona

		[JsonProperty("sd", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string sd { get; set; } // Sindhi

		[JsonProperty("si", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string si { get; set; } // Sinhala

		[JsonProperty("sk", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string sk { get; set; } // Slovak

		[JsonProperty("sl", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string sl { get; set; } // Slovenian

		[JsonProperty("so", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string so { get; set; } // Somali

		[JsonProperty("es", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string es { get; set; } // Spanish

		[JsonProperty("su", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string su { get; set; } // Sundanese

		[JsonProperty("sw", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string sw { get; set; } // Swahili

		[JsonProperty("sv", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string sv { get; set; } // Swedish

		[JsonProperty("tg", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string tg { get; set; } // Tajik

		[JsonProperty("ta", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ta { get; set; } // Tamil

		[JsonProperty("tt", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string tt { get; set; } // Tatar

		[JsonProperty("te", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string te { get; set; } // Telugu

		[JsonProperty("th", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string th { get; set; } // Thai

		[JsonProperty("tr", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string tr { get; set; } // Turkish

		[JsonProperty("tk", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string tk { get; set; } // Turkmen

		[JsonProperty("uk", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string uk { get; set; } // Ukrainian

		[JsonProperty("ur", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ur { get; set; } // Urdu

		[JsonProperty("ug", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ug { get; set; } // Uyghur

		[JsonProperty("uz", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string uz { get; set; } // Uzbek

		[JsonProperty("vi", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string vi { get; set; } // Vietnamese

		[JsonProperty("cy", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string cy { get; set; } // Welsh

		[JsonProperty("xh", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string xh { get; set; } // Xhosa

		[JsonProperty("yi", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string yi { get; set; } // Yiddish

		[JsonProperty("yo", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string yo { get; set; } // Yoruba

		[JsonProperty("zu", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string zu { get; set; } // Zulu

		#endregion

		public static readonly Dictionary<string, string> EnglishLanguageNameStringsByLanguageCode = new Dictionary<string, string>()
		{
			{"af",  "Afrikaans"},
			{"sq",  "Albanian"},
			{"am",  "Amharic"},
			{"ar",  "Arabic"},
			{"hy",  "Armenian"},
			{"az",  "Azerbaijani"},
			{"eu",  "Basque"},
			{"be",  "Belarusian"},
			{"bn",  "Bengali"},
			{"bs",  "Bosnian"},
			{"bg",  "Bulgarian"},
			{"ca",  "Catalan"},
			{"ceb", "Cebuano"},
			{"ny",  "Chichewa"},
			{"zh",  "Chinese_Simplified"},
			{"zhCN","Chinese_Simplified_CN"},
			{"zhTW","Chinese_Traditional_TW"},
			{"co",  "Corsican"},
			{"hr",  "Croatian"},
			{"cs",  "Czech"},
			{"da",  "Danish"},
			{"nl",  "Dutch"},
			{"en",  "English"},
			{"eo",  "Esperanto"},
			{"et",  "Estonian"},
			{"tl",  "Filipino"},
			{"fi",  "Finnish"},
			{"fr",  "French"},
			{"fy",  "Frisian"},
			{"gl",  "Galician"},
			{"ka",  "Georgian"},
			{"de",  "German"},
			{"el",  "Greek"},
			{"gu",  "Gujarati"},
			{"ht",  "Haitian_Creole"},
			{"ha",  "Hausa"},
			{"haw", "Hawaiian"},
			{"iw",  "Hebrew_IW"},
			{"he",  "Hebrew_HE"},
			{"hi",  "Hindi"},
			{"hmn", "Hmong"},
			{"hu",  "Hungarian"},
			{"ic",  "Icelandic"},
			{"ig",  "Igbo"},
			{"id",  "Indonesian"},
			{"ga",  "Irish"},
			{"it",  "Italian"},
			{"ja",  "Japanese"},
			{"jw",  "Javanese"},
			{"kn",  "Kannada"},
			{"kk",  "Kazakh"},
			{"km",  "Khmer"},
			{"rw",  "Kinyarwanda"},
			{"ko",  "Korean"},
			{"ku",  "Kurdish_Kurmanji"},
			{"ky",  "Kyrgyz"},
			{"lo",  "Lao"},
			{"la",  "Latin"},
			{"lv",  "Latvian"},
			{"lt",  "Lithuanian"},
			{"lb",  "Luxembourgish"},
			{"mk",  "Macedonian"},
			{"mg",  "Malagasy"},
			{"ms",  "Malay"},
			{"ml",  "Malayalam"},
			{"mt",  "Maltese"},
			{"mi",  "Maori"},
			{"mr",  "Marathi"},
			{"mn",  "Mongolian"},
			{"my",  "Myanmar_Burmese"},
			{"ne",  "Nepali"},
			{"no",  "Norwegian"},
			{"or",  "Odia_Oriya"},
			{"ps",  "Pashto"},
			{"fa",  "Persian"},
			{"pl",  "Polish"},
			{"pt",  "Portuguese"},
			{"pa",  "Punjabi"},
			{"ro",  "Romanian"},
			{"ru",  "Russian"},
			{"sm",  "Samoan"},
			{"gd",  "Scots_Gaelic"},
			{"sr",  "Serbian"},
			{"st",  "Sesotho"},
			{"sn",  "Shona"},
			{"sd",  "Sindhi"},
			{"si",  "Sinhala"},
			{"sk",  "Slovak"},
			{"sl",  "Slovenian"},
			{"so",  "Somali"},
			{"es",  "Spanish"},
			{"su",  "Sundanese"},
			{"sw",  "Swahili"},
			{"sv",  "Swedish"},
			{"tg",  "Tajik"},
			{"ta",  "Tamil"},
			{"tt",  "Tatar"},
			{"te",  "Telugu"},
			{"th",  "Thai"},
			{"tr",  "Turkish"},
			{"tk",  "Turkmen"},
			{"uk",  "Ukrainian"},
			{"ur",  "Urdu"},
			{"ug",  "Uyghur"},
			{"uz",  "Uzbek"},
			{"vi",  "Vietnamese"},
			{"cy",  "Welsh"},
			{"xh",  "Xhosa"},
			{"yi",  "Yiddish"},
			{"yo",  "Yoruba"},
			{"zu",  "Zulu"},
		};
		public static readonly Dictionary<string, string> LanguageCodesByEnglishLanguageNameString = new Dictionary<string, string>()
		{
			{"Afrikaans",                "af"},
			{"Albanian",                 "sq"},
			{"Amharic",                  "am"},
			{"Arabic",                   "ar"},
			{"Armenian",                 "hy"},
			{"Azerbaijani",              "az"},
			{"Basque",                   "eu"},
			{"Belarusian",               "be"},
			{"Bengali",                  "bn"},
			{"Bosnian",                  "bs"},
			{"Bulgarian",                "bg"},
			{"Catalan",                  "ca"},
			{"Cebuano",                  "ceb"},
			{"Chichewa",                 "ny"},
			{"Chinese_Simplified",       "zh"},
			{"Chinese_Simplified_CN",    "zhCN"},
			{"Chinese_Traditional_TW",   "zhTW"},
			{"Corsican",                 "co"},
			{"Croatian",                 "hr"},
			{"Czech",                    "cs"},
			{"Danish",                   "da"},
			{"Dutch",                    "nl"},
			{"English",                  "en"},
			{"Esperanto",                "eo"},
			{"Estonian",                 "et"},
			{"Filipino",                 "tl"},
			{"Finnish",                  "fi"},
			{"French",                   "fr"},
			{"Frisian",                  "fy"},
			{"Galician",                 "gl"},
			{"Georgian",                 "ka"},
			{"German",                   "de"},
			{"Greek",                    "el"},
			{"Gujarati",                 "gu"},
			{"Haitian_Creole",           "ht"},
			{"Hausa",                    "ha"},
			{"Hawaiian",                 "haw"},
			{"Hebrew_IW",                "iw"},
			{"Hebrew_HE",                "he"},
			{"Hindi",                    "hi"},
			{"Hmong",                    "hmn"},
			{"Hungarian",                "hu"},
			{"Icelandic",                "ic"},
			{"Igbo",                     "ig"},
			{"Indonesian",               "id"},
			{"Irish",                    "ga"},
			{"Italian",                  "it"},
			{"Japanese",                 "ja"},
			{"Javanese",                 "jw"},
			{"Kannada",                  "kn"},
			{"Kazakh",                   "kk"},
			{"Khmer",                    "km"},
			{"Kinyarwanda",              "rw"},
			{"Korean",                   "ko"},
			{"Kurdish_Kurmanji",         "ku"},
			{"Kyrgyz",                   "ky"},
			{"Lao",                      "lo"},
			{"Latin",                    "la"},
			{"Latvian",                  "lv"},
			{"Lithuanian",               "lt"},
			{"Luxembourgish",            "lb"},
			{"Macedonian",               "mk"},
			{"Malagasy",                 "mg"},
			{"Malay",                    "ms"},
			{"Malayalam",                "ml"},
			{"Maltese",                  "mt"},
			{"Maori",                    "mi"},
			{"Marathi",                  "mr"},
			{"Mongolian",                "mn"},
			{"Myanmar_Burmese",          "my"},
			{"Nepali",                   "ne"},
			{"Norwegian",                "no"},
			{"Odia_Oriya",               "or"},
			{"Pashto",                   "ps"},
			{"Persian",                  "fa"},
			{"Polish",                   "pl"},
			{"Portuguese",               "pt"},
			{"Punjabi",                  "pa"},
			{"Romanian",                 "ro"},
			{"Russian",                  "ru"},
			{"Samoan",                   "sm"},
			{"Scots_Gaelic",             "gd"},
			{"Serbian",                  "sr"},
			{"Sesotho",                  "st"},
			{"Shona",                    "sn"},
			{"Sindhi",                   "sd"},
			{"Sinhala",                  "si"},
			{"Slovak",                   "sk"},
			{"Slovenian",                "sl"},
			{"Somali",                   "so"},
			{"Spanish",                  "es"},
			{"Sundanese",                "su"},
			{"Swahili",                  "sw"},
			{"Swedish",                  "sv"},
			{"Tajik",                    "tg"},
			{"Tamil",                    "ta"},
			{"Tatar",                    "tt"},
			{"Telugu",                   "te"},
			{"Thai",                     "th"},
			{"Turkish",                  "tr"},
			{"Turkmen",                  "tk"},
			{"Ukrainian",                "uk"},
			{"Urdu",                     "ur"},
			{"Uyghur",                   "ug"},
			{"Uzbek",                    "uz"},
			{"Vietnamese",               "vi"},
			{"Welsh",                    "cy"},
			{"Xhosa",                    "xh"},
			{"Yiddish",                  "yi"},
			{"Yoruba",                   "yo"},
			{"Zulu",                     "zu"},
		};
		public static readonly Dictionary<EnglishLanguageNames, string> LanguageCodesByEnglishLanguageName = new Dictionary<EnglishLanguageNames, string>()
		{
			{EnglishLanguageNames.Afrikaans,                "af"},
			{EnglishLanguageNames.Albanian,                 "sq"},
			{EnglishLanguageNames.Amharic,                  "am"},
			{EnglishLanguageNames.Arabic,                   "ar"},
			{EnglishLanguageNames.Armenian,                 "hy"},
			{EnglishLanguageNames.Azerbaijani,              "az"},
			{EnglishLanguageNames.Basque,                   "eu"},
			{EnglishLanguageNames.Belarusian,               "be"},
			{EnglishLanguageNames.Bengali,                  "bn"},
			{EnglishLanguageNames.Bosnian,                  "bs"},
			{EnglishLanguageNames.Bulgarian,                "bg"},
			{EnglishLanguageNames.Catalan,                  "ca"},
			{EnglishLanguageNames.Cebuano,                  "ceb"},
			{EnglishLanguageNames.Chichewa,                 "ny"},
			{EnglishLanguageNames.Chinese_Simplified,       "zh"},
			{EnglishLanguageNames.Chinese_Simplified_CN,    "zhCN"},
			{EnglishLanguageNames.Chinese_Traditional_TW,   "zhTW"},
			{EnglishLanguageNames.Corsican,                 "co"},
			{EnglishLanguageNames.Croatian,                 "hr"},
			{EnglishLanguageNames.Czech,                    "cs"},
			{EnglishLanguageNames.Danish,                   "da"},
			{EnglishLanguageNames.Dutch,                    "nl"},
			{EnglishLanguageNames.English,                  "en"},
			{EnglishLanguageNames.Esperanto,                "eo"},
			{EnglishLanguageNames.Estonian,                 "et"},
			{EnglishLanguageNames.Filipino,                 "tl"},
			{EnglishLanguageNames.Finnish,                  "fi"},
			{EnglishLanguageNames.French,                   "fr"},
			{EnglishLanguageNames.Frisian,                  "fy"},
			{EnglishLanguageNames.Galician,                 "gl"},
			{EnglishLanguageNames.Georgian,                 "ka"},
			{EnglishLanguageNames.German,                   "de"},
			{EnglishLanguageNames.Greek,                    "el"},
			{EnglishLanguageNames.Gujarati,                 "gu"},
			{EnglishLanguageNames.Haitian_Creole,           "ht"},
			{EnglishLanguageNames.Hausa,                    "ha"},
			{EnglishLanguageNames.Hawaiian,                 "haw"},
			{EnglishLanguageNames.Hebrew_IW,                "iw"},
			{EnglishLanguageNames.Hebrew_HE,                "he"},
			{EnglishLanguageNames.Hindi,                    "hi"},
			{EnglishLanguageNames.Hmong,                    "hmn"},
			{EnglishLanguageNames.Hungarian,                "hu"},
			{EnglishLanguageNames.Icelandic,                "ic"},
			{EnglishLanguageNames.Igbo,                     "ig"},
			{EnglishLanguageNames.Indonesian,               "id"},
			{EnglishLanguageNames.Irish,                    "ga"},
			{EnglishLanguageNames.Italian,                  "it"},
			{EnglishLanguageNames.Japanese,                 "ja"},
			{EnglishLanguageNames.Javanese,                 "jw"},
			{EnglishLanguageNames.Kannada,                  "kn"},
			{EnglishLanguageNames.Kazakh,                   "kk"},
			{EnglishLanguageNames.Khmer,                    "km"},
			{EnglishLanguageNames.Kinyarwanda,              "rw"},
			{EnglishLanguageNames.Korean,                   "ko"},
			{EnglishLanguageNames.Kurdish_Kurmanji,         "ku"},
			{EnglishLanguageNames.Kyrgyz,                   "ky"},
			{EnglishLanguageNames.Lao,                      "lo"},
			{EnglishLanguageNames.Latin,                    "la"},
			{EnglishLanguageNames.Latvian,                  "lv"},
			{EnglishLanguageNames.Lithuanian,               "lt"},
			{EnglishLanguageNames.Luxembourgish,            "lb"},
			{EnglishLanguageNames.Macedonian,               "mk"},
			{EnglishLanguageNames.Malagasy,                 "mg"},
			{EnglishLanguageNames.Malay,                    "ms"},
			{EnglishLanguageNames.Malayalam,                "ml"},
			{EnglishLanguageNames.Maltese,                  "mt"},
			{EnglishLanguageNames.Maori,                    "mi"},
			{EnglishLanguageNames.Marathi,                  "mr"},
			{EnglishLanguageNames.Mongolian,                "mn"},
			{EnglishLanguageNames.Myanmar_Burmese,          "my"},
			{EnglishLanguageNames.Nepali,                   "ne"},
			{EnglishLanguageNames.Norwegian,                "no"},
			{EnglishLanguageNames.Odia_Oriya,               "or"},
			{EnglishLanguageNames.Pashto,                   "ps"},
			{EnglishLanguageNames.Persian,                  "fa"},
			{EnglishLanguageNames.Polish,                   "pl"},
			{EnglishLanguageNames.Portuguese,               "pt"},
			{EnglishLanguageNames.Punjabi,                  "pa"},
			{EnglishLanguageNames.Romanian,                 "ro"},
			{EnglishLanguageNames.Russian,                  "ru"},
			{EnglishLanguageNames.Samoan,                   "sm"},
			{EnglishLanguageNames.Scots_Gaelic,             "gd"},
			{EnglishLanguageNames.Serbian,                  "sr"},
			{EnglishLanguageNames.Sesotho,                  "st"},
			{EnglishLanguageNames.Shona,                    "sn"},
			{EnglishLanguageNames.Sindhi,                   "sd"},
			{EnglishLanguageNames.Sinhala,                  "si"},
			{EnglishLanguageNames.Slovak,                   "sk"},
			{EnglishLanguageNames.Slovenian,                "sl"},
			{EnglishLanguageNames.Somali,                   "so"},
			{EnglishLanguageNames.Spanish,                  "es"},
			{EnglishLanguageNames.Sundanese,                "su"},
			{EnglishLanguageNames.Swahili,                  "sw"},
			{EnglishLanguageNames.Swedish,                  "sv"},
			{EnglishLanguageNames.Tajik,                    "tg"},
			{EnglishLanguageNames.Tamil,                    "ta"},
			{EnglishLanguageNames.Tatar,                    "tt"},
			{EnglishLanguageNames.Telugu,                   "te"},
			{EnglishLanguageNames.Thai,                     "th"},
			{EnglishLanguageNames.Turkish,                  "tr"},
			{EnglishLanguageNames.Turkmen,                  "tk"},
			{EnglishLanguageNames.Ukrainian,                "uk"},
			{EnglishLanguageNames.Urdu,                     "ur"},
			{EnglishLanguageNames.Uyghur,                   "ug"},
			{EnglishLanguageNames.Uzbek,                    "uz"},
			{EnglishLanguageNames.Vietnamese,               "vi"},
			{EnglishLanguageNames.Welsh,                    "cy"},
			{EnglishLanguageNames.Xhosa,                    "xh"},
			{EnglishLanguageNames.Yiddish,                  "yi"},
			{EnglishLanguageNames.Yoruba,                   "yo"},
			{EnglishLanguageNames.Zulu,                     "zu"},
		};
		public enum EnglishLanguageNames
		{
			Afrikaans,
			Albanian,
			Amharic,
			Arabic,
			Armenian,
			Azerbaijani,
			Basque,
			Belarusian,
			Bengali,
			Bosnian,
			Bulgarian,
			Catalan,
			Cebuano,
			Chichewa,
			Chinese_Simplified,
			Chinese_Simplified_CN,
			Chinese_Traditional_TW,
			Corsican,
			Croatian,
			Czech,
			Danish,
			Dutch,
			English,
			Esperanto,
			Estonian,
			Filipino,
			Finnish,
			French,
			Frisian,
			Galician,
			Georgian,
			German,
			Greek,
			Gujarati,
			Haitian_Creole,
			Hausa,
			Hawaiian,
			Hebrew_IW,
			Hebrew_HE,
			Hindi,
			Hmong,
			Hungarian,
			Icelandic,
			Igbo,
			Indonesian,
			Irish,
			Italian,
			Japanese,
			Javanese,
			Kannada,
			Kazakh,
			Khmer,
			Kinyarwanda,
			Korean,
			Kurdish_Kurmanji,
			Kyrgyz,
			Lao,
			Latin,
			Latvian,
			Lithuanian,
			Luxembourgish,
			Macedonian,
			Malagasy,
			Malay,
			Malayalam,
			Maltese,
			Maori,
			Marathi,
			Mongolian,
			Myanmar_Burmese,
			Nepali,
			Norwegian,
			Odia_Oriya,
			Pashto,
			Persian,
			Polish,
			Portuguese,
			Punjabi,
			Romanian,
			Russian,
			Samoan,
			Scots_Gaelic,
			Serbian,
			Sesotho,
			Shona,
			Sindhi,
			Sinhala,
			Slovak,
			Slovenian,
			Somali,
			Spanish,
			Sundanese,
			Swahili,
			Swedish,
			Tajik,
			Tamil,
			Tatar,
			Telugu,
			Thai,
			Turkish,
			Turkmen,
			Ukrainian,
			Urdu,
			Uyghur,
			Uzbek,
			Vietnamese,
			Welsh,
			Xhosa,
			Yiddish,
			Yoruba,
			Zulu,
		}

	}
}
