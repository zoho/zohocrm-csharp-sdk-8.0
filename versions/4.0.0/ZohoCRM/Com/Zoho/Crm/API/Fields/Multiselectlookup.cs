using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class Multiselectlookup : Model
	{
		private LinkingDetails linkingDetails;
		private ConnectedDetails connectedDetails;
		private LookupRelatedList relatedList;
		private bool? recordAccess;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public LinkingDetails LinkingDetails
		{
			/// <summary>The method to get the linkingDetails</summary>
			/// <returns>Instance of LinkingDetails</returns>
			get
			{
				return  this.linkingDetails;

			}
			/// <summary>The method to set the value to linkingDetails</summary>
			/// <param name="linkingDetails">Instance of LinkingDetails</param>
			set
			{
				 this.linkingDetails=value;

				 this.keyModified["linking_details"] = 1;

			}
		}

		public ConnectedDetails ConnectedDetails
		{
			/// <summary>The method to get the connectedDetails</summary>
			/// <returns>Instance of ConnectedDetails</returns>
			get
			{
				return  this.connectedDetails;

			}
			/// <summary>The method to set the value to connectedDetails</summary>
			/// <param name="connectedDetails">Instance of ConnectedDetails</param>
			set
			{
				 this.connectedDetails=value;

				 this.keyModified["connected_details"] = 1;

			}
		}

		public LookupRelatedList RelatedList
		{
			/// <summary>The method to get the relatedList</summary>
			/// <returns>Instance of LookupRelatedList</returns>
			get
			{
				return  this.relatedList;

			}
			/// <summary>The method to set the value to relatedList</summary>
			/// <param name="relatedList">Instance of LookupRelatedList</param>
			set
			{
				 this.relatedList=value;

				 this.keyModified["related_list"] = 1;

			}
		}

		public bool? RecordAccess
		{
			/// <summary>The method to get the recordAccess</summary>
			/// <returns>bool? representing the recordAccess</returns>
			get
			{
				return  this.recordAccess;

			}
			/// <summary>The method to set the value to recordAccess</summary>
			/// <param name="recordAccess">bool?</param>
			set
			{
				 this.recordAccess=value;

				 this.keyModified["record_access"] = 1;

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