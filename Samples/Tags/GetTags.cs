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
    public class GetTags
    {
        public static void GetTags_1()
        {
            try
            {
                TagsOperations tagsOperations = new TagsOperations();

                ParameterMap paramInstance = new ParameterMap();
                paramInstance.Add(TagsOperations.GetTagsParam.MODULE, "Leads");
                paramInstance.Add(TagsOperations.GetTagsParam.MY_TAGS, "false");

                APIResponse<ResponseHandler> response = tagsOperations.GetTags(paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<Tag> tags = responseWrapper.Tags;

                            Console.WriteLine($"Retrieved {tags?.Count ?? 0} tags:");

                            if (tags != null && tags.Count > 0)
                            {
                                foreach (Tag tag in tags)
                                {
                                    Console.WriteLine("\n--- Tag Details ---");

                                    Console.WriteLine("Tag ID: " + tag.Id);
                                    Console.WriteLine("Tag Name: " + tag.Name);

                                    if (tag.ColorCode != null)
                                    {
                                        Console.WriteLine("Color Code: " + tag.ColorCode);
                                    }

                                    if (tag.CreatedBy != null)
                                    {
                                        Console.WriteLine("Created By: " + tag.CreatedBy.Name);
                                    }

                                    if (tag.ModifiedBy != null)
                                    {
                                        Console.WriteLine("Modified By: " + tag.ModifiedBy.Name);
                                    }

                                    if (tag.CreatedTime != null)
                                    {
                                        Console.WriteLine("Created Time: " + tag.CreatedTime);
                                    }

                                    if (tag.ModifiedTime != null)
                                    {
                                        Console.WriteLine("Modified Time: " + tag.ModifiedTime);
                                    }

                                    Console.WriteLine("---");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No tags found");
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

                GetTags_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}