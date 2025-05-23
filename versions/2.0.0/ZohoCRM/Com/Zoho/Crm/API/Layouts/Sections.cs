using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.Layouts
{

	public class Sections : Model
	{
		private string displayLabel;
		private int? sequenceNumber;
		private ActionsAllowed actionsAllowed;
		private bool? issubformsection;
		private string tabTraversal;
		private string apiName;
		private int? columnCount;
		private string name;
		private string generatedType;
		private long? id;
		private string type;
		private List<SectionField> fields;
		private Properties properties;
		private Delete1 delete;
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

		public int? SequenceNumber
		{
			/// <summary>The method to get the sequenceNumber</summary>
			/// <returns>int? representing the sequenceNumber</returns>
			get
			{
				return  this.sequenceNumber;

			}
			/// <summary>The method to set the value to sequenceNumber</summary>
			/// <param name="sequenceNumber">int?</param>
			set
			{
				 this.sequenceNumber=value;

				 this.keyModified["sequence_number"] = 1;

			}
		}

		public ActionsAllowed ActionsAllowed
		{
			/// <summary>The method to get the actionsAllowed</summary>
			/// <returns>Instance of ActionsAllowed</returns>
			get
			{
				return  this.actionsAllowed;

			}
			/// <summary>The method to set the value to actionsAllowed</summary>
			/// <param name="actionsAllowed">Instance of ActionsAllowed</param>
			set
			{
				 this.actionsAllowed=value;

				 this.keyModified["actions_allowed"] = 1;

			}
		}

		public bool? Issubformsection
		{
			/// <summary>The method to get the issubformsection</summary>
			/// <returns>bool? representing the issubformsection</returns>
			get
			{
				return  this.issubformsection;

			}
			/// <summary>The method to set the value to issubformsection</summary>
			/// <param name="issubformsection">bool?</param>
			set
			{
				 this.issubformsection=value;

				 this.keyModified["isSubformSection"] = 1;

			}
		}

		public string TabTraversal
		{
			/// <summary>The method to get the tabTraversal</summary>
			/// <returns>string representing the tabTraversal</returns>
			get
			{
				return  this.tabTraversal;

			}
			/// <summary>The method to set the value to tabTraversal</summary>
			/// <param name="tabTraversal">string</param>
			set
			{
				 this.tabTraversal=value;

				 this.keyModified["tab_traversal"] = 1;

			}
		}

		public string APIName
		{
			/// <summary>The method to get the aPIName</summary>
			/// <returns>string representing the apiName</returns>
			get
			{
				return  this.apiName;

			}
			/// <summary>The method to set the value to aPIName</summary>
			/// <param name="apiName">string</param>
			set
			{
				 this.apiName=value;

				 this.keyModified["api_name"] = 1;

			}
		}

		public int? ColumnCount
		{
			/// <summary>The method to get the columnCount</summary>
			/// <returns>int? representing the columnCount</returns>
			get
			{
				return  this.columnCount;

			}
			/// <summary>The method to set the value to columnCount</summary>
			/// <param name="columnCount">int?</param>
			set
			{
				 this.columnCount=value;

				 this.keyModified["column_count"] = 1;

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

		public string GeneratedType
		{
			/// <summary>The method to get the generatedType</summary>
			/// <returns>string representing the generatedType</returns>
			get
			{
				return  this.generatedType;

			}
			/// <summary>The method to set the value to generatedType</summary>
			/// <param name="generatedType">string</param>
			set
			{
				 this.generatedType=value;

				 this.keyModified["generated_type"] = 1;

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

		public string Type
		{
			/// <summary>The method to get the type</summary>
			/// <returns>string representing the type</returns>
			get
			{
				return  this.type;

			}
			/// <summary>The method to set the value to type</summary>
			/// <param name="type">string</param>
			set
			{
				 this.type=value;

				 this.keyModified["type"] = 1;

			}
		}

		public List<SectionField> Fields
		{
			/// <summary>The method to get the fields</summary>
			/// <returns>Instance of List<SectionField></returns>
			get
			{
				return  this.fields;

			}
			/// <summary>The method to set the value to fields</summary>
			/// <param name="fields">Instance of List<SectionField></param>
			set
			{
				 this.fields=value;

				 this.keyModified["fields"] = 1;

			}
		}

		public Properties Properties
		{
			/// <summary>The method to get the properties</summary>
			/// <returns>Instance of Properties</returns>
			get
			{
				return  this.properties;

			}
			/// <summary>The method to set the value to properties</summary>
			/// <param name="properties">Instance of Properties</param>
			set
			{
				 this.properties=value;

				 this.keyModified["properties"] = 1;

			}
		}

		public Delete1 Delete
		{
			/// <summary>The method to get the delete</summary>
			/// <returns>Instance of Delete1</returns>
			get
			{
				return  this.delete;

			}
			/// <summary>The method to set the value to delete</summary>
			/// <param name="delete">Instance of Delete1</param>
			set
			{
				 this.delete=value;

				 this.keyModified["_delete"] = 1;

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