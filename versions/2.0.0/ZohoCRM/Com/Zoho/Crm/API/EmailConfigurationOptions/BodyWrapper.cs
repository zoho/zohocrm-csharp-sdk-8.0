using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailConfigurationOptions
{

	public class BodyWrapper : Model
	{
		private List<ConfigurationOptions> configurationOptions;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<ConfigurationOptions> ConfigurationOptions
		{
			/// <summary>The method to get the configurationOptions</summary>
			/// <returns>Instance of List<ConfigurationOptions></returns>
			get
			{
				return  this.configurationOptions;

			}
			/// <summary>The method to set the value to configurationOptions</summary>
			/// <param name="configurationOptions">Instance of List<ConfigurationOptions></param>
			set
			{
				 this.configurationOptions=value;

				 this.keyModified["configuration_options"] = 1;

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