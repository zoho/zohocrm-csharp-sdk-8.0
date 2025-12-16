using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using DeletedRecordsHandler = Com.Zoho.Crm.API.Record.DeletedRecordsHandler;
using DeletedRecordsWrapper = Com.Zoho.Crm.API.Record.DeletedRecordsWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Users;
using Info = Com.Zoho.Crm.API.Record.Info;

namespace Samples.Record
{
    public class GetDeletedRecords
    {
        /// <summary>
        /// This method is used to get deleted records from a module
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        public static void GetDeletedRecords_1(string moduleAPIName)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of ParameterMap class
                ParameterMap paramInstance = new ParameterMap();

                // Add parameters
                paramInstance.Add(RecordOperations.GetDeletedRecordsParam.TYPE, "all");
                paramInstance.Add(RecordOperations.GetDeletedRecordsParam.PAGE, 1);
                paramInstance.Add(RecordOperations.GetDeletedRecordsParam.PER_PAGE, 10);

                // Get instance of HeaderMap class
                HeaderMap headerInstance = new HeaderMap();

                // Add header to get records modified since a particular date
                headerInstance.Add(RecordOperations.GetDeletedRecordsHeader.IF_MODIFIED_SINCE,
                    new DateTimeOffset(2023, 01, 01, 0, 0, 0, TimeSpan.Zero));

                // Call GetDeletedRecords method that takes ParameterMap instance and HeaderMap instance as parameter
                APIResponse<DeletedRecordsHandler> response = recordOperations.GetDeletedRecords(paramInstance, headerInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        DeletedRecordsHandler deletedRecordsHandler = response.Object;

                        if (deletedRecordsHandler is DeletedRecordsWrapper deletedRecordsWrapper)
                        {
                            List<DeletedRecord> deletedRecords = deletedRecordsWrapper.Data;
                            foreach (DeletedRecord deletedRecord in deletedRecords)
                            {
                                MinifiedUser deletedBy = deletedRecord.DeletedBy;
                                if (deletedBy != null)
                                {
                                    Console.WriteLine("DeletedRecord Deleted By User-Name: " + deletedBy.Name);
                                    Console.WriteLine("DeletedRecord Deleted By User-ID: " + deletedBy.Id);
                                }
                                Console.WriteLine("Deleted Record ID: " + deletedRecord.Id);
                                Console.WriteLine("Deleted Record DisplayName: " + deletedRecord.DisplayName);
                                Console.WriteLine("Deleted Type: " + deletedRecord.Type);
                                MinifiedUser createdBy = deletedRecord.CreatedBy;
                                if (createdBy != null)
                                {
                                    Console.WriteLine("DeletedRecord Created By User-Name: " + createdBy.Name);
                                    Console.WriteLine("DeletedRecord Created By User-ID: " + createdBy.Id);
                                }
                                Console.WriteLine("Deleted DeletedTime: " + deletedRecord.DeletedTime);

                                Console.WriteLine("-------------------");
                            }

                            // Check for pagination info
                            Info info = deletedRecordsWrapper.Info;
                            if (info != null)
                            {
                                Console.WriteLine("Record Count: " + info.Count);
                                Console.WriteLine("More Records: " + info.MoreRecords);
                                Console.WriteLine("Page Token: " + info.NextPageToken);
                            }
                        }
                        else if (deletedRecordsHandler is APIException exception)
                        {
                            Console.WriteLine("Status: " + exception.Status.Value);
                            Console.WriteLine("Code: " + exception.Code.Value);
                            Console.WriteLine("Details: ");

                            foreach (KeyValuePair<string, object> entry in exception.Details)
                            {
                                Console.WriteLine(entry.Key + ": " + entry.Value);
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

                GetDeletedRecords_1("Leads");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}