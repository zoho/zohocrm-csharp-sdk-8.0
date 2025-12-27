using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.Users_1
{
    public class GetUsers
    {
        public static void GetUsers_1()
        {
            try
            {
                UsersOperations usersOperations = new UsersOperations();

                // Create parameter map for filtering and pagination
                ParameterMap paramInstance = new ParameterMap();

                // Optional parameters - uncomment as needed
                // paramInstance.Add(UsersOperations.GetUsersParam.TYPE, "ActiveUsers");
                // paramInstance.Add(UsersOperations.GetUsersParam.PAGE, 1);
                // paramInstance.Add(UsersOperations.GetUsersParam.PER_PAGE, 200);
                // paramInstance.Add(UsersOperations.GetUsersParam.IDS, "554023000000567023,554023000000567024");

                // Create header map for conditional requests
                HeaderMap headerInstance = new HeaderMap();

                // Optional header - uncomment as needed
                // headerInstance.Add(UsersOperations.GetUsersHeader.IF_MODIFIED_SINCE, DateTimeOffset.Now.AddDays(-7));

                // Call API
                APIResponse<ResponseHandler> response = usersOperations.GetUsers(paramInstance, headerInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.StatusCode == 204)
                    {
                        Console.WriteLine("No Content - No users found");
                        return;
                    }

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<Users> users = responseWrapper.Users;

                            Console.WriteLine($"Retrieved {users?.Count ?? 0} users:");

                            if (users != null && users.Count > 0)
                            {
                                foreach (Users user in users)
                                {
                                    Console.WriteLine("\n--- User Details ---");
                                    Console.WriteLine("User ID: " + user.Id);
                                    Console.WriteLine("User Name: " + user.Name);
                                    Console.WriteLine("Email: " + user.Email);
                                    Console.WriteLine("First Name: " + user.FirstName);
                                    Console.WriteLine("Last Name: " + user.LastName);

                                    if (user.Role != null)
                                    {
                                        Console.WriteLine("Role ID: " + user.Role.Id);
                                        Console.WriteLine("Role Name: " + user.Role.Name);
                                    }

                                    if (user.Profile != null)
                                    {
                                        Console.WriteLine("Profile ID: " + user.Profile.Id);
                                        Console.WriteLine("Profile Name: " + user.Profile.Name);
                                    }

                                    if (user.Status != null)
                                    {
                                        Console.WriteLine("Status: " + user.Status);
                                    }

                                    if (user.CreatedBy != null)
                                    {
                                        Console.WriteLine("Created By: " + user.CreatedBy.Name + " (ID: " + user.CreatedBy.Id + ")");
                                    }

                                    if (user.ModifiedBy != null)
                                    {
                                        Console.WriteLine("Modified By: " + user.ModifiedBy.Name + " (ID: " + user.ModifiedBy.Id + ")");
                                    }

                                    if (user.CreatedTime != null)
                                    {
                                        Console.WriteLine("Created Time: " + user.CreatedTime);
                                    }

                                    if (user.ModifiedTime != null)
                                    {
                                        Console.WriteLine("Modified Time: " + user.ModifiedTime);
                                    }

                                    Console.WriteLine("---");
                                }

                                // Display pagination info if available
                                Info info = responseWrapper.Info;
                                if (info != null)
                                {
                                    Console.WriteLine("\n--- Pagination Info ---");
                                    if (info.Page != null)
                                    {
                                        Console.WriteLine("Current Page: " + info.Page);
                                    }
                                    if (info.PerPage != null)
                                    {
                                        Console.WriteLine("Per Page: " + info.PerPage);
                                    }
                                    if (info.Count != null)
                                    {
                                        Console.WriteLine("Total Count: " + info.Count);
                                    }
                                    if (info.MoreRecords != null)
                                    {
                                        Console.WriteLine("More Records Available: " + info.MoreRecords);
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No users found in the organization");
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

                // Get all users
                GetUsers_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}