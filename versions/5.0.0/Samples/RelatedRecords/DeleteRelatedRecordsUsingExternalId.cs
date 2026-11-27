using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.RelatedRecords;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.RelatedRecords.RelatedRecordsOperations;

namespace Samples.RelatedRecords
{
    public class DeleteRelatedRecordsUsingExternalId
    {
        public static void DeleteRelatedRecordsUsingExternalId_1(string moduleAPIName, string relatedListAPIName, string externalValue, List<string> relatedRecordIds)
        {
            try
            {
                RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName);

                ParameterMap paramInstance = new ParameterMap();

                // Add multiple IDs to delete
                foreach (string id in relatedRecordIds)
                {
                    paramInstance.Add(DeleteRelatedRecordsUsingExternalIDParam.IDS, id);
                }

                HeaderMap headerInstance = new HeaderMap();

                headerInstance.Add(DeleteRelatedRecordsUsingExternalIDHeader.X_EXTERNAL, "Leads.External,Products.Products_External");
                APIResponse<ActionHandler> response = relatedRecordsOperations.DeleteRelatedRecordsUsingExternalId(externalValue, paramInstance, headerInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                            List<ActionResponse> actionResponses = actionWrapper.Data;

                            foreach (ActionResponse actionResponse in actionResponses)
                            {
                                if (actionResponse is SuccessResponse)
                                {
                                    SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                    Console.WriteLine("Related record deleted successfully!");
                                    Console.WriteLine("Status: " + successResponse.Status.Value);
                                    Console.WriteLine("Code: " + successResponse.Code.Value);
                                    Console.WriteLine("Message: " + successResponse.Message.Value);

                                    Console.WriteLine("Details: ");

                                    foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                    {
                                        Console.WriteLine(entry.Key + ": " + entry.Value);
                                    }
                                }
                                else if (actionResponse is APIException)
                                {
                                    APIException exception = (APIException)actionResponse;

                                    Console.WriteLine("Error deleting related record:");
                                    Console.WriteLine("Status: " + exception.Status.Value);
                                    Console.WriteLine("Code: " + exception.Code.Value);
                                    Console.WriteLine("Message: " + exception.Message.Value);

                                    if (exception.Details != null)
                                    {
                                        Console.WriteLine("Details: ");
                                        foreach (KeyValuePair<string, object> entry in exception.Details)
                                        {
                                            Console.WriteLine(entry.Key + ": " + entry.Value);
                                        }
                                    }
                                }
                            }
                        }
                        else if (actionHandler is APIException)
                        {
                            APIException exception = (APIException)actionHandler;

                            Console.WriteLine("Status: " + exception.Status.Value);
                            Console.WriteLine("Code: " + exception.Code.Value);
                            Console.WriteLine("Message: " + exception.Message.Value);
                            if (exception.Details != null)
                            {
                                Console.WriteLine("Details: ");
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    Console.WriteLine(entry.Key + ": " + entry.Value);
                                }
                            }
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

                string moduleAPIName = "Leads";
                string relatedListAPIName = "Products";
                string externalValue = "External"; // External ID of the record
                List<string> relatedRecordIds = new List<string> { "34770615001", "34770615002" };
                DeleteRelatedRecordsUsingExternalId_1(moduleAPIName, relatedListAPIName, externalValue, relatedRecordIds);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}