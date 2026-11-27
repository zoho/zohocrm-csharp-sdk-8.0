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
    public class CreateVariables
    {
        public static void CreateVariables_1()
        {
            try
            {
                VariablesOperations variablesOperations = new VariablesOperations();

                // Create request body
                BodyWrapper request = new BodyWrapper();
                List<Variable> variablesList = new List<Variable>();

                // Create first variable
                Variable variable1 = new Variable();
                variable1.Name = "Test Variable 1";
                variable1.APIName = "Test_Variable_1";
                variable1.Value = "Test Value 1";
                variable1.Type = new Choice<String>("text");
                variable1.Description = "This is a test variable created via API";

                // Set variable group
                VariableGroup variableGroup1 = new VariableGroup();
                variableGroup1.APIName = "General"; // Replace with actual variable group API name
                variable1.VariableGroup = variableGroup1;

                variablesList.Add(variable1);

                // Create second variable
                Variable variable2 = new Variable();
                variable2.Name = "Test Variable 2";
                variable2.APIName = "Test_Variable_2";
                variable2.Value = "12345";
                variable2.Type = new Choice<String>("integer");
                variable2.Description = "This is another test variable created via API";

                // Set variable group
                VariableGroup variableGroup2 = new VariableGroup();
                variableGroup2.APIName = "General"; // Replace with actual variable group API name
                variable2.VariableGroup = variableGroup2;

                variablesList.Add(variable2);

                request.Variables = variablesList;

                // Call API
                APIResponse<ActionHandler> response = variablesOperations.CreateVariables(request);

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

                            Console.WriteLine($"Processed {actionResponses?.Count ?? 0} variable creations:");

                            if (actionResponses != null && actionResponses.Count > 0)
                            {
                                foreach (ActionResponse actionResponse in actionResponses)
                                {
                                    if (actionResponse is SuccessResponse)
                                    {
                                        SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                        Console.WriteLine("\n--- Variable Creation Success ---");
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
                                        Console.WriteLine("---");
                                    }
                                    else if (actionResponse is APIException)
                                    {
                                        APIException exception = (APIException)actionResponse;

                                        Console.WriteLine("\n--- Variable Creation Failed ---");
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
                                        Console.WriteLine("---");
                                    }
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

                // Create variables
                CreateVariables_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}