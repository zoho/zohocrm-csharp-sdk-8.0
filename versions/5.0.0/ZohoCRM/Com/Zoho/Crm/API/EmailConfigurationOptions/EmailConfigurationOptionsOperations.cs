using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.EmailConfigurationOptions
{

	public class EmailConfigurationOptionsOperations
	{
		/// <summary>		/// Creates an instance of EmailConfigurationOptionsOperations
		
		public EmailConfigurationOptionsOperations()
		{

		}

		/// <summary>The method to get email configuration options</summary>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetEmailConfigurationOptions()
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/email/v8/settings/compose/configuration_options");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}

		/// <summary>The method to update email configuration options</summary>
		/// <param name="request">Instance of BodyWrapper</param>
		/// <returns>Instance of APIResponse<ActionHandler></returns>
		public APIResponse<ActionHandler> UpdateEmailConfigurationOptions(BodyWrapper request)
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/email/v8/settings/compose/configuration_options");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_PUT;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_UPDATE;

			handlerInstance.ContentType="application/json";

			handlerInstance.Request=request;

			handlerInstance.MandatoryChecker=true;

			return handlerInstance.APICall<ActionHandler>(typeof(ActionHandler), "application/json");


		}


		public static class GetEmailConfigurationOptionsHeader
		{
		}


		public static class UpdateEmailConfigurationOptionsHeader
		{
		}

	}
}