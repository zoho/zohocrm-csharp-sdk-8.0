using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SharingRules
{

	public class BodyWrapper : Model
	{
		private List<SharingRules> sharingRules;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<SharingRules> SharingRules
		{
			/// <summary>The method to get the sharingRules</summary>
			/// <returns>Instance of List<SharingRules></returns>
			get
			{
				return  this.sharingRules;

			}
			/// <summary>The method to set the value to sharingRules</summary>
			/// <param name="sharingRules">Instance of List<SharingRules></param>
			set
			{
				 this.sharingRules=value;

				 this.keyModified["sharing_rules"] = 1;

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