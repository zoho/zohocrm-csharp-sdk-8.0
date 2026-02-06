using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.AssignmentRules.APIException;
using AssignmentRulesOperations = Com.Zoho.Crm.API.AssignmentRules.AssignmentRulesOperations;
using ResponseHandler = Com.Zoho.Crm.API.AssignmentRules.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.AssignmentRules.ResponseWrapper;
using GetAssignmentRulesParam = Com.Zoho.Crm.API.AssignmentRules.AssignmentRulesOperations.GetAssignmentRulesParam;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.AssignmentRules
{
    public class GetAssignmentRules
    {
        public static void GetAssignmentRules_1()
        {
            AssignmentRulesOperations assignmentRulesOperations = new AssignmentRulesOperations();
            ParameterMap paramInstance = new ParameterMap();
            paramInstance.Add(GetAssignmentRulesParam.MODULE, "Leads");
            APIResponse<ResponseHandler> response = assignmentRulesOperations.GetAssignmentRules(paramInstance);
            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
                    return;
                }
                if (response.IsExpected)
                {
                    ResponseHandler responseHandler = response.Object;
                    if (responseHandler is ResponseWrapper)
                    {
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;
                        List<Com.Zoho.Crm.API.AssignmentRules.AssignmentRules> assignmentRules = responseWrapper.AssignmentRules;
                        foreach (Com.Zoho.Crm.API.AssignmentRules.AssignmentRules assignmentRule in assignmentRules)
                        {
                            Console.WriteLine("AssignmentRule ID: " + assignmentRule.Id);
                            Console.WriteLine("AssignmentRule Name: " + assignmentRule.Name);
                            Console.WriteLine("AssignmentRule APIName: " + assignmentRule.APIName);
                            Console.WriteLine("AssignmentRule Description: " + assignmentRule.Description);
                            Console.WriteLine("AssignmentRule CreatedTime: " + assignmentRule.CreatedTime);
                            Console.WriteLine("AssignmentRule ModifiedTime: " + assignmentRule.ModifiedTime);

                            Com.Zoho.Crm.API.AssignmentRules.User createdBy = assignmentRule.CreatedBy;
                            if (createdBy != null)
                            {
                                Console.WriteLine("AssignmentRule CreatedBy User-ID: " + createdBy.Id);
                                Console.WriteLine("AssignmentRule CreatedBy User-Name: " + createdBy.Name);
                                Console.WriteLine("AssignmentRule CreatedBy User-Email: " + createdBy.Email);
                            }

                            Com.Zoho.Crm.API.AssignmentRules.User modifiedBy = assignmentRule.ModifiedBy;
                            if (modifiedBy != null)
                            {
                                Console.WriteLine("AssignmentRule ModifiedBy User-ID: " + modifiedBy.Id);
                                Console.WriteLine("AssignmentRule ModifiedBy User-Name: " + modifiedBy.Name);
                                Console.WriteLine("AssignmentRule ModifiedBy User-Email: " + modifiedBy.Email);
                            }

                            Com.Zoho.Crm.API.Modules.MinifiedModule module = assignmentRule.Module;
                            if (module != null)
                            {
                                Console.WriteLine("AssignmentRule Module ID: " + module.Id);
                                Console.WriteLine("AssignmentRule Module APIName: " + module.APIName);
                            }

                            Com.Zoho.Crm.API.AssignmentRules.DefaultAssignee defaultAssignee = assignmentRule.DefaultAssignee;
                            if (defaultAssignee != null)
                            {
                                Console.WriteLine("AssignmentRule DefaultAssignee ID: " + defaultAssignee.Id);
                                Console.WriteLine("AssignmentRule DefaultAssignee Name: " + defaultAssignee.Name);
                            }
                        }
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
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                {
                    Model responseObject = response.Model;
                    Type type = responseObject.GetType();
                    Console.WriteLine("Type is : {0}", type.Name);
                    PropertyInfo[] props = type.GetProperties();
                    foreach (PropertyInfo prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} : {1}", prop.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} : <Indexed>", prop.Name);
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
                GetAssignmentRules_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}