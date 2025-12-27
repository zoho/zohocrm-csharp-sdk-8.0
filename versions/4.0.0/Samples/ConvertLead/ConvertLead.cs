using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.ConvertLead;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using Com.Zoho.Crm.API.Record;
using BodyWrapper = Com.Zoho.Crm.API.ConvertLead.BodyWrapper;
using Com.Zoho.Crm.API.Users;
using ActionHandler = Com.Zoho.Crm.API.ConvertLead.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.ConvertLead.ActionWrapper;
using ActionResponse = Com.Zoho.Crm.API.ConvertLead.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.ConvertLead.SuccessResponse;
using APIException = Com.Zoho.Crm.API.ConvertLead.APIException;

namespace Samples.ConvertLead
{
    public class ConvertLead
    {
        public static void ConvertLead_1(long leadId)
        {
            try
            {
                ConvertLeadOperations convertLeadOperations = new ConvertLeadOperations(leadId);

                BodyWrapper bodyWrapper = new BodyWrapper();

                List<LeadConverter> data = new List<LeadConverter>();

                LeadConverter convertLeadData = new LeadConverter();
                convertLeadData.Overwrite = true;
                convertLeadData.NotifyLeadOwner = true;
                convertLeadData.NotifyNewEntityOwner = true;

                Com.Zoho.Crm.API.Record.Record account = new Com.Zoho.Crm.API.Record.Record();
                account.Id = 34770607004L;
                convertLeadData.Accounts = account;

                Com.Zoho.Crm.API.Record.Record contact = new Com.Zoho.Crm.API.Record.Record();
                contact.Id = 3477064004L;
                convertLeadData.Contacts = contact;

                // Set deal fields if converting to deal
                Com.Zoho.Crm.API.Record.Record deal = new Com.Zoho.Crm.API.Record.Record();
                deal.AddFieldValue(Deals.DEAL_NAME, "Sample Deal Name");
                deal.AddFieldValue(Deals.STAGE, new Choice<string>("Qualification"));
                deal.AddFieldValue(Deals.AMOUNT, 5000.0);
                deal.AddFieldValue(Deals.CLOSING_DATE, new DateTime(2024, 12, 31, 0, 0, 0));
                deal.AddKeyValue("Pipeline", new Choice<String>("Qualification"));

                convertLeadData.Deals = deal;

                MinifiedUser assignTo = new MinifiedUser();
                assignTo.Id = 3477173021L;
                convertLeadData.AssignTo = assignTo;

                data.Add(convertLeadData);

                bodyWrapper.Data = data;

                // Call API
                APIResponse<ActionHandler> response = convertLeadOperations.ConvertLead(bodyWrapper);

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
                                    Console.WriteLine("Details: ");

                                    foreach (KeyValuePair<string, object> entry in successResponse.Details)
                                    {
                                        Console.WriteLine(entry.Key + ": " + entry.Value);
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

                                    Console.WriteLine("Message: " + exception.Message.Value);
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

                long leadId = 34770615177002L;
                ConvertLead_1(leadId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}