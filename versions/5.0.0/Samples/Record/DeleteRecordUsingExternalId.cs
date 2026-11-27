using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Record
{
    public class DeleteRecordUsingExternalId
    {
        /// <summary>
        /// This method is used to delete a record using external ID
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        /// <param name="externalFieldValue">The external field value</param>
        public static void DeleteRecordUsingExternalId_1(string moduleAPIName, string externalFieldValue)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of ParameterMap class
                ParameterMap paramInstance = new ParameterMap();

                // Add parameter to trigger workflow
                paramInstance.Add(RecordOperations.DeleteRecordUsingExternalIDParam.WF_TRIGGER, true);

                // Get instance of HeaderMap class
                HeaderMap headerInstance = new HeaderMap();

                // Add header to specify external field
                headerInstance.Add(RecordOperations.DeleteRecordUsingExternalIDHeader.X_EXTERNAL, "External");

                // Call DeleteRecordUsingExternalId method
                APIResponse<ActionHandler> response = recordOperations.DeleteRecordUsingExternalId(externalFieldValue, paramInstance, headerInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper actionWrapper)
                        {
                            List<ActionResponse> actionResponses = actionWrapper.Data;

                            foreach (ActionResponse actionResponse in actionResponses)
                            {
                                if (actionResponse is SuccessResponse successResponse)
                                {
                                    Console.WriteLine("Status: " + successResponse.Status.Value);
                                    Console.WriteLine("Code: " + successResponse.Code.Value);
                                    Console.WriteLine("Details: ");

                                    if (successResponse.Details != null)
                                    {
                                        foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                        {
                                            Console.WriteLine(entry.Key + ": " + entry.Value);

                                            if (entry.Key.Equals("id"))
                                            {
                                                Console.WriteLine("Deleted Record ID: " + entry.Value);
                                            }
                                        }
                                    }
                                    Console.WriteLine("Message: " + successResponse.Message.Value);
                                    Console.WriteLine("Record deleted successfully using external ID: " + externalFieldValue);
                                }
                                else if (actionResponse is APIException exception)
                                {
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
                                    Console.WriteLine("Message: " + exception.Message.Value);
                                }
                            }
                        }
                        else if (actionHandler is APIException exception)
                        {
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
                            Console.WriteLine("Message: " + exception.Message.Value);
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
                Console.WriteLine(e.ToString());
            }
        }

        public static void Call()
        {
            try
            {
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder()
                    .ClientId("Client_Id")
                    .ClientSecret("Client_Secret")
                    .RefreshToken("Refresh_Token")
                    .Build();

                new Initializer.Builder()
                    .Environment(environment)
                    .Token(token)
                    .Initialize();

                DeleteRecordUsingExternalId_1("Leads", "External123");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}