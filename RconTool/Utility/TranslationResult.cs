using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RconTool
{
	[JsonObject(MemberSerialization.OptIn)]
	public class TranslationResult
	{

		//{
		//	"code": 200,
		//	"detected": { "lang": "es" },
		//	"lang": "es-en",
		//	"text": [ "hello" ]
		//}

		[JsonProperty("code")]
		public int StatusCode { get; set; } = 0;
		[JsonProperty("lang")]
		public string TranslationCode { get; set; } = "";
		[JsonProperty("text")]
		public List<string> Translation { get; set; } = new List<string>();
		public string DetectedLanguage { get { return detected?.Language; } }
		[JsonProperty("detected")]
		private DetectedLanguageJsonObject detected { get; set; } = null;

		[JsonObject(MemberSerialization.OptIn)]
		private class DetectedLanguageJsonObject
		{
			[JsonProperty("lang")]
			public string Language { get; set; }
		}

	}
}
