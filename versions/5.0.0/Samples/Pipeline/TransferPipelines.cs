using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Pipeline;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Pipeline.APIException;
using TransferPipelineActionHandler = Com.Zoho.Crm.API.Pipeline.TransferPipelineActionHandler;
using TransferPipelineActionResponse = Com.Zoho.Crm.API.Pipeline.TransferPipelineActionResponse;
using TransferPipelineActionWrapper = Com.Zoho.Crm.API.Pipeline.TransferPipelineActionWrapper;
using TransferPipelineSuccessResponse = Com.Zoho.Crm.API.Pipeline.TransferPipelineSuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Pipeline
{
    public class TransferPipelines
    {
        public static void TransferPipelines_1()
        {
            try
            {
                long layoutId = 1055806000000091023l;
                PipelineOperations pipelineOperations = new PipelineOperations(layoutId);
                TransferPipelineWrapper request = new TransferPipelineWrapper();
                List<TransferPipeline> transferPipelines = new List<TransferPipeline>();

                TransferPipeline transferPipeline = new TransferPipeline();

                TPipeline pipeline = new TPipeline();
                pipeline.From = 1055806000000006800L;
                pipeline.To = 1055806000000006802L;
                transferPipeline.Pipeline = pipeline;

                List<Stages> stages = new List<Stages>();
                Stages stage = new Stages();
                stage.From = 1055806000000006801L;
                stage.To = 1055806000000006803L;
                stages.Add(stage);

                transferPipeline.Stages = stages;
                transferPipelines.Add(transferPipeline);
                request.TransferPipeline = transferPipelines;

                APIResponse<TransferPipelineActionHandler> response = pipelineOperations.TransferPipelines(request);

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
                        TransferPipelineActionHandler transferActionHandler = response.Object;

                        if (transferActionHandler is TransferPipelineActionWrapper)
                        {
                            TransferPipelineActionWrapper transferActionWrapper = (TransferPipelineActionWrapper)transferActionHandler;
                            List<TransferPipelineActionResponse> transferPipelineActionResponses = transferActionWrapper.TransferPipeline;

                            if (transferPipelineActionResponses != null)
                            {
                                foreach (TransferPipelineActionResponse transferActionResponse in transferPipelineActionResponses)
                                {
                                    if (transferActionResponse is TransferPipelineSuccessResponse)
                                    {
                                        TransferPipelineSuccessResponse successResponse = (TransferPipelineSuccessResponse)transferActionResponse;
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
                                    else if (transferActionResponse is APIException)
                                    {
                                        APIException exception = (APIException)transferActionResponse;
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
                        else if (transferActionHandler is APIException)
                        {
                            APIException exception = (APIException)transferActionHandler;
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

                TransferPipelines_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}