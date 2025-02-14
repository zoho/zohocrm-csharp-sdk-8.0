﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API.Dc;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.SharingRules;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using Module = Com.Zoho.Crm.API.SharingRules.Module;

namespace csharpsdksampleapplication.Samples.SharingRules1
{
    public class SearchSharingRulesSummary
    {
        public static void SearchSharingRulesSummary_1(String moduleAPIName)
        {
            SharingRulesOperations sharingRulesOperations = new SharingRulesOperations(moduleAPIName);
            FiltersBody request = new FiltersBody();
            Criteria criteria = new Criteria();
            criteria.GroupOperator = "and";
            List<Criteria> group = new List<Criteria>();

            Criteria groupCriteria1 = new Criteria();
            Field field1 = new Field();
            field1.APIName = "shared_from.type";
            groupCriteria1.Field = field1;
            groupCriteria1.Value = "${EMPTY}";
            groupCriteria1.Comparator = "equal";
            group.Add(groupCriteria1);


            Criteria groupCriteria2 = new Criteria();
            Field field2 = new Field();
            field2.APIName = "superiors_allowed";
            groupCriteria2.Field = field2;
            groupCriteria2.Value = "false";
            groupCriteria2.Comparator = "equal";
            group.Add(groupCriteria2);

            Criteria groupCriteria3 = new Criteria();
            Field field3 = new Field();
            field3.APIName = "status";
            groupCriteria3.Field = field3;
            groupCriteria3.Value = "active";
            groupCriteria3.Comparator = "equal";
            group.Add(groupCriteria3);


            Criteria groupCriteria4 = new Criteria();
            groupCriteria4.GroupOperator = "or";

            List<Criteria> group4 = new List<Criteria>();

            Criteria group4Criteria1 = new Criteria();
            group4Criteria1.GroupOperator = "and";

            List<Criteria> group41 = new List<Criteria>();

            Criteria group41Criteria1 = new Criteria();
            Field group41Criteria1field1 = new Field();
            group41Criteria1field1.APIName = "shared_to.resource.id";
            group41Criteria1.Field = group41Criteria1field1;
            group41Criteria1.Value = new List<long>() { 1111078, 111117098 };
            group41Criteria1.Comparator = "in";
            group41.Add(group41Criteria1);

            Criteria group41Criteria2 = new Criteria();
            Field group41Criteria1field2 = new Field();
            group41Criteria1field2.APIName = "shared_to.type";
            group41Criteria2.Field = group41Criteria1field2;
            group41Criteria2.Value = "groups";
            group41Criteria2.Comparator = "equal";
            group41.Add(group41Criteria2);

            group4Criteria1.Group = group41;
            group4.Add(group4Criteria1);


            Criteria group4Criteria2 = new Criteria();
            group4Criteria2.GroupOperator = "and";

            List<Criteria> group42 = new List<Criteria>();

            Criteria group42Criteria1 = new Criteria();
            Field group42Criteria1field1 = new Field();
            group42Criteria1field1.APIName = "shared_to.resource.id";
            group42Criteria1.Field = group42Criteria1field1;
            group42Criteria1.Value = new List<long>() { 111117078, 111198 };
            group42Criteria1.Comparator = "in";
            group42.Add(group42Criteria1);

            Criteria group42Criteria2 = new Criteria();
            Field group42Criteria1field2 = new Field();
            group42Criteria1field2.APIName = "shared_to.type";
            group42Criteria2.Field = group42Criteria1field2;
            group42Criteria2.Value = "roles";
            group42Criteria2.Comparator = "equal";
            group42.Add(group42Criteria2);

            group4Criteria2.Group = group42;
            group4.Add(group4Criteria2);

            groupCriteria4.Group = group4;
            group.Add(groupCriteria4);

            criteria.Group = group;

            request.Filters = new List<Criteria>() { criteria };
            APIResponse<SummaryResponseHandler> response = sharingRulesOperations.SearchSharingRulesSummary(request);
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
                    SummaryResponseHandler responseHandler = response.Object;
                    if (responseHandler is SummaryResponseWrapper)
                    {
                        SummaryResponseWrapper responseWrapper = (SummaryResponseWrapper)responseHandler;
                        List<RulesSummary> rulesSummary = responseWrapper.SharingRulesSummary;
                        foreach (RulesSummary ruleSummary in rulesSummary)
                        {
                            Module module = ruleSummary.Module;
                            if (module != null)
                            {
                                Console.WriteLine("RulesSummary Module APIName: " + module.APIName);
                                Console.WriteLine("RulesSummary Module Id: " + module.Id);
                            }
                            Console.WriteLine("RulesSummary RuleComputationStatus: " + ruleSummary.RuleComputationStatus);
                            Console.WriteLine("RulesSummary RuleCount: " + ruleSummary.RuleCount);
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
                else if (response.StatusCode != 204)
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
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                String moduleAPIName = "Leads";
                SearchSharingRulesSummary_1(moduleAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}
