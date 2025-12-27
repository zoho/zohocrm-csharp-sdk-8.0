using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.GetRelatedRecordsCount
{

	public class Params : Model
	{
		private bool? approved;
		private bool? converted;
		private bool? associated;
		private Choice<string> category;
		private Choice<string> approvalState;
		private Filters filters;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public bool? Approved
		{
			/// <summary>The method to get the approved</summary>
			/// <returns>bool? representing the approved</returns>
			get
			{
				return  this.approved;

			}
			/// <summary>The method to set the value to approved</summary>
			/// <param name="approved">bool?</param>
			set
			{
				 this.approved=value;

				 this.keyModified["approved"] = 1;

			}
		}

		public bool? Converted
		{
			/// <summary>The method to get the converted</summary>
			/// <returns>bool? representing the converted</returns>
			get
			{
				return  this.converted;

			}
			/// <summary>The method to set the value to converted</summary>
			/// <param name="converted">bool?</param>
			set
			{
				 this.converted=value;

				 this.keyModified["converted"] = 1;

			}
		}

		public bool? Associated
		{
			/// <summary>The method to get the associated</summary>
			/// <returns>bool? representing the associated</returns>
			get
			{
				return  this.associated;

			}
			/// <summary>The method to set the value to associated</summary>
			/// <param name="associated">bool?</param>
			set
			{
				 this.associated=value;

				 this.keyModified["associated"] = 1;

			}
		}

		public Choice<string> Category
		{
			/// <summary>The method to get the category</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.category;

			}
			/// <summary>The method to set the value to category</summary>
			/// <param name="category">Instance of Choice<string></param>
			set
			{
				 this.category=value;

				 this.keyModified["category"] = 1;

			}
		}

		public Choice<string> ApprovalState
		{
			/// <summary>The method to get the approvalState</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.approvalState;

			}
			/// <summary>The method to set the value to approvalState</summary>
			/// <param name="approvalState">Instance of Choice<string></param>
			set
			{
				 this.approvalState=value;

				 this.keyModified["approval_state"] = 1;

			}
		}

		public Filters Filters
		{
			/// <summary>The method to get the filters</summary>
			/// <returns>Instance of Filters</returns>
			get
			{
				return  this.filters;

			}
			/// <summary>The method to set the value to filters</summary>
			/// <param name="filters">Instance of Filters</param>
			set
			{
				 this.filters=value;

				 this.keyModified["filters"] = 1;

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