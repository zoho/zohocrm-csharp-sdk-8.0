using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Webforms
{

	public class ButtonAttributes : Model
	{
		private string color;
		private string name;
		private string align;
		private string borderRadiusPx;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

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

		public string BorderRadiusPx
		{
			/// <summary>The method to get the borderRadiusPx</summary>
			/// <returns>string representing the borderRadiusPx</returns>
			get
			{
				return  this.borderRadiusPx;

			}
			/// <summary>The method to set the value to borderRadiusPx</summary>
			/// <param name="borderRadiusPx">string</param>
			set
			{
				 this.borderRadiusPx=value;

				 this.keyModified["border_radius_px"] = 1;

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