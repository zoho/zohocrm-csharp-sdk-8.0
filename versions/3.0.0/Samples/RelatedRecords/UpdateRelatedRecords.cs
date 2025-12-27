using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.RelatedRecords;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using Com.Zoho.Crm.API.Record;
using BodyWrapper = Com.Zoho.Crm.API.RelatedRecords.BodyWrapper;
using ActionHandler = Com.Zoho.Crm.API.RelatedRecords.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.RelatedRecords.ActionWrapper;
using ActionResponse = Com.Zoho.Crm.API.RelatedRecords.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.RelatedRecords.SuccessResponse;
using APIException = Com.Zoho.Crm.API.RelatedRecords.APIException;

namespace Samples.RelatedRecords
{
    public class UpdateRelatedRecords
    {
        public static void UpdateRelatedRecords_1(string moduleAPIName, long recordId, string relatedListAPIName)
        {
            try
            {
                RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName);

                BodyWrapper bodyWrapper = new BodyWrapper();
                List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

                // First record to update
                Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();
                record1.Id = 1055806000000308001L; // Related record ID
                record1.AddFieldValue(Contacts.LAST_NAME, "Updated Last Name 1");
                record1.AddFieldValue(Contacts.EMAIL, "updated1@example.com");
                record1.AddFieldValue(Contacts.PHONE, "9876543210");
                records.Add(record1);

                // Second record to update
                Com.Zoho.Crm.API.Record.Record record2 = new Com.Zoho.Crm.API.Record.Record();
                record2.Id = 34770615002L; // Related record ID
                record2.AddFieldValue(Contacts.LAST_NAME, "Updated Last Name 2");
                record2.AddFieldValue(Contacts.EMAIL, "updated2@example.com");
                record2.AddFieldValue(Contacts.PHONE, "8765432109");
                records.Add(record2);

                bodyWrapper.Data = records;

                HeaderMap headerInstance = new HeaderMap();

                // Call API
                APIResponse<ActionHandler> response = relatedRecordsOperations.UpdateRelatedRecords(recordId, bodyWrapper, headerInstance);

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

                                    Console.WriteLine("Status: " + successResponse.Status.Value);
                                    Console.WriteLine("Code: " + successResponse.Code.Value);
                                    Console.WriteLine("Details: ");

                                    foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                    {
                                        Console.WriteLine(entry.Key + ": " + entry.Value);
                                    }

                                    Console.WriteLine("Message: " + successResponse.Message.Value);
                                }
                                else if (actionResponse is APIException)
                                {
                                    APIException exception = (APIException)actionResponse;

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
                long recordId = 34770615177002L;
                UpdateRelatedRecords_1(moduleAPIName, recordId, relatedListAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}