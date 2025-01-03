using System;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Com.Zoho.Crm.API.Dc;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Functions;
using Com.Zoho.Crm.API.Util;
using Newtonsoft.Json;

namespace Samples.Functions
{
    public class ExecuteFunctionUsingFile
    {
        public static void ExecuteFunctionUsingFile_1()
        {
            string functionName = "get_record_lead";
            string authType = "oauth";

            FunctionsOperations functionsOperations = new FunctionsOperations(functionName, authType, null);

            FileBodyWrapper fileBodyWrapper = new FileBodyWrapper();
            StreamWrapper streamWrapper = new StreamWrapper("./sample.txt");
            fileBodyWrapper.Inputfile = streamWrapper;

            ParameterMap paramInstance = new ParameterMap();
            Dictionary<string, string> param = new Dictionary<string, string>
            {
                { "1221", "2111221" },
                { "15221", "21113221" },
                { "12421", "211341221" }
            };
            paramInstance.Add(new Param<Dictionary<string, string>>("Java", new Dictionary<string, string>().GetType().FullName), param);
            HeaderMap headerInstance = new HeaderMap();
            APIResponse<ResponseWrapper> response = functionsOperations.ExecuteFunctionUsingFile(fileBodyWrapper, paramInstance, headerInstance);
            if (response != null)
            {
                Console.WriteLine("Status Code: " + response.StatusCode);
                if (response.IsExpected)
                {
                    ResponseWrapper responseWrapper = response.Object;
                    if (responseWrapper is SuccessResponse)
                    {
                        SuccessResponse successResponse = (SuccessResponse)responseWrapper;
                        Console.WriteLine("Code: " + successResponse.Code.Value);
                        Console.WriteLine("Details: ");
                        foreach (KeyValuePair<string, object> entry in successResponse.Details)
                        {
                            Console.WriteLine(entry.Key + ": " + entry.Value);
                        }
                        Console.WriteLine("Message: " + successResponse.Message.Value);
                    }
                    else if (responseWrapper is APIException)
                    {
                        APIException exception = (APIException)responseWrapper;
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
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                ExecuteFunctionUsingFile_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}
