using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.Currencies.APIException;
using CurrenciesOperations = Com.Zoho.Crm.API.Currencies.CurrenciesOperations;
using ActionHandler = Com.Zoho.Crm.API.Currencies.ActionHandler;
using BodyWrapper = Com.Zoho.Crm.API.Currencies.BodyWrapper;
using ActionWrapper = Com.Zoho.Crm.API.Currencies.ActionWrapper;
using ActionResponse = Com.Zoho.Crm.API.Currencies.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.Currencies.SuccessResponse;
using Currency = Com.Zoho.Crm.API.Currencies.Currency;
using Format = Com.Zoho.Crm.API.Currencies.Format;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Currencies
{
    public class AddCurrencies
    {
        public static void AddCurrencies_1()
        {
            CurrenciesOperations currenciesOperations = new CurrenciesOperations();
            BodyWrapper bodyWrapper = new BodyWrapper();
            List<Currency> currencyList = new List<Currency>();

            Currency currency1 = new Currency();
            currency1.Name = "Angolan Kwanza - AOA";
            currency1.IsoCode = "AOA";
            currency1.Symbol = "Kz";
            currency1.ExchangeRate = "20.00";
            currency1.IsActive = true;

            Format format1 = new Format();
            format1.DecimalSeparator = new Choice<string>("Period");
            format1.ThousandSeparator = new Choice<string>("Comma");
            format1.DecimalPlaces = new Choice<string>("2");
            currency1.Format = format1;

            Currency currency2 = new Currency();
            currency2.Name = "British Pound";
            currency2.IsoCode = "GBP";
            currency2.Symbol = "£";
            currency2.ExchangeRate = "0.75";
            currency2.IsActive = true;

            Format format2 = new Format();
            format2.DecimalSeparator = new Choice<string>("Period");
            format2.ThousandSeparator = new Choice<string>("Comma");
            format2.DecimalPlaces = new Choice<string>("2");
            currency2.Format = format2;

            currencyList.Add(currency1);
            currencyList.Add(currency2);
            bodyWrapper.Currencies = currencyList;

            APIResponse<ActionHandler> response = currenciesOperations.AddCurrencies(bodyWrapper);

            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionHandler actionHandler = response.Object;
                    if (actionHandler is ActionWrapper)
                    {
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                        List<ActionResponse> actionResponses = actionWrapper.Currencies;

                        foreach (ActionResponse actionResponse in actionResponses)
                        {
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
                                Console.WriteLine("Message: " + exception.Message);
                            }
                        }
                    }
                    else if (actionHandler is APIException)
                    {
                        APIException exception = (APIException)actionHandler;
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
                AddCurrencies_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}