using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using APIException = Com.Zoho.Crm.API.Currencies.APIException;
using CurrenciesOperations = Com.Zoho.Crm.API.Currencies.CurrenciesOperations;
using ActionHandler = Com.Zoho.Crm.API.Currencies.ActionHandler;
using BaseCurrencyWrapper = Com.Zoho.Crm.API.Currencies.BaseCurrencyWrapper;
using BaseCurrencyActionWrapper = Com.Zoho.Crm.API.Currencies.BaseCurrencyActionWrapper;
using BaseCurrencyActionResponse = Com.Zoho.Crm.API.Currencies.BaseCurrencyActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.Currencies.SuccessResponse;
using BaseCurrency = Com.Zoho.Crm.API.Currencies.BaseCurrency;
using Format = Com.Zoho.Crm.API.Currencies.Format;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Currencies
{
    public class UpdateBaseCurrency
    {
        public static void UpdateBaseCurrency_1()
        {
            CurrenciesOperations currenciesOperations = new CurrenciesOperations();
            BaseCurrencyWrapper baseCurrencyWrapper = new BaseCurrencyWrapper();

            BaseCurrency baseCurrency = new BaseCurrency();
            baseCurrency.PrefixSymbol = true;
            baseCurrency.Name = "United States Dollar"; // Updated name
            baseCurrency.IsoCode = "USD";
            baseCurrency.Symbol = "$";
            baseCurrency.ExchangeRate = "1.0";

            Format format = new Format();
            format.DecimalSeparator = new Choice<string>("Period");
            format.ThousandSeparator = new Choice<string>("Comma");
            format.DecimalPlaces = new Choice<string>("2");
            baseCurrency.Format = format;

            baseCurrencyWrapper.BaseCurrency = baseCurrency;

            APIResponse<ActionHandler> response = currenciesOperations.UpdateBaseCurrency(baseCurrencyWrapper);

            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionHandler actionHandler = response.Object;
                    if (actionHandler is BaseCurrencyActionWrapper)
                    {
                        BaseCurrencyActionWrapper actionWrapper = (BaseCurrencyActionWrapper)actionHandler;
                        BaseCurrencyActionResponse actionResponse = actionWrapper.BaseCurrency;
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
                UpdateBaseCurrency_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}