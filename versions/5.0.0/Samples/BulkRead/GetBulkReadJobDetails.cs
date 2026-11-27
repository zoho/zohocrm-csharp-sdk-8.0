using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.BulkRead.APIException;
using BulkReadOperations = Com.Zoho.Crm.API.BulkRead.BulkReadOperations;
using ResponseHandler = Com.Zoho.Crm.API.BulkRead.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.BulkRead.ResponseWrapper;
using JobDetail = Com.Zoho.Crm.API.BulkRead.JobDetail;
using Query = Com.Zoho.Crm.API.BulkRead.Query;
using Result = Com.Zoho.Crm.API.BulkRead.Result;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.BulkRead
{
    public class GetBulkReadJobDetails
    {
        public static void GetBulkReadJobDetails_1(long jobId)
        {
            BulkReadOperations bulkReadOperations = new BulkReadOperations();
            APIResponse<ResponseHandler> response = bulkReadOperations.GetBulkReadJobDetails(jobId);

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
                        List<JobDetail> jobDetails = responseWrapper.Data;

                        foreach (JobDetail jobDetail in jobDetails)
                        {
                            Console.WriteLine("Bulk read Job ID: " + jobDetail.Id);
                            Console.WriteLine("Bulk read Operation: " + jobDetail.Operation);
                            Console.WriteLine("Bulk read State: " + jobDetail.State);
                            Console.WriteLine("Bulk read Result: " + jobDetail.Result);
                            Console.WriteLine("Bulk read File Type: " + jobDetail.FileType);
                            Console.WriteLine("Bulk read Created Time: " + jobDetail.CreatedTime);

                            Com.Zoho.Crm.API.Users.MinifiedUser createdBy = jobDetail.CreatedBy;
                            if (createdBy != null)
                            {
                                Console.WriteLine("Bulk read Created By User-Name: " + createdBy.Name);
                                Console.WriteLine("Bulk read Created By User-ID: " + createdBy.Id);
                                Console.WriteLine("Bulk read Created By User-Email: " + createdBy.Email);
                            }

                            Query query = jobDetail.Query;
                            if (query != null)
                            {
                                Console.WriteLine("Bulk read Query Module: " + query.Module);
                                Console.WriteLine("Bulk read Query Page: " + query.Page);
                                Console.WriteLine("Bulk read Query CVId: " + query.Cvid);
                                if (query.Fields != null)
                                {
                                    Console.WriteLine("Bulk read Query Fields: ");
                                    foreach (string field in query.Fields)
                                    {
                                        Console.WriteLine("  " + field);
                                    }
                                }

                                Com.Zoho.Crm.API.BulkRead.Criteria criteria = query.Criteria;
                                if (criteria != null)
                                {
                                    Console.WriteLine("Bulk read Query Criteria: ");
                                    Console.WriteLine("  Group Operator: " + criteria.GroupOperator);
                                    Console.WriteLine("  Field: " + criteria.Field);
                                    Console.WriteLine("  Comparator: " + criteria.Comparator);
                                    Console.WriteLine("  Value: " + criteria.Value);
                                }
                            }

                            Result result = jobDetail.Result;
                            if (result != null)
                            {
                                Console.WriteLine("Bulk read Job Result Page: " + result.Page);
                                Console.WriteLine("Bulk read Job Result Count: " + result.Count);
                                Console.WriteLine("Bulk read Job Result Download URL: " + result.DownloadUrl);
                                Console.WriteLine("Bulk read Job Result Per Page: " + result.PerPage);
                                Console.WriteLine("Bulk read Job Result More Records: " + result.MoreRecords);
                            }
                        }
                    }
                    else if (responseHandler is APIException)
                    {
                        APIException exception = (APIException)responseHandler;
                        Console.WriteLine("Status: " + exception.Status.Value);
                        Console.WriteLine("Code: " + exception.Code.Value);
                        Console.WriteLine("Details: ");
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            Console.WriteLine(entry.Key + ": " + entry.Value);
                        }
                        Console.WriteLine("Message: " + exception.Message);
                    }
                }
                else
                {
                    Model responseObject = response.Model;
                    Type type = responseObject.GetType();
                    Console.WriteLine("Type is : {0}", type.Name);
                    PropertyInfo[] props = type.GetProperties();
                    Console.WriteLine("Properties (N = {0}) :", props.Length);
                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) in {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) in <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }

        public static void Call()
        {
            try
            {
                Environment environment = INDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                long jobId = 4402480774074L;
                GetBulkReadJobDetails_1(jobId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}