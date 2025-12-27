using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.Currencies.APIException;
using CurrenciesOperations = Com.Zoho.Crm.API.Currencies.CurrenciesOperations;
using ResponseHandler = Com.Zoho.Crm.API.Currencies.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Currencies.ResponseWrapper;
using Currency = Com.Zoho.Crm.API.Currencies.Currency;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.Currencies
{
    public class GetCurrencies
    {
        public static void GetCurrencies_1()
        {
            CurrenciesOperations currenciesOperations = new CurrenciesOperations();
            APIResponse<ResponseHandler> response = currenciesOperations.GetCurrencies();

            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (new List<int>() { 204, 304 }.Contains(response.StatusCode))
                {
                    Console.WriteLine(response.StatusCode == 204 ? "No Content" : "Not Modified");
                    return;
                }
                if (response.IsExpected)
                {
                    ResponseHandler responseHandler = response.Object;
                    if (responseHandler is ResponseWrapper)
                    {
                        ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;
                        List<Currency> currencies = responseWrapper.Currencies;

                        foreach (Currency currency in currencies)
                        {
                            Console.WriteLine("Currency ID: " + currency.Id);
                            Console.WriteLine("Currency Name: " + currency.Name);
                            Console.WriteLine("Currency IsoCode: " + currency.IsoCode);
                            Console.WriteLine("Currency Symbol: " + currency.Symbol);
                            Console.WriteLine("Currency CreatedTime: " + currency.CreatedTime);
                            Console.WriteLine("Currency ModifiedTime: " + currency.ModifiedTime);
                            Console.WriteLine("Currency IsActive: " + currency.IsActive);
                            Console.WriteLine("Currency ExchangeRate: " + currency.ExchangeRate);
                            Console.WriteLine("Currency Format: ");

                            Com.Zoho.Crm.API.Currencies.Format format = currency.Format;
                            if (format != null)
                            {
                                Console.WriteLine("Currency Format DecimalSeparator: " + format.DecimalSeparator);
                                Console.WriteLine("Currency Format ThousandSeparator: " + format.ThousandSeparator);
                                Console.WriteLine("Currency Format DecimalPlaces: " + format.DecimalPlaces);
                            }

                            Com.Zoho.Crm.API.Users.MinifiedUser createdBy = currency.CreatedBy;
                            if (createdBy != null)
                            {
                                Console.WriteLine("Currency CreatedBy User-Name: " + createdBy.Name);
                                Console.WriteLine("Currency CreatedBy User-ID: " + createdBy.Id);
                                Console.WriteLine("Currency CreatedBy User-Email: " + createdBy.Email);
                            }

                            Com.Zoho.Crm.API.Users.MinifiedUser modifiedBy = currency.ModifiedBy;
                            if (modifiedBy != null)
                            {
                                Console.WriteLine("Currency ModifiedBy User-Name: " + modifiedBy.Name);
                                Console.WriteLine("Currency ModifiedBy User-ID: " + modifiedBy.Id);
                                Console.WriteLine("Currency ModifiedBy User-Email: " + modifiedBy.Email);
                            }
                        }
                    }
                    else if (responseHandler is APIException)
                    {
                        APIException exception = (APIException)responseHandler;
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
                GetCurrencies_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}