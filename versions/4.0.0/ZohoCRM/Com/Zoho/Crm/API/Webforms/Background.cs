using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Webforms
{

	public class Background : Model
	{
		private string imageName;
		private string color;
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

		public string Color
		{
			/// <summary>The method to get the color</summary>
			/// <returns>string representing the color</returns>
			get
			{
				return  this.color;

			}
			/// <summary>The method to set the value to color</summary>
			/// <param name="color">string</param>
			set
			{
				 this.color=value;

				 this.keyModified["color"] = 1;

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