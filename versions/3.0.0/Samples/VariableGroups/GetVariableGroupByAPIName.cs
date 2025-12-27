using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.VariableGroups;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.VariableGroups_1
{
    public class GetVariableGroupByAPIName
    {
        public static void GetVariableGroupByAPIName_1()
        {
            try
            {
                string apiName = "General"; // Replace with actual API name
                VariableGroupsOperations variableGroupsOperations = new VariableGroupsOperations();
                APIResponse<ResponseHandler> response = variableGroupsOperations.GetVariableGroupByAPIName(apiName);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<VariableGroup> variableGroups = responseWrapper.VariableGroups;

                            if (variableGroups != null && variableGroups.Count > 0)
                            {
                                Console.WriteLine($"\n=== Variable Group by API Name ===");

                                VariableGroup variableGroup = variableGroups[0]; // Single variable group response

                                Console.WriteLine("ID: " + variableGroup.Id);
                                Console.WriteLine("Name: " + variableGroup.Name);
                                Console.WriteLine("Display Label: " + variableGroup.DisplayLabel);
                                Console.WriteLine("Description: " + variableGroup.Description);
                                Console.WriteLine("API Name: " + variableGroup.APIName);

                                Console.WriteLine("===================================");
                            }
                            else
                            {
                                Console.WriteLine($"No variable group found with API name: {apiName}");
                            }
                        }
                        else if (responseHandler is APIException)
                        {
                            APIException exception = (APIException)responseHandler;

                            Console.WriteLine("Status: " + exception.Status.Value);
                            Console.WriteLine("Code: " + exception.Code.Value);
                            Console.WriteLine("Details: ");

                            if (exception.Details != null)
                            {
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    Console.WriteLine(entry.Key + ": " + entry.Value);
                                }
                            }

                            Console.WriteLine("Message: " + exception.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Response not as expected");
                        Console.WriteLine(response.StatusCode);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }

        private static bool IsStandardField(string fieldName)
        {
            string[] standardFields = {
                "id", "name", "display_label", "description", "api_name",
                "created_by", "modified_by", "created_time", "modified_time"
            };

            return Array.Exists(standardFields, field => field.Equals(fieldName, StringComparison.OrdinalIgnoreCase));
        }

        public static void Call()
        {
            try
            {
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();

                // Get variable group by API name
                GetVariableGroupByAPIName_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}