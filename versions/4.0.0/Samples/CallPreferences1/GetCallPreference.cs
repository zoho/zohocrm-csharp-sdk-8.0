using System;
using Com.Zoho.Crm.API.CallPreferences;
using Com.Zoho.Crm.API.Util;
using System.Collections.Generic;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using System.Reflection;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Newtonsoft.Json;

namespace Samples.CallPreferences1
{
    public class GetCallPreference
	{
        public static void GetCallPreference_1()
        {
            CallPreferencesOperations callPreferencesOperations = new CallPreferencesOperations();
            APIResponse<ResponseHandler> response = callPreferencesOperations.GetCallPreference();
		    if (response != null)
		    {
			    Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.StatusCode == 204)
                {
                    Console.WriteLine("No Content");
                    return;
                }
			    if (response.IsExpected)
			    {
				    ResponseHandler responseHandler = response.Object;
				    if (responseHandler is ResponseWrapper)
				    {
					    ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;
                        CallPreferences callPreferences = responseWrapper.CallPreferences;
                        Console.WriteLine("CallPreferences ShowFromNumber: " + callPreferences.ShowFromNumber);
                        Console.WriteLine("CallPreferences ShowToNumber: " + callPreferences.ShowToNumber);
                    }
                    else if (responseHandler is APIException)
				    {
                        APIException exception = (APIException)responseHandler;
                        Console.WriteLine("Status: " + exception.Status.Value);
                        Console.WriteLine("Code: " + exception.Code.Value);
                        Console.WriteLine("Details: ");
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            Console.WriteLine(entry.Key + ": " + entry.Value);
                        }
                        Console.WriteLine("Message: " + exception.Message);
                    }
			    }
                else if (response.StatusCode != 204)
                {
                    Model responseObject = response.Model;
                    Type type = responseObject.GetType();
                    Console.WriteLine("Type is : {0}", type.Name);
                    PropertyInfo[] props = type.GetProperties();
                    Console.WriteLine("Properties (N = {0}) :", props.Length);
                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) in {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) in <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
		    }
	    }

        public static void Call()
        {
            try
            {
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                GetCallPreference_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
	}
}

