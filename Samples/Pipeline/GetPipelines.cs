using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Pipeline;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Pipeline.APIException;
using ResponseHandler = Com.Zoho.Crm.API.Pipeline.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Pipeline.ResponseWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Pipeline
{
    public class GetPipelines
    {
        public static void GetPipelines_1()
        {
            try
            {
                long layoutId = 1055806000000091023l;
                PipelineOperations pipelineOperations = new PipelineOperations(layoutId);
                APIResponse<ResponseHandler> response = pipelineOperations.GetPipelines();

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
                            List<Com.Zoho.Crm.API.Pipeline.Pipeline> pipelines = responseWrapper.Pipeline;

                            if (pipelines != null)
                            {
                                foreach (Com.Zoho.Crm.API.Pipeline.Pipeline pipeline in pipelines)
                                {
                                    Console.WriteLine("Pipeline ID: " + pipeline.Id);
                                    Console.WriteLine("Pipeline DisplayValue: " + pipeline.DisplayValue);
                                    Console.WriteLine("Pipeline ActualValue: " + pipeline.ActualValue);
                                    Console.WriteLine("Pipeline Default: " + pipeline.Default);
                                    Console.WriteLine("Pipeline ChildAvailable: " + pipeline.ChildAvailable);

                                    Com.Zoho.Crm.API.Pipeline.Pipeline parent = pipeline.Parent;
                                    if (parent != null)
                                    {
                                        Console.WriteLine("Pipeline Parent ID: " + parent.Id);
                                        Console.WriteLine("Pipeline Parent DisplayValue: " + parent.DisplayValue);
                                    }

                                    List<Maps> maps = pipeline.Maps;
                                    if (maps != null)
                                    {
                                        foreach (Maps map in maps)
                                        {
                                            Console.WriteLine("Maps ID: " + map.Id);
                                            Console.WriteLine("Maps DisplayValue: " + map.DisplayValue);
                                            Console.WriteLine("Maps ActualValue: " + map.ActualValue);
                                            Console.WriteLine("Maps SequenceNumber: " + map.SequenceNumber);
                                            Console.WriteLine("Maps ForecastType: " + map.ForecastType);
                                            Console.WriteLine("Maps ColourCode: " + map.ColourCode);

                                            ForecastCategory forecastCategory = map.ForecastCategory;
                                            if (forecastCategory != null)
                                            {
                                                Console.WriteLine("ForecastCategory ID: " + forecastCategory.Id);
                                                Console.WriteLine("ForecastCategory Name: " + forecastCategory.Name);
                                            }
                                        }
                                    }

                                    Console.WriteLine("---------------------------");
                                }
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

                GetPipelines_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}