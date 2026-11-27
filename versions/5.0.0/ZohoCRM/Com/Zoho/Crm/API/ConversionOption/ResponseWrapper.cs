using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.ConversionOption
{

	public class ResponseWrapper : Model, ResponseHandler
	{
		private ConversionOptions conversionOptions;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public ConversionOptions ConversionOptions
		{
			/// <summary>The method to get the conversionOptions</summary>
			/// <returns>Instance of ConversionOptions</returns>
			get
			{
				return  this.conversionOptions;

			}
			/// <summary>The method to set the value to conversionOptions</summary>
			/// <param name="conversionOptions">Instance of ConversionOptions</param>
			set
			{
				 this.conversionOptions=value;

				 this.keyModified["__conversion_options"] = 1;

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