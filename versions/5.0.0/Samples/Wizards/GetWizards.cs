using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Wizards;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using System.Reflection;

namespace Samples.Wizards
{
    public class GetWizards
    {
        public static void GetWizards_1()
        {
            try
            {
                WizardsOperations wizardsOperations = new WizardsOperations();

                // Call API
                APIResponse<ResponseHandler> response = wizardsOperations.GetWizards();

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<Wizard> wizards = responseWrapper.Wizards;

                            if (wizards != null)
                            {
                                foreach (Wizard wizard in wizards)
                                {
                                    Console.WriteLine("Wizard ID: " + wizard.Id);
                                    Console.WriteLine("Wizard Name: " + wizard.Name);
                                    Console.WriteLine("Wizard CreatedTime: " + wizard.CreatedTime);
                                    Console.WriteLine("Wizard ModifiedTime: " + wizard.ModifiedTime);
                                    Console.WriteLine("Wizard Module: " + wizard.Module);
                                    Console.WriteLine("Wizard Active: " + wizard.Active);

                                    if (wizard.CreatedBy != null)
                                    {
                                        Console.WriteLine("Wizard CreatedBy ID: " + wizard.CreatedBy.Id);
                                        Console.WriteLine("Wizard CreatedBy Name: " + wizard.CreatedBy.Name);
                                    }

                                    if (wizard.ModifiedBy != null)
                                    {
                                        Console.WriteLine("Wizard ModifiedBy ID: " + wizard.ModifiedBy.Id);
                                        Console.WriteLine("Wizard ModifiedBy Name: " + wizard.ModifiedBy.Name);
                                    }

                                    List<Container> containers = wizard.Containers;
                                    if (containers != null)
                                    {
                                        foreach (Container container in containers)
                                        {
                                            Console.WriteLine("Container ID: " + container.Id);

                                            if (container.Layout != null)
                                            {
                                                Console.WriteLine("Container Layout ID: " + container.Layout.Id);
                                                Console.WriteLine("Container Layout Name: " + container.Layout.Name);
                                            }
                                        }
                                    }

                                    Console.WriteLine("-----------------------------");
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

                GetWizards_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}