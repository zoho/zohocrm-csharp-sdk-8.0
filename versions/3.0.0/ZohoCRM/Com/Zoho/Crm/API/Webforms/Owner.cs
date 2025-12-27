using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Webforms
{

	public class Owner : Model
	{
		private string name;
		private string id;
		private bool? systemMail;
		private Dictionary<string, object> emailTemplate;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Name
		{
			/// <summary>The method to get the name</summary>
			/// <returns>string representing the name</returns>
			get
			{
				return  this.name;

			}
			/// <summary>The method to set the value to name</summary>
			/// <param name="name">string</param>
			set
			{
				 this.name=value;

				 this.keyModified["name"] = 1;

			}
		}

		public string Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>string representing the id</returns>
			get
			{
				return  this.id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">string</param>
			set
			{
				 this.id=value;

				 this.keyModified["id"] = 1;

			}
		}

		public bool? SystemMail
		{
			/// <summary>The method to get the systemMail</summary>
			/// <returns>bool? representing the systemMail</returns>
			get
			{
				return  this.systemMail;

			}
			/// <summary>The method to set the value to systemMail</summary>
			/// <param name="systemMail">bool?</param>
			set
			{
				 this.systemMail=value;

				 this.keyModified["system_mail"] = 1;

			}
		}

		public Dictionary<string, object> EmailTemplate
		{
			/// <summary>The method to get the emailTemplate</summary>
			/// <returns>Dictionary representing the emailTemplate<String,Object></returns>
			get
			{
				return  this.emailTemplate;

			}
			/// <summary>The method to set the value to emailTemplate</summary>
			/// <param name="emailTemplate">Dictionary<string,object></param>
			set
			{
				 this.emailTemplate=value;

				 this.keyModified["email_template"] = 1;

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