using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Modules;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Modules.APIException;
using ResponseHandler = Com.Zoho.Crm.API.Modules.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Modules.ResponseWrapper;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Profiles;
using Com.Zoho.Crm.API.Users;

namespace Samples.Modules1
{
    public class GetModuleByAPIName
    {
        public static void GetModuleByAPIName_1()
        {
            try
            {
                string apiName = "Leads";
                ModulesOperations modulesOperations = new ModulesOperations();
                APIResponse<ResponseHandler> response = modulesOperations.GetModuleByAPIName(apiName);

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
                            List<Modules> modules = responseWrapper.Modules;

                            if (modules != null)
                            {
                                foreach (Modules module in modules)
                                {
                                    Console.WriteLine("Module GlobalSearchSupported: " + module.GlobalSearchSupported);
                                    if (module.KanbanView != null)
                                    {
                                        Console.WriteLine("Module KanbanView: " + module.KanbanView);
                                    }
                                    Console.WriteLine("Module Deletable: " + module.Deletable);
                                    Console.WriteLine("Module Description: " + module.Description);
                                    Console.WriteLine("Module Creatable: " + module.Creatable);
                                    if (module.FilterStatus != null)
                                    {
                                        Console.WriteLine("Module FilterStatus: " + module.FilterStatus);
                                    }
                                    Console.WriteLine("Module InventoryTemplateSupported: " + module.InventoryTemplateSupported);
                                    if (module.ModifiedTime != null)
                                    {
                                        Console.WriteLine("Module ModifiedTime: " + module.ModifiedTime);
                                    }
                                    Console.WriteLine("Module PluralLabel: " + module.PluralLabel);
                                    Console.WriteLine("Module PresenceSubMenu: " + module.PresenceSubMenu);
                                    Console.WriteLine("Module TriggersSupported: " + module.TriggersSupported);
                                    Console.WriteLine("Module Id: " + module.Id);
                                    Console.WriteLine("Module IsBlueprintSupported : " + module.Isblueprintsupported);
                                    RelatedListProperties relatedListProperties = module.RelatedListProperties;
                                    if (relatedListProperties != null)
                                    {
                                        Console.WriteLine("Module RelatedListProperties SortBy: " + relatedListProperties.SortBy);
                                        List<String> fields = relatedListProperties.Fields;
                                        if (fields != null)
                                        {
                                            foreach (Object fieldName in fields)
                                            {
                                                Console.WriteLine("Module RelatedListProperties Fields: " + fieldName);
                                            }
                                        }
                                        Console.WriteLine("Module RelatedListProperties SortOrder: " + relatedListProperties.SortOrder);
                                    }
                                    Console.WriteLine("Module PerPage: " + module.PerPage);
                                    List<String> properties = module.Properties;
                                    if (properties != null)
                                    {
                                        foreach (Object fieldName in properties)
                                        {
                                            Console.WriteLine("Module Properties Fields: " + fieldName);
                                        }
                                    }
                                    Console.WriteLine("Module visible: " + module.Visible);
                                    Console.WriteLine("Module Visibility: " + module.Visibility);
                                    Console.WriteLine("Module Convertable: " + module.Convertable);
                                    Console.WriteLine("Module Editable: " + module.Editable);
                                    Console.WriteLine("Module EmailtemplateSupport: " + module.EmailtemplateSupport);
                                    List<MinifiedProfile> profiles = module.Profiles;
                                    if (profiles != null)
                                    {
                                        foreach (MinifiedProfile profile in profiles)
                                        {
                                            Console.WriteLine("Module Profile Name: " + profile.Name);
                                            Console.WriteLine("Module Profile Id: " + profile.Id);
                                        }
                                    }
                                    Console.WriteLine("Module FilterSupported: " + module.FilterSupported);
                                    List<String> onDemandProperties = module.OnDemandProperties;
                                    if (onDemandProperties != null)
                                    {
                                        foreach (Object fieldName in onDemandProperties)
                                        {
                                            Console.WriteLine("Module onDemandProperties Fields: " + fieldName);
                                        }
                                    }
                                    Console.WriteLine("Module DisplayField: " + module.DisplayField);
                                    List<String> searchLayoutFields = module.SearchLayoutFields;
                                    if (searchLayoutFields != null)
                                    {
                                        foreach (Object fieldName in searchLayoutFields)
                                        {
                                            Console.WriteLine("Module SearchLayoutFields Fields: " + fieldName);
                                        }
                                    }
                                    if (module.KanbanViewSupported != null)
                                    {
                                        Console.WriteLine("Module KanbanViewSupported: " + module.KanbanViewSupported);
                                    }
                                    Console.WriteLine("Module ShowAsTab: " + module.ShowAsTab);
                                    Console.WriteLine("Module WebLink: " + module.WebLink);
                                    Console.WriteLine("Module SequenceNumber: " + module.SequenceNumber);
                                    Console.WriteLine("Module SingularLabel: " + module.SingularLabel);
                                    Console.WriteLine("Module Viewable: " + module.Viewable);
                                    Console.WriteLine("Module APISupported: " + module.APISupported);
                                    Console.WriteLine("Module APIName: " + module.APIName);
                                    Console.WriteLine("Module QuickCreate: " + module.QuickCreate);
                                    MinifiedUser modifiedBy = module.ModifiedBy;
                                    if (modifiedBy != null)
                                    {
                                        Console.WriteLine("Module Modified By User-Name: " + modifiedBy.Name);
                                        Console.WriteLine("Module Modified By User-ID: " + modifiedBy.Id);
                                    }
                                    Console.WriteLine("Module GeneratedType: " + module.GeneratedType.Value);
                                    Console.WriteLine("Module FeedsRequired: " + module.FeedsRequired);
                                    Console.WriteLine("Module ScoringSupported: " + module.ScoringSupported);
                                    Console.WriteLine("Module WebformSupported: " + module.WebformSupported);
                                    List<Argument> arguments = module.Arguments;
                                    if (arguments != null)
                                    {
                                        foreach (Argument argument in arguments)
                                        {
                                            Console.WriteLine("Module Argument Name: " + argument.Name);
                                            Console.WriteLine("Module Argument Value: " + argument.Value);
                                        }
                                    }
                                    Console.WriteLine("Module ModuleName: " + module.ModuleName);
                                    Console.WriteLine("Module BusinessCardFieldLimit: " + module.BusinessCardFieldLimit);
                                    Com.Zoho.Crm.API.CustomViews.CustomViews customView = module.CustomView;
                                    if (customView != null)
                                    {
                                        PrintCustomView(customView);
                                    }
                                    MinifiedModule parentModule = module.ParentModule;
                                    if (parentModule != null)
                                    {
                                        Console.WriteLine("Module Parent Module Name: " + parentModule.APIName);
                                        Console.WriteLine("Module Parent Module Id: " + parentModule.Id);
                                    }

                                    Territory territory = module.Territory;
                                    if (territory != null)
                                    {
                                        Console.WriteLine("Module Territory Name: " + territory.Name);
                                        Console.WriteLine("Module Territory Id: " + territory.Id);

                                        Console.WriteLine("Module Territory Subordinates: " + territory.Subordinates);
                                    }
                                    Console.WriteLine("---------------------------");
                                }
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

        private static void PrintCustomView(Com.Zoho.Crm.API.CustomViews.CustomViews customView)
        {
            Console.WriteLine("Module CustomView DisplayValue: " + customView.DisplayValue);
            if (customView.CreatedTime != null)
            {
                Console.WriteLine("Module CustomView CreatedTime: " + customView.CreatedTime);
            }
            Console.WriteLine("Module CustomView AccessType: " + customView.AccessType);
            Com.Zoho.Crm.API.CustomViews.Criteria criteria = customView.Criteria;
            if (criteria != null)
            {
                PrintCriteria(criteria);
            }
            Console.WriteLine("Module CustomView SystemName: " + customView.SystemName);
            Console.WriteLine("Module CustomView SortBy: " + customView.SortBy);
            Com.Zoho.Crm.API.CustomViews.Owner createdBy = customView.CreatedBy;
            if (createdBy != null)
            {
                Console.WriteLine("Module Created By User-Name: " + createdBy.Name);
                Console.WriteLine("Module Created By User-ID: " + createdBy.Id);
            }
            List<Com.Zoho.Crm.API.CustomViews.SharedTo> sharedToDetails = customView.SharedTo;
            if (sharedToDetails != null)
            {
                foreach (Com.Zoho.Crm.API.CustomViews.SharedTo sharedTo in sharedToDetails)
                {
                    Console.WriteLine("SharedDetails Name: " + sharedTo.Name);
                    Console.WriteLine("SharedDetails ID: " + sharedTo.Id);
                    Console.WriteLine("SharedDetails Type: " + sharedTo.Type);
                    Console.WriteLine("SharedDetails Subordinates: " + sharedTo.Subordinates);
                }
            }
            Console.WriteLine("Module CustomView Default: " + customView.Default);
            if (customView.ModifiedTime != null)
            {
                Console.WriteLine("Module CustomView ModifiedTime: " + customView.ModifiedTime);
            }
            Console.WriteLine("Module CustomView Name: " + customView.Name);
            Console.WriteLine("Module CustomView SystemDefined: " + customView.SystemDefined);
            Com.Zoho.Crm.API.CustomViews.Owner modifiedBy = customView.ModifiedBy;
            if (modifiedBy != null)
            {
                Console.WriteLine("Module Modified By User-Name: " + modifiedBy.Name);
                Console.WriteLine("Module Modified By User-ID: " + modifiedBy.Id);
            }
            Console.WriteLine("Module CustomView ID: " + customView.Id);
            List<Com.Zoho.Crm.API.CustomViews.Fields> fields = customView.Fields;
            if (fields != null)
            {
                foreach (Com.Zoho.Crm.API.CustomViews.Fields field in fields)
                {
                    Console.WriteLine("Module CustomView Field Id: " + field.Id);
                    Console.WriteLine("Module CustomView Field APIName: " + field.APIName);
                }
            }
            Console.WriteLine("Module CustomView Category: " + customView.Category);
            if (customView.LastAccessedTime != null)
            {
                Console.WriteLine("Module CustomView LastAccessedTime: " + customView.LastAccessedTime);
            }
            if (customView.Favorite != null)
            {
                Console.WriteLine("Module CustomView Favorite: " + customView.Favorite);
            }
            if (customView.SortOrder != null)
            {
                Console.WriteLine("Module CustomView SortOrder: " + customView.SortOrder);
            }
        }

        private static void PrintCriteria(Com.Zoho.Crm.API.CustomViews.Criteria criteria)
        {
            if (criteria.Comparator != null)
            {
                Console.WriteLine("CustomView Criteria Comparator: " + criteria.Comparator);
            }
            if (criteria.Field != null)
            {
                Console.WriteLine("CustomView Criteria field name: " + criteria.Field.APIName);
            }
            if (criteria.Value != null)
            {
                Console.WriteLine("CustomView Criteria Value: " + criteria.Value);
            }
            List<Com.Zoho.Crm.API.CustomViews.Criteria> criteriaGroup = criteria.Group;
            if (criteriaGroup != null)
            {
                foreach (Com.Zoho.Crm.API.CustomViews.Criteria criteria1 in criteriaGroup)
                {
                    PrintCriteria(criteria1);
                }
            }
            if (criteria.GroupOperator != null)
            {
                Console.WriteLine("CustomView Criteria Group Operator: " + criteria.GroupOperator);
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

                GetModuleByAPIName_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}