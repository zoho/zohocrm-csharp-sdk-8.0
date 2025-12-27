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
    public class UpdateVariableByApiname
    {
        public static void UpdateVariableByApiname_1()
        {
            try
            {
                string apiName = "Test_Variable_1"; // Replace with actual variable API name

                VariablesOperations variablesOperations = new VariablesOperations();

                // Create request body
                BodyWrapper request = new BodyWrapper();
                List<Variable> variablesList = new List<Variable>();

                Variable variable = new Variable();
                variable.Name = "Updated Variable Name by API Name";
                variable.Value = "Updated Variable Value by API Name";
                variable.Description = "Updated variable description using API name";
                
                variablesList.Add(variable);
                request.Variables = variablesList;

                ParameterMap parameterMap = new ParameterMap();

                // Add group parameter
                parameterMap.Add(VariablesOperations.UpdateVariableByAPINameParam.GROUP, "General");

                // Call API
                APIResponse<ActionHandler> response = variablesOperations.UpdateVariableByApiname(apiName, request, parameterMap);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                            List<ActionResponse> actionResponses = actionWrapper.Variables;

                            if (actionResponses != null && actionResponses.Count > 0)
                            {
                                ActionResponse actionResponse = actionResponses[0];

                                if (actionResponse is SuccessResponse)
                                {
                                    SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                    Console.WriteLine("\n--- Variable Update Success ---");
                                    Console.WriteLine("Status: " + successResponse.Status.Value);
                                    Console.WriteLine("Code: " + successResponse.Code.Value);
                                    Console.WriteLine("Message: " + successResponse.Message);

                                    if (successResponse.Details != null)
                                    {
                                        Console.WriteLine("Details:");
                                        foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                        {
                                            Console.WriteLine("  " + entry.Key + ": " + entry.Value);
                                        }
                                    }
                                    Console.WriteLine("Variable with API name '" + apiName + "' updated successfully");
                                    Console.WriteLine("---");
                                }
                                else if (actionResponse is APIException)
                                {
                                    APIException exception = (APIException)actionResponse;

                                    Console.WriteLine("\n--- Variable Update Failed ---");
                                    Console.WriteLine("Status: " + exception.Status.Value);
                                    Console.WriteLine("Code: " + exception.Code.Value);

                                    if (exception.Details != null)
                                    {
                                        Console.WriteLine("Details:");
                                        foreach (KeyValuePair<string, object> entry in exception.Details)
                                        {
                                            Console.WriteLine("  " + entry.Key + ": " + entry.Value);
                                        }
                                    }

                                    Console.WriteLine("Message: " + exception.Message);
                                    Console.WriteLine("Failed to update variable with API name: " + apiName);
                                    Console.WriteLine("---");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No action responses received");
                            }
                        }
                        else if (actionHandler is APIException)
                        {
                            APIException exception = (APIException)actionHandler;

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

        public static void Call()
        {
            try
            {
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();

                // Update variable by API name
                UpdateVariableByApiname_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}