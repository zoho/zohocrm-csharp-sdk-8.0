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
    public class RevokeSharedRecord
    {
        public static void RevokeSharedRecord_1(string moduleAPIName, long recordId)
        {
            try
            {
                ShareRecordsOperations shareRecordsOperations = new ShareRecordsOperations(recordId, moduleAPIName);
                APIResponse<DeleteActionHandler> response = shareRecordsOperations.RevokeSharedRecord();

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        DeleteActionHandler delActionHandler = response.Object;

                        if (delActionHandler is DeleteActionWrapper)
                        {
                            DeleteActionWrapper delActionWrapper = (DeleteActionWrapper)delActionHandler;

                            DeleteActionResponse delActionResponse = delActionWrapper.Share;

                            if (delActionResponse is SuccessResponse)
                            {
                                SuccessResponse successResponse = (SuccessResponse)delActionResponse;

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
                            else if (delActionResponse is APIException)
                            {
                                APIException exception = (APIException)delActionResponse;

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
                        else if (delActionHandler is APIException)
                        {
                            APIException exception = (APIException)delActionHandler;

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

                RevokeSharedRecord_1(moduleAPIName, recordId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}