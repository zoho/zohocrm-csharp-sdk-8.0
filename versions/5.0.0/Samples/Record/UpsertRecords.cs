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
    public class UpsertRecords
    {
        /// <summary>
        /// This method is used to upsert records in a module
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        public static void UpsertRecords_1(string moduleAPIName)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);
                
                // Get instance of BodyWrapper class
                BodyWrapper bodyWrapper = new BodyWrapper();
                
                // List to hold Record instances
                List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();
                
                // Create record instance for upsert
                Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();
                
                // For upsert, either provide ID for update or unique field for insert/update
                if (moduleAPIName.Equals("Leads"))
                {
                    record1.AddKeyValue("Last_Name", "Upsert Lead");
                    record1.AddKeyValue("First_Name", "Sample");
                    record1.AddKeyValue("Company", "Upsert Company");
                    record1.AddKeyValue("Email", "upsert@example.com"); // Email can be used as duplicate check field
                    record1.AddKeyValue("Phone", "5555555555");
                }
                else if (moduleAPIName.Equals("Contacts"))
                {
                    record1.AddKeyValue("Last_Name", "Upsert Contact");
                    record1.AddKeyValue("First_Name", "Sample");
                    record1.AddKeyValue("Email", "upsertcontact@example.com"); // Email can be used as duplicate check field
                    record1.AddKeyValue("Phone", "4444444444");
                }
                else if (moduleAPIName.Equals("Accounts"))
                {
                    record1.AddKeyValue("Account_Name", "Upsert Account");
                    record1.AddKeyValue("Phone", "3333333333");
                    record1.AddKeyValue("Website", "www.upsertexample.com");
                }
                else if (moduleAPIName.Equals("Deals"))
                {
                    record1.AddKeyValue("Deal_Name", "Upsert Deal");
                    record1.AddKeyValue("Stage", "Qualification");
                    record1.AddKeyValue("Amount", 15000.00);
                    record1.AddKeyValue("Closing_Date", new DateTimeOffset(2025, 02, 28, 0, 0, 0, TimeSpan.Zero));
                }
                
                // Add tags to the record
                List<Tag> tagList = new List<Tag>();
                
                Tag tag1 = new Tag();
                tag1.Name = "Upsert Tag";
                tagList.Add(tag1);
                
                record1.Tag = tagList;
                
                // Set record owner
                MinifiedUser owner = new MinifiedUser();
                owner.Id = 4834857410003030001L; // Replace with actual user ID
                record1.AddKeyValue("Owner", owner);
                
                records.Add(record1);
                
                // Create another record for upsert with ID (for update)
                Com.Zoho.Crm.API.Record.Record record2 = new Com.Zoho.Crm.API.Record.Record();
                
                // If you have an existing record ID, you can set it for update
                // record2.Id = 4834857410003040001L; // Replace with actual record ID
                
                if (moduleAPIName.Equals("Leads"))
                {
                    record2.AddKeyValue("Last_Name", "Upsert Lead 2");
                    record2.AddKeyValue("First_Name", "Sample 2");
                    record2.AddKeyValue("Company", "Upsert Company 2");
                    record2.AddKeyValue("Email", "upsert2@example.com");
                    record2.AddKeyValue("Phone", "6666666666");
                }
                else if (moduleAPIName.Equals("Contacts"))
                {
                    record2.AddKeyValue("Last_Name", "Upsert Contact 2");
                    record2.AddKeyValue("First_Name", "Sample 2");
                    record2.AddKeyValue("Email", "upsertcontact2@example.com");
                    record2.AddKeyValue("Phone", "2222222222");
                }
                
                records.Add(record2);
                
                bodyWrapper.Data = records;
                
                // Get instance of HeaderMap class
                HeaderMap headerInstance = new HeaderMap();
                
                // Call UpsertRecords method that takes BodyWrapper instance and HeaderMap instance as parameter
                APIResponse<ActionHandler> response = recordOperations.UpsertRecords(bodyWrapper, headerInstance);
                
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

            UpsertRecords_1("Leads");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
}