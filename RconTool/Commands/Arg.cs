using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RconTool
{
	
	public class Arg
	{
		public string Name { get; set; } = "";
		public string Description { get; set; } = "";
		public bool IsRequired { get; set; } = true;
		public Type ArgType { get; set; } = Arg.Type.String;
		public Arg(string Name, string Description) : this(Name, Description, Type.String) {}
		public Arg(string Name, string Description, Type ArgType) : this(Name, Description, ArgType, false) { }
		public Arg(string Name, string Description, Type ArgType, bool Optional)
		{
			this.Name = Name;
			this.Description = Description;
			this.ArgType = ArgType;
			IsRequired = !Optional;
		}
		
		public enum Type
		{
			PlayerName,
			BaseMap,
			MapName,
			BaseVariant,
			VariantName,
			CommandName,
			String,
			FileNameJSON,
			LanguageCode
		}

	}

}
