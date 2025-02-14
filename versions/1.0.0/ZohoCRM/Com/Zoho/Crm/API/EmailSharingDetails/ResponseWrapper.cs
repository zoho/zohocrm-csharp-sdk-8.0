using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailSharingDetails
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		private List<EmailSharings> emailsSharingDetails;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<EmailSharings> EmailsSharingDetails
		{
			/// <summary>The method to get the emailsSharingDetails</summary>
			/// <returns>Instance of List<EmailSharings></returns>
			get
			{
				return  this.emailsSharingDetails;

			}
			/// <summary>The method to set the value to emailsSharingDetails</summary>
			/// <param name="emailsSharingDetails">Instance of List<EmailSharings></param>
			set
			{
				 this.emailsSharingDetails=value;

				 this.keyModified["__emails_sharing_details"] = 1;

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