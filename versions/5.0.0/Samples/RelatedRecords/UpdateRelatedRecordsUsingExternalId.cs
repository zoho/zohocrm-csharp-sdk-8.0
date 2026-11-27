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
    public class UpdateRelatedRecordsUsingExternalId
    {
        public static void UpdateRelatedRecordsUsingExternalId_1(string moduleAPIName, string externalValue, string relatedListAPIName)
        {
            try
            {
                RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName);

                BodyWrapper request = new BodyWrapper();

                // List to hold records
                List<Com.Zoho.Crm.API.Record.Record> recordList = new List<Com.Zoho.Crm.API.Record.Record>();

                // First record to update
                Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();

                record1.Id = 34770615001L; // ID of the related record to update
                record1.AddKeyValue("Last_Name", "Updated Last Name 1");
                record1.AddKeyValue("First_Name", "Updated First Name 1");
                record1.AddKeyValue("Email", "updated1@example.com");
                record1.AddKeyValue("Phone", "+1-555-0101");
                record1.AddKeyValue("Title", "Senior Manager");
                record1.AddKeyValue("Products_External", "Products_External");

                recordList.Add(record1);

                // Second record to update
                Com.Zoho.Crm.API.Record.Record record2 = new Com.Zoho.Crm.API.Record.Record();

                record2.Id = 34770615002L; // ID of another related record to update
                record2.AddKeyValue("Last_Name", "Updated Last Name 2");
                record2.AddKeyValue("First_Name", "Updated First Name 2");
                record2.AddKeyValue("Email", "updated2@example.com");
                record2.AddKeyValue("Phone", "+1-555-0102");
                record2.AddKeyValue("Department", "Engineering");
                record1.AddKeyValue("Products_External", "Products_External");

                recordList.Add(record2);

                request.Data = recordList;

                HeaderMap headerInstance = new HeaderMap();
                headerInstance.Add(UpdateRelatedRecordsUsingExternalIDHeader.X_EXTERNAL, "Leads.External,Products.Products_External");
                APIResponse<ActionHandler> response = relatedRecordsOperations.UpdateRelatedRecordsUsingExternalId(externalValue, request, headerInstance);

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

                                    Console.WriteLine("Related record updated successfully!");
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

                                    Console.WriteLine("Error updating related record:");
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
                string externalValue = "External"; // External ID of the account
                UpdateRelatedRecordsUsingExternalId_1(moduleAPIName, externalValue, relatedListAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}