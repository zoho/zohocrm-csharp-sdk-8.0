using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Webforms
{

	public class VisitorTracking : Model
	{
		private string portalName;
		private string trackingCode;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string PortalName
		{
			/// <summary>The method to get the portalName</summary>
			/// <returns>string representing the portalName</returns>
			get
			{
				return  this.portalName;

			}
			/// <summary>The method to set the value to portalName</summary>
			/// <param name="portalName">string</param>
			set
			{
				 this.portalName=value;

				 this.keyModified["portal_name"] = 1;

			}
		}

		public string TrackingCode
		{
			/// <summary>The method to get the trackingCode</summary>
			/// <returns>string representing the trackingCode</returns>
			get
			{
				return  this.trackingCode;

			}
			/// <summary>The method to set the value to trackingCode</summary>
			/// <param name="trackingCode">string</param>
			set
			{
				 this.trackingCode=value;

				 this.keyModified["tracking_code"] = 1;

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