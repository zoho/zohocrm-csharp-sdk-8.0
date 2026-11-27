using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.InventoryTemplates.APIException;
using InventoryTemplatesOperations = Com.Zoho.Crm.API.InventoryTemplates.InventoryTemplatesOperations;
using ResponseHandler = Com.Zoho.Crm.API.InventoryTemplates.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.InventoryTemplates.ResponseWrapper;
using InventoryTemplates = Com.Zoho.Crm.API.InventoryTemplates.InventoryTemplates;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.InventoryTemplates
{
    public class GetInventoryTemplates
    {
        public static void GetInventoryTemplates_1()
        {
            InventoryTemplatesOperations inventoryTemplatesOperations = new InventoryTemplatesOperations();
            ParameterMap paramInstance = new ParameterMap();

            // Add parameters if needed
            // paramInstance.Add(InventoryTemplatesOperations.GetInventoryTemplatesParam.MODULE, "Quotes");
            // paramInstance.Add(InventoryTemplatesOperations.GetInventoryTemplatesParam.CATEGORY, "created_by_admin");
            // paramInstance.Add(InventoryTemplatesOperations.GetInventoryTemplatesParam.SORT_BY, "name");
            // paramInstance.Add(InventoryTemplatesOperations.GetInventoryTemplatesParam.SORT_ORDER, "asc");

            APIResponse<ResponseHandler> response = inventoryTemplatesOperations.GetInventoryTemplates(paramInstance);

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
                        List<Com.Zoho.Crm.API.InventoryTemplates.InventoryTemplates> inventoryTemplates = responseWrapper.InventoryTemplates;

                        if (inventoryTemplates != null)
                        {
                            foreach (Com.Zoho.Crm.API.InventoryTemplates.InventoryTemplates inventoryTemplate in inventoryTemplates)
                            {
                                Console.WriteLine("InventoryTemplate ID: " + inventoryTemplate.Id);
                                Console.WriteLine("InventoryTemplate Name: " + inventoryTemplate.Name);
                                Console.WriteLine("InventoryTemplate Category: " + inventoryTemplate.Category);
                                Console.WriteLine("InventoryTemplate EditorMode: " + inventoryTemplate.EditorMode);
                                Console.WriteLine("InventoryTemplate Active: " + inventoryTemplate.Active);
                                Console.WriteLine("InventoryTemplate Favorite: " + inventoryTemplate.Favorite);
                                Console.WriteLine("InventoryTemplate Content: " + inventoryTemplate.Content);
                                Console.WriteLine("InventoryTemplate MailContent: " + inventoryTemplate.MailContent);
                                Console.WriteLine("InventoryTemplate CreatedTime: " + inventoryTemplate.CreatedTime);
                                Console.WriteLine("InventoryTemplate ModifiedTime: " + inventoryTemplate.ModifiedTime);
                                Console.WriteLine("InventoryTemplate LastUsageTime: " + inventoryTemplate.LastUsageTime);

                                var folder = inventoryTemplate.Folder;
                                if (folder != null)
                                {
                                    Console.WriteLine("InventoryTemplate Folder ID: " + folder.Id);
                                    Console.WriteLine("InventoryTemplate Folder Name: " + folder.Name);
                                }

                                var module = inventoryTemplate.Module;
                                if (module != null)
                                {
                                    Console.WriteLine("InventoryTemplate Module ID: " + module.Id);
                                    Console.WriteLine("InventoryTemplate Module APIName: " + module.APIName);
                                }

                                var createdBy = inventoryTemplate.CreatedBy;
                                if (createdBy != null)
                                {
                                    Console.WriteLine("InventoryTemplate CreatedBy User-Name: " + createdBy.Name);
                                    Console.WriteLine("InventoryTemplate CreatedBy User-ID: " + createdBy.Id);
                                }

                                var modifiedBy = inventoryTemplate.ModifiedBy;
                                if (modifiedBy != null)
                                {
                                    Console.WriteLine("InventoryTemplate ModifiedBy User-Name: " + modifiedBy.Name);
                                    Console.WriteLine("InventoryTemplate ModifiedBy User-ID: " + modifiedBy.Id);
                                }

                                Console.WriteLine("------------------------");
                            }
                        }

                        var info = responseWrapper.Info;
                        if (info != null)
                        {
                            Console.WriteLine("InventoryTemplates Info:");
                            Console.WriteLine("  Page: " + info.Page);
                            Console.WriteLine("  PerPage: " + info.PerPage);
                            Console.WriteLine("  Count: " + info.Count);
                            Console.WriteLine("  MoreRecords: " + info.MoreRecords);
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
                GetInventoryTemplates_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}