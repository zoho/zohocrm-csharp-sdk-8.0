using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API.Dc;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.DataSharing;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using Module = Com.Zoho.Crm.API.DataSharing.Module;

namespace csharpsdksampleapplication.Samples.DataSharing1
{
    public class UpdateDataSharing
    {
        public static void UpdateDataSharing_1()
        {
            DataSharingOperations dataSharingOperations = new DataSharingOperations();
            BodyWrapper request = new BodyWrapper();
            List<DataSharing> dataSharing = new List<DataSharing>();
            DataSharing dataSharing1 = new DataSharing();
            dataSharing1.ShareType = new Choice<String>("private");
            Module module = new Module();
            module.APIName = "Leads";
            module.Id = 34770612175;
            dataSharing1.Module = module;
            dataSharing.Add(dataSharing1);
            request.DataSharing = dataSharing;
            APIResponse<ActionHandler> response = dataSharingOperations.UpdateDataSharing(request);
            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionHandler actionHandler = response.Object;
                    if (actionHandler is ActionWrapper)
                    {
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                        List<ActionResponse> actionResponses = actionWrapper.DataSharing;
                        foreach (ActionResponse actionResponse in actionResponses)
                        {
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
                                Console.WriteLine("Message: " + exception.Message.Value);
                            }
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
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
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
                UpdateDataSharing_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }

}
