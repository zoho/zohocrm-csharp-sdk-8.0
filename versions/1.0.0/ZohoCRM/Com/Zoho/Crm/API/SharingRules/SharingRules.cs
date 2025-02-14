using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SharingRules
{

	public class SharingRules : Model
	{
		private long? id;
		private Choice<string> permissionType;
		private bool? superiorsAllowed;
		private string name;
		private Choice<string> type;
		private Shared sharedFrom;
		private Shared sharedTo;
		private Criteria criteria;
		private Module module;
		private Choice<string> status;
		private bool? matchLimitExceeded;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

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

		public Choice<string> PermissionType
		{
			/// <summary>The method to get the permissionType</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.permissionType;

			}
			/// <summary>The method to set the value to permissionType</summary>
			/// <param name="permissionType">Instance of Choice<string></param>
			set
			{
				 this.permissionType=value;

				 this.keyModified["permission_type"] = 1;

			}
		}

		public bool? SuperiorsAllowed
		{
			/// <summary>The method to get the superiorsAllowed</summary>
			/// <returns>bool? representing the superiorsAllowed</returns>
			get
			{
				return  this.superiorsAllowed;

			}
			/// <summary>The method to set the value to superiorsAllowed</summary>
			/// <param name="superiorsAllowed">bool?</param>
			set
			{
				 this.superiorsAllowed=value;

				 this.keyModified["superiors_allowed"] = 1;

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

		public Shared SharedFrom
		{
			/// <summary>The method to get the sharedFrom</summary>
			/// <returns>Instance of Shared</returns>
			get
			{
				return  this.sharedFrom;

			}
			/// <summary>The method to set the value to sharedFrom</summary>
			/// <param name="sharedFrom">Instance of Shared</param>
			set
			{
				 this.sharedFrom=value;

				 this.keyModified["shared_from"] = 1;

			}
		}

		public Shared SharedTo
		{
			/// <summary>The method to get the sharedTo</summary>
			/// <returns>Instance of Shared</returns>
			get
			{
				return  this.sharedTo;

			}
			/// <summary>The method to set the value to sharedTo</summary>
			/// <param name="sharedTo">Instance of Shared</param>
			set
			{
				 this.sharedTo=value;

				 this.keyModified["shared_to"] = 1;

			}
		}

		public Criteria Criteria
		{
			/// <summary>The method to get the criteria</summary>
			/// <returns>Instance of Criteria</returns>
			get
			{
				return  this.criteria;

			}
			/// <summary>The method to set the value to criteria</summary>
			/// <param name="criteria">Instance of Criteria</param>
			set
			{
				 this.criteria=value;

				 this.keyModified["criteria"] = 1;

			}
		}

		public Module Module
		{
			/// <summary>The method to get the module</summary>
			/// <returns>Instance of Module</returns>
			get
			{
				return  this.module;

			}
			/// <summary>The method to set the value to module</summary>
			/// <param name="module">Instance of Module</param>
			set
			{
				 this.module=value;

				 this.keyModified["module"] = 1;

			}
		}

		public Choice<string> Status
		{
			/// <summary>The method to get the status</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.status;

			}
			/// <summary>The method to set the value to status</summary>
			/// <param name="status">Instance of Choice<string></param>
			set
			{
				 this.status=value;

				 this.keyModified["status"] = 1;

			}
		}

		public bool? MatchLimitExceeded
		{
			/// <summary>The method to get the matchLimitExceeded</summary>
			/// <returns>bool? representing the matchLimitExceeded</returns>
			get
			{
				return  this.matchLimitExceeded;

			}
			/// <summary>The method to set the value to matchLimitExceeded</summary>
			/// <param name="matchLimitExceeded">bool?</param>
			set
			{
				 this.matchLimitExceeded=value;

				 this.keyModified["match_limit_exceeded"] = 1;

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