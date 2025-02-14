using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Layouts
{

	public class SubformProperty : Model
	{
		private bool? pinnedColumn;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? PinnedColumn
		{
			/// <summary>The method to get the pinnedColumn</summary>
			/// <returns>bool? representing the pinnedColumn</returns>
			get
			{
				return  this.pinnedColumn;

			}
			/// <summary>The method to set the value to pinnedColumn</summary>
			/// <param name="pinnedColumn">bool?</param>
			set
			{
				 this.pinnedColumn=value;

				 this.keyModified["pinned_column"] = 1;

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