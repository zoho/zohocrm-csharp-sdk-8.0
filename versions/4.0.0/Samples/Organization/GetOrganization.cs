using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Org;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Org.APIException;
using ResponseHandler = Com.Zoho.Crm.API.Org.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Org.ResponseWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Organization
{
    public class GetOrganization
    {
        public static void GetOrganization_1()
        {
            try
            {
                OrgOperations orgOperations = new OrgOperations();
                APIResponse<ResponseHandler> response = orgOperations.GetOrganization();

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
                            List<Com.Zoho.Crm.API.Org.Org> organizations = responseWrapper.Org;

                            if (organizations != null)
                            {
                                foreach (Com.Zoho.Crm.API.Org.Org organization in organizations)
                                {
                                    Console.WriteLine("Organization Country: " + organization.Country);
                                    Console.WriteLine("Organization PhotoId: " + organization.PhotoId);
                                    Console.WriteLine("Organization City: " + organization.City);
                                    Console.WriteLine("Organization Description: " + organization.Description);
                                    Console.WriteLine("Organization McStatus: " + organization.McStatus);
                                    Console.WriteLine("Organization GappsEnabled: " + organization.GappsEnabled);
                                    Console.WriteLine("Organization DomainName: " + organization.DomainName);
                                    Console.WriteLine("Organization TranslationEnabled: " + organization.TranslationEnabled);
                                    Console.WriteLine("Organization Street: " + organization.Street);
                                    Console.WriteLine("Organization Alias: " + organization.Alias);
                                    Console.WriteLine("Organization Currency: " + organization.Currency);
                                    Console.WriteLine("Organization Id: " + organization.Id);
                                    Console.WriteLine("Organization State: " + organization.State);
                                    Console.WriteLine("Organization Fax: " + organization.Fax);
                                    Console.WriteLine("Organization EmployeeCount: " + organization.EmployeeCount);
                                    Console.WriteLine("Organization Zip: " + organization.Zip);
                                    Console.WriteLine("Organization Website: " + organization.Website);
                                    Console.WriteLine("Organization CurrencySymbol: " + organization.CurrencySymbol);
                                    Console.WriteLine("Organization Mobile: " + organization.Mobile);
                                    Console.WriteLine("Organization CurrencyLocale: " + organization.CurrencyLocale);
                                    Console.WriteLine("Organization PrimaryZuid: " + organization.PrimaryZuid);
                                    Console.WriteLine("Organization ZiaPortalId: " + organization.ZiaPortalId);
                                    Console.WriteLine("Organization TimeZone: " + organization.TimeZone);
                                    Console.WriteLine("Organization Zgid: " + organization.Zgid);
                                    Console.WriteLine("Organization CountryCode: " + organization.CountryCode);
                                    Console.WriteLine("Organization LicenseDetails: " + organization.LicenseDetails);
                                    Console.WriteLine("Organization Phone: " + organization.Phone);
                                    Console.WriteLine("Organization CompanyName: " + organization.CompanyName);
                                    Console.WriteLine("Organization PrivacySettings: " + organization.PrivacySettings);
                                    Console.WriteLine("Organization PrimaryEmail: " + organization.PrimaryEmail);
                                    Console.WriteLine("Organization IsoCode: " + organization.IsoCode);
                                    Console.WriteLine("---------------------------");
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

                GetOrganization_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}