using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.RelatedRecords;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.RelatedRecords
{
    public class UpdateRelatedRecord
    {
        public static void UpdateRelatedRecord_1(string moduleAPIName, long recordId, string relatedListAPIName, long relatedRecordId)
        {
            try
            {
                RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName);

                BodyWrapper request = new BodyWrapper();

                List<Com.Zoho.Crm.API.Record.Record> recordList = new List<Com.Zoho.Crm.API.Record.Record>();

                Com.Zoho.Crm.API.Record.Record record = new Com.Zoho.Crm.API.Record.Record();

                // Set the ID of the record to update
                record.Id = relatedRecordId;

                // Update standard fields
                record.AddKeyValue("Last_Name", "Updated Last Name");
                record.AddKeyValue("First_Name", "Updated First Name");
                record.AddKeyValue("Email", "updated.email@example.com");
                record.AddKeyValue("Phone", "+1-555-0199");
                record.AddKeyValue("Mobile", "+1-555-0299");
                record.AddKeyValue("Title", "Senior Manager");
                record.AddKeyValue("Department", "Sales");
                record.AddKeyValue("Lead_Source", "Web Form");

                // Update address information
                record.AddKeyValue("Mailing_Street", "123 Updated Street");
                record.AddKeyValue("Mailing_City", "Updated City");
                record.AddKeyValue("Mailing_State", "Updated State");
                record.AddKeyValue("Mailing_Zip", "12345");
                record.AddKeyValue("Mailing_Country", "USA");

                // Add description
                record.AddKeyValue("Description", "Updated through API - Single record update");

                recordList.Add(record);

                request.Data = recordList;

                HeaderMap headerInstance = new HeaderMap();

                // Call API to update the specific related record
                APIResponse<ActionHandler> response = relatedRecordsOperations.UpdateRelatedRecord(relatedRecordId, recordId, request, headerInstance);

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

                                    Console.WriteLine("Update Details: ");

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
                                        Console.WriteLine("Error Details: ");
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

                string moduleAPIName = "Accounts";
                string relatedListAPIName = "Contacts";
                long recordId = 34770615177002L; // Parent record ID (Account)
                long relatedRecordId = 34770615001L; // Related record ID (Contact)
                UpdateRelatedRecord_1(moduleAPIName, recordId, relatedListAPIName, relatedRecordId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}