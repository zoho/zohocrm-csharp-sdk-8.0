using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.RelatedRecords;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.RelatedRecords.RelatedRecordsOperations;

namespace Samples.RelatedRecords
{
    public class GetDeletedParentRecordsRelatedRecord
    {
        public static void GetDeletedParentRecordsRelatedRecord_1(string moduleAPIName, long recordId, string relatedListAPIName)
        {
            try
            {
                RelatedRecordsOperations relatedRecordsOperations = new RelatedRecordsOperations(relatedListAPIName, moduleAPIName);

                ParameterMap paramInstance = new ParameterMap();

                // Optional parameters for filtering and pagination
                paramInstance.Add(GetDeletedParentRecordsRelatedRecordParam.PAGE, 1);
                paramInstance.Add(GetDeletedParentRecordsRelatedRecordParam.PER_PAGE, 20);
                paramInstance.Add(GetDeletedParentRecordsRelatedRecordParam.FIELDS, "Last_Name,First_Name,Email,Phone,Created_Time,Modified_Time,Deleted_Time");
                //paramInstance.Add(GetDeletedParentRecordsRelatedRecordParam.IDS, "1055806000000308001,1055806000000308032");
                APIResponse<ResponseHandler> response = relatedRecordsOperations.GetDeletedParentRecordsRelatedRecord(recordId, paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;

                            Console.WriteLine("=== Related Records from Deleted Parents ===");
                            Console.WriteLine($"Found {records?.Count ?? 0} related records from deleted parent records");

                            if (records != null && records.Count > 0)
                            {
                                foreach (Com.Zoho.Crm.API.Record.Record record in records)
                                {
                                    Console.WriteLine("\n--- Related Record Details ---");
                                    Console.WriteLine("Record ID: " + record.Id);

                                    foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
                                    {
                                        string fieldName = entry.Key;
                                        object value = entry.Value;

                                        if (value != null)
                                        {
                                            if (value is List<object>)
                                            {
                                                Console.WriteLine(fieldName + ": ");

                                                List<object> dataList = (List<object>)value;

                                                foreach (object data in dataList)
                                                {
                                                    if (data is Dictionary<string, object>)
                                                    {
                                                        Dictionary<string, object> dataMap = (Dictionary<string, object>)data;

                                                        foreach (KeyValuePair<string, object> mapEntry in dataMap)
                                                        {
                                                            Console.WriteLine("  " + mapEntry.Key + ": " + mapEntry.Value);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("  " + data);
                                                    }
                                                }
                                            }
                                            else if (value is Dictionary<string, object>)
                                            {
                                                Console.WriteLine(fieldName + ": ");

                                                Dictionary<string, object> dataMap = (Dictionary<string, object>)value;

                                                foreach (KeyValuePair<string, object> mapEntry in dataMap)
                                                {
                                                    Console.WriteLine("  " + mapEntry.Key + ": " + mapEntry.Value);
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine(fieldName + ": " + value);
                                            }
                                        }
                                    }

                                    // Highlight deletion context
                                    Console.WriteLine("--- Deletion Context ---");
                                    Console.WriteLine("Parent Module: " + moduleAPIName);
                                    Console.WriteLine("Related List: " + relatedListAPIName);
                                    if (record.GetKeyValue("Deleted_Time") != null)
                                    {
                                        Console.WriteLine("Parent Deleted Time: " + record.GetKeyValue("Deleted_Time"));
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No related records found from deleted parent records.");
                            }

                            // Print pagination info
                            Com.Zoho.Crm.API.Record.Info info = responseWrapper.Info;
                            if (info != null)
                            {
                                Console.WriteLine("\n--- Pagination Info ---");
                                Console.WriteLine("Page: " + info.Page);
                                Console.WriteLine("Per Page: " + info.PerPage);
                                Console.WriteLine("Count: " + info.Count);
                                Console.WriteLine("More Records: " + info.MoreRecords);
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
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();

                string moduleAPIName = "Accounts";
                string relatedListAPIName = "Contacts";
                long recordId = 233L;
                GetDeletedParentRecordsRelatedRecord_1(moduleAPIName, recordId, relatedListAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}