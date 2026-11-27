using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Coql;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Coql.APIException;
using ResponseHandler = Com.Zoho.Crm.API.Coql.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Coql.ResponseWrapper;
using BodyWrapper = Com.Zoho.Crm.API.Coql.BodyWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Newtonsoft.Json;
using System.Collections;

namespace Samples.Coql
{
    public class CoqlGetRecords
    {
        public static void GetRecords_1()
        {
            try
            {
                CoqlOperations coqlOperations = new CoqlOperations();
                BodyWrapper bodyWrapper = new BodyWrapper();

                // Set the COQL query
                string selectQuery = "select Last_Name, First_Name, Full_Name, Email, Phone from Leads where Last_Name is not null limit 200";
                bodyWrapper.SelectQuery = selectQuery;

                APIResponse<ResponseHandler> response = coqlOperations.GetRecords(bodyWrapper);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                    {
                        Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
                        return;
                    }

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;
                            List<Com.Zoho.Crm.API.Record.Record> records = responseWrapper.Data;

                            if (records != null)
                            {
                                foreach (Com.Zoho.Crm.API.Record.Record record in records)
                                {
                                    Console.WriteLine("Record Details:");
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
                                    Console.WriteLine("---------------------------");
                                }
                            }

                            Com.Zoho.Crm.API.Record.Info info = responseWrapper.Info;
                            if (info != null)
                            {
                                Console.WriteLine("Record Info Count: " + info.Count);
                                Console.WriteLine("Record Info MoreRecords: " + info.MoreRecords);
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
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

                GetRecords_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}