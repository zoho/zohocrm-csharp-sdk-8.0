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
    public class GetOrgTax
    {
        public static void GetTax_1(long taxId)
        {
            try
            {
                TaxesOperations taxesOperations = new TaxesOperations();
                APIResponse<ResponseHandler> response = taxesOperations.GetTax(taxId);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.StatusCode == 204)
                    {
                        Console.WriteLine("No Content - Tax not found");
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
                                Console.WriteLine("No tax found with the specified ID: " + taxId);
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

                // Get specific tax by ID - Replace with actual tax ID
                long taxId = 554023000000567023L;
                GetTax_1(taxId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}