using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Webforms
{

	public class Logo : Model
	{
		private string imageName;
		private string align;
		private string size;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string ImageName
		{
			/// <summary>The method to get the imageName</summary>
			/// <returns>string representing the imageName</returns>
			get
			{
				return  this.imageName;

			}
			/// <summary>The method to set the value to imageName</summary>
			/// <param name="imageName">string</param>
			set
			{
				 this.imageName=value;

				 this.keyModified["image_name"] = 1;

			}
		}

		public string Align
		{
			/// <summary>The method to get the align</summary>
			/// <returns>string representing the align</returns>
			get
			{
				return  this.align;

			}
			/// <summary>The method to set the value to align</summary>
			/// <param name="align">string</param>
			set
			{
				 this.align=value;

				 this.keyModified["align"] = 1;

			}
		}

		public string Size
		{
			/// <summary>The method to get the size</summary>
			/// <returns>string representing the size</returns>
			get
			{
				return  this.size;

			}
			/// <summary>The method to set the value to size</summary>
			/// <param name="size">string</param>
			set
			{
				 this.size=value;

				 this.keyModified["size"] = 1;

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