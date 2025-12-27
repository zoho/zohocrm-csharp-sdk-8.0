using System;
using System.Collections.Generic;
using System.IO;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Org;
using Com.Zoho.Crm.API.Util;
using ResponseHandler = Com.Zoho.Crm.API.Org.ResponseHandler;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Organization
{
    public class GetOrgPhoto
    {
        public static void GetOrgPhoto_1()
        {
            try
            {
                OrgOperations orgOperations = new OrgOperations();
                APIResponse<ResponseHandler> response = orgOperations.GetOrgPhoto();

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.StatusCode == 204)
                    {
                        Console.WriteLine("No Content - Organization photo not found");
                        return;
                    }

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is FileBodyWrapper)
                        {
                            FileBodyWrapper fileBodyWrapper = (FileBodyWrapper)responseHandler;
                            StreamWrapper streamWrapper = fileBodyWrapper.File;

                            // Create a file to save the photo
                            string filePath = Path.Combine(Directory.GetCurrentDirectory(), streamWrapper.Name);

                            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                            {
                                streamWrapper.Stream.CopyTo(fileStream);
                            }

                            Console.WriteLine("Organization photo downloaded successfully");
                            Console.WriteLine("File saved at: " + filePath);
                            Console.WriteLine("File size: " + new FileInfo(filePath).Length + " bytes");
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }

        public static void Call()
        {
            try
            {
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder()
                    .ClientId("Client_Id")
                    .ClientSecret("Client_Secret")
                    .RefreshToken("Refresh_Token")
                    .Build();

                new Initializer.Builder()
                    .Environment(environment)
                    .Token(token)
                    .Initialize();

                GetOrgPhoto_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}