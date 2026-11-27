using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Territories;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;

namespace Samples.Territories_1
{
    public class GetTerritories
    {
        public static void GetTerritories_1()
        {
            try
            {
                TerritoriesOperations territoriesOperations = new TerritoriesOperations();

                // Create parameter map for filtering and pagination
                ParameterMap paramInstance = new ParameterMap();

                // Optional parameters - uncomment as needed
                // paramInstance.Add(TerritoriesOperations.GetTerritoriesParam.PAGE, 1);
                // paramInstance.Add(TerritoriesOperations.GetTerritoriesParam.PER_PAGE, 200);
                // paramInstance.Add(TerritoriesOperations.GetTerritoriesParam.INCLUDE, "criteria");
                // paramInstance.Add(TerritoriesOperations.GetTerritoriesParam.FILTERS, "name:starts_with:Territory");
                // paramInstance.Add(TerritoriesOperations.GetTerritoriesParam.IDS, "554023000000567023,554023000000567024");

                // Call API
                APIResponse<ResponseHandler> response = territoriesOperations.GetTerritories(paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.StatusCode == 204)
                    {
                        Console.WriteLine("No Content - No territories found");
                        return;
                    }

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<Territories> territories = responseWrapper.Territories;

                            Console.WriteLine($"Retrieved {territories?.Count ?? 0} territory(ies):");

                            if (territories != null && territories.Count > 0)
                            {
                                foreach (Territories territory in territories)
                                {
                                    Console.WriteLine("\n--- Territory Details ---");
                                    Console.WriteLine("Territory ID: " + territory.Id);
                                    Console.WriteLine("Territory Name: " + territory.Name);
                                    Console.WriteLine("Description: " + territory.Description);

                                    if (territory.Manager != null)
                                    {
                                        Console.WriteLine("Territory Manager User-ID: " + territory.Manager.Id);
                                        Console.WriteLine("Territory Manager User-Name: " + territory.Manager.Name);
                                    }

                                    if (territory.ReportingTo != null)
                                    {
                                        Console.WriteLine("Reporting To: " + territory.ReportingTo.Name + " (ID: " + territory.ReportingTo.Id + ")");
                                    }

                                    if (territory.AccountRuleCriteria != null)
                                    {
                                        PrintCriteria(territory.AccountRuleCriteria);
                                    }

                                    if (territory.DealRuleCriteria != null)
                                    {
                                        PrintCriteria(territory.DealRuleCriteria);
                                    }

                                    if (territory.LeadRuleCriteria != null)
                                    {
                                        PrintCriteria(territory.LeadRuleCriteria);
                                    }

                                    if (territory.CreatedBy != null)
                                    {
                                        Console.WriteLine("Created By: " + territory.CreatedBy.Name + " (ID: " + territory.CreatedBy.Id + ")");
                                    }

                                    if (territory.ModifiedBy != null)
                                    {
                                        Console.WriteLine("Modified By: " + territory.ModifiedBy.Name + " (ID: " + territory.ModifiedBy.Id + ")");
                                    }

                                    if (territory.CreatedTime != null)
                                    {
                                        Console.WriteLine("Created Time: " + territory.CreatedTime);
                                    }

                                    if (territory.ModifiedTime != null)
                                    {
                                        Console.WriteLine("Modified Time: " + territory.ModifiedTime);
                                    }

                                    Console.WriteLine("---");
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
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }

        private static void PrintCriteria(Criteria criteria)
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
            List<Criteria> criteriaGroup = criteria.Group;
            if (criteriaGroup != null)
            {
                foreach (Criteria criteria1 in criteriaGroup)
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
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();

                // Get all territories
                GetTerritories_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}