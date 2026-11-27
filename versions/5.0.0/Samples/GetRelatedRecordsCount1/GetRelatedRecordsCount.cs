using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.GetRelatedRecordsCount;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using System.Reflection;
using Com.Zoho.Crm.API.Dc;

namespace Samples.GetRelatedRecordsCount1
{
    public class GetRelatedRecordsCount
    {
        public static void GetRelatedRecordsCount_1(long recordId, String moduleAPIName)
        {
            GetRelatedRecordsCountOperations getRelatedRecordsCountOperations = new GetRelatedRecordsCountOperations(recordId, moduleAPIName);
            BodyWrapper request = new BodyWrapper();
            List<GetRelatedRecordCount> getRelatedRecordsCount = new List<GetRelatedRecordCount>();
            GetRelatedRecordCount getRelatedRecordsCount1 = new GetRelatedRecordCount();
            RelatedList relatedList = new RelatedList();
            relatedList.APIName = "Notes";
            relatedList.Id = 34770602197;
            getRelatedRecordsCount1.RelatedList = relatedList;
            getRelatedRecordsCount.Add(getRelatedRecordsCount1);
            request.RelatedRecordsCount = getRelatedRecordsCount;
            APIResponse<ActionHandler> response = getRelatedRecordsCountOperations.GetRelatedRecordsCount(request);
            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionHandler actionHandler = response.Object;
                    if (actionHandler is ActionWrapper)
                    {
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                        List<ActionResponse> actionResponses = actionWrapper.RelatedRecordsCount;
                        foreach (ActionResponse actionResponse in actionResponses)
                        {
                            if (actionResponse is SuccessResponse)
                            {
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;
                                Console.WriteLine("Count: " + successResponse.Count);
                                relatedList = successResponse.RelatedList;
                                if (relatedList != null)
                                {
                                    Console.WriteLine("RelatedList APIName: " + relatedList.APIName);
                                    Console.WriteLine("RelatedList Id: " + relatedList.Id);
                                }
                            }
                            else if (actionResponse is APIException)
                            {
                                APIException exception = (APIException)actionResponse;
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
                    }
                    else if (actionHandler is APIException)
                    {
                        APIException exception = (APIException)actionHandler;
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
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                long recordId = 34770002l;
                String moduleAPIName = "Leads";
                GetRelatedRecordsCount_1(recordId, moduleAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }

}
