using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Org;
using Com.Zoho.Crm.API.Util;
using ActionHandler = Com.Zoho.Crm.API.Org.ActionHandler;
using APIException = Com.Zoho.Crm.API.Org.APIException;
using SuccessResponse = Com.Zoho.Crm.API.Org.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Organization
{
    public class DeleteOrganizationPhoto
    {
        public static void DeleteOrganizationPhoto_1()
        {
            try
            {
                OrgOperations orgOperations = new OrgOperations();
                APIResponse<ActionHandler> response = orgOperations.DeleteOrganizationPhoto();

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionResponse = response.Object;

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

                DeleteOrganizationPhoto_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}