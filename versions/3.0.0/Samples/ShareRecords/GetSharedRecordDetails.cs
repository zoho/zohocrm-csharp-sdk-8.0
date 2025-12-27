using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.ShareRecords;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.ShareRecords
{
    public class GetSharedRecordDetails
    {
        public static void GetSharedRecordDetails_1(string moduleAPIName, long recordId)
        {
            try
            {
                ShareRecordsOperations shareRecordsOperations = new ShareRecordsOperations(recordId, moduleAPIName);
                ParameterMap paramInstance = new ParameterMap();
                APIResponse<ResponseHandler> response = shareRecordsOperations.GetSharedRecordDetails(paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<Com.Zoho.Crm.API.ShareRecords.ShareRecord> shareRecords = responseWrapper.Share;

                            Console.WriteLine($"Retrieved {shareRecords?.Count ?? 0} shared record details:");

                            if (shareRecords != null && shareRecords.Count > 0)
                            {
                                foreach (Com.Zoho.Crm.API.ShareRecords.ShareRecord shareRecord in shareRecords)
                                {
                                    Console.WriteLine("\n--- Share Record Details ---");

                                    if (shareRecord.ShareRelatedRecords != null)
                                    {
                                        Console.WriteLine("Share Related Records: " + shareRecord.ShareRelatedRecords.Value);
                                    }

                                    if (shareRecord.SharedThrough != null)
                                    {
                                        Console.WriteLine("Shared Through ID: " + shareRecord.SharedThrough.Id);
                                        Console.WriteLine("Shared Through Name: " + shareRecord.SharedThrough.Name);
                                    }

                                    if (shareRecord.Permission != null)
                                    {
                                        Console.WriteLine("Permission: " + shareRecord.Permission);
                                    }

                                    if (shareRecord.SharedTime != null)
                                    {
                                        Console.WriteLine("Shared Time: " + shareRecord.SharedTime);
                                    }

                                    if (shareRecord.User != null)
                                    {
                                        Console.WriteLine("Shared User ID: " + shareRecord.User.Id);
                                        Console.WriteLine("Shared User Name: " + shareRecord.User.Name);
                                        Console.WriteLine("Shared User Email: " + shareRecord.User.Email);
                                        Console.WriteLine("Shared User ZUID: " + shareRecord.User.Zuid);
                                    }

                                    Console.WriteLine("---");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No shared records found");
                            }
                        }
                        else if (responseHandler is APIException)
                        {
                            APIException exception = (APIException)responseHandler;

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
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }

        public static void Call()
        {
            try
            {
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();

                string moduleAPIName = "Contacts"; // Replace with actual module API name
                long recordId = 554023000000567023L; // Replace with actual record ID

                GetSharedRecordDetails_1(moduleAPIName, recordId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}