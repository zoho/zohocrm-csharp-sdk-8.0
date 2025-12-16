using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.BulkRead.APIException;
using BulkReadOperations = Com.Zoho.Crm.API.BulkRead.BulkReadOperations;
using BodyWrapper = Com.Zoho.Crm.API.BulkRead.BodyWrapper;
using ActionHandler = Com.Zoho.Crm.API.BulkRead.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.BulkRead.ActionWrapper;
using ActionResponse = Com.Zoho.Crm.API.BulkRead.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.BulkRead.SuccessResponse;
using Query = Com.Zoho.Crm.API.BulkRead.Query;
using Criteria = Com.Zoho.Crm.API.BulkRead.Criteria;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using Com.Zoho.Crm.API.Modules;
using Com.Zoho.Crm.API.Fields;

namespace Samples.BulkRead
{
    public class CreateBulkReadJob
    {
        public static void CreateBulkReadJob_1()
        {
            BulkReadOperations bulkReadOperations = new BulkReadOperations();
            BodyWrapper bodyWrapper = new BodyWrapper();

            // Create Query object
            Query query = new Query();
            MinifiedModule module = new MinifiedModule();
            module.APIName = "Leads";
            query.Module = module;
            query.Page = 1;

            // Optional: Add criteria for filtering
            Criteria criteria = new Criteria();
            MinifiedField field = new MinifiedField();
            field.APIName = "Company";
            criteria.Field = field;
            criteria.Comparator = new Choice<string>("not_equal");
            criteria.Value = "Zoho";
            query.Criteria = criteria;

            // Set fields to export
            List<string> fields = new List<string>
            {
                "Last_Name",
                "First_Name",
                "Company",
                "Email",
                "Phone"
            };
            query.Fields = fields;

            bodyWrapper.Query = query;

            // Optional: Add callback URL
            Com.Zoho.Crm.API.BulkRead.CallBack callBack = new Com.Zoho.Crm.API.BulkRead.CallBack();
            callBack.Url = "https://www.example.com/callback";
            callBack.Method = new Choice<string>("post");
            bodyWrapper.Callback = callBack;

            APIResponse<ActionHandler> response = bulkReadOperations.CreateBulkReadJob(bodyWrapper);

            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionHandler actionHandler = response.Object;
                    if (actionHandler is ActionWrapper)
                    {
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                        List<ActionResponse> actionResponses = actionWrapper.Data;
                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            if (actionResponse is SuccessResponse)
                            {
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;
                                Console.WriteLine("Status: " + successResponse.Status.Value);
                                Console.WriteLine("Code: " + successResponse.Code.Value);
                                Console.WriteLine("Details: ");
                                if (successResponse.Details != null)
                                {
                                    foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                    {
                                        Console.WriteLine(entry.Key + ": " + entry.Value);
                                    }
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
                Environment environment = INDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                CreateBulkReadJob_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}