using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.CallPreferences
{

	public class CallPreferences : Model
	{
		private bool? showFromNumber;
		private bool? showToNumber;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? ShowFromNumber
		{
			/// <summary>The method to get the showFromNumber</summary>
			/// <returns>bool? representing the showFromNumber</returns>
			get
			{
				return  this.showFromNumber;

			}
			/// <summary>The method to set the value to showFromNumber</summary>
			/// <param name="showFromNumber">bool?</param>
			set
			{
				 this.showFromNumber=value;

				 this.keyModified["show_from_number"] = 1;

			}
		}

		public bool? ShowToNumber
		{
			/// <summary>The method to get the showToNumber</summary>
			/// <returns>bool? representing the showToNumber</returns>
			get
			{
				return  this.showToNumber;

			}
			/// <summary>The method to set the value to showToNumber</summary>
			/// <param name="showToNumber">bool?</param>
			set
			{
				 this.showToNumber=value;

				 this.keyModified["show_to_number"] = 1;

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