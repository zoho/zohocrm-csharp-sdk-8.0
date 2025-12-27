using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Util;

namespace Com.Zoho.Crm.API.FromAddresses
{

	public class FromAddressesOperations
	{
		private string userId;

		/// <summary>		/// Creates an instance of FromAddressesOperations with the given parameters
		/// <param name="userId">string</param>
		
		public FromAddressesOperations(string userId)
		{
			 this.userId=userId;


		}

		/// <summary>The method to get from addresses</summary>
		/// <returns>Instance of APIResponse<ResponseHandler></returns>
		public APIResponse<ResponseHandler> GetFromAddresses()
		{
			CommonAPIHandler handlerInstance=new CommonAPIHandler();

			string apiPath="";

			apiPath=string.Concat(apiPath, "/crm/v8/settings/emails/actions/from_addresses");

			handlerInstance.APIPath=apiPath;

			handlerInstance.HttpMethod=Constants.REQUEST_METHOD_GET;

			handlerInstance.CategoryMethod=Constants.REQUEST_CATEGORY_READ;

			handlerInstance.AddParam(new Param<string>("user_id", "com.zoho.crm.api.FromAddresses.GetFromAddressesParam"),  this.userId);

			return handlerInstance.APICall<ResponseHandler>(typeof(ResponseHandler), "application/json");


		}


		public static class GetFromAddressesParam
		{
		}

	}
}