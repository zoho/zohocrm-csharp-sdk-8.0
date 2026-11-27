using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Layouts
{

	public class ActionsAllowed : Model
	{
		private bool? edit;
		private bool? rename;
		private bool? clone;
		private bool? downgrade;
		private bool? delete;
		private bool? deactivate;
		private bool? setLayoutPermissions;
		private bool? addField;
		private bool? changeTabTraversal;
		private bool? reorder;
		private bool? removeField;
		private bool? changeColumnCount;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? Edit
		{
			/// <summary>The method to get the edit</summary>
			/// <returns>bool? representing the edit</returns>
			get
			{
				return  this.edit;

			}
			/// <summary>The method to set the value to edit</summary>
			/// <param name="edit">bool?</param>
			set
			{
				 this.edit=value;

				 this.keyModified["edit"] = 1;

			}
		}

		public bool? Rename
		{
			/// <summary>The method to get the rename</summary>
			/// <returns>bool? representing the rename</returns>
			get
			{
				return  this.rename;

			}
			/// <summary>The method to set the value to rename</summary>
			/// <param name="rename">bool?</param>
			set
			{
				 this.rename=value;

				 this.keyModified["rename"] = 1;

			}
		}

		public bool? Clone
		{
			/// <summary>The method to get the clone</summary>
			/// <returns>bool? representing the clone</returns>
			get
			{
				return  this.clone;

			}
			/// <summary>The method to set the value to clone</summary>
			/// <param name="clone">bool?</param>
			set
			{
				 this.clone=value;

				 this.keyModified["clone"] = 1;

			}
		}

		public bool? Downgrade
		{
			/// <summary>The method to get the downgrade</summary>
			/// <returns>bool? representing the downgrade</returns>
			get
			{
				return  this.downgrade;

			}
			/// <summary>The method to set the value to downgrade</summary>
			/// <param name="downgrade">bool?</param>
			set
			{
				 this.downgrade=value;

				 this.keyModified["downgrade"] = 1;

			}
		}

		public bool? Delete
		{
			/// <summary>The method to get the delete</summary>
			/// <returns>bool? representing the delete</returns>
			get
			{
				return  this.delete;

			}
			/// <summary>The method to set the value to delete</summary>
			/// <param name="delete">bool?</param>
			set
			{
				 this.delete=value;

				 this.keyModified[Constants.REQUEST_METHOD_DELETE] = 1;

			}
		}

		public bool? Deactivate
		{
			/// <summary>The method to get the deactivate</summary>
			/// <returns>bool? representing the deactivate</returns>
			get
			{
				return  this.deactivate;

			}
			/// <summary>The method to set the value to deactivate</summary>
			/// <param name="deactivate">bool?</param>
			set
			{
				 this.deactivate=value;

				 this.keyModified["deactivate"] = 1;

			}
		}

		public bool? SetLayoutPermissions
		{
			/// <summary>The method to get the setLayoutPermissions</summary>
			/// <returns>bool? representing the setLayoutPermissions</returns>
			get
			{
				return  this.setLayoutPermissions;

			}
			/// <summary>The method to set the value to setLayoutPermissions</summary>
			/// <param name="setLayoutPermissions">bool?</param>
			set
			{
				 this.setLayoutPermissions=value;

				 this.keyModified["set_layout_permissions"] = 1;

			}
		}

		public bool? AddField
		{
			/// <summary>The method to get the addField</summary>
			/// <returns>bool? representing the addField</returns>
			get
			{
				return  this.addField;

			}
			/// <summary>The method to set the value to addField</summary>
			/// <param name="addField">bool?</param>
			set
			{
				 this.addField=value;

				 this.keyModified["add_field"] = 1;

			}
		}

		public bool? ChangeTabTraversal
		{
			/// <summary>The method to get the changeTabTraversal</summary>
			/// <returns>bool? representing the changeTabTraversal</returns>
			get
			{
				return  this.changeTabTraversal;

			}
			/// <summary>The method to set the value to changeTabTraversal</summary>
			/// <param name="changeTabTraversal">bool?</param>
			set
			{
				 this.changeTabTraversal=value;

				 this.keyModified["change_tab_traversal"] = 1;

			}
		}

		public bool? Reorder
		{
			/// <summary>The method to get the reorder</summary>
			/// <returns>bool? representing the reorder</returns>
			get
			{
				return  this.reorder;

			}
			/// <summary>The method to set the value to reorder</summary>
			/// <param name="reorder">bool?</param>
			set
			{
				 this.reorder=value;

				 this.keyModified["reorder"] = 1;

			}
		}

		public bool? RemoveField
		{
			/// <summary>The method to get the removeField</summary>
			/// <returns>bool? representing the removeField</returns>
			get
			{
				return  this.removeField;

			}
			/// <summary>The method to set the value to removeField</summary>
			/// <param name="removeField">bool?</param>
			set
			{
				 this.removeField=value;

				 this.keyModified["remove_field"] = 1;

			}
		}

		public bool? ChangeColumnCount
		{
			/// <summary>The method to get the changeColumnCount</summary>
			/// <returns>bool? representing the changeColumnCount</returns>
			get
			{
				return  this.changeColumnCount;

			}
			/// <summary>The method to set the value to changeColumnCount</summary>
			/// <param name="changeColumnCount">bool?</param>
			set
			{
				 this.changeColumnCount=value;

				 this.keyModified["change_column_count"] = 1;

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