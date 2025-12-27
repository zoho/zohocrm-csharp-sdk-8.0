using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailSignatures
{

	public class BodyWrapper : Model
	{
		private List<EmailSignatures> emailSignatures;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<EmailSignatures> EmailSignatures
		{
			/// <summary>The method to get the emailSignatures</summary>
			/// <returns>Instance of List<EmailSignatures></returns>
			get
			{
				return  this.emailSignatures;

			}
			/// <summary>The method to set the value to emailSignatures</summary>
			/// <param name="emailSignatures">Instance of List<EmailSignatures></param>
			set
			{
				 this.emailSignatures=value;

				 this.keyModified["email_signatures"] = 1;

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