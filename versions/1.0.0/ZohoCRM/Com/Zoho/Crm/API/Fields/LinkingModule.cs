using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class LinkingModule : Model
	{
		private string pluralLabel;
		private int? visibility;
		private string apiName;
		private long? id;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string PluralLabel
		{
			/// <summary>The method to get the pluralLabel</summary>
			/// <returns>string representing the pluralLabel</returns>
			get
			{
				return  this.pluralLabel;

			}
			/// <summary>The method to set the value to pluralLabel</summary>
			/// <param name="pluralLabel">string</param>
			set
			{
				 this.pluralLabel=value;

				 this.keyModified["plural_label"] = 1;

			}
		}

		public int? Visibility
		{
			/// <summary>The method to get the visibility</summary>
			/// <returns>int? representing the visibility</returns>
			get
			{
				return  this.visibility;

			}
			/// <summary>The method to set the value to visibility</summary>
			/// <param name="visibility">int?</param>
			set
			{
				 this.visibility=value;

				 this.keyModified["visibility"] = 1;

			}
		}

		public string APIName
		{
			/// <summary>The method to get the aPIName</summary>
			/// <returns>string representing the apiName</returns>
			get
			{
				return  this.apiName;

			}
			/// <summary>The method to set the value to aPIName</summary>
			/// <param name="apiName">string</param>
			set
			{
				 this.apiName=value;

				 this.keyModified["api_name"] = 1;

			}
		}

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				return  this.id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
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