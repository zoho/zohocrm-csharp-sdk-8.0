using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.ZiaPeopleEnrichment
{

	public class BodyWrapper : Model
	{
		private List<ZiaPeopleEnrichment> ziaPeopleEnrichment;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<ZiaPeopleEnrichment> ZiaPeopleEnrichment
		{
			/// <summary>The method to get the ziaPeopleEnrichment</summary>
			/// <returns>Instance of List<ZiaPeopleEnrichment></returns>
			get
			{
				return  this.ziaPeopleEnrichment;

			}
			/// <summary>The method to set the value to ziaPeopleEnrichment</summary>
			/// <param name="ziaPeopleEnrichment">Instance of List<ZiaPeopleEnrichment></param>
			set
			{
				 this.ziaPeopleEnrichment=value;

				 this.keyModified["__zia_people_enrichment"] = 1;

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