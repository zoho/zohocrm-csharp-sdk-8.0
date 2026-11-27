using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Record;
using Com.Zoho.Crm.API.Tags;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using BodyWrapper = Com.Zoho.Crm.API.Record.BodyWrapper;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;

namespace Samples.Shifthours
{
    public class CreateRecords
    {
        //public static void Main(string[] args)
        //{
        //    Environment environment = USDataCenter.PRODUCTION;
        //    IToken token = new OAuthToken.Builder().ClientId("ClientId").ClientSecret("ClientSecret").RefreshToken("RefreshToken").RedirectURL("Redirect_URL").Build();
        //    new Initializer.Builder().Environment(environment).Token(token).Initialize();
        //    CreateRecords_1("Leads");
        //}
        public static void CreateRecords_1(string moduleAPIName)
        {
            RecordOperations recordOperations = new RecordOperations(moduleAPIName);
            BodyWrapper bodyWrapper = new BodyWrapper();
            List<Com.Zoho.Crm.API.Record.Record> records = new List<Com.Zoho.Crm.API.Record.Record>();
            Com.Zoho.Crm.API.Record.Record record1 = new Com.Zoho.Crm.API.Record.Record();
            record1.AddFieldValue(Leads.CITY, "City");
            record1.AddFieldValue(Leads.LAST_NAME, "Last Name");
            //record1.AddFieldValue(Leads.FIRST_NAME, "First Name");
            //record1.AddFieldValue(Leads.COMPANY, "KKRNP");
            //List<Tag> tagList = new List<Tag>();
            //Tag tag = new Tag();
            //tag.Name = "Testtask";
            //tagList.Add(tag);
            //record1.Tag = tagList;

            //Com.Zoho.Crm.API.Record.Record subform = new Com.Zoho.Crm.API.Record.Record();
            //subform.AddFieldValue(Leads.CITY, "");
            //subform.AddFieldValue(Leads.LAST_NAME, "Last Name");
            //subform.AddFieldValue(Leads.FIRST_NAME, "First Name");
            //subform.AddFieldValue(Leads.COMPANY, "");
            //List<Com.Zoho.Crm.API.Record.Record> subformlist = new List<Com.Zoho.Crm.API.Record.Record>();
            //subformlist.Add(subform);
            //record1.AddKeyValue("Subform", subformlist);
            
            record1.AddKeyValue("Total_Meetings_Created", "1");
            records.Add(record1);
            bodyWrapper.Data = records;
            HeaderMap headerInstance = new HeaderMap();
            APIResponse<ActionHandler> response = recordOperations.UpsertRecords(bodyWrapper, headerInstance);
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
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                                }
                                Console.WriteLine("Message: " + successResponse.Message.Value);
                            }
                            else if (actionResponse is APIException)
                            {
                                APIException exception = (APIException)actionResponse;
                                Console.WriteLine("Status: " + exception.Status.Value);
                                Console.WriteLine("Code: " + exception.Code.Value);
                                Console.WriteLine("Details: ");
                                foreach (KeyValuePair<string, object> entry in exception.Details)
                                {
                                    Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
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
                        foreach (KeyValuePair<string, object> entry in exception.Details)
                        {
                            Console.WriteLine(entry.Key + ": " + JsonConvert.SerializeObject(entry.Value));
                        }
                        Console.WriteLine("Message: " + exception.Message.Value);
                    }
                }
                else
                {
                    Model responseObject = response.Model;
                    System.Type type = responseObject.GetType();
                    Console.WriteLine("Type is: {0}", type.Name);
                    PropertyInfo[] props = type.GetProperties();
                    Console.WriteLine("Properties (N = {0}):", props.Length);
                    foreach (var prop in props)
                    {
                        if (prop.GetIndexParameters().Length == 0)
                        {
                            Console.WriteLine("{0} ({1}) : {2}", prop.Name, prop.PropertyType.Name, prop.GetValue(responseObject));
                        }
                        else
                        {
                            Console.WriteLine("{0} ({1}) : <Indexed>", prop.Name, prop.PropertyType.Name);
                        }
                    }
                }
            }
        }


        //    public static void CreateRecords_1()
        //    {
        //        RecordOperations recordOperations = new RecordOperations("Accounts");
        //        HeaderMap headerInstance = new HeaderMap();
        //        BodyWrapper bodyWrapper = new BodyWrapper();

        //        bodyWrapper.Data = new List<Com.Zoho.Crm.API.Record.Record>() { new CreateRecords().AccountRecord() };

        //        APIResponse<ActionHandler> response = recordOperations.UpsertRecords(bodyWrapper, null);
        //    }

        //    private Com.Zoho.Crm.API.Record.Record AccountRecord()
        //    {
        //        Com.Zoho.Crm.API.Record.Record resultAccount = new Com.Zoho.Crm.API.Record.Record();
        //        List<Com.Zoho.Crm.API.Record.Record> resultAccountSiteInfo = new List<Com.Zoho.Crm.API.Record.Record>();

        //        resultAccount.AddKeyValue("Owner", new MinifiedUser { Email = "abuyukliev@sapsservices.com" });
        //        if (!string.IsNullOrEmpty("Restaurant"))
        //            resultAccount.AddKeyValue("Industry", new Choice<string>("Restaurant"));

        //        resultAccount.AddKeyValue("Phone", 1112225796);
        //        resultAccount.AddKeyValue("Account_Name", "EASTERN EUROPEAN RESTAURANT");
        //        resultAccount.AddKeyValue("Business_Name", "EASTERN EUROPEAN RESTAURANT");

        //        resultAccount.AddKeyValue("Alternate_Phone", "2221115796");
        //        resultAccount.AddKeyValue("Reference_Number", 2619707);
        //        resultAccount.AddKeyValue("Email", "55796@test.com");
        //        resultAccount.AddKeyValue("Website", "");
        //        resultAccount.AddKeyValue("Contact_Type", new Choice<string>("In Person"));

        //        Com.Zoho.Crm.API.Record.Record currentSiteInfo = new Com.Zoho.Crm.API.Record.Record();
        //        currentSiteInfo.AddKeyValue("Parent_Id", new MinifiedUser { Id = 609596905476020 });
        //        currentSiteInfo.AddKeyValue("Site_Number", "");
        //        currentSiteInfo.AddKeyValue("Type", "Gas");
        //        currentSiteInfo.AddKeyValue("Status", "");
        //        currentSiteInfo.AddKeyValue("Account_Number", "");
        //        currentSiteInfo.AddKeyValue("Supplier", "");
        //        currentSiteInfo.AddKeyValue("Address", "");
        //        //currentSiteInfo.AddKeyValue("Signup", Convert.ToDateTime(siteInfo.Signup).ToString("yyyy-MM-dd"));
        //        currentSiteInfo.AddKeyValue("Signup", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy-MM-dd"));
        //        currentSiteInfo.AddKeyValue("Flow", Convert.ToDateTime(DateTime.UtcNow).ToString("yyyy-MM-dd"));

        //        resultAccountSiteInfo.Add(currentSiteInfo);

        //        resultAccount.AddKeyValue("Site_Info", resultAccountSiteInfo);

        //        return resultAccount;
        //    }
        //}
    }
}