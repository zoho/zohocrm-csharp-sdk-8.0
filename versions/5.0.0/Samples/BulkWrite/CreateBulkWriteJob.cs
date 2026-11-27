using System;
using System.Reflection;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API;
using APIException = Com.Zoho.Crm.API.BulkWrite.APIException;
using BulkWriteOperations = Com.Zoho.Crm.API.BulkWrite.BulkWriteOperations;
using RequestWrapper = Com.Zoho.Crm.API.BulkWrite.RequestWrapper;
using ActionResponse = Com.Zoho.Crm.API.BulkWrite.ActionResponse;
using SuccessResponse = Com.Zoho.Crm.API.BulkWrite.SuccessResponse;
using Resource = Com.Zoho.Crm.API.BulkWrite.Resource;
using CallBack = Com.Zoho.Crm.API.BulkWrite.CallBack;
using FieldMapping = Com.Zoho.Crm.API.BulkWrite.FieldMapping;
using DefaultValue = Com.Zoho.Crm.API.BulkWrite.DefaultValue;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using Com.Zoho.Crm.API.Modules;

namespace Samples.BulkWrite
{
    public class CreateBulkWriteJob
    {
        public static void CreateBulkWriteJob_1()
        {
            BulkWriteOperations bulkWriteOperations = new BulkWriteOperations();
            RequestWrapper requestWrapper = new RequestWrapper();

            requestWrapper.Operation = new Choice<string>("insert");

            CallBack callBack = new CallBack();
            callBack.Url = "https://www.example.com/callback";
            callBack.Method = new Choice<string>("post");
            requestWrapper.Callback = callBack;

            List<Resource> resources = new List<Resource>();
            Resource resource = new Resource();
            resource.Type = new Choice<string>("data");
            MinifiedModule module = new MinifiedModule();
            module.APIName = "Leads";
            resource.Module = module;
            resource.FileId = "1055806000028582001";
            resource.IgnoreEmpty = true;
            //resource.FindBy = "Email";

            List<FieldMapping> fieldMappings = new List<FieldMapping>();

            FieldMapping fieldMapping1 = new FieldMapping();
            fieldMapping1.APIName = "Last_Name";
            fieldMapping1.Index = 0;
            fieldMappings.Add(fieldMapping1);

            FieldMapping fieldMapping2 = new FieldMapping();
            fieldMapping2.APIName = "First_Name";
            fieldMapping2.Index = 1;
            fieldMappings.Add(fieldMapping2);

            FieldMapping fieldMapping3 = new FieldMapping();
            fieldMapping3.APIName = "Company";
            fieldMapping3.Index = 2;
            fieldMappings.Add(fieldMapping3);

            FieldMapping fieldMapping4 = new FieldMapping();
            fieldMapping4.APIName = "Email";
            fieldMapping4.Index = 3;
            fieldMappings.Add(fieldMapping4);

            FieldMapping fieldMapping5 = new FieldMapping();
            fieldMapping5.APIName = "Phone";
            fieldMapping5.Index = 4;
            fieldMappings.Add(fieldMapping5);

            FieldMapping fieldMapping6 = new FieldMapping();
            fieldMapping6.APIName = "Lead_Status";
            DefaultValue defaultValue = new DefaultValue();
            defaultValue.Value = "Not Contacted";
            fieldMapping6.DefaultValue = defaultValue;
            fieldMappings.Add(fieldMapping6);

            resource.FieldMappings = fieldMappings;
            resources.Add(resource);
            requestWrapper.Resource = resources;

            APIResponse<ActionResponse> response = bulkWriteOperations.CreateBulkWriteJob(requestWrapper);

            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ActionResponse actionResponse = response.Object;
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
                            Console.WriteLine(entry.Key + ": " + entry.Value);
                        }
                        Console.WriteLine("Message: " + exception.Message.Value);
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
                CreateBulkWriteJob_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}