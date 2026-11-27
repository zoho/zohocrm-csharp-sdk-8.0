using System;
using System.IO;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using System.Reflection;
using Newtonsoft.Json;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Files;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using System.Collections.Generic;

namespace Samples.Files
{
    public class GetFile
    {
        /// <summary>
        /// This method is used to download a file from Zoho CRM
        /// </summary>
        /// <param name="fileId">The ID of the file to download</param>
        /// <param name="destinationFolderPath">The destination folder path to save the file</param>
        public static void GetFileMethod(string fileId, string destinationFolderPath)
        {
            FilesOperations filesOperations = new FilesOperations();
            ParameterMap paramInstance = new ParameterMap();

            paramInstance.Add(FilesOperations.GetFileParam.ID, fileId);

            try
            {
                APIResponse<ResponseHandler> response = filesOperations.GetFile(paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is FileBodyWrapper)
                        {
                            FileBodyWrapper fileBodyWrapper = (FileBodyWrapper)responseHandler;
                            StreamWrapper streamWrapper = fileBodyWrapper.File;

                            string fileName = streamWrapper.Name;
                            if (string.IsNullOrEmpty(fileName))
                            {
                                fileName = "downloaded_file_" + fileId;
                            }

                            string fullFilePath = Path.Combine(destinationFolderPath, fileName);

                            if (!Directory.Exists(destinationFolderPath))
                            {
                                Directory.CreateDirectory(destinationFolderPath);
                            }

                            using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                            {
                                streamWrapper.Stream.CopyTo(outputFileStream);
                            }

                            Console.WriteLine("File downloaded successfully!");
                            Console.WriteLine("File Name: " + fileName);
                            Console.WriteLine("File Path: " + fullFilePath);
                            Console.WriteLine("File Size: " + new FileInfo(fullFilePath).Length + " bytes");

                            streamWrapper.Stream.Close();
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
                Console.WriteLine("Error downloading file: " + e.Message);
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
                string fileId = "";
                string destinationFolderPat = "./";
                GetFileMethod(fileId, destinationFolderPat);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}