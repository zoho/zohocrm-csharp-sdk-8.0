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
    public class CreateUsers
    {
        public static void CreateUsers_1()
        {
            try
            {
                UsersOperations usersOperations = new UsersOperations();

                // Create request body
                BodyWrapper request = new BodyWrapper();
                List<Users> usersList = new List<Users>();

                // Create first user
                Users user1 = new Users();
                user1.FirstName = "John";
                user1.LastName = "Doe";
                user1.Email = "john.doe@example.com";

                Role role1 = new Role();
                role1.Id = 1055806000000026008L; // Replace with actual role ID
                user1.Role = role1;

                Com.Zoho.Crm.API.Users.Profile profile1 = new Com.Zoho.Crm.API.Users.Profile();
                profile1.Id = 1055806000000026014L; // Replace with actual profile ID
                user1.Profile = profile1;

                usersList.Add(user1);
                request.Users = usersList;

                APIResponse<ActionHandler> response = usersOperations.CreateUsers(request);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                            List<ActionResponse> actionResponses = actionWrapper.Users;

                            Console.WriteLine($"Processed {actionResponses?.Count ?? 0} user creations:");

                            if (actionResponses != null && actionResponses.Count > 0)
                            {
                                foreach (ActionResponse actionResponse in actionResponses)
                                {
                                    if (actionResponse is SuccessResponse)
                                    {
                                        SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                        Console.WriteLine("\n--- User Creation Success ---");
                                        Console.WriteLine("Status: " + successResponse.Status.Value);
                                        Console.WriteLine("Code: " + successResponse.Code.Value);
                                        Console.WriteLine("Message: " + successResponse.Message);

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

                                        Console.WriteLine("\n--- User Creation Failed ---");
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

                                        Console.WriteLine("Message: " + exception.Message);
                                        Console.WriteLine("---");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("No action responses received");
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

                // Create users
                CreateUsers_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}