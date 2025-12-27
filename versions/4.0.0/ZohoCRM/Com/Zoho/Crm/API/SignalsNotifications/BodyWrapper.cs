using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SignalsNotifications
{

	public class BodyWrapper : Model
	{
		private List<Signals> signals;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<Signals> Signals
		{
			/// <summary>The method to get the signals</summary>
			/// <returns>Instance of List<Signals></returns>
			get
			{
				return  this.signals;

			}
			/// <summary>The method to set the value to signals</summary>
			/// <param name="signals">Instance of List<Signals></param>
			set
			{
				 this.signals=value;

				 this.keyModified["signals"] = 1;

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