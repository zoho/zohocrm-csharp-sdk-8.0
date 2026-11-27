using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Signals
{

	public class Signals : Model
	{
		private string displayLabel;
		private string namespace1;
		private bool? chatEnabled;
		private bool? enabled;
		private long? id;
		private FeatureAvailability featureAvailability;
		private Extension extension;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

		public string DisplayLabel
		{
			/// <summary>The method to get the displayLabel</summary>
			/// <returns>string representing the displayLabel</returns>
			get
			{
				return  this.displayLabel;

			}
			/// <summary>The method to set the value to displayLabel</summary>
			/// <param name="displayLabel">string</param>
			set
			{
				 this.displayLabel=value;

				 this.keyModified["display_label"] = 1;

			}
		}

		public string Namespace
		{
			/// <summary>The method to get the namespace</summary>
			/// <returns>string representing the namespace1</returns>
			get
			{
				return  this.namespace1;

			}
			/// <summary>The method to set the value to namespace</summary>
			/// <param name="namespace1">string</param>
			set
			{
				 this.namespace1=value;

				 this.keyModified["namespace"] = 1;

			}
		}

		public bool? ChatEnabled
		{
			/// <summary>The method to get the chatEnabled</summary>
			/// <returns>bool? representing the chatEnabled</returns>
			get
			{
				return  this.chatEnabled;

			}
			/// <summary>The method to set the value to chatEnabled</summary>
			/// <param name="chatEnabled">bool?</param>
			set
			{
				 this.chatEnabled=value;

				 this.keyModified["chat_enabled"] = 1;

			}
		}

		public bool? Enabled
		{
			/// <summary>The method to get the enabled</summary>
			/// <returns>bool? representing the enabled</returns>
			get
			{
				return  this.enabled;

			}
			/// <summary>The method to set the value to enabled</summary>
			/// <param name="enabled">bool?</param>
			set
			{
				 this.enabled=value;

				 this.keyModified["enabled"] = 1;

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

		public FeatureAvailability FeatureAvailability
		{
			/// <summary>The method to get the featureAvailability</summary>
			/// <returns>Instance of FeatureAvailability</returns>
			get
			{
				return  this.featureAvailability;

			}
			/// <summary>The method to set the value to featureAvailability</summary>
			/// <param name="featureAvailability">Instance of FeatureAvailability</param>
			set
			{
				 this.featureAvailability=value;

				 this.keyModified["feature_availability"] = 1;

			}
		}

		public Extension Extension
		{
			/// <summary>The method to get the extension</summary>
			/// <returns>Instance of Extension</returns>
			get
			{
				return  this.extension;

			}
			/// <summary>The method to set the value to extension</summary>
			/// <param name="extension">Instance of Extension</param>
			set
			{
				 this.extension=value;

				 this.keyModified["extension"] = 1;

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