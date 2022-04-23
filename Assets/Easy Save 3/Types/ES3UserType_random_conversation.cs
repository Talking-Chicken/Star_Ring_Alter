using System;
using UnityEngine;

namespace ES3Types
{
	[UnityEngine.Scripting.Preserve]
	[ES3PropertiesAttribute("lines")]
	public class ES3UserType_random_conversation : ES3ComponentType
	{
		public static ES3Type Instance = null;

		public ES3UserType_random_conversation() : base(typeof(random_conversation)){ Instance = this; priority = 1;}


		protected override void WriteComponent(object obj, ES3Writer writer)
		{
			var instance = (random_conversation)obj;
			
			writer.WriteProperty("lines", random_conversation.lines, ES3Type_StringArray.Instance);
		}

		protected override void ReadComponent<T>(ES3Reader reader, object obj)
		{
			var instance = (random_conversation)obj;
			foreach(string propertyName in reader.Properties)
			{
				switch(propertyName)
				{
					
					case "lines":
						random_conversation.lines = reader.Read<System.String[]>(ES3Type_StringArray.Instance);
						break;
					default:
						reader.Skip();
						break;
				}
			}
		}
	}


	public class ES3UserType_random_conversationArray : ES3ArrayType
	{
		public static ES3Type Instance;

		public ES3UserType_random_conversationArray() : base(typeof(random_conversation[]), ES3UserType_random_conversation.Instance)
		{
			Instance = this;
		}
	}
}