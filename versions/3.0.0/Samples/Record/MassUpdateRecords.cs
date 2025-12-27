using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using MassUpdateActionHandler = Com.Zoho.Crm.API.Record.MassUpdateActionHandler;
using MassUpdateActionWrapper = Com.Zoho.Crm.API.Record.MassUpdateActionWrapper;
using MassUpdateBodyWrapper = Com.Zoho.Crm.API.Record.MassUpdateBodyWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Record
{
    public class MassUpdateRecords
    {
        /// <summary>
        /// This method is used to perform mass update on records in a module
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        public static void MassUpdateRecords_1(string moduleAPIName)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of MassUpdateBodyWrapper class
                MassUpdateBodyWrapper bodyWrapper = new MassUpdateBodyWrapper();

                // List to hold MassUpdate instances
                List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();
                Com.Zoho.Crm.API.Record.Record record = new Com.Zoho.Crm.API.Record.Record();
                record.AddKeyValue("City", "Value");
                //record.AddKeyValue("Company", "Value");

                records.Add(record);
                bodyWrapper.Data = records;

                //bodyWrapper.Cvid = "3477061087501";
                List<String> ids = new List<string>() { "1055806000004381002" };
                bodyWrapper.Ids = ids;
                //Com.Zoho.Crm.API.Record.MassUpdateTerritory territory = new Com.Zoho.Crm.API.Record.MassUpdateTerritory();
                //territory.Id = 0L;
                //territory.IncludeChild = true;
                //bodyWrapper.Territory = territory;
                bodyWrapper.OverWrite = true;

                // Call MassUpdateRecords method that takes MassUpdateBodyWrapper instance as parameter
                APIResponse<MassUpdateActionHandler> response = recordOperations.MassUpdateRecords(bodyWrapper);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        MassUpdateActionHandler massUpdateActionHandler = response.Object;

                        if (massUpdateActionHandler is MassUpdateActionWrapper massUpdateActionWrapper)
                        {
                            List<MassUpdateActionResponse> massUpdateActionResponses = massUpdateActionWrapper.Data;

                            foreach (MassUpdateActionResponse massUpdateActionResponse in massUpdateActionResponses)
                            {
                                if (massUpdateActionResponse is MassUpdateSuccessResponse massUpdateSuccessResponse)
                                {
                                    Console.WriteLine("Status: " + massUpdateSuccessResponse.Status.Value);
                                    Console.WriteLine("Code: " + massUpdateSuccessResponse.Code.Value);
                                    Console.WriteLine("Details: ");

                                    if (massUpdateSuccessResponse.Details != null)
                                    {
                                        foreach (KeyValuePair<string, object> entry in massUpdateSuccessResponse.Details)
                                        {
                                            Console.WriteLine(entry.Key + ": " + entry.Value);
                                        }
                                    }
                                    Console.WriteLine("Message: " + massUpdateSuccessResponse.Message.Value);
                                }
                                else if (massUpdateActionResponse is APIException exception)
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
                        else if (massUpdateActionHandler is APIException exception)
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

                MassUpdateRecords_1("Leads");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}