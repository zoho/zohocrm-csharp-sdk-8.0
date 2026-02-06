using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using ResponseHandler = Com.Zoho.Crm.API.Record.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Record.ResponseWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Newtonsoft.Json;
using System.Collections;

namespace Samples.Record
{
    public class GetFullDataForRichText
    {
        /// <summary>
        /// This method is used to get full data for rich text fields of a record
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        /// <param name="recordId">The ID of the record</param>
        public static void GetFullDataForRichText_1(string moduleAPIName, long recordId)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of ParameterMap class
                ParameterMap paramInstance = new ParameterMap();

                paramInstance.Add(RecordOperations.GetFullDataForRichTextParam.FIELDS, "Company,Email");

                // Call GetFullDataForRichText method that takes recordId and ParameterMap instance as parameter
                APIResponse<ResponseHandler> response = recordOperations.GetFullDataForRichText(recordId, paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper responseWrapper)
                        {
                            List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;

                            foreach (Com.Zoho.Crm.API.Record.Record record in records)
                            {
                                Console.WriteLine("Record ID: " + record.Id);
                                Console.WriteLine("Module: " + moduleAPIName);
                                Console.WriteLine("Created by: " + record.CreatedBy?.Name);
                                Console.WriteLine("Created Time: " + record.CreatedTime);
                                Console.WriteLine("Modified by: " + record.ModifiedBy?.Name);
                                Console.WriteLine("Modified Time: " + record.ModifiedTime);

                                foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
                                {
                                    string keyName = entry.Key;

                                    object value = entry.Value;

                                    if (value is IList)
                                    {
                                        Console.WriteLine("Record KeyName : " + keyName);

                                        IList dataList = (IList)value;

                                        foreach (object data in dataList)
                                        {
                                            if (data is IDictionary)
                                            {
                                                Console.WriteLine("Record KeyName : " + keyName + " - Value : ");

                                                foreach (KeyValuePair<string, object> entry1 in (Dictionary<string, object>)data)
                                                {
                                                    Console.WriteLine(entry1.Key + " : " + JsonConvert.SerializeObject(entry1.Value));
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine(JsonConvert.SerializeObject(data));
                                            }
                                        }
                                    }
                                    else if (value is IDictionary)
                                    {
                                        Console.WriteLine("Record KeyName : " + keyName + " - Value : ");

                                        foreach (KeyValuePair<string, object> entry1 in (Dictionary<string, object>)value)
                                        {
                                            Console.WriteLine(entry1.Key + " : " + JsonConvert.SerializeObject(entry1.Value));
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Record KeyName : " + keyName + " - Value : " + JsonConvert.SerializeObject(value));
                                    }
                                }
                            }
                        }
                        else if (responseHandler is APIException exception)
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

                GetFullDataForRichText_1("Leads", 4834857410003040001L);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}