using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.DealContactRoles.APIException;
using DealContactRolesOperations = Com.Zoho.Crm.API.DealContactRoles.DealContactRolesOperations;
using ResponseHandler = Com.Zoho.Crm.API.DealContactRoles.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.DealContactRoles.ResponseWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using Com.Zoho.Crm.API.DealContactRoles;

namespace Samples.DealContactRoles
{
    public class GetAssociatedContactRoles
    {
        public static void GetAssociatedContactRoles_1(long dealId)
        {
            DealContactRolesOperations dealContactRolesOperations = new DealContactRolesOperations();
            ParameterMap paramInstance = new ParameterMap();

            // Optional parameters
             paramInstance.Add(DealContactRolesOperations.GetAssociatedContactRolesParam.IDS, "1055806000028564009,3477061000004381002");
            // paramInstance.Add(DealContactRolesOperations.GetAssociatedContactRolesParam.FIELDS, "Contact_Role,Full_Name,Email");

            APIResponse<ResponseHandler> response = dealContactRolesOperations.GetAssociatedContactRoles(dealId, paramInstance);

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
                        List<Data> records = responseWrapper.Data;

                        foreach (Data record in records)
                        {
                            Console.WriteLine("Record ID: " + record.Id);

                            // Get the contact role information
                            Console.WriteLine("Record CreatedBy User-ID: " + record.CreatedBy?.Id);
                            Console.WriteLine("Record CreatedBy User-Name: " + record.CreatedBy?.Name);
                            Console.WriteLine("Record CreatedBy User-Email: " + record.CreatedBy?.Email);

                            Console.WriteLine("Record ModifiedBy User-ID: " + record.ModifiedBy?.Id);
                            Console.WriteLine("Record ModifiedBy User-Name: " + record.ModifiedBy?.Name);
                            Console.WriteLine("Record ModifiedBy User-Email: " + record.ModifiedBy?.Email);

                            Console.WriteLine("Record CreatedTime: " + record.CreatedTime);
                            Console.WriteLine("Record ModifiedTime: " + record.ModifiedTime);

                            // Get key-value pairs from record
                            foreach (KeyValuePair<string, object> entry in record.GetKeyValues())
                            {
                                Console.WriteLine(entry.Key + ": " + entry.Value);
                            }
                        }

                        // Get the Object obtained Info instance
                        Info info = responseWrapper.Info;
                        if (info != null)
                        {
                            if (info.Count != null)
                            {
                                Console.WriteLine("Record Info Count: " + info.Count.ToString());
                            }
                            if (info.MoreRecords != null)
                            {
                                Console.WriteLine("Record Info MoreRecords: " + info.MoreRecords.ToString());
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
                Environment environment = INDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                long dealId = 3477061000004381001L;
                GetAssociatedContactRoles_1(dealId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}