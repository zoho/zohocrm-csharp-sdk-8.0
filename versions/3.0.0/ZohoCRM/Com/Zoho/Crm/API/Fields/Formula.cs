using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Fields
{

	public class Formula : Model
	{
		private string returnType;
		private bool? assumeDefault;
		private string expression;
		private bool? dynamic;
		private bool? stopComputeConditionally;
		private string stopComputeExpression;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string ReturnType
		{
			/// <summary>The method to get the returnType</summary>
			/// <returns>string representing the returnType</returns>
			get
			{
				return  this.returnType;

			}
			/// <summary>The method to set the value to returnType</summary>
			/// <param name="returnType">string</param>
			set
			{
				 this.returnType=value;

				 this.keyModified["return_type"] = 1;

			}
		}

		public bool? AssumeDefault
		{
			/// <summary>The method to get the assumeDefault</summary>
			/// <returns>bool? representing the assumeDefault</returns>
			get
			{
				return  this.assumeDefault;

			}
			/// <summary>The method to set the value to assumeDefault</summary>
			/// <param name="assumeDefault">bool?</param>
			set
			{
				 this.assumeDefault=value;

				 this.keyModified["assume_default"] = 1;

			}
		}

		public string Expression
		{
			/// <summary>The method to get the expression</summary>
			/// <returns>string representing the expression</returns>
			get
			{
				return  this.expression;

			}
			/// <summary>The method to set the value to expression</summary>
			/// <param name="expression">string</param>
			set
			{
				 this.expression=value;

				 this.keyModified["expression"] = 1;

			}
		}

		public bool? Dynamic
		{
			/// <summary>The method to get the dynamic</summary>
			/// <returns>bool? representing the dynamic</returns>
			get
			{
				return  this.dynamic;

			}
			/// <summary>The method to set the value to dynamic</summary>
			/// <param name="dynamic">bool?</param>
			set
			{
				 this.dynamic=value;

				 this.keyModified["dynamic"] = 1;

			}
		}

		public bool? StopComputeConditionally
		{
			/// <summary>The method to get the stopComputeConditionally</summary>
			/// <returns>bool? representing the stopComputeConditionally</returns>
			get
			{
				return  this.stopComputeConditionally;

			}
			/// <summary>The method to set the value to stopComputeConditionally</summary>
			/// <param name="stopComputeConditionally">bool?</param>
			set
			{
				 this.stopComputeConditionally=value;

				 this.keyModified["stop_compute_conditionally"] = 1;

			}
		}

		public string StopComputeExpression
		{
			/// <summary>The method to get the stopComputeExpression</summary>
			/// <returns>string representing the stopComputeExpression</returns>
			get
			{
				return  this.stopComputeExpression;

			}
			/// <summary>The method to set the value to stopComputeExpression</summary>
			/// <param name="stopComputeExpression">string</param>
			set
			{
				 this.stopComputeExpression=value;

				 this.keyModified["stop_compute_expression"] = 1;

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