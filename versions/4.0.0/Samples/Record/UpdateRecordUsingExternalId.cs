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
    public class UpdateRecordUsingExternalId
    {
        /// <summary>
        /// This method is used to update a record using external ID
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        /// <param name="externalFieldValue">The external field value</param>
        public static void UpdateRecordUsingExternalId_1(string moduleAPIName, string externalFieldValue)
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

                // Set field values for the record based on module
                if (moduleAPIName.Equals("Leads"))
                {
                    record.AddKeyValue("Last_Name", "Updated via External ID");
                    record.AddKeyValue("First_Name", "Updated First Name");
                    record.AddKeyValue("Company", "Updated Company via External");
                    record.AddKeyValue("Email", "updated_external@example.com");
                    record.AddKeyValue("Phone", "9999999999");
                    record.AddKeyValue("Lead_Source", new Choice<string>("Advertisement"));
                    record.AddKeyValue("Lead_Status", new Choice<string>("Contacted"));
                }
                else if (moduleAPIName.Equals("Contacts"))
                {
                    record.AddKeyValue("Last_Name", "Updated Contact via External ID");
                    record.AddKeyValue("First_Name", "Updated First Name");
                    record.AddKeyValue("Email", "updated_external_contact@example.com");
                    record.AddKeyValue("Phone", "8888888888");
                    record.AddKeyValue("Title", "Updated Title");
                }
                else if (moduleAPIName.Equals("Accounts"))
                {
                    record.AddKeyValue("Account_Name", "Updated Account via External ID");
                    record.AddKeyValue("Phone", "7777777777");
                    record.AddKeyValue("Website", "www.updatedexternalexample.com");
                    record.AddKeyValue("Account_Type", "Customer");
                    record.AddKeyValue("Industry", "Technology");
                }
                else if (moduleAPIName.Equals("Deals"))
                {
                    record.AddKeyValue("Deal_Name", "Updated Deal via External ID");
                    record.AddKeyValue("Stage", "Proposal/Price Quote");
                    record.AddKeyValue("Amount", 50000.00);
                    record.AddKeyValue("Closing_Date", new DateTimeOffset(2025, 06, 30, 0, 0, 0, TimeSpan.Zero));
                    record.AddKeyValue("Next_Step", "Updated next step");
                }

                // Add tags to the record
                List<Tag> tagList = new List<Tag>();

                Tag tag1 = new Tag();
                tag1.Name = "External ID Updated";
                tagList.Add(tag1);

                Tag tag2 = new Tag();
                tag2.Name = "API Integration";
                tagList.Add(tag2);

                record.Tag = tagList;

                // Set record owner (optional)
                MinifiedUser owner = new MinifiedUser();
                owner.Id = 4834857410003030001L; // Replace with actual user ID
                record.AddKeyValue("Owner", owner);

                // Add description field
                record.AddKeyValue("Description", "Record updated using external ID via API on " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                records.Add(record);
                bodyWrapper.Data = records;

                // Get instance of HeaderMap class
                HeaderMap headerInstance = new HeaderMap();

                // Add header to specify external field
                headerInstance.Add(RecordOperations.UpdateRecordUsingExternalIDHeader.X_EXTERNAL, "External");

                // Call UpdateRecordUsingExternalId method
                APIResponse<ActionHandler> response = recordOperations.UpdateRecordUsingExternalId(externalFieldValue, bodyWrapper, headerInstance);

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
                                                Console.WriteLine("Updated Record ID: " + entry.Value);
                                            }
                                        }
                                    }
                                    Console.WriteLine("Message: " + successResponse.Message.Value);
                                    Console.WriteLine("Record updated successfully using external ID: " + externalFieldValue);
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

                UpdateRecordUsingExternalId_1("Leads", "External123");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}