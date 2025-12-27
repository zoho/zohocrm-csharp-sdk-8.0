using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Taxes;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.Taxes_1
{
    public class UpdateOrgTaxes
    {
        public static void UpdateTaxes_1()
        {
            try
            {
                TaxesOperations taxesOperations = new TaxesOperations();

                // Create request body
                BodyWrapper request = new BodyWrapper();
                OrgTax orgTax = new OrgTax();
                List<Tax> taxList = new List<Tax>();

                // Create first tax to update
                Tax tax1 = new Tax();
                tax1.Id = 1055806000013694003L; // Replace with actual tax ID
                tax1.Name = "GST";
                tax1.DisplayLabel = "Goods and Services Tax";
                tax1.Value = 18.0; // 18% GST
                taxList.Add(tax1);
                orgTax.Taxes = taxList;

                Preference preference = new Preference();
                preference.AutoPopulateTax = false;
                preference.ModifyTaxRates = false;
                orgTax.Preference = preference;
                request.OrgTaxes = orgTax;

                APIResponse<ActionHandler> response = taxesOperations.UpdateTaxes(request);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                            ActionResponse actionResponse = actionWrapper.OrgTaxes;

                            if (actionResponse is SuccessResponse)
                            {
                                SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                Console.WriteLine("\n--- Tax Update Success ---");
                                Console.WriteLine("Status: " + successResponse.Status.Value);
                                Console.WriteLine("Code: " + successResponse.Code.Value);
                                Console.WriteLine("Message: " + successResponse.Message.Value);

                                if (successResponse.Details != null)
                                {
                                    Console.WriteLine("Details:");
                                    foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                    {
                                        Console.WriteLine("  " + entry.Key + ": " + entry.Value);
                                    }
                                }
                                Console.WriteLine("---");
                            }
                            else if (actionResponse is APIException)
                            {
                                APIException exception = (APIException)actionResponse;

                                Console.WriteLine("\n--- Tax Update Failed ---");
                                Console.WriteLine("Status: " + exception.Status.Value);
                                Console.WriteLine("Code: " + exception.Code.Value);

                                if (exception.Details != null)
                                {
                                    Console.WriteLine("Details:");
                                    foreach (KeyValuePair<string, object> entry in exception.Details)
                                    {
                                        Console.WriteLine("  " + entry.Key + ": " + entry.Value);
                                    }
                                }

                                Console.WriteLine("Message: " + exception.Message.Value);
                                Console.WriteLine("---");
                            }
                        }
                        else if (actionHandler is APIException)
                        {
                            APIException exception = (APIException)actionHandler;

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

                // Update taxes
                UpdateTaxes_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}