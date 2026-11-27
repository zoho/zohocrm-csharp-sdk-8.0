using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using BodyWrapper = Com.Zoho.Crm.API.Record.BodyWrapper;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;

namespace Samples.Record
{
    public class AssignTerritoriesToMultipleRecords
    {
        /// <summary>
        /// This method is used to assign territories to multiple records
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        public static void AssignTerritoriesToMultipleRecords_1(string moduleAPIName)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of BodyWrapper class
                BodyWrapper bodyWrapper = new BodyWrapper();

                // List to hold records
                List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();

                // Create record instances with IDs
                Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();
                record1.Id = 4834857410003040001L; // Replace with actual record ID
                List<Territory> territories = new List<Territory>();
                Territory territory = new Territory();
                territory.Id = 4834857410003051001L; // Replace with actual territory ID
                territories.Add(territory);
                record1.AddKeyValue("Territories", territories);
                records.Add(record1);
                bodyWrapper.Data = records;
                // Call AssignTerritoriesToMultipleRecords method that takes BodyWrapper instance as parameter
                APIResponse<ActionHandler> response = recordOperations.AssignTerritoriesToMultipleRecords(bodyWrapper);

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

                AssignTerritoriesToMultipleRecords_1("Leads");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}