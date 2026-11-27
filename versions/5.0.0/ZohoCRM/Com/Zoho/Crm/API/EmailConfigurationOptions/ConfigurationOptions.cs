using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailConfigurationOptions
{

	public class ConfigurationOptions : Model
	{
		private string name;
		private List<ValueDetails> values;
		private Choice<string> dataType;
		private List<PropertyDetails> properties;
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

		public List<ValueDetails> Values
		{
			/// <summary>The method to get the values</summary>
			/// <returns>Instance of List<ValueDetails></returns>
			get
			{
				return  this.values;

			}
			/// <summary>The method to set the value to values</summary>
			/// <param name="values">Instance of List<ValueDetails></param>
			set
			{
				 this.values=value;

				 this.keyModified["values"] = 1;

			}
		}

		public Choice<string> DataType
		{
			/// <summary>The method to get the dataType</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.dataType;

			}
			/// <summary>The method to set the value to dataType</summary>
			/// <param name="dataType">Instance of Choice<string></param>
			set
			{
				 this.dataType=value;

				 this.keyModified["data_type"] = 1;

			}
		}

		public List<PropertyDetails> Properties
		{
			/// <summary>The method to get the properties</summary>
			/// <returns>Instance of List<PropertyDetails></returns>
			get
			{
				return  this.properties;

			}
			/// <summary>The method to set the value to properties</summary>
			/// <param name="properties">Instance of List<PropertyDetails></param>
			set
			{
				 this.properties=value;

				 this.keyModified["properties"] = 1;

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