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
    public class AddTagsToMultipleRecords
    {
        public static void AddTagsToMultipleRecords_1(string moduleAPIName)
        {
            try
            {
                TagsOperations tagsOperations = new TagsOperations();

                NewTagRequestWrapper request = new NewTagRequestWrapper();

                List<Tag> tagsList = new List<Tag>();

                // Create first tag to add
                Tag tag1 = new Tag();
                tag1.Name = "Bulk Important";
                tagsList.Add(tag1);

                // Create second tag to add
                Tag tag2 = new Tag();
                tag2.Name = "Mass Update";
                tagsList.Add(tag2);

                request.Tags = tagsList;

                // Set record IDs to add tags to
                List<long?> recordIds = new List<long?>
                {
                    554023000000567023L, // Replace with actual record IDs
                    1055806000028562118L,
                    554023000000567025L
                };

                request.Ids = recordIds;

                ParameterMap paramInstance = new ParameterMap();
                paramInstance.Add(TagsOperations.AddTagsToMultipleRecordsParam.OVER_WRITE, "false");

                APIResponse<RecordActionHandler> response = tagsOperations.AddTagsToMultipleRecords(moduleAPIName, request, paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        RecordActionHandler recordActionHandler = response.Object;

                        if (recordActionHandler is RecordActionWrapper)
                        {
                            RecordActionWrapper recordActionWrapper = (RecordActionWrapper)recordActionHandler;

                            List<RecordActionResponse> recordActionResponses = recordActionWrapper.Data;

                            foreach (RecordActionResponse recordActionResponse in recordActionResponses)
                            {
                                if (recordActionResponse is RecordSuccessResponse)
                                {
                                    RecordSuccessResponse recordSuccessResponse = (RecordSuccessResponse)recordActionResponse;

                                    Console.WriteLine("Status: " + recordSuccessResponse.Status.Value);
                                    Console.WriteLine("Code: " + recordSuccessResponse.Code.Value);
                                    Console.WriteLine("Details: ");

                                    if (recordSuccessResponse.Details != null)
                                    {
                                        foreach (KeyValuePair<string, object> entry in recordSuccessResponse.Details)
                                        {
                                            Console.WriteLine(entry.Key + ": " + entry.Value);
                                        }
                                    }

                                    Console.WriteLine("Message: " + recordSuccessResponse.Message);
                                }
                                else if (recordActionResponse is APIException)
                                {
                                    APIException exception = (APIException)recordActionResponse;

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
                        else if (recordActionHandler is APIException)
                        {
                            APIException exception = (APIException)recordActionHandler;

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

                string moduleAPIName = "Contacts"; // Replace with actual module API name

                AddTagsToMultipleRecords_1(moduleAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}