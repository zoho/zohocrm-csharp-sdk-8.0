using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Layouts
{

	public class Profiles : Model
	{
		private bool? default1;
		private string name;
		private long? id;
		private DefaultView defaultView;
		private DefaultAssignmentView defaultAssignmentView;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? Default
		{
			/// <summary>The method to get the default</summary>
			/// <returns>bool? representing the default1</returns>
			get
			{
				return  this.default1;

			}
			/// <summary>The method to set the value to default</summary>
			/// <param name="default1">bool?</param>
			set
			{
				 this.default1=value;

				 this.keyModified["default"] = 1;

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

		public long? Id
		{
			/// <summary>The method to get the id</summary>
			/// <returns>long? representing the id</returns>
			get
			{
				return  this.id;

			}
			/// <summary>The method to set the value to id</summary>
			/// <param name="id">long?</param>
			set
			{
				 this.id=value;

				 this.keyModified["id"] = 1;

			}
		}

		public DefaultView DefaultView
		{
			/// <summary>The method to get the defaultView</summary>
			/// <returns>Instance of DefaultView</returns>
			get
			{
				return  this.defaultView;

			}
			/// <summary>The method to set the value to defaultView</summary>
			/// <param name="defaultView">Instance of DefaultView</param>
			set
			{
				 this.defaultView=value;

				 this.keyModified["_default_view"] = 1;

			}
		}

		public DefaultAssignmentView DefaultAssignmentView
		{
			/// <summary>The method to get the defaultAssignmentView</summary>
			/// <returns>Instance of DefaultAssignmentView</returns>
			get
			{
				return  this.defaultAssignmentView;

			}
			/// <summary>The method to set the value to defaultAssignmentView</summary>
			/// <param name="defaultAssignmentView">Instance of DefaultAssignmentView</param>
			set
			{
				 this.defaultAssignmentView=value;

				 this.keyModified["_default_assignment_view"] = 1;

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