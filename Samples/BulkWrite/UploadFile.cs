using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.BulkWrite.APIException;
using BulkWriteOperations = Com.Zoho.Crm.API.BulkWrite.BulkWriteOperations;
using FileBodyWrapper = Com.Zoho.Crm.API.BulkWrite.FileBodyWrapper;
using ActionResponse = Com.Zoho.Crm.API.BulkWrite.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.BulkWrite.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.BulkWrite
{
    public class UploadFile
    {
        public static void UploadFile_1(string filePath)
        {
            BulkWriteOperations bulkWriteOperations = new BulkWriteOperations();
            FileBodyWrapper fileBodyWrapper = new FileBodyWrapper();

            StreamWrapper streamWrapper = new StreamWrapper(filePath);
            fileBodyWrapper.File = streamWrapper;

            HeaderMap headerInstance = new HeaderMap();
            headerInstance.Add(BulkWriteOperations.UploadFileHeader.FEATURE, "bulk-write");
            headerInstance.Add(BulkWriteOperations.UploadFileHeader.X_CRM_ORG, "your_org_id");

            APIResponse<ActionResponse> response = bulkWriteOperations.UploadFile(fileBodyWrapper, headerInstance);

            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionResponse actionResponse = response.Object;
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
                string filePath = "./bulk_write_data.csv";
                UploadFile_1(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}