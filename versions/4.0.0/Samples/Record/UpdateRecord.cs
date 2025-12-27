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
    public class UpdateRecord
    {
        /// <summary>
        /// This method is used to update a single record in a module
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        /// <param name="recordId">The ID of the record to update</param>
        public static void UpdateRecord_1(string moduleAPIName, long recordId)
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

                // Set record ID
                record.Id = recordId;

                // Set field values for the record
                if (moduleAPIName.Equals("Leads"))
                {
                    record.AddKeyValue("Last_Name", "Updated Lead Name");
                    record.AddKeyValue("First_Name", "Updated First Name");
                    record.AddKeyValue("Company", "Updated Company");
                    record.AddKeyValue("Email", "updated@example.com");
                    record.AddKeyValue("Phone", "9999999999");
                    record.AddKeyValue("Lead_Source", new Choice<string>("Advertisement"));
                }
                else if (moduleAPIName.Equals("Contacts"))
                {
                    record.AddKeyValue("Last_Name", "Updated Contact Name");
                    record.AddKeyValue("First_Name", "Updated First Name");
                    record.AddKeyValue("Email", "updatedcontact@example.com");
                    record.AddKeyValue("Phone", "8888888888");
                }
                else if (moduleAPIName.Equals("Accounts"))
                {
                    record.AddKeyValue("Account_Name", "Updated Account Name");
                    record.AddKeyValue("Phone", "7777777777");
                    record.AddKeyValue("Website", "www.updatedexample.com");
                }
                else if (moduleAPIName.Equals("Deals"))
                {
                    record.AddKeyValue("Deal_Name", "Updated Deal Name");
                    record.AddKeyValue("Stage", "Proposal/Price Quote");
                    record.AddKeyValue("Amount", 20000.00);
                    record.AddKeyValue("Closing_Date", new DateTimeOffset(2025, 01, 31, 0, 0, 0, TimeSpan.Zero));
                }

                // Update record owner
                MinifiedUser owner = new MinifiedUser();
                owner.Id = 4834857410003030001L; // Replace with actual user ID
                record.AddKeyValue("Owner", owner);

                // Update tags for the record
                List<Tag> tagList = new List<Tag>();

                Tag tag1 = new Tag();
                tag1.Name = "Updated Tag 1";
                tagList.Add(tag1);

                Tag tag2 = new Tag();
                tag2.Name = "Updated Tag 2";
                tagList.Add(tag2);

                record.Tag = tagList;

                records.Add(record);
                bodyWrapper.Data = records;

                // Get instance of HeaderMap class
                HeaderMap headerInstance = new HeaderMap();

                // Call UpdateRecord method that takes recordId, BodyWrapper instance and HeaderMap instance as parameter
                APIResponse<ActionHandler> response = recordOperations.UpdateRecord(recordId, bodyWrapper, headerInstance);

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

                UpdateRecord_1("Leads", 4834857410003040001L);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}