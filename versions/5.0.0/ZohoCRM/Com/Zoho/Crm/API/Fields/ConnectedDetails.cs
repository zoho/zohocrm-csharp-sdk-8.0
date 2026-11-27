using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class ConnectedDetails : Model
	{
		private LinkingModule module;
		private LookupField field;
		private List<LookupLayout> layouts;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public LinkingModule Module
		{
			/// <summary>The method to get the module</summary>
			/// <returns>Instance of LinkingModule</returns>
			get
			{
				return  this.module;

			}
			/// <summary>The method to set the value to module</summary>
			/// <param name="module">Instance of LinkingModule</param>
			set
			{
				 this.module=value;

				 this.keyModified["module"] = 1;

			}
		}

		public LookupField Field
		{
			/// <summary>The method to get the field</summary>
			/// <returns>Instance of LookupField</returns>
			get
			{
				return  this.field;

			}
			/// <summary>The method to set the value to field</summary>
			/// <param name="field">Instance of LookupField</param>
			set
			{
				 this.field=value;

				 this.keyModified["field"] = 1;

			}
		}

		public List<LookupLayout> Layouts
		{
			/// <summary>The method to get the layouts</summary>
			/// <returns>Instance of List<LookupLayout></returns>
			get
			{
				return  this.layouts;

			}
			/// <summary>The method to set the value to layouts</summary>
			/// <param name="layouts">Instance of List<LookupLayout></param>
			set
			{
				 this.layouts=value;

				 this.keyModified["layouts"] = 1;

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