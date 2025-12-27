using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Webforms
{

	public class DoubleOptinEmailTemplate : Model
	{
		private FromAddress fromAddress;
		private string content;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public FromAddress FromAddress
		{
			/// <summary>The method to get the fromAddress</summary>
			/// <returns>Instance of FromAddress</returns>
			get
			{
				return  this.fromAddress;

			}
			/// <summary>The method to set the value to fromAddress</summary>
			/// <param name="fromAddress">Instance of FromAddress</param>
			set
			{
				 this.fromAddress=value;

				 this.keyModified["from_address"] = 1;

			}
		}

		public string Content
		{
			/// <summary>The method to get the content</summary>
			/// <returns>string representing the content</returns>
			get
			{
				return  this.content;

			}
			/// <summary>The method to set the value to content</summary>
			/// <param name="content">string</param>
			set
			{
				 this.content=value;

				 this.keyModified["content"] = 1;

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