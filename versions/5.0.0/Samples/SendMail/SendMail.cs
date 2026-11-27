using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.SendMail;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using BodyWrapper = Com.Zoho.Crm.API.SendMail.BodyWrapper;
using ActionHandler = Com.Zoho.Crm.API.SendMail.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.SendMail.ActionWrapper;
using ActionResponse = Com.Zoho.Crm.API.SendMail.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.SendMail.SuccessResponse;
using APIException = Com.Zoho.Crm.API.SendMail.APIException;

namespace Samples.SendMail
{
    public class SendMail
    {
        public static void SendMail_1(string moduleAPIName, long recordId)
        {
            try
            {
                SendMailOperations sendMailOperations = new SendMailOperations(recordId, moduleAPIName);

                BodyWrapper request = new BodyWrapper();

                List<Data> dataList = new List<Data>();

                // Create mail data
                Data mailData = new Data();

                // Set From details
                From from = new From();
                from.UserName = "John Doe";
                from.Email = "john.doe@example.com";
                mailData.From = from;

                // Set To recipients
                List<To> toList = new List<To>();
                To toRecipient = new To();
                toRecipient.UserName = "Jane Smith";
                toRecipient.Email = "jane.smith@example.com";
                toList.Add(toRecipient);
                mailData.To = toList;

                // Set CC recipients (optional)
                List<Cc> ccList = new List<Cc>();
                Cc ccRecipient = new Cc();
                ccRecipient.UserName = "Manager";
                ccRecipient.Email = "manager@example.com";
                ccList.Add(ccRecipient);
                mailData.Cc = ccList;

                // Set mail subject and content
                mailData.Subject = "Important Update Regarding Your Request";
                mailData.Content = "<html><body><p>Dear Customer,</p><p>We wanted to update you on the status of your recent request...</p><p>Best regards,<br/>Support Team</p></body></html>";

                // Set attachments (optional)
                List<Attachment> attachmentsList = new List<Attachment>();
                Attachment attachment = new Attachment();
                attachment.Id = "attachment_id_123";
                attachmentsList.Add(attachment);
                mailData.Attachments = attachmentsList;

                // Set inventory details (optional - for quotes/invoices)
                InventoryDetails inventoryDetails = new InventoryDetails();
                InventoryTemplate inventoryTemplate = new InventoryTemplate();
                inventoryTemplate.Id = 987654321L;
                inventoryTemplate.Name = "Standard Invoice Template";
                inventoryDetails.InventoryTemplate = inventoryTemplate;
                mailData.InventoryDetails = inventoryDetails;

                // Set linked record details (optional)
                LinkedRecord linkedRecord = new LinkedRecord();
                linkedRecord.Id = recordId;
                linkedRecord.Name = "Associated Record";
                LinkedModule linkedModule = new LinkedModule();
                linkedModule.APIName = moduleAPIName;
                linkedModule.Id = recordId;
                linkedRecord.Module = linkedModule;
                mailData.LinkedRecord = linkedRecord;

                // Set data subject request (optional - for GDPR compliance)
                DataSubjectRequest dataSubjectRequest = new DataSubjectRequest();
                dataSubjectRequest.Id = recordId;
                dataSubjectRequest.Type = "access";
                mailData.DataSubjectRequest = dataSubjectRequest;

                // Set reply-to information (optional)
                InReplyTo inReplyTo = new InReplyTo();
                inReplyTo.MessageId = "previous_message_id";
                Owner owner = new Owner();
                owner.Id = 123456789L;
                owner.Name = "Record Owner";
                inReplyTo.Owner = owner;
                mailData.InReplyTo = inReplyTo;

                // Add mail configuration options
                mailData.MailFormat = new Choice<String>("html"); // or "text"
                mailData.ConsentEmail = true;
                mailData.OrgEmail = false;

                dataList.Add(mailData);
                request.Data = dataList;

                // Call API
                APIResponse<ActionHandler> response = sendMailOperations.SendMail(request);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ActionHandler actionHandler = response.Object;

                        if (actionHandler is ActionWrapper)
                        {
                            ActionWrapper actionWrapper = (ActionWrapper)actionHandler;

                            List<ActionResponse> actionResponses = actionWrapper.Data;

                            foreach (ActionResponse actionResponse in actionResponses)
                            {
                                if (actionResponse is SuccessResponse)
                                {
                                    SuccessResponse successResponse = (SuccessResponse)actionResponse;

                                    Console.WriteLine("Status: " + successResponse.Status.Value);
                                    Console.WriteLine("Code: " + successResponse.Code.Value);
                                    Console.WriteLine("Message: " + successResponse.Message);

                                    Console.WriteLine("Details: ");
                                    if (successResponse.Details != null)
                                    {
                                        foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                        {
                                            Console.WriteLine(entry.Key + ": " + entry.Value);
                                        }
                                    }
                                }
                                else if (actionResponse is APIException)
                                {
                                    APIException exception = (APIException)actionResponse;

                                    Console.WriteLine("Status: " + exception.Status.Value);
                                    Console.WriteLine("Code: " + exception.Code.Value);
                                    Console.WriteLine("Message: " + exception.Message);

                                    if (exception.Details != null)
                                    {
                                        Console.WriteLine("Details: ");
                                        foreach (KeyValuePair<string, object> entry in exception.Details)
                                        {
                                            Console.WriteLine(entry.Key + ": " + entry.Value);
                                        }
                                    }
                                }
                            }
                        }
                        else if (actionHandler is APIException)
                        {
                            APIException exception = (APIException)actionHandler;

                            Console.WriteLine("Status: " + exception.Status.Value);
                            Console.WriteLine("Code: " + exception.Code.Value);
                            Console.WriteLine("Message: " + exception.Message);

                            if (exception.Details != null)
                            {
                                Console.WriteLine("Details: ");
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    Console.WriteLine(entry.Key + ": " + entry.Value);
                                }
                            }
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

                string moduleAPIName = "Leads";
                long recordId = 34770615177002L; // Replace with actual record ID
                SendMail_1(moduleAPIName, recordId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}