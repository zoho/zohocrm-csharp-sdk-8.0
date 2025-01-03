﻿using System;
using Newtonsoft.Json;
using Com.Zoho.API.Authenticator;
using Initializer = Com.Zoho.Crm.API.Initializer;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.DataSharing;
using Com.Zoho.Crm.API.Util;
using System.Reflection;
using System.Collections.Generic;
using Module = Com.Zoho.Crm.API.DataSharing.Module;
using Com.Zoho.Crm.API.Dc;

namespace csharpsdksampleapplication.Samples.DataSharing1
{
	public class GetDataSharing
	{
		public static void GetDataSharing_1()
		{
			DataSharingOperations dataSharingOperations = new DataSharingOperations();
			APIResponse<ResponseHandler> response = dataSharingOperations.GetDataSharing();
			if (response != null)
			{
				Console.WriteLine ("Status Code: " + response.StatusCode);
				if (response.IsExpected)
				{
					ResponseHandler responseHandler = response.Object;
					if (responseHandler is ResponseWrapper)
					{
						ResponseWrapper responseWrapper = (ResponseWrapper) responseHandler;
						List<DataSharing> dataSharing = responseWrapper.DataSharing;
						foreach (DataSharing dataSharing1 in dataSharing)
						{
							Console.WriteLine ("DataSharing PublicInPortals: " + dataSharing1.PublicInPortals);
							Console.WriteLine ("DataSharing ShareType: " + dataSharing1.ShareType.Value);
							Module module = dataSharing1.Module;
							if(module != null)
							{
								Console.WriteLine ("DataSharing Module APIName: " + module.APIName);
								Console.WriteLine ("DataSharing Module Id: " + module.Id);
							}
							Console.WriteLine ("DataSharing RuleComputationRunning: " + dataSharing1.RuleComputationRunning);
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
                GetDataSharing_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
	}
}