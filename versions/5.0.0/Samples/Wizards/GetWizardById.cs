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
    public class GetWizardById
    {
        public static void GetWizardById_1(string wizardId, string layoutId)
        {
            try
            {
                WizardsOperations wizardsOperations = new WizardsOperations();

                ParameterMap paramInstance = new ParameterMap();
                paramInstance.Add(WizardsOperations.GetWizardByIDParam.LAYOUT_ID, layoutId);

                // Call API
                APIResponse<ResponseHandler> response = wizardsOperations.GetWizardById(wizardId, paramInstance);

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

                                            if (container.ChartData != null)
                                            {
                                                Console.WriteLine("Container has ChartData");

                                                if (container.ChartData.Nodes != null)
                                                {
                                                    Console.WriteLine("Chart Nodes count: " + container.ChartData.Nodes.Count);
                                                }

                                                if (container.ChartData.Connections != null)
                                                {
                                                    Console.WriteLine("Chart Connections count: " + container.ChartData.Connections.Count);
                                                }
                                            }

                                            List<Screen> screens = container.Screens;
                                            if (screens != null)
                                            {
                                                foreach (Screen screen in screens)
                                                {
                                                    Console.WriteLine("Screen ID: " + screen.Id);
                                                    Console.WriteLine("Screen DisplayLabel: " + screen.DisplayLabel);
                                                    Console.WriteLine("Screen APIName: " + screen.APIName);
                                                    Console.WriteLine("Screen ReferenceId: " + screen.ReferenceId);

                                                    List<Segment> segments = screen.Segments;
                                                    if (segments != null)
                                                    {
                                                        foreach (Segment segment in segments)
                                                        {
                                                            Console.WriteLine("Segment ID: " + segment.Id);
                                                            Console.WriteLine("Segment DisplayLabel: " + segment.DisplayLabel);
                                                            Console.WriteLine("Segment Type: " + segment.Type);
                                                            Console.WriteLine("Segment SequenceNumber: " + segment.SequenceNumber);
                                                            Console.WriteLine("Segment ColumnCount: " + segment.ColumnCount);
                                                        }
                                                    }
                                                }
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

                string wizardId = "347706117751001";
                string layoutId = "347706111383001";
                GetWizardById_1(wizardId, layoutId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}