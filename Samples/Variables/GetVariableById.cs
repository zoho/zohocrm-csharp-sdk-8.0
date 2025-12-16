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
    public class GetVariableById
    {
        public static void GetVariableById_1()
        {
            try
            {
                long variableId = 1055806000028634001L; // Replace with actual variable ID

                VariablesOperations variablesOperations = new VariablesOperations();

                ParameterMap parameterMap = new ParameterMap();

                // Add group parameter to filter by variable group
                parameterMap.Add(VariablesOperations.GetVariableByIDParam.GROUP, "General");

                // Call API
                APIResponse<ResponseHandler> response = variablesOperations.GetVariableById(variableId, parameterMap);

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
                                Console.WriteLine($"\n=== Variable Details ===");

                                Variable variable = variables[0]; // Single variable response

                                Console.WriteLine("ID: " + variable.Id);
                                Console.WriteLine("Name: " + variable.Name);
                                Console.WriteLine("API Name: " + variable.APIName);
                                Console.WriteLine("Value: " + variable.Value);
                                Console.WriteLine("Type: " + variable.Type);
                                Console.WriteLine("Description: " + variable.Description);

                                if (variable.VariableGroup != null)
                                {
                                    Console.WriteLine("Variable Group ID: " + variable.VariableGroup.Id);
                                    Console.WriteLine("Variable Group Name: " + variable.VariableGroup.Name);
                                    Console.WriteLine("Variable Group API Name: " + variable.VariableGroup.APIName);
                                }
                                Console.WriteLine("=========================");
                            }
                            else
                            {
                                Console.WriteLine($"No variable found with ID: {variableId}");
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

                // Get variable by ID
                GetVariableById_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}