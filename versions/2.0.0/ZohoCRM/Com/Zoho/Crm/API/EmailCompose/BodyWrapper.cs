using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailCompose
{

	public class BodyWrapper : Model
	{
		private List<EmailCompose> emailCompose;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<EmailCompose> EmailCompose
		{
			/// <summary>The method to get the emailCompose</summary>
			/// <returns>Instance of List<EmailCompose></returns>
			get
			{
				return  this.emailCompose;

			}
			/// <summary>The method to set the value to emailCompose</summary>
			/// <param name="emailCompose">Instance of List<EmailCompose></param>
			set
			{
				 this.emailCompose=value;

				 this.keyModified["email_compose"] = 1;

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