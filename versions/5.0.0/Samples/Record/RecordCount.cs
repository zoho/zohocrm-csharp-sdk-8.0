using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using CountHandler = Com.Zoho.Crm.API.Record.CountHandler;
using CountWrapper = Com.Zoho.Crm.API.Record.CountWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Record
{
    public class RecordCount
    {
        /// <summary>
        /// This method is used to get the count of records in a module
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        public static void RecordCount_1(string moduleAPIName)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of ParameterMap class
                ParameterMap paramInstance = new ParameterMap();
                paramInstance.Add(RecordOperations.RecordCountParam.WORD, "Test");

                // Call RecordCount method that takes ParameterMap instance as parameter
                APIResponse<CountHandler> response = recordOperations.RecordCount(paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        CountHandler countHandler = response.Object;

                        if (countHandler is CountWrapper countWrapper)
                        {
                            long? count = countWrapper.Count;
                            Console.WriteLine("Record Count: " + count);
                        }
                        else if (countHandler is APIException exception)
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

                RecordCount_1("Leads");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}