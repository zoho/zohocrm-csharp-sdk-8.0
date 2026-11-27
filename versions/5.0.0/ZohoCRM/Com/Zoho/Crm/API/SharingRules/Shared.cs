using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SharingRules
{

	public class Shared : Model
	{
		private Resource resource;
		private bool? subordinates;
		private Choice<string> type;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public Resource Resource
		{
			/// <summary>The method to get the resource</summary>
			/// <returns>Instance of Resource</returns>
			get
			{
				return  this.resource;

			}
			/// <summary>The method to set the value to resource</summary>
			/// <param name="resource">Instance of Resource</param>
			set
			{
				 this.resource=value;

				 this.keyModified["resource"] = 1;

			}
		}

		public bool? Subordinates
		{
			/// <summary>The method to get the subordinates</summary>
			/// <returns>bool? representing the subordinates</returns>
			get
			{
				return  this.subordinates;

			}
			/// <summary>The method to set the value to subordinates</summary>
			/// <param name="subordinates">bool?</param>
			set
			{
				 this.subordinates=value;

				 this.keyModified["subordinates"] = 1;

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