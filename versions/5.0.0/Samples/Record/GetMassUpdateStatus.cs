using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using MassUpdateResponseHandler = Com.Zoho.Crm.API.Record.MassUpdateResponseHandler;
using MassUpdateResponseWrapper = Com.Zoho.Crm.API.Record.MassUpdateResponseWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Record
{
    public class GetMassUpdateStatus
    {
        /// <summary>
        /// This method is used to get the status of mass update job
        /// </summary>
        /// <param name="moduleAPIName">The API name of the module</param>
        /// <param name="jobId">The ID of the mass update job</param>
        public static void GetMassUpdateStatus_1(string moduleAPIName, string jobId)
        {
            try
            {
                // Get instance of RecordOperations class
                RecordOperations recordOperations = new RecordOperations(moduleAPIName);

                // Get instance of ParameterMap class
                ParameterMap paramInstance = new ParameterMap();

                // Add job ID parameter
                paramInstance.Add(RecordOperations.GetMassUpdateStatusParam.JOB_ID, jobId);

                // Call GetMassUpdateStatus method that takes ParameterMap instance as parameter
                APIResponse<MassUpdateResponseHandler> response = recordOperations.GetMassUpdateStatus(paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        MassUpdateResponseHandler massUpdateResponseHandler = response.Object;

                        if (massUpdateResponseHandler is MassUpdateResponseWrapper massUpdateResponseWrapper)
                        {
                            List<MassUpdateResponse> massUpdateResponses = massUpdateResponseWrapper.Data;

                            foreach (MassUpdateResponse massUpdateResponse in massUpdateResponses)
                            {
                                if (massUpdateResponse is MassUpdate massUpdate)
                                {
                                    Console.WriteLine("MassUpdate Status: " + massUpdate.Status?.Value);
                                    Console.WriteLine("MassUpdate FailedCount: " + massUpdate.FailedCount);
                                    Console.WriteLine("MassUpdate UpdatedCount: " + massUpdate.UpdatedCount);
                                    Console.WriteLine("MassUpdate NotUpdatedCount: " + massUpdate.NotUpdatedCount);
                                    Console.WriteLine("MassUpdate TotalCount: " + massUpdate.TotalCount);
                                }
                                else if (massUpdateResponse is APIException exception)
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
                        else if (massUpdateResponseHandler is APIException exception)
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

                GetMassUpdateStatus_1("Leads", "738964000002112003");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}