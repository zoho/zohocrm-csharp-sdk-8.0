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

namespace csharpsdksampleapplication.Samples.SharingRules1
{
    public class GetSharingRuleSummary
    {
        public static void GetSharingRuleSummary_1(String moduleAPIName)
        {
            SharingRulesOperations sharingRulesOperations = new SharingRulesOperations(moduleAPIName);
            APIResponse<SummaryResponseHandler> response = sharingRulesOperations.GetSharingRuleSummary();
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
                    SummaryResponseHandler responseHandler = response.Object;
                    if (responseHandler is SummaryResponseWrapper)
                    {
                        SummaryResponseWrapper responseWrapper = (SummaryResponseWrapper)responseHandler;
                        List<RulesSummary> rulesSummary = responseWrapper.SharingRulesSummary;
                        foreach (RulesSummary ruleSummary in rulesSummary)
                        {
                            Module module = ruleSummary.Module;
                            if (module != null)
                            {
                                Console.WriteLine("RulesSummary Module APIName: " + module.APIName);
                                Console.WriteLine("RulesSummary Module Id: " + module.Id);
                            }
                            Console.WriteLine("RulesSummary RuleComputationStatus: " + ruleSummary.RuleComputationStatus);
                            Console.WriteLine("RulesSummary RuleCount: " + ruleSummary.RuleCount);
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
                GetSharingRuleSummary_1(moduleAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}
