using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.GetRelatedRecordsCount
{

	public class GetRelatedRecordsCountOperations
	{
		private string moduleAPIName;
		private long? recordId;

		/// <summary>		/// Creates an instance of GetRelatedRecordsCountOperations with the given parameters
		/// <param name="recordId">long?</param>
		/// <param name="moduleAPIName">string</param>
		
		public GetRelatedRecordsCountOperations(long? recordId, string moduleAPIName)
		{
			 this.recordId=recordId;

			 this.moduleAPIName=moduleAPIName;


		}

		/// <summary>The method to get related records count</summary>
		/// <param name="request">Instance of BodyWrapper</param>
		/// <returns>Instance of APIResponse<ActionHandler></returns>
		public APIResponse<ActionHandler> GetRelatedRecordsCount(BodyWrapper request)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v8/");

			apiPath=string.Concat(apiPath,  this.moduleAPIName.ToString());

			apiPath=string.Concat(apiPath, "/");

			apiPath=string.Concat(apiPath,  this.recordId.ToString());

			apiPath=string.Concat(apiPath, "/actions/get_related_records_count");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_POST;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_ACTION;

			handlerInstance.ContentType="application/json";

			handlerInstance.Request=request;

			return handlerInstance.APICall<ActionHandler>(typeof(ActionHandler), "application/json");


		}


	}
}