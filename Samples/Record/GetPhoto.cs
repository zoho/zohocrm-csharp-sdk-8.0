using System;
using System.Collections.Generic;
using System.IO;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using DownloadHandler = Com.Zoho.Crm.API.Record.DownloadHandler;
using FileBodyWrapper = Com.Zoho.Crm.API.Record.FileBodyWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Record
{
    public class GetPhoto
    {
        /// <summary>
        /// This method is used to get photo of a record
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        /// <param name="recordId">The ID of the record</param>
        public static void GetPhoto_1(string moduleAPIName, long recordId)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Call GetPhoto method that takes recordId as parameter
                APIResponse<DownloadHandler> response = recordOperations.GetPhoto(recordId);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        DownloadHandler downloadHandler = response.Object;

                        if (downloadHandler is FileBodyWrapper fileBodyWrapper)
                        {
                            StreamWrapper streamWrapper = fileBodyWrapper.File;
                            Stream file = streamWrapper.Stream;
                            string fullFilePath = Path.Combine("./", streamWrapper.Name);
                            using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                            {
                                file.CopyTo(outputFileStream);
                            }
                        }
                        else if (downloadHandler is APIException exception)
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

                GetPhoto_1("Leads", 4834857410003040001L);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}