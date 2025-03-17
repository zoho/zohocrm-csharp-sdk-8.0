using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailCompose
{

	public class EmailCompose : Model
	{
		private DefaultFromAddress defaultFromAddress;
		private DefaultReplytoAddress defaultReplytoAddress;
		private Font font;
		private Choice<string> type;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public DefaultFromAddress DefaultFromAddress
		{
			/// <summary>The method to get the defaultFromAddress</summary>
			/// <returns>Instance of DefaultFromAddress</returns>
			get
			{
				return  this.defaultFromAddress;

			}
			/// <summary>The method to set the value to defaultFromAddress</summary>
			/// <param name="defaultFromAddress">Instance of DefaultFromAddress</param>
			set
			{
				 this.defaultFromAddress=value;

				 this.keyModified["default_from_address"] = 1;

			}
		}

		public DefaultReplytoAddress DefaultReplytoAddress
		{
			/// <summary>The method to get the defaultReplytoAddress</summary>
			/// <returns>Instance of DefaultReplytoAddress</returns>
			get
			{
				return  this.defaultReplytoAddress;

			}
			/// <summary>The method to set the value to defaultReplytoAddress</summary>
			/// <param name="defaultReplytoAddress">Instance of DefaultReplytoAddress</param>
			set
			{
				 this.defaultReplytoAddress=value;

				 this.keyModified["default_replyto_address"] = 1;

			}
		}

		public Font Font
		{
			/// <summary>The method to get the font</summary>
			/// <returns>Instance of Font</returns>
			get
			{
				return  this.font;

			}
			/// <summary>The method to set the value to font</summary>
			/// <param name="font">Instance of Font</param>
			set
			{
				 this.font=value;

				 this.keyModified["font"] = 1;

			}
		}

		public Choice<string> Type
		{
			/// <summary>The method to get the type</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.type;

			}
			/// <summary>The method to set the value to type</summary>
			/// <param name="type">Instance of Choice<string></param>
			set
			{
				 this.type=value;

				 this.keyModified["type"] = 1;

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