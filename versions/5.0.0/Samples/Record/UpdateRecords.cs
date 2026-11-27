using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Tags;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Record.BodyWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;
using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;

namespace Samples.Record
{
    public class UpdateRecords
    {
        /// <summary>
        /// This method is used to update multiple records in a module
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        public static void UpdateRecords_1(string moduleAPIName)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of BodyWrapper class
                BodyWrapper bodyWrapper = new BodyWrapper();

                // List to hold Record instances
                List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

                // Create first record instance
                Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();

                // Set record ID for the first record
                record1.Id = 1055806000028592005L; // Replace with actual record ID

                // Set field values for the first record
                if (moduleAPIName.Equals("Leads"))
                {
                    record1.AddKeyValue("Last_Name", "Bulk Updated Lead 1");
                    record1.AddKeyValue("First_Name", "Updated First 1");
                    record1.AddKeyValue("Company", "Updated Company 1");
                    record1.AddKeyValue("Email", "bulkupdate1@example.com");
                    record1.AddKeyValue("Phone", "1111111111");
                }
                else if (moduleAPIName.Equals("Contacts"))
                {
                    record1.AddKeyValue("Last_Name", "Bulk Updated Contact 1");
                    record1.AddKeyValue("First_Name", "Updated First 1");
                    record1.AddKeyValue("Email", "bulkcontact1@example.com");
                    record1.AddKeyValue("Phone", "2222222222");
                }
                else if (moduleAPIName.Equals("Accounts"))
                {
                    record1.AddKeyValue("Account_Name", "Bulk Updated Account 1");
                    record1.AddKeyValue("Phone", "3333333333");
                    record1.AddKeyValue("Website", "www.bulkupdated1.com");
                }
                else if (moduleAPIName.Equals("Deals"))
                {
                    record1.AddKeyValue("Deal_Name", "Bulk Updated Deal 1");
                    record1.AddKeyValue("Stage", "Negotiation/Review");
                    record1.AddKeyValue("Amount", 25000.00);
                    record1.AddKeyValue("Closing_Date", new DateTimeOffset(2025, 03, 15, 0, 0, 0, TimeSpan.Zero));
                }

                // Update tags for the first record
                List<Tag> tagList1 = new List<Tag>();
                Tag tag1 = new Tag();
                tag1.Name = "Bulk Updated Tag 1";
                tagList1.Add(tag1);
                record1.Tag = tagList1;

                records.Add(record1);

                // Create second record instance
                Com.Zoho.Crm.API.Record.Record record2 = new Com.Zoho.Crm.API.Record.Record();

                // Set record ID for the second record
                record2.Id = 4834857410003040002L; // Replace with actual record ID

                // Set field values for the second record
                if (moduleAPIName.Equals("Leads"))
                {
                    record2.AddKeyValue("Last_Name", "Bulk Updated Lead 2");
                    record2.AddKeyValue("First_Name", "Updated First 2");
                    record2.AddKeyValue("Company", "Updated Company 2");
                    record2.AddKeyValue("Email", "bulkupdate2@example.com");
                    record2.AddKeyValue("Phone", "4444444444");
                }
                else if (moduleAPIName.Equals("Contacts"))
                {
                    record2.AddKeyValue("Last_Name", "Bulk Updated Contact 2");
                    record2.AddKeyValue("First_Name", "Updated First 2");
                    record2.AddKeyValue("Email", "bulkcontact2@example.com");
                    record2.AddKeyValue("Phone", "5555555555");
                }

                // Update tags for the second record
                List<Tag> tagList2 = new List<Tag>();
                Tag tag2 = new Tag();
                tag2.Name = "Bulk Updated Tag 2";
                tagList2.Add(tag2);
                record2.Tag = tagList2;

                records.Add(record2);

                bodyWrapper.Data = records;

                // Get instance of HeaderMap class
                HeaderMap headerInstance = new HeaderMap();

                // Call UpdateRecords method that takes BodyWrapper instance and HeaderMap instance as parameter
                APIResponse<ActionHandler> response = recordOperations.UpdateRecords(bodyWrapper, headerInstance);

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
                                        }
                                    }
                                    Console.WriteLine("Message: " + successResponse.Message.Value);
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

                UpdateRecords_1("Leads");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}