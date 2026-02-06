using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.RelatedLists;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.RelatedLists
{
    public class GetRelatedList
    {
        public static void GetRelatedList_1(long? layoutId, long relatedListId, string moduleAPIName)
        {
            try
            {
                RelatedListsOperations relatedListsOperations = new RelatedListsOperations(layoutId);

                ParameterMap paramInstance = new ParameterMap();
                paramInstance.Add(RelatedListsOperations.GetRelatedListParam.MODULE, moduleAPIName);

                // Call API
                APIResponse<ResponseHandler> response = relatedListsOperations.GetRelatedList(relatedListId, paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<RelatedList> relatedLists = responseWrapper.RelatedLists;

                            if (relatedLists != null && relatedLists.Count > 0)
                            {
                                RelatedList relatedList = relatedLists[0];

                                Console.WriteLine("RelatedList ID: " + relatedList.Id);
                                Console.WriteLine("RelatedList SequenceNumber: " + relatedList.SequenceNumber);
                                Console.WriteLine("RelatedList DisplayLabel: " + relatedList.DisplayLabel);
                                Console.WriteLine("RelatedList APIName: " + relatedList.APIName);
                                Console.WriteLine("RelatedList Module: " + relatedList.Module);
                                Console.WriteLine("RelatedList Name: " + relatedList.Name);
                                Console.WriteLine("RelatedList Action: " + relatedList.Action);
                                Console.WriteLine("RelatedList Href: " + relatedList.Href);
                                Console.WriteLine("RelatedList Type: " + relatedList.Type);
                                Console.WriteLine("RelatedList Visible: " + relatedList.Visible);

                                if (relatedList.Connectedmodule != null)
                                {
                                    Console.WriteLine("RelatedList Connectedmodule: " + relatedList.Connectedmodule);
                                }

                                if (relatedList.Linkingmodule != null)
                                {
                                    Console.WriteLine("RelatedList Linkingmodule: " + relatedList.Linkingmodule);
                                }

                                // Display field details if available
                                if (relatedList.Fields != null && relatedList.Fields.Count > 0)
                                {
                                    Console.WriteLine("Related List Fields:");
                                    foreach (var field in relatedList.Fields)
                                    {
                                        Console.WriteLine("  Field ID: " + field.Id);
                                        Console.WriteLine("  Field APIName: " + field.APIName);
                                        Console.WriteLine("  -------------------------");
                                    }
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

                            Console.WriteLine("Message: " + exception.Message);
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

                long layoutId = 34770615177002L;
                long relatedListId = 34770615001L;
                string moduleAPIName = "Leads";
                GetRelatedList_1(layoutId, relatedListId, moduleAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}