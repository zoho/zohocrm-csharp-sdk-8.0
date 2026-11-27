using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailConfigurationOptions
{

	public class ValueDetails : Model
	{
		private string type;
		private object value;
		private bool? delete;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string Type
		{
			/// <summary>The method to get the type</summary>
			/// <returns>string representing the type</returns>
			get
			{
				return  this.type;

			}
			/// <summary>The method to set the value to type</summary>
			/// <param name="type">string</param>
			set
			{
				 this.type=value;

				 this.keyModified["type"] = 1;

			}
		}

		public object Value
		{
			/// <summary>The method to get the value</summary>
			/// <returns>object representing the value</returns>
			get
			{
				return  this.value;

			}
			/// <summary>The method to set the value to value</summary>
			/// <param name="value">object</param>
			set
			{
				 this.value=value;

				 this.keyModified["value"] = 1;

			}
		}

		public bool? Delete
		{
			/// <summary>The method to get the delete</summary>
			/// <returns>bool? representing the delete</returns>
			get
			{
				return  this.delete;

			}
			/// <summary>The method to set the value to delete</summary>
			/// <param name="delete">bool?</param>
			set
			{
				 this.delete=value;

				 this.keyModified["_delete"] = 1;

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