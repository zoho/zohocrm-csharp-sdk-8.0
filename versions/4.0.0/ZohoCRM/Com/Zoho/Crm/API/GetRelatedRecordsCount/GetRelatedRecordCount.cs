using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.GetRelatedRecordsCount
{

	public class GetRelatedRecordCount : Model
	{
		private RelatedList relatedList;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public RelatedList RelatedList
		{
			/// <summary>The method to get the relatedList</summary>
			/// <returns>Instance of RelatedList</returns>
			get
			{
				return  this.relatedList;

			}
			/// <summary>The method to set the value to relatedList</summary>
			/// <param name="relatedList">Instance of RelatedList</param>
			set
			{
				 this.relatedList=value;

				 this.keyModified["related_list"] = 1;

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