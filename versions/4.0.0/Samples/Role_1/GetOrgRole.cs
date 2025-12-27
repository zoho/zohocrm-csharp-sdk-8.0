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
    public class GetOrgRole
    {
        public static void GetRole_1(long roleId)
        {
            try
            {
                RolesOperations rolesOperations = new RolesOperations();

                // Call API
                APIResponse<ResponseHandler> response = rolesOperations.GetRole(roleId);

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

                            if (roles != null && roles.Count > 0)
                            {
                                Role role = roles[0];

                                Console.WriteLine("\n=== Role Details ===");
                                Console.WriteLine("Role ID: " + role.Id);
                                Console.WriteLine("Role Name: " + role.Name);
                                Console.WriteLine("Display Label: " + role.DisplayLabel);
                                Console.WriteLine("Description: " + role.Description);

                                // Reporting details
                                if (role.ReportingTo != null)
                                {
                                    Console.WriteLine("Reports To: " + role.ReportingTo.Name + " (ID: " + role.ReportingTo.Id + ")");
                                }
                                else
                                {
                                    Console.WriteLine("Reports To: Top level role (No reporting hierarchy)");
                                }

                                Console.WriteLine("Admin User: " + role.AdminUser);

                                // Share with peers
                                if (role.ShareWithPeers != null)
                                {
                                    Console.WriteLine("Share With Peers: " + role.ShareWithPeers.Value);
                                }

                                Console.WriteLine("===================");
                            }
                            else
                            {
                                Console.WriteLine("No role found with the specified ID");
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

                long roleId = 554023000000406001L; // Replace with actual role ID
                GetRole_1(roleId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}