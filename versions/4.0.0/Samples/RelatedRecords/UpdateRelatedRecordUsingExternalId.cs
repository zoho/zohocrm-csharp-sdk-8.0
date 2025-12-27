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
    public class UpdateRelatedRecordUsingExternalId
    {
        public static void UpdateRelatedRecordUsingExternalId_1(string moduleAPIName, string externalValue, string relatedListAPIName, string externalFieldValue)
        {
            try
            {
                RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName);

                BodyWrapper request = new BodyWrapper();

                List<Com.Zoho.Crm.API.Record.Record> recordList = new List<Com.Zoho.Crm.API.Record.Record>();

                Com.Zoho.Crm.API.Record.Record record = new Com.Zoho.Crm.API.Record.Record();

                // Update standard fields
                record.AddKeyValue("Last_Name", "Updated via External ID");
                record.AddKeyValue("First_Name", "External Update");
                record.AddKeyValue("Email", "external.update@example.com");
                record.AddKeyValue("Phone", "+1-555-0199");
                record.AddKeyValue("Mobile", "+1-555-0299");
                record.AddKeyValue("Title", "Senior Manager (Updated)");
                record.AddKeyValue("Department", "Sales Operations");
                record.AddKeyValue("Lead_Source", "API Update");

                // Update address information
                record.AddKeyValue("Mailing_Street", "456 External Update Avenue");
                record.AddKeyValue("Mailing_City", "Updated City");
                record.AddKeyValue("Mailing_State", "Updated State");
                record.AddKeyValue("Mailing_Zip", "54321");
                record.AddKeyValue("Mailing_Country", "USA");

                // Add description with external ID context
                record.AddKeyValue("Description", $"Updated via API using parent external ID: {externalValue} on {DateTime.Now:yyyy-MM-dd HH:mm:ss}");

                recordList.Add(record);

                request.Data = recordList;

                HeaderMap headerInstance = new HeaderMap();
                headerInstance.Add(UpdateRelatedRecordUsingExternalIDHeader.X_EXTERNAL, "Leads.External,Products.Products_External");
                APIResponse<ActionHandler> response = relatedRecordsOperations.UpdateRelatedRecordUsingExternalId(externalFieldValue, externalValue, request, headerInstance);

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

                                    Console.WriteLine("Related record updated successfully using external ID!");
                                    Console.WriteLine("Status: " + successResponse.Status.Value);
                                    Console.WriteLine("Code: " + successResponse.Code.Value);
                                    Console.WriteLine("Message: " + successResponse.Message.Value);

                                    Console.WriteLine("Update Context:");
                                    Console.WriteLine("Parent External ID: " + externalValue);
                                    Console.WriteLine("Updated External FieldValue: " + externalFieldValue);

                                    Console.WriteLine("Update Details: ");

                                    foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                    {
                                        Console.WriteLine(entry.Key + ": " + entry.Value);
                                    }
                                }
                                else if (actionResponse is APIException)
                                {
                                    APIException exception = (APIException)actionResponse;

                                    Console.WriteLine("Error updating related record via external ID:");
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

                string moduleAPIName = "Leads";
                string relatedListAPIName = "Products";
                string externalValue = "External"; // External ID of the account
                string externalFieldValue = "externalFieldValue";
                UpdateRelatedRecordUsingExternalId_1(moduleAPIName, externalValue, relatedListAPIName, externalFieldValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}