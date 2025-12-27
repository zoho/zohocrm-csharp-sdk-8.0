using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Tags;
using Com.Zoho.Crm.API.Users;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Record.BodyWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;

namespace Samples.Record
{
    public class CreateMultipleRecord
    {
        /// <summary>
        /// This method is used to create records in a module
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        public static void CreateRecords_1(string moduleAPIName)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of BodyWrapper class
                BodyWrapper bodyWrapper = new BodyWrapper();

                // List to hold Record instances
                List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

                // Create record instance
                Com.Zoho.Crm.API.Record.Record record = new Com.Zoho.Crm.API.Record.Record();

                // Set field values for the record
                if (moduleAPIName.Equals("Leads"))
                {
                    record.AddKeyValue("Last_Name", "Test Lead");
                    record.AddKeyValue("First_Name", "Sample");
                    record.AddKeyValue("Company", "Sample Company");
                    record.AddKeyValue("Email", "sample@example.com");
                    record.AddKeyValue("Phone", "1234567890");
                    record.AddKeyValue("Lead_Source", new Choice<string>("Advertisement"));
                }
                else if (moduleAPIName.Equals("Contacts"))
                {
                    record.AddKeyValue("Last_Name", "Test Contact");
                    record.AddKeyValue("First_Name", "Sample");
                    record.AddKeyValue("Email", "contact@example.com");
                    record.AddKeyValue("Phone", "9876543210");
                }
                else if (moduleAPIName.Equals("Accounts"))
                {
                    record.AddKeyValue("Account_Name", "Sample Account");
                    record.AddKeyValue("Phone", "1234567890");
                    record.AddKeyValue("Website", "www.example.com");
                }
                else if (moduleAPIName.Equals("Deals"))
                {
                    record.AddKeyValue("Deal_Name", "Sample Deal");
                    record.AddKeyValue("Stage", "Qualification");
                    record.AddKeyValue("Amount", 10000.00);
                    record.AddKeyValue("Closing_Date", new DateTimeOffset(2024, 12, 31, 0, 0, 0, TimeSpan.Zero));
                }

                // Add tags to the record
                List<Tag> tagList = new List<Tag>();

                Tag tag1 = new Tag();
                tag1.Name = "Test Tag 1";
                tagList.Add(tag1);

                Tag tag2 = new Tag();
                tag2.Name = "Test Tag 2";
                tagList.Add(tag2);

                record.Tag = tagList;

                // Set record owner
                MinifiedUser owner = new MinifiedUser();
                owner.Id = 4834857410003030001L; // Replace with actual user ID
                record.AddKeyValue("Owner", owner);

                records.Add(record);

                // Create another record
                Com.Zoho.Crm.API.Record.Record record2 = new Com.Zoho.Crm.API.Record.Record();

                if (moduleAPIName.Equals("Leads"))
                {
                    record2.AddKeyValue("Last_Name", "Test Lead 2");
                    record2.AddKeyValue("First_Name", "Sample 2");
                    record2.AddKeyValue("Company", "Sample Company 2");
                    record2.AddKeyValue("Email", "sample2@example.com");
                    record2.AddKeyValue("Phone", "0987654321");
                }
                else if (moduleAPIName.Equals("Contacts"))
                {
                    record2.AddKeyValue("Last_Name", "Test Contact 2");
                    record2.AddKeyValue("First_Name", "Sample 2");
                    record2.AddKeyValue("Email", "contact2@example.com");
                    record2.AddKeyValue("Phone", "1234509876");
                }

                records.Add(record2);

                bodyWrapper.Data = records;

                // Get instance of HeaderMap class
                HeaderMap headerInstance = new HeaderMap();

                // Call CreateRecords method that takes BodyWrapper instance and HeaderMap instance as parameter
                APIResponse<ActionHandler> response = recordOperations.CreateRecords(bodyWrapper, headerInstance);

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

                CreateRecords_1("Leads");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}