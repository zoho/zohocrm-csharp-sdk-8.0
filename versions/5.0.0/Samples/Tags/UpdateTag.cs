using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Tags;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.Tags
{
    public class UpdateTag
    {
        public static void UpdateTag_1(long tagId)
        {
            try
            {
                TagsOperations tagsOperations = new TagsOperations();

                BodyWrapper request = new BodyWrapper();

                List<Tag> tagsList = new List<Tag>();

                Tag tag = new Tag();
                //tag.Name = "Updated Tag Name123";
                tag.ColorCode = "#0000FF"; // Blue color

                tagsList.Add(tag);

                request.Tags = tagsList;

                ParameterMap paramInstance = new ParameterMap();
                paramInstance.Add(TagsOperations.UpdateTagParam.MODULE, "Leads");

                APIResponse<ActionHandler> response = tagsOperations.UpdateTag(tagId, request, paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                            List<ActionResponse> actionResponses = actionWrapper.Tags;

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

                                    Console.WriteLine("Message: " + successResponse.Message.Value);
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

                long tagId = 554023000000567023L; // Replace with actual tag ID
                UpdateTag_1(tagId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}