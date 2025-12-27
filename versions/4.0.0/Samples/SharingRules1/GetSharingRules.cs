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
using Module = Com.Zoho.Crm.API.SharingRules.Module;
using static Com.Zoho.Crm.API.SharingRules.SharingRulesOperations;
using Com.Zoho.Crm.API;

namespace csharpsdksampleapplication.Samples.SharingRules1
{
    public class GetSharingRules
    {
        public static void GetSharingRules_1(String moduleAPIName)
        {
            SharingRulesOperations sharingRulesOperations = new SharingRulesOperations(moduleAPIName);
            ParameterMap paramInstance = new ParameterMap();
            paramInstance.Add(GetSharingRulesParam.PAGE, 1);
            paramInstance.Add(GetSharingRulesParam.PER_PAGE, 5);
            APIResponse<ResponseHandler> response = sharingRulesOperations.GetSharingRules(paramInstance);
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
                        List<SharingRules> sharingRules = responseWrapper.SharingRules;
                        foreach (SharingRules sharingRule in sharingRules)
                        {
                            Module module = sharingRule.Module;
                            if (module != null)
                            {
                                Console.WriteLine("SharingRules Module APIName: " + module.APIName);
                                Console.WriteLine("SharingRules Module Name: " + module.Name);
                                Console.WriteLine("SharingRules Module Id: " + module.Id);
                            }
                            Console.WriteLine("SharingRules SuperiorsAllowed: " + sharingRule.SuperiorsAllowed);
                            Console.WriteLine("SharingRules Type: " + sharingRule.Type.Value);
                            Shared sharedTo = sharingRule.SharedTo;
                            if (sharedTo != null)
                            {
                                Resource resource = sharedTo.Resource;
                                if (resource != null)
                                {
                                    Console.WriteLine("SharingRules SharedTo Resource Name: " + resource.Name);
                                    Console.WriteLine("SharingRules SharedTo Resource Id: " + resource.Id);
                                }
                                Console.WriteLine("SharingRules SharedTo Type: " + sharedTo.Type.Value);
                                Console.WriteLine("SharingRules SharedTo Subordinates: " + sharedTo.Subordinates);
                            }

                            Shared sharedFrom = sharingRule.SharedFrom;
                            if (sharedFrom != null)
                            {
                                Resource resource = sharedFrom.Resource;
                                if (resource != null)
                                {
                                    Console.WriteLine("SharingRules SharedFrom Resource Name: " + resource.Name);
                                    Console.WriteLine("SharingRules SharedFrom Resource Id: " + resource.Id);
                                }
                                Console.WriteLine("SharingRules SharedFrom Type: " + sharedFrom.Type.Value);
                                Console.WriteLine("SharingRules SharedFrom Subordinates: " + sharedFrom.Subordinates);
                            }

                            Console.WriteLine("SharingRules PermissionType: " + sharingRule.PermissionType.Value);
                            Console.WriteLine("SharingRules Name: " + sharingRule.Name);
                            Console.WriteLine("SharingRules Id: " + sharingRule.Id);
                            Console.WriteLine("SharingRules Status: " + sharingRule.Status.Value);
                            Console.WriteLine("SharingRules MatchLimitExceeded: " + sharingRule.MatchLimitExceeded);
                        }
                        Info info = responseWrapper.Info;
                        Console.WriteLine("SharingRules Info PerPage: " + info.PerPage);
                        Console.WriteLine("SharingRules Info Count: " + info.Count);
                        Console.WriteLine("SharingRules Info Page: " + info.Page);
                        Console.WriteLine("SharingRules Info MoreRecords: " + info.MoreRecords);
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
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                String moduleAPIName = "Leads";
                GetSharingRules_1(moduleAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}
