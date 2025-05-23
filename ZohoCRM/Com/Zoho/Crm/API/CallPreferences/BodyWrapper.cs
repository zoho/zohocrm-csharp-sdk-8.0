using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.CallPreferences
{

	public class BodyWrapper : Model
	{
		private CallPreferences callPreferences;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public CallPreferences CallPreferences
		{
			/// <summary>The method to get the callPreferences</summary>
			/// <returns>Instance of CallPreferences</returns>
			get
			{
				return  this.callPreferences;

			}
			/// <summary>The method to set the value to callPreferences</summary>
			/// <param name="callPreferences">Instance of CallPreferences</param>
			set
			{
				 this.callPreferences=value;

				 this.keyModified["call_preferences"] = 1;

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