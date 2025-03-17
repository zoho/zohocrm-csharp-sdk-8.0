using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SharingRules
{

	public class FiltersBody : Model
	{
		private List<Criteria> filters;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<Criteria> Filters
		{
			/// <summary>The method to get the filters</summary>
			/// <returns>Instance of List<Criteria></returns>
			get
			{
				return  this.filters;

			}
			/// <summary>The method to set the value to filters</summary>
			/// <param name="filters">Instance of List<Criteria></param>
			set
			{
				 this.filters=value;

				 this.keyModified["filters"] = 1;

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