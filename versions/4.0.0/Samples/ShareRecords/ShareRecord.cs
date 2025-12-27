using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.ShareRecords;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using Com.Zoho.Crm.API.Users;
using BodyWrapper = Com.Zoho.Crm.API.ShareRecords.BodyWrapper;
using ActionHandler = Com.Zoho.Crm.API.ShareRecords.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.ShareRecords.ActionWrapper;
using ActionResponse = Com.Zoho.Crm.API.ShareRecords.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.ShareRecords.SuccessResponse;
using APIException = Com.Zoho.Crm.API.ShareRecords.APIException;

namespace Samples.ShareRecords
{
    public class ShareRecord
    {
        public static void ShareRecord_1(string moduleAPIName, long recordId)
        {
            try
            {
                ShareRecordsOperations shareRecordsOperations = new ShareRecordsOperations(recordId, moduleAPIName);

                BodyWrapper request = new BodyWrapper();

                List<Com.Zoho.Crm.API.ShareRecords.ShareRecord> shareRecordsList = new List<Com.Zoho.Crm.API.ShareRecords.ShareRecord>();

                // Create share record
                Com.Zoho.Crm.API.ShareRecords.ShareRecord shareRecord = new Com.Zoho.Crm.API.ShareRecords.ShareRecord();

                //Com.Zoho.Crm.API.Users.Users user = new Com.Zoho.Crm.API.Users.Users();
                //user.Id = 554023000000235013L; // Replace with actual user ID
                //shareRecord.User = user;

                shareRecord.Permission = "full_access";
                shareRecord.ShareRelatedRecords = true;
                shareRecord.Type = new Choice<String>("private");
                Users sharedWith = new Users();
                sharedWith.Id = 1055806000017236002L;
                sharedWith.AddKeyValue("type", "groups");
                shareRecord.SharedWith = sharedWith;

                shareRecordsList.Add(shareRecord);

                request.Share = shareRecordsList;

                APIResponse<ActionHandler> response = shareRecordsOperations.ShareRecord(request);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                            List<ActionResponse> actionResponses = actionWrapper.Share;

                            foreach (ActionResponse actionResponse in actionResponses)
                            {
                                if (actionResponse is SuccessResponse)
                                {
                                    SuccessResponse successResponse = (SuccessResponse)actionResponse;

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
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();

                string moduleAPIName = "Contacts"; // Replace with actual module API name
                long recordId = 554023000000567023L; // Replace with actual record ID

                ShareRecord_1(moduleAPIName, recordId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}