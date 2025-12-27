using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.ContactRoles.APIException;
using ContactRolesOperations = Com.Zoho.Crm.API.ContactRoles.ContactRolesOperations;
using BodyWrapper = Com.Zoho.Crm.API.ContactRoles.BodyWrapper;
using ActionHandler = Com.Zoho.Crm.API.ContactRoles.ActionHandler;
using ActionWrapper = Com.Zoho.Crm.API.ContactRoles.ActionWrapper;
using ActionResponse = Com.Zoho.Crm.API.ContactRoles.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.ContactRoles.SuccessResponse;
using ContactRole = Com.Zoho.Crm.API.ContactRoles.ContactRole;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.ContactRoles
{
    public class UpdateRoles
    {
        public static void UpdateRoles_1()
        {
            ContactRolesOperations contactRolesOperations = new ContactRolesOperations();
            BodyWrapper bodyWrapper = new BodyWrapper();

            List<ContactRole> contactRoles = new List<ContactRole>();

            ContactRole contactRole1 = new ContactRole();
            contactRole1.Id = 3477061000004381001L;
            contactRole1.Name = "Primary Decision Maker";
            contactRole1.SequenceNumber = 1;
            contactRoles.Add(contactRole1);

            ContactRole contactRole2 = new ContactRole();
            contactRole2.Id = 1055806000012517003L;
            contactRole2.Name = "Technical Influencer";
            contactRole2.SequenceNumber = 2;
            contactRoles.Add(contactRole2);

            bodyWrapper.ContactRoles = contactRoles;

            APIResponse<ActionHandler> response = contactRolesOperations.UpdateRoles(bodyWrapper);

            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionHandler actionHandler = response.Object;
                    if (actionHandler is ActionWrapper)
                    {
                        ActionWrapper actionWrapper = (ActionWrapper)actionHandler;
                        List<ActionResponse> actionResponses = actionWrapper.ContactRoles;
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
                                Console.WriteLine("Message: " + successResponse.Message);
                            }
                            else if (actionResponse is APIException)
                            {
                                APIException exception = (APIException)actionResponse;
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
                    }
                    else if (actionHandler is APIException)
                    {
                        APIException exception = (APIException)actionHandler;
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
                Environment environment = INDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                UpdateRoles_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}