using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Record
{
    public class DeletePhoto
    {
        /// <summary>
        /// This method is used to delete photo of a record
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        /// <param name="recordId">The ID of the record</param>
        public static void DeletePhoto_1(string moduleAPIName, long recordId)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Call DeletePhoto method that takes recordId as parameter
                APIResponse<FileHandler> response = recordOperations.DeletePhoto(recordId);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        FileHandler fileHandler = response.Object;

                        if (fileHandler is SuccessResponse successResponse)
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
                            Console.WriteLine("Photo deleted successfully for record ID: " + recordId);
                        }
                        else if (fileHandler is APIException exception)
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

                DeletePhoto_1("Leads", 4834857410003040001L);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}