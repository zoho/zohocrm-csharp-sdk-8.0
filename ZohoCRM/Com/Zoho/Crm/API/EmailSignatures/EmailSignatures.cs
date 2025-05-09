using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;

namespace Com.Zoho.Crm.API.EmailSignatures
{

	public class EmailSignatures : Model
	{
		private string name;
		private List<FromAddress> from;
		private Choice<string> editorMode;
		private long? id;
		private string content;
		private Dictionary<string, int?> keyModified=new Dictionary<string, int?>();

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

		public List<FromAddress> From
		{
			/// <summary>The method to get the from</summary>
			/// <returns>Instance of List<FromAddress></returns>
			get
			{
				return  this.from;

			}
			/// <summary>The method to set the value to from</summary>
			/// <param name="from">Instance of List<FromAddress></param>
			set
			{
				 this.from=value;

				 this.keyModified["from"] = 1;

			}
		}

		public Choice<string> EditorMode
		{
			/// <summary>The method to get the editorMode</summary>
			/// <returns>Instance of Choice<String></returns>
			get
			{
				return  this.editorMode;

			}
			/// <summary>The method to set the value to editorMode</summary>
			/// <param name="editorMode">Instance of Choice<string></param>
			set
			{
				 this.editorMode=value;

				 this.keyModified["editor_mode"] = 1;

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

		public string Content
		{
			/// <summary>The method to get the content</summary>
			/// <returns>string representing the content</returns>
			get
			{
				return  this.content;

			}
			/// <summary>The method to set the value to content</summary>
			/// <param name="content">string</param>
			set
			{
				 this.content=value;

				 this.keyModified["content"] = 1;

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