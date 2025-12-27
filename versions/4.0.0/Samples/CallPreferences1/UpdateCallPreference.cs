using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.Crm.API.CallPreferences;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.API.Authenticator;
using Newtonsoft.Json;
using Com.Zoho.Crm.API;

namespace Samples.CallPreferences1
{
    public class UpdateCallPreference
	{
        public static void UpdateCallPreference_1()
        {
            CallPreferencesOperations callPreferencesOperations = new CallPreferencesOperations();
            BodyWrapper bodyWrapper = new BodyWrapper();
            CallPreferences callPreferences = new CallPreferences();
            callPreferences.ShowFromNumber = true;
            callPreferences.ShowToNumber = true;
            bodyWrapper.CallPreferences = callPreferences;
            APIResponse<ActionHandler> response = callPreferencesOperations.UpdateCallPreference(bodyWrapper);
            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionHandler actionHandler = response.Object;
                    if (actionHandler is ActionWrapper)
                    {

                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                        ActionResponse actionResponse = actionWrapper.CallPreferences;
                        if (actionResponse is SuccessResponse)
                        {
                            SuccessResponse successResponse = (SuccessResponse)actionResponse;
                            Console.WriteLine("Status: " + successResponse.Status.Value);
                            Console.WriteLine("Code: " + successResponse.Code.Value);
                            Console.WriteLine("Details: ");
                            foreach (KeyValuePair<string, object> entry in successResponse.Details)
                            {
                                Console.WriteLine(entry.Key + ": " + entry.Value);
                            }
                            Console.WriteLine("Message: " + successResponse.Message);
                        }
                        else if (actionResponse is APIException)
                        {
                            APIException exception = (APIException)actionResponse;
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
                    else if (actionHandler is APIException)
                    {
                        APIException exception = (APIException)actionHandler;
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
                UpdateCallPreference_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
	}
}

