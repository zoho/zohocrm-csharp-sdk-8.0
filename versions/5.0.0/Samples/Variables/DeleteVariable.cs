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
    public class DeleteVariable
    {
        public static void DeleteVariable_1()
        {
            try
            {
                long variableId = 1055806000028633002L; // Replace with actual variable ID

                VariablesOperations variablesOperations = new VariablesOperations();

                // Call API
                APIResponse<ActionHandler> response = variablesOperations.DeleteVariable(variableId);

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

                                    Console.WriteLine("\n--- Variable Deletion Success ---");
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
                                    Console.WriteLine("Variable ID " + variableId + " deleted successfully");
                                    Console.WriteLine("---");
                                }
                                else if (actionResponse is APIException)
                                {
                                    APIException exception = (APIException)actionResponse;

                                    Console.WriteLine("\n--- Variable Deletion Failed ---");
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
                                    Console.WriteLine("Failed to delete variable ID: " + variableId);
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

                // Delete variable
                DeleteVariable_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}