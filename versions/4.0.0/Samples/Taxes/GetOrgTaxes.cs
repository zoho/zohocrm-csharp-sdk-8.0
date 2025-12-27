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
    public class GetOrgTaxes
    {
        public static void GetTaxes_1()
        {
            try
            {
                TaxesOperations taxesOperations = new TaxesOperations();
                APIResponse<ResponseHandler> response = taxesOperations.GetTaxes();

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.StatusCode == 204)
                    {
                        Console.WriteLine("No Content - No taxes found");
                        return;
                    }

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            OrgTax orgTax = responseWrapper.OrgTaxes;
                            List<Tax> taxes = orgTax.Taxes;
                            if (taxes != null && taxes.Count > 0)
                            {
                                foreach (Tax tax in taxes)
                                {
                                    Console.WriteLine("\n--- Tax Details ---");
                                    Console.WriteLine("Tax ID: " + tax.Id);
                                    Console.WriteLine("Tax Name: " + tax.Name);
                                    Console.WriteLine("Display Label: " + tax.DisplayLabel);

                                    if (tax.Value != null)
                                    {
                                        Console.WriteLine("Tax Value: " + tax.Value);
                                    }

                                    Preference preference = orgTax.Preference;
                                    if (preference != null)
                                    {
                                        Console.WriteLine("Preference AutoPopulateTax: " + preference.AutoPopulateTax);
                                        if (preference.ModifyTaxRates != null)
                                        {
                                            Console.WriteLine("Preference ModifyTaxRates: " + preference.ModifyTaxRates);
                                        }
                                    }

                                    Console.WriteLine("---");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No taxes found in the organization");
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

                GetTaxes_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}