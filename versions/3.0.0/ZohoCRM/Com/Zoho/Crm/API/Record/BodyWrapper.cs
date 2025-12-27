using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Record
{

	public class BodyWrapper : Model
	{
		private List<Record> data;
		private List<string> trigger;
		private List<string> process;
		private List<string> duplicateCheckFields;
		private string wfTrigger;
		private string larId;
		private List<ApplyFeatureExecution> applyFeatureExecution;
		private string applyValidationRule;
		private string applyFunctionValidationRule;
		private List<SkipFeatureExecution> skipFeatureExecution;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public List<Record> Data
		{
			/// <summary>The method to get the data</summary>
			/// <returns>Instance of List<Record></returns>
			get
			{
				return  this.data;

			}
			/// <summary>The method to set the value to data</summary>
			/// <param name="data">Instance of List<Record></param>
			set
			{
				 this.data=value;

				 this.keyModified["data"] = 1;

			}
		}

		public List<string> Trigger
		{
			/// <summary>The method to get the trigger</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  this.trigger;

			}
			/// <summary>The method to set the value to trigger</summary>
			/// <param name="trigger">Instance of List<string></param>
			set
			{
				 this.trigger=value;

				 this.keyModified["trigger"] = 1;

			}
		}

		public List<string> Process
		{
			/// <summary>The method to get the process</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  this.process;

			}
			/// <summary>The method to set the value to process</summary>
			/// <param name="process">Instance of List<string></param>
			set
			{
				 this.process=value;

				 this.keyModified["process"] = 1;

			}
		}

		public List<string> DuplicateCheckFields
		{
			/// <summary>The method to get the duplicateCheckFields</summary>
			/// <returns>Instance of List<String></returns>
			get
			{
				return  this.duplicateCheckFields;

			}
			/// <summary>The method to set the value to duplicateCheckFields</summary>
			/// <param name="duplicateCheckFields">Instance of List<string></param>
			set
			{
				 this.duplicateCheckFields=value;

				 this.keyModified["duplicate_check_fields"] = 1;

			}
		}

		public string WfTrigger
		{
			/// <summary>The method to get the wfTrigger</summary>
			/// <returns>string representing the wfTrigger</returns>
			get
			{
				return  this.wfTrigger;

			}
			/// <summary>The method to set the value to wfTrigger</summary>
			/// <param name="wfTrigger">string</param>
			set
			{
				 this.wfTrigger=value;

				 this.keyModified["wf_trigger"] = 1;

			}
		}

		public string LarId
		{
			/// <summary>The method to get the larId</summary>
			/// <returns>string representing the larId</returns>
			get
			{
				return  this.larId;

			}
			/// <summary>The method to set the value to larId</summary>
			/// <param name="larId">string</param>
			set
			{
				 this.larId=value;

				 this.keyModified["lar_id"] = 1;

			}
		}

		public List<ApplyFeatureExecution> ApplyFeatureExecution
		{
			/// <summary>The method to get the applyFeatureExecution</summary>
			/// <returns>Instance of List<ApplyFeatureExecution></returns>
			get
			{
				return  this.applyFeatureExecution;

			}
			/// <summary>The method to set the value to applyFeatureExecution</summary>
			/// <param name="applyFeatureExecution">Instance of List<ApplyFeatureExecution></param>
			set
			{
				 this.applyFeatureExecution=value;

				 this.keyModified["apply_feature_execution"] = 1;

			}
		}

		public string ApplyValidationRule
		{
			/// <summary>The method to get the applyValidationRule</summary>
			/// <returns>string representing the applyValidationRule</returns>
			get
			{
				return  this.applyValidationRule;

			}
			/// <summary>The method to set the value to applyValidationRule</summary>
			/// <param name="applyValidationRule">string</param>
			set
			{
				 this.applyValidationRule=value;

				 this.keyModified["apply_validation_rule"] = 1;

			}
		}

		public string ApplyFunctionValidationRule
		{
			/// <summary>The method to get the applyFunctionValidationRule</summary>
			/// <returns>string representing the applyFunctionValidationRule</returns>
			get
			{
				return  this.applyFunctionValidationRule;

			}
			/// <summary>The method to set the value to applyFunctionValidationRule</summary>
			/// <param name="applyFunctionValidationRule">string</param>
			set
			{
				 this.applyFunctionValidationRule=value;

				 this.keyModified["apply_function_validation_rule"] = 1;

			}
		}

		public List<SkipFeatureExecution> SkipFeatureExecution
		{
			/// <summary>The method to get the skipFeatureExecution</summary>
			/// <returns>Instance of List<SkipFeatureExecution></returns>
			get
			{
				return  this.skipFeatureExecution;

			}
			/// <summary>The method to set the value to skipFeatureExecution</summary>
			/// <param name="skipFeatureExecution">Instance of List<SkipFeatureExecution></param>
			set
			{
				 this.skipFeatureExecution=value;

				 this.keyModified["skip_feature_execution"] = 1;

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