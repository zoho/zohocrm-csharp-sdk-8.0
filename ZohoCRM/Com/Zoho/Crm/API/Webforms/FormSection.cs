using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Webforms
{

	public class FormSection : Model
	{
		private List<Fields> formFields;
		private string name;
		private string description;
		private string helpMessage;
		private string id;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<Fields> FormFields
		{
			/// <summary>The method to get the formFields</summary>
			/// <returns>Instance of List<Fields></returns>
			get
			{
				return  this.formFields;

			}
			/// <summary>The method to set the value to formFields</summary>
			/// <param name="formFields">Instance of List<Fields></param>
			set
			{
				 this.formFields=value;

				 this.keyModified["form_fields"] = 1;

			}
		}

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

		public string Description
		{
			/// <summary>The method to get the description</summary>
			/// <returns>string representing the description</returns>
			get
			{
				return  this.description;

			}
			/// <summary>The method to set the value to description</summary>
			/// <param name="description">string</param>
			set
			{
				 this.description=value;

				 this.keyModified["description"] = 1;

			}
		}

		public string HelpMessage
		{
			/// <summary>The method to get the helpMessage</summary>
			/// <returns>string representing the helpMessage</returns>
			get
			{
				return  this.helpMessage;

			}
			/// <summary>The method to set the value to helpMessage</summary>
			/// <param name="helpMessage">string</param>
			set
			{
				 this.helpMessage=value;

				 this.keyModified["help_message"] = 1;

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