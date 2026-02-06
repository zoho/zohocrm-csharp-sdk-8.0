using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Pipeline;
using Com.Zoho.Crm.API.Util;
using ActionHandler = Com.Zoho.Crm.API.Pipeline.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Pipeline.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Pipeline.ActionWrapper;
using APIException = Com.Zoho.Crm.API.Pipeline.APIException;
using BodyWrapper = Com.Zoho.Crm.API.Pipeline.BodyWrapper;
using SuccessResponse = Com.Zoho.Crm.API.Pipeline.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Pipeline
{
    public class UpdatePipelines
    {
        public static void UpdatePipelines_1()
        {
            try
            {
                long layoutId = 1055806000000091023l;
                PipelineOperations pipelineOperations = new PipelineOperations(layoutId);
                BodyWrapper bodyWrapper = new BodyWrapper();
                List<Com.Zoho.Crm.API.Pipeline.Pipeline> pipelines = new List<Com.Zoho.Crm.API.Pipeline.Pipeline>();

                Com.Zoho.Crm.API.Pipeline.Pipeline pipeline = new Com.Zoho.Crm.API.Pipeline.Pipeline();
                pipeline.Id = 1055806000029055001L;
                pipeline.DisplayValue = "Updated Pipeline Name";
                pipeline.ActualValue = "updated_pipeline";

                List<Maps> maps = new List<Maps>();
                Maps map = new Maps();
                map.Id = 1055806000000006801L;
                map.DisplayValue = "Updated Stage";
                map.ActualValue = "updated_stage";
                map.ForecastType = "pipeline";
                map.SequenceNumber = 1;
                map.ColourCode = "#28a745";

                ForecastCategory forecastCategory = new ForecastCategory();
                forecastCategory.Name = "Commit";
                forecastCategory.Id = 1055806000000006817L;
                map.ForecastCategory = forecastCategory;

                maps.Add(map);
                pipeline.Maps = maps;

                pipelines.Add(pipeline);
                bodyWrapper.Pipeline = pipelines;

                APIResponse<ActionHandler> response = pipelineOperations.UpdatePipelines(bodyWrapper);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                            List<ActionResponse> actionResponses = actionWrapper.Pipeline;

                            if (actionResponses != null)
                            {
                                foreach (ActionResponse actionResponse in actionResponses)
                                {
                                    if (actionResponse is SuccessResponse)
                                    {
                                        SuccessResponse successResponse = (SuccessResponse)actionResponse;
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

                                        Console.WriteLine("Message: " + successResponse.Message);
                                    }
                                    else if (actionResponse is APIException)
                                    {
                                        APIException exception = (APIException)actionResponse;
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
                        }
                        else if (actionHandler is APIException)
                        {
                            APIException exception = (APIException)actionHandler;
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

                UpdatePipelines_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}