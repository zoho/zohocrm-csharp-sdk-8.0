using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.CallPreferences
{

	public class CallPreferencesOperations
	{
		/// <summary>The method to get call preference</summary>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetCallPreference()
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v8/settings/call_preferences");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to update call preference</summary>
		/// <param name="request">Instance of BodyWrapper</param>
		/// <returns>Instance of APIResponse<ActionHandler></returns>
		public APIResponse<ActionHandler> UpdateCallPreference(BodyWrapper request)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v8/settings/call_preferences");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_PUT;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_UPDATE;

			handlerInstance.ContentType="application/json";

			handlerInstance.Request=request;

			handlerInstance.MandatoryChecker=true;

			return handlerInstance.APICall<ActionHandler>(typeof(ActionHandler), "application/json");


		}


	}
}