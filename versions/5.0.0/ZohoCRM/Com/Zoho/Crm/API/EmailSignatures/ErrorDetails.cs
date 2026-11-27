using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailSignatures
{

	public class ErrorDetails : Model
	{
		private string apiName;
		private string jsonPath;
		private List<object> supportedValues;
		private RangeStructure range;
		private string paramName;
		private string regex;
		private string expectedDataType;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

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

		public string JsonPath
		{
			/// <summary>The method to get the jsonPath</summary>
			/// <returns>string representing the jsonPath</returns>
			get
			{
				return  this.jsonPath;

			}
			/// <summary>The method to set the value to jsonPath</summary>
			/// <param name="jsonPath">string</param>
			set
			{
				 this.jsonPath=value;

				 this.keyModified["json_path"] = 1;

			}
		}

		public List<object> SupportedValues
		{
			/// <summary>The method to get the supportedValues</summary>
			/// <returns>Instance of List<Object></returns>
			get
			{
				return  this.supportedValues;

			}
			/// <summary>The method to set the value to supportedValues</summary>
			/// <param name="supportedValues">Instance of List<object></param>
			set
			{
				 this.supportedValues=value;

				 this.keyModified["supported_values"] = 1;

			}
		}

		public RangeStructure Range
		{
			/// <summary>The method to get the range</summary>
			/// <returns>Instance of RangeStructure</returns>
			get
			{
				return  this.range;

			}
			/// <summary>The method to set the value to range</summary>
			/// <param name="range">Instance of RangeStructure</param>
			set
			{
				 this.range=value;

				 this.keyModified["range"] = 1;

			}
		}

		public string ParamName
		{
			/// <summary>The method to get the paramName</summary>
			/// <returns>string representing the paramName</returns>
			get
			{
				return  this.paramName;

			}
			/// <summary>The method to set the value to paramName</summary>
			/// <param name="paramName">string</param>
			set
			{
				 this.paramName=value;

				 this.keyModified["param_name"] = 1;

			}
		}

		public string Regex
		{
			/// <summary>The method to get the regex</summary>
			/// <returns>string representing the regex</returns>
			get
			{
				return  this.regex;

			}
			/// <summary>The method to set the value to regex</summary>
			/// <param name="regex">string</param>
			set
			{
				 this.regex=value;

				 this.keyModified["regex"] = 1;

			}
		}

		public string ExpectedDataType
		{
			/// <summary>The method to get the expectedDataType</summary>
			/// <returns>string representing the expectedDataType</returns>
			get
			{
				return  this.expectedDataType;

			}
			/// <summary>The method to set the value to expectedDataType</summary>
			/// <param name="expectedDataType">string</param>
			set
			{
				 this.expectedDataType=value;

				 this.keyModified["expected_data_type"] = 1;

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