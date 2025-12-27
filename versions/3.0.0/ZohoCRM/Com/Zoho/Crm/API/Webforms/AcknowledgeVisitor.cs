using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Webforms
{

	public class AcknowledgeVisitor : Model
	{
		private FromAddress replyToAddress;
		private string templateName;
		private AutoResponseRule autoResponseRule;
		private string templateId;
		private FromAddress fromAddress;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public FromAddress ReplyToAddress
		{
			/// <summary>The method to get the replyToAddress</summary>
			/// <returns>Instance of FromAddress</returns>
			get
			{
				return  this.replyToAddress;

			}
			/// <summary>The method to set the value to replyToAddress</summary>
			/// <param name="replyToAddress">Instance of FromAddress</param>
			set
			{
				 this.replyToAddress=value;

				 this.keyModified["reply_to_address"] = 1;

			}
		}

		public string TemplateName
		{
			/// <summary>The method to get the templateName</summary>
			/// <returns>string representing the templateName</returns>
			get
			{
				return  this.templateName;

			}
			/// <summary>The method to set the value to templateName</summary>
			/// <param name="templateName">string</param>
			set
			{
				 this.templateName=value;

				 this.keyModified["template_name"] = 1;

			}
		}

		public AutoResponseRule AutoResponseRule
		{
			/// <summary>The method to get the autoResponseRule</summary>
			/// <returns>Instance of AutoResponseRule</returns>
			get
			{
				return  this.autoResponseRule;

			}
			/// <summary>The method to set the value to autoResponseRule</summary>
			/// <param name="autoResponseRule">Instance of AutoResponseRule</param>
			set
			{
				 this.autoResponseRule=value;

				 this.keyModified["auto_response_rule"] = 1;

			}
		}

		public string TemplateId
		{
			/// <summary>The method to get the templateId</summary>
			/// <returns>string representing the templateId</returns>
			get
			{
				return  this.templateId;

			}
			/// <summary>The method to set the value to templateId</summary>
			/// <param name="templateId">string</param>
			set
			{
				 this.templateId=value;

				 this.keyModified["template_id"] = 1;

			}
		}

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