using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API.Dc;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.SharingRules;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;

namespace csharpsdksampleapplication.Samples.SharingRules1
{
    public class UpdateSharingRules
    {
        public static void UpdateSharingRules_1(String moduleAPIName)
        {
            SharingRulesOperations sharingRulesOperations = new SharingRulesOperations(moduleAPIName);
            BodyWrapper request = new BodyWrapper();
            List<SharingRules> sharingRules = new List<SharingRules>();
            SharingRules sharingRule = new SharingRules();
            sharingRule.Id = 347725551001;

            sharingRule.Type = new Choice<String>("Record_Owner_Based");
            Shared sharedFrom = new Shared();
            Resource resource = new Resource();
            resource.Id = 347707236002;
            sharedFrom.Resource = resource;
            sharedFrom.Type = new Choice<String>("groups");
            sharedFrom.Subordinates = false;
            sharingRule.SharedFrom = sharedFrom;

            //sharingRule.Type = new Choice<String>("Criteria_Based");
            //Criteria criteria = new Criteria();
            //criteria.Comparator = "equal";
            //Field field = new Field();
            //field.APIName = "Wizard";
            //field.Id = 11111195003;
            //criteria.Field = field;
            //Dictionary<String,String> value = new Dictionary<String, String>
            //{
            //    { "name", "TestWizards" },
            //    { "id", "111111095001" }
            //};
            //criteria.Value = value;
            //sharingRule.Criteria = criteria;

            sharingRule.SuperiorsAllowed = false;

            Shared sharedTo = new Shared();
            resource = new Resource();
            resource.Id = 347736002;
            sharedTo.Resource = resource;
            sharedTo.Type = new Choice<String>("groups");
            sharedTo.Subordinates = false;
            sharingRule.SharedTo = sharedTo;

            sharingRule.PermissionType = new Choice<String>("read_write_delete");
            sharingRule.Name = "TestJavaSDK";

            sharingRules.Add(sharingRule);
            request.SharingRules = sharingRules;
            APIResponse<ActionHandler> response = sharingRulesOperations.UpdateSharingRules(request);
            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionHandler actionHandler = response.Object;
                    if (actionHandler is ActionWrapper)
                    {
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                        List<ActionResponse> actionResponses = actionWrapper.SharingRules;
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
                                Console.WriteLine("Message: " + successResponse.Message.Value);
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
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                String moduleAPIName = "Leads";
                UpdateSharingRules_1(moduleAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}
