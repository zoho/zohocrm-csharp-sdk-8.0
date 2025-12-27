using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Notifications;
using Com.Zoho.Crm.API.Util;
using ActionHandler = Com.Zoho.Crm.API.Notifications.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Notifications.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Notifications.ActionWrapper;
using APIException = Com.Zoho.Crm.API.Notifications.APIException;
using BodyWrapper = Com.Zoho.Crm.API.Notifications.BodyWrapper;
using SuccessResponse = Com.Zoho.Crm.API.Notifications.SuccessResponse;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Notifications_1
{
    public class EnableNotifications
    {
        public static void EnableNotifications_1()
        {
            try
            {
                NotificationsOperations notificationsOperations = new NotificationsOperations();
                BodyWrapper bodyWrapper = new BodyWrapper();
                List<Notification> notifications = new List<Notification>();

                Notification notification = new Notification();
                notification.ChannelId = "1006800211";

                List<string> events = new List<string>
                {
                    "Accounts.all"
                };
                notification.Events = events;

                notification.ChannelExpiry = new DateTimeOffset(new DateTime(2025, 12, 31, 12, 0, 0, DateTimeKind.Utc));
                notification.Token = "TOKEN_FOR_VERIFICATION_OF_1006800211";
                notification.NotifyUrl = "https://www.example.com/notifications";

                notifications.Add(notification);
                bodyWrapper.Watch = notifications;

                APIResponse<ActionHandler> response = notificationsOperations.EnableNotifications(bodyWrapper);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                            List<ActionResponse> actionResponses = actionWrapper.Watch;

                            if (actionResponses != null)
                            {
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

                                        Console.WriteLine("Message: " + successResponse.Message.Value);
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

                EnableNotifications_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}