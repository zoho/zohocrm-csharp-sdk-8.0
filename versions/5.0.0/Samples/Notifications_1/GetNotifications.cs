using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Notifications;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Notifications.APIException;
using ResponseHandler = Com.Zoho.Crm.API.Notifications.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Notifications.ResponseWrapper;
using GetNotificationsParam = Com.Zoho.Crm.API.Notifications.NotificationsOperations.GetNotificationsParam;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Notifications_1
{
    public class GetNotifications
    {
        public static void GetNotifications_1()
        {
            try
            {
                NotificationsOperations notificationsOperations = new NotificationsOperations();
                ParameterMap paramInstance = new ParameterMap();
                paramInstance.Add(GetNotificationsParam.PAGE, 1);
                paramInstance.Add(GetNotificationsParam.PER_PAGE, 10);
                paramInstance.Add(GetNotificationsParam.MODULE, "Leads");

                APIResponse<ResponseHandler> response = notificationsOperations.GetNotifications(paramInstance);

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
                            List<Com.Zoho.Crm.API.Notifications.Notification> notifications = responseWrapper.Watch;

                            if (notifications != null)
                            {
                                foreach (Com.Zoho.Crm.API.Notifications.Notification notification in notifications)
                                {
                                    Console.WriteLine("Notification ChannelId: " + notification.ChannelId);
                                    Console.WriteLine("Notification ResourceUri: " + notification.ResourceUri);
                                    Console.WriteLine("Notification ResourceId: " + notification.ResourceId);
                                    Console.WriteLine("Notification ResourceName: " + notification.ResourceName);
                                    Console.WriteLine("Notification NotifyUrl: " + notification.NotifyUrl);
                                    Console.WriteLine("Notification ChannelExpiry: " + notification.ChannelExpiry);
                                    Console.WriteLine("Notification Token: " + notification.Token);

                                    List<string> events = notification.Events;
                                    if (events != null)
                                    {
                                        foreach (string eventName in events)
                                        {
                                            Console.WriteLine("Notification Event: " + eventName);
                                        }
                                    }

                                    Console.WriteLine("---------------------------");
                                }
                            }

                            Com.Zoho.Crm.API.Notifications.Info info = responseWrapper.Info;
                            if (info != null)
                            {
                                Console.WriteLine("Notification Info PerPage: " + info.PerPage);
                                Console.WriteLine("Notification Info Count: " + info.Count);
                                Console.WriteLine("Notification Info Page: " + info.Page);
                                Console.WriteLine("Notification Info MoreRecords: " + info.MoreRecords);
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

                GetNotifications_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}