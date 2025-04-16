using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailCompose
{

	public class Font : Model
	{
		private int? size;
		private string family;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public int? Size
		{
			/// <summary>The method to get the size</summary>
			/// <returns>int? representing the size</returns>
			get
			{
				return  this.size;

			}
			/// <summary>The method to set the value to size</summary>
			/// <param name="size">int?</param>
			set
			{
				 this.size=value;

				 this.keyModified["size"] = 1;

			}
		}

		public string Family
		{
			/// <summary>The method to get the family</summary>
			/// <returns>string representing the family</returns>
			get
			{
				return  this.family;

			}
			/// <summary>The method to set the value to family</summary>
			/// <param name="family">string</param>
			set
			{
				 this.family=value;

				 this.keyModified["family"] = 1;

			}
		}

		/// <summary>The method to check if the user has modified the given key</summary>
		/// <param name="key">string</param>
		/// <returns>int? representing the modification</returns>
		public int? IsKeyModified(string key)
		{
			if((( this.keyModified.ContainsKey(key))))
			{
				return  this.keyModified[key];

			}
			return null;


		}

		/// <summary>The method to mark the given key as modified</summary>
		/// <param name="key">string</param>
		/// <param name="modification">int?</param>
		public void SetKeyModified(string key, int? modification)
		{
			 this.keyModified[key] = modification;


		}


	}
}