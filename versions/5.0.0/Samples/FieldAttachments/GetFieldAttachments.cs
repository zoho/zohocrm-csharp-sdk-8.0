using System;
using System.Collections.Generic;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using System.Reflection;
using Newtonsoft.Json;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.FieldAttachments;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using System.IO;

namespace Samples.FieldAttachments
{
    public class GetFieldAttachments
    {
        public static void GetFieldAttachmentsMethod(string moduleAPIName, long recordId, long fieldsAttachmentId, string destinationFolderPath)
        {
            FieldAttachmentsOperations fieldAttachmentsOperations = new FieldAttachmentsOperations(moduleAPIName, recordId, fieldsAttachmentId);

            try
            {
                APIResponse<ResponseHandler> response = fieldAttachmentsOperations.GetFieldAttachments();

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

                        if (responseHandler is FileBodyWrapper)
                        {
                            FileBodyWrapper fileBodyWrapper = (FileBodyWrapper)responseHandler;
                            StreamWrapper streamWrapper = fileBodyWrapper.File;

                            if (streamWrapper != null)
                            {
                                string fileName = streamWrapper.Name;

                                string fullFilePath = Path.Combine(destinationFolderPath, fileName);

                                if (!Directory.Exists(destinationFolderPath))
                                {
                                    Directory.CreateDirectory(destinationFolderPath);
                                }

                                using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                                {
                                    streamWrapper.Stream.CopyTo(outputFileStream);
                                }
                                streamWrapper.Stream.Close();
                            }
                        }
                        else if (responseHandler is APIException)
                        {
                            APIException exception = (APIException)responseHandler;
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

                // Replace with actual values
                string moduleAPIName = "Leads"; // Example module
                long recordId = 34770610001L; // Example record ID
                long fieldsAttachmentId = 34770610002L; // Example field attachment ID

                GetFieldAttachmentsMethod(moduleAPIName, recordId, fieldsAttachmentId, "./");
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}