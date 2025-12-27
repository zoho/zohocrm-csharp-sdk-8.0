using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Roles;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.Role_1
{
    public class GetOrgRoles
    {
        public static void GetRoles_1()
        {
            try
            {
                RolesOperations rolesOperations = new RolesOperations();

                // Call API
                APIResponse<ResponseHandler> response = rolesOperations.GetRoles();

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<Role> roles = responseWrapper.Roles;

                            Console.WriteLine($"Retrieved {roles?.Count ?? 0} roles:");

                            if (roles != null && roles.Count > 0)
                            {
                                foreach (Role role in roles)
                                {
                                    Console.WriteLine("\n--- Role Details ---");
                                    Console.WriteLine("Role ID: " + role.Id);
                                    Console.WriteLine("Role Name: " + role.Name);
                                    Console.WriteLine("Display Label: " + role.DisplayLabel);
                                    Console.WriteLine("Description: " + role.Description);

                                    if (role.ForecastManager != null)
                                    {
                                        Console.WriteLine("Role Forecast Manager User-ID: " + role.ForecastManager.Id);
                                        Console.WriteLine("Role Forecast Manager User-Name: " + role.ForecastManager.Name);
                                    }

                                    // Reporting details
                                    if (role.ReportingTo != null)
                                    {
                                        Console.WriteLine("Reporting To: " + role.ReportingTo.Name + " (ID: " + role.ReportingTo.Id + ")");
                                    }
                                    Console.WriteLine("Admin User: " + role.AdminUser);
                                    if (role.ShareWithPeers != null)
                                    {
                                        Console.WriteLine("Share With Peers: " + role.ShareWithPeers.Value);
                                    }

                                    Console.WriteLine("---");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No roles found in the organization");
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

                // Get all roles
                GetRoles_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}