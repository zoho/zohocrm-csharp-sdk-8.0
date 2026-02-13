using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.CustomViews.APIException;
using CustomViewsOperations = Com.Zoho.Crm.API.CustomViews.CustomViewsOperations;
using ResponseHandler = Com.Zoho.Crm.API.CustomViews.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.CustomViews.ResponseWrapper;
using CustomView = Com.Zoho.Crm.API.CustomViews.CustomViews;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Owner = Com.Zoho.Crm.API.CustomViews.Owner;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;


namespace Samples.CustomViews
{
    public class GetCustomView
    {
        public static void GetCustomView_1(long customViewId)
        {
            CustomViewsOperations customViewsOperations = new CustomViewsOperations();
            ParameterMap paramInstance = new ParameterMap();
            paramInstance.Add(CustomViewsOperations.GetCustomViewParam.MODULE, "Leads");

            APIResponse<ResponseHandler> response = customViewsOperations.GetCustomView(customViewId, paramInstance);

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
                        List<CustomView> customViews = responseWrapper.CustomViews;

                        foreach (CustomView customView in customViews)
                        {
                            Console.WriteLine("CustomView ID: " + customView.Id);
                            Console.WriteLine("CustomView Name: " + customView.Name);
                            Console.WriteLine("CustomView DisplayValue: " + customView.DisplayValue);
                            Console.WriteLine("CustomView SystemName: " + customView.SystemName);
                            Console.WriteLine("CustomView Category: " + customView.Category);
                            Console.WriteLine("CustomView SortBy: " + customView.SortBy);
                            Console.WriteLine("CustomView SortOrder: " + customView.SortOrder);
                            Console.WriteLine("CustomView Favorite: " + customView.Favorite);
                            Console.WriteLine("CustomView Default: " + customView.Default);
                            Console.WriteLine("CustomView SystemDefined: " + customView.SystemDefined);
                            Console.WriteLine("CustomView Offline: " + customView.Offline);

                            List<Com.Zoho.Crm.API.CustomViews.Fields> fields = customView.Fields;
                            if (fields != null)
                            {
                                Console.WriteLine("CustomView Fields:");
                                foreach (Com.Zoho.Crm.API.CustomViews.Fields field in fields)
                                {
                                    Console.WriteLine("Field APIName: " + field.APIName);
                                    Console.WriteLine("Field ID: " + field.Id);
                                }
                            }

                            Com.Zoho.Crm.API.CustomViews.Criteria criteria = customView.Criteria;
                            if (criteria != null)
                            {
                                Console.WriteLine("CustomView Criteria Comparator: " + criteria.Comparator);
                                Console.WriteLine("CustomView Criteria Value: " + criteria.Value);
                                if (criteria.Field != null)
                                {
                                    Console.WriteLine("CustomView Criteria Field APIName: " + criteria.Field.APIName);
                                }
                            }

                            Owner createdBy = customView.CreatedBy;
                            if (createdBy != null)
                            {
                                Console.WriteLine("CustomView CreatedBy User-Name: " + createdBy.Name);
                                Console.WriteLine("CustomView CreatedBy User-ID: " + createdBy.Id);
                                Console.WriteLine("CustomView CreatedBy User-Email: " + createdBy.Email);
                            }

                            Owner modifiedBy = customView.ModifiedBy;
                            if (modifiedBy != null)
                            {
                                Console.WriteLine("CustomView ModifiedBy User-Name: " + modifiedBy.Name);
                                Console.WriteLine("CustomView ModifiedBy User-ID: " + modifiedBy.Id);
                                Console.WriteLine("CustomView ModifiedBy User-Email: " + modifiedBy.Email);
                            }

                            Console.WriteLine("CustomView CreatedTime: " + customView.CreatedTime);
                            Console.WriteLine("CustomView ModifiedTime: " + customView.ModifiedTime);
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
                long customViewId = 3477061000004381001L;
                GetCustomView_1(customViewId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}