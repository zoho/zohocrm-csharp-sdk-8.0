using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.BulkWrite.APIException;
using BulkWriteOperations = Com.Zoho.Crm.API.BulkWrite.BulkWriteOperations;
using ResponseHandler = Com.Zoho.Crm.API.BulkWrite.ResponseHandler;
using FileBodyWrapper = Com.Zoho.Crm.API.BulkWrite.FileBodyWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.BulkWrite
{
    public class DownloadBulkWriteResult
    {
        public static void DownloadBulkWriteResult_1(string downloadUrl, string destinationFolder)
        {
            BulkWriteOperations bulkWriteOperations = new BulkWriteOperations();
            APIResponse<ResponseHandler> response = bulkWriteOperations.DownloadBulkWriteResult(downloadUrl);

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
                        Stream file = streamWrapper.Stream;
                        string fullFilePath = Path.Combine(destinationFolder, streamWrapper.Name);
                        using (FileStream outputFileStream = new FileStream(fullFilePath, FileMode.Create))
                        {
                            file.CopyTo(outputFileStream);
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
                Environment environment = INDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                string downloadUrl = "https://download-accl.zoho.com/v2/aaaaaa/bulk-write/4402480774074/4402480774074.zip";
                string destinationFolder = "./downloads";
                DownloadBulkWriteResult_1(downloadUrl, destinationFolder);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}