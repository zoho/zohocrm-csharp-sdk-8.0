using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Webforms
{

	public class DoubleOptinDetails : Model
	{
		private DoubleOptinEmailTemplate emailTemplate;
		private string confirmPageContent;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public DoubleOptinEmailTemplate EmailTemplate
		{
			/// <summary>The method to get the emailTemplate</summary>
			/// <returns>Instance of DoubleOptinEmailTemplate</returns>
			get
			{
				return  this.emailTemplate;

			}
			/// <summary>The method to set the value to emailTemplate</summary>
			/// <param name="emailTemplate">Instance of DoubleOptinEmailTemplate</param>
			set
			{
				 this.emailTemplate=value;

				 this.keyModified["email_template"] = 1;

			}
		}

		public string ConfirmPageContent
		{
			/// <summary>The method to get the confirmPageContent</summary>
			/// <returns>string representing the confirmPageContent</returns>
			get
			{
				return  this.confirmPageContent;

			}
			/// <summary>The method to set the value to confirmPageContent</summary>
			/// <param name="confirmPageContent">string</param>
			set
			{
				 this.confirmPageContent=value;

				 this.keyModified["confirm_page_content"] = 1;

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