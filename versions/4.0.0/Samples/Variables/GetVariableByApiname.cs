using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Variables;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.Variables_1
{
    public class GetVariableByApiname
    {
        public static void GetVariableByApiname_1()
        {
            try
            {
                string apiName = "Test_Variable_1"; // Replace with actual variable API name

                VariablesOperations variablesOperations = new VariablesOperations();

                ParameterMap parameterMap = new ParameterMap();

                // Add group parameter to filter by variable group
                //parameterMap.Add(VariablesOperations.GetVariableByAPINameParam.GROUP, "General");

                // Call API
                APIResponse<ResponseHandler> response = variablesOperations.GetVariableByApiname(apiName, parameterMap);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<Variable> variables = responseWrapper.Variables;

                            if (variables != null && variables.Count > 0)
                            {
                                Console.WriteLine($"\n=== Variable by API Name ===");

                                Variable variable = variables[0]; // Single variable response

                                Console.WriteLine("ID: " + variable.Id);
                                Console.WriteLine("Name: " + variable.Name);
                                Console.WriteLine("API Name: " + variable.APIName);
                                Console.WriteLine("Value: " + variable.Value);
                                Console.WriteLine("Type: " + variable.Type);
                                Console.WriteLine("Description: " + variable.Description);

                                Console.WriteLine("=============================");
                            }
                            else
                            {
                                Console.WriteLine($"No variable found with API name: {apiName}");
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
                "id", "name", "api_name", "value", "type", "description",
                "variable_group", "created_by", "modified_by", "created_time", "modified_time"
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

                // Get variable by API name
                GetVariableByApiname_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}