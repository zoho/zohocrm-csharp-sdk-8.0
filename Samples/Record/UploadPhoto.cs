using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using FileBodyWrapper = Com.Zoho.Crm.API.Record.FileBodyWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Record
{
    public class UploadPhoto
    {
        public static void UploadPhoto_1(string moduleAPIName, long recordId, string photoFilePath)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of FileBodyWrapper class
                FileBodyWrapper fileBodyWrapper = new FileBodyWrapper();

                StreamWrapper streamWrapper = new StreamWrapper(photoFilePath);

                fileBodyWrapper.File = streamWrapper;

                // Get instance of ParameterMap class
                ParameterMap paramInstance = new ParameterMap();

                // Add parameter to restrict triggers (optional)
                paramInstance.Add(RecordOperations.UploadPhotoParam.RESTRICT_TRIGGERS, "workflow");

                // Call UploadPhoto method that takes recordId, FileBodyWrapper instance and ParameterMap instance as parameter
                APIResponse<FileHandler> response = recordOperations.UploadPhoto(recordId, fileBodyWrapper, paramInstance);

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
                            Console.WriteLine("Photo uploaded successfully for record ID: " + recordId);
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

                UploadPhoto_1("Leads", 4834857410003040001L, "/Users/user/Desktop/image.jpg");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}