using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.SharingRules
{

	public class RulesSummary : Model
	{
		private Module module;
		private bool? ruleComputationStatus;
		private int? ruleCount;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

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

		public bool? RuleComputationStatus
		{
			/// <summary>The method to get the ruleComputationStatus</summary>
			/// <returns>bool? representing the ruleComputationStatus</returns>
			get
			{
				return  this.ruleComputationStatus;

			}
			/// <summary>The method to set the value to ruleComputationStatus</summary>
			/// <param name="ruleComputationStatus">bool?</param>
			set
			{
				 this.ruleComputationStatus=value;

				 this.keyModified["rule_computation_status"] = 1;

			}
		}

		public int? RuleCount
		{
			/// <summary>The method to get the ruleCount</summary>
			/// <returns>int? representing the ruleCount</returns>
			get
			{
				return  this.ruleCount;

			}
			/// <summary>The method to set the value to ruleCount</summary>
			/// <param name="ruleCount">int?</param>
			set
			{
				 this.ruleCount=value;

				 this.keyModified["rule_count"] = 1;

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