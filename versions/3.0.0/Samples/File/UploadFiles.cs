using System;
using System.Collections.Generic;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using System.Reflection;
using Newtonsoft.Json;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Files;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;

namespace Samples.Files
{
    public class UploadFiles
    {
        public static void UploadFilesMethod(string filePath)
        {
            FilesOperations filesOperations = new FilesOperations();
            BodyWrapper bodyWrapper = new BodyWrapper();
            ParameterMap paramInstance = new ParameterMap();
            paramInstance.Add(FilesOperations.UploadFilesParam.TYPE, "inline");
            List<StreamWrapper> fileList = new List<StreamWrapper>();
            StreamWrapper streamWrapper = new StreamWrapper(filePath);
            fileList.Add(streamWrapper);
            bodyWrapper.File = fileList;
            try
            {
                APIResponse<ActionHandler> response = filesOperations.UploadFiles(bodyWrapper, paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                            List<ActionResponse> actionResponses = actionWrapper.Data;

                            foreach (ActionResponse actionResponse in actionResponses)
                            {
                                if (actionResponse is SuccessResponse)
                                {
                                    SuccessResponse successResponse = (SuccessResponse)actionResponse;
                                    Console.WriteLine("Status: " + successResponse.Status.Value);
                                    Console.WriteLine("Code: " + successResponse.Code.Value);
                                    Console.WriteLine("Details: ");

                                    foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                    {
                                        Console.WriteLine(entry.Key + ": " + entry.Value);
                                    }

                                    Console.WriteLine("Message: " + successResponse.Message);
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
                        Console.WriteLine("Values : ");

                        foreach (PropertyInfo prop in props)
                        {
                            if (prop.GetIndexParameters().Length == 0)
                            {
                                Console.WriteLine("{0} : {1}", prop.Name, prop.GetValue(responseObject));
                            }
                            else
                            {
                                Console.WriteLine("{0} : {1}", prop.Name, "Indexed Property");
                            }
                        }
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
                UploadFilesMethod("./image.png");
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}