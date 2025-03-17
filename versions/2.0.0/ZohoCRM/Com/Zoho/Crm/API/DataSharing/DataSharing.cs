using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.DataSharing
{

	public class DataSharing : Model
	{
		private Choice<string> shareType;
		private bool? publicInPortals;
		private Module module;
		private bool? ruleComputationRunning;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public Choice<string> ShareType
		{
			/// <summary>The method to get the shareType</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.shareType;

			}
			/// <summary>The method to set the value to shareType</summary>
			/// <param name="shareType">Instance of Choice<string></param>
			set
			{
				 this.shareType=value;

				 this.keyModified["share_type"] = 1;

			}
		}

		public bool? PublicInPortals
		{
			/// <summary>The method to get the publicInPortals</summary>
			/// <returns>bool? representing the publicInPortals</returns>
			get
			{
				return  this.publicInPortals;

			}
			/// <summary>The method to set the value to publicInPortals</summary>
			/// <param name="publicInPortals">bool?</param>
			set
			{
				 this.publicInPortals=value;

				 this.keyModified["public_in_portals"] = 1;

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

		public bool? RuleComputationRunning
		{
			/// <summary>The method to get the ruleComputationRunning</summary>
			/// <returns>bool? representing the ruleComputationRunning</returns>
			get
			{
				return  this.ruleComputationRunning;

			}
			/// <summary>The method to set the value to ruleComputationRunning</summary>
			/// <param name="ruleComputationRunning">bool?</param>
			set
			{
				 this.ruleComputationRunning=value;

				 this.keyModified["rule_computation_running"] = 1;

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