using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.EmailTemplates.APIException;
using EmailTemplatesOperations = Com.Zoho.Crm.API.EmailTemplates.EmailTemplatesOperations;
using ResponseHandler = Com.Zoho.Crm.API.EmailTemplates.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.EmailTemplates.ResponseWrapper;
using EmailTemplate = Com.Zoho.Crm.API.EmailTemplates.EmailTemplate;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.EmailTemplates
{
    public class GetEmailTemplate
    {
        public static void GetEmailTemplate_1()
        {
            EmailTemplatesOperations emailTemplatesOperations = new EmailTemplatesOperations();
            long? templateId = 1055806000027604013L; // Replace with actual template ID

            APIResponse<ResponseHandler> response = emailTemplatesOperations.GetEmailTemplate(templateId);

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
                        List<EmailTemplate> emailTemplates = responseWrapper.EmailTemplates;

                        if (emailTemplates != null && emailTemplates.Count > 0)
                        {
                            EmailTemplate emailTemplate = emailTemplates[0];
                            Console.WriteLine("EmailTemplate ID: " + emailTemplate.Id);
                            Console.WriteLine("EmailTemplate Name: " + emailTemplate.Name);
                            Console.WriteLine("EmailTemplate Subject: " + emailTemplate.Subject);
                            Console.WriteLine("EmailTemplate Description: " + emailTemplate.Description);
                            Console.WriteLine("EmailTemplate Category: " + emailTemplate.Category);
                            Console.WriteLine("EmailTemplate EditorMode: " + emailTemplate.EditorMode);
                            Console.WriteLine("EmailTemplate Content: " + emailTemplate.Content);
                            Console.WriteLine("EmailTemplate MailContent: " + emailTemplate.MailContent);
                            Console.WriteLine("EmailTemplate Active: " + emailTemplate.Active);
                            Console.WriteLine("EmailTemplate Favorite: " + emailTemplate.Favorite);
                            Console.WriteLine("EmailTemplate Associated: " + emailTemplate.Associated);
                            Console.WriteLine("EmailTemplate ConsentLinked: " + emailTemplate.ConsentLinked);
                            Console.WriteLine("EmailTemplate CreatedTime: " + emailTemplate.CreatedTime);
                            Console.WriteLine("EmailTemplate ModifiedTime: " + emailTemplate.ModifiedTime);
                            Console.WriteLine("EmailTemplate LastUsageTime: " + emailTemplate.LastUsageTime);

                            var folder = emailTemplate.Folder;
                            if (folder != null)
                            {
                                Console.WriteLine("EmailTemplate Folder ID: " + folder.Id);
                                Console.WriteLine("EmailTemplate Folder Name: " + folder.Name);
                            }

                            var module = emailTemplate.Module;
                            if (module != null)
                            {
                                Console.WriteLine("EmailTemplate Module ID: " + module.Id);
                                Console.WriteLine("EmailTemplate Module APIName: " + module.APIName);
                            }

                            var createdBy = emailTemplate.CreatedBy;
                            if (createdBy != null)
                            {
                                Console.WriteLine("EmailTemplate CreatedBy User-Name: " + createdBy.Name);
                                Console.WriteLine("EmailTemplate CreatedBy User-ID: " + createdBy.Id);
                            }

                            var modifiedBy = emailTemplate.ModifiedBy;
                            if (modifiedBy != null)
                            {
                                Console.WriteLine("EmailTemplate ModifiedBy User-Name: " + modifiedBy.Name);
                                Console.WriteLine("EmailTemplate ModifiedBy User-ID: " + modifiedBy.Id);
                            }

                            var attachments = emailTemplate.Attachments;
                            if (attachments != null && attachments.Count > 0)
                            {
                                Console.WriteLine("EmailTemplate Attachments: ");
                                foreach (var attachment in attachments)
                                {
                                    Console.WriteLine("  Attachment ID: " + attachment.Id);
                                    Console.WriteLine("  Attachment FileName: " + attachment.FileName);
                                    Console.WriteLine("  Attachment FileId: " + attachment.FileId);
                                    Console.WriteLine("  Attachment Size: " + attachment.Size);
                                }
                            }

                            var lastVersionStats = emailTemplate.LastVersionStatistics;
                            if (lastVersionStats != null)
                            {
                                Console.WriteLine("EmailTemplate LastVersionStatistics:");
                                Console.WriteLine("  Tracked: " + lastVersionStats.Tracked);
                                Console.WriteLine("  Delivered: " + lastVersionStats.Delivered);
                                Console.WriteLine("  Opened: " + lastVersionStats.Opened);
                                Console.WriteLine("  Bounced: " + lastVersionStats.Bounced);
                                Console.WriteLine("  Sent: " + lastVersionStats.Sent);
                                Console.WriteLine("  Clicked: " + lastVersionStats.Clicked);
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
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                GetEmailTemplate_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}