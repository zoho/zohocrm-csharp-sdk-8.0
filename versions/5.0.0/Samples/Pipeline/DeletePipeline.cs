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
using SuccessResponse = Com.Zoho.Crm.API.Pipeline.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Pipeline
{
    public class DeletePipeline
    {
        public static void DeletePipeline_1(long pipelineId)
        {
            try
            {
                long layoutId = 1055806000000091023l;
                PipelineOperations pipelineOperations = new PipelineOperations(layoutId);
                DPipelineWrapper request = new DPipelineWrapper();
                List<DPipeline> pipelineList = new List<DPipeline>();

                DPipeline dPipeline = new DPipeline();
                Delete delete = new Delete();
                delete.Permanent = true;
                dPipeline.Delete = delete;

                pipelineList.Add(dPipeline);
                request.Pipeline = pipelineList;

                APIResponse<ActionHandler> response = pipelineOperations.DeletePipeline(pipelineId, request);

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

                long pipelineId = 1055806000000006800L;
                DeletePipeline_1(pipelineId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}