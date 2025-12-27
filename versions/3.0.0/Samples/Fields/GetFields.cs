using System;
using Com.Zoho.Crm.API;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Fields;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using Com.Zoho.Crm.API.Modules;
using ResponseHandler = Com.Zoho.Crm.API.Fields.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Fields.ResponseWrapper;
using LookupField = Com.Zoho.Crm.API.Fields.LookupField;
using APIException = Com.Zoho.Crm.API.Fields.APIException;

namespace Samples.Fields
{
    public class GetFields
    {
        /// <summary>
        /// This method is used to get metadata about all the fields of a module.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module to get fields</param>
        public static void GetFieldsMethod(string moduleAPIName)
        {
            FieldsOperations fieldsOperations = new FieldsOperations();
            ParameterMap paramInstance = new ParameterMap();
            paramInstance.Add(FieldsOperations.GetFieldsParam.MODULE, moduleAPIName);

            try
            {
                APIResponse<ResponseHandler> response = fieldsOperations.GetFields(paramInstance);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;
                            List<Com.Zoho.Crm.API.Fields.Fields> fields = responseWrapper.Fields;

                            foreach (Com.Zoho.Crm.API.Fields.Fields field in fields)
                            {
                                Console.WriteLine("Field SystemMandatory: " + field.SystemMandatory);
                                Console.WriteLine("Field Webhook: " + field.Webhook);
                                Console.WriteLine("Field JsonType: " + field.JsonType);

                                Private privateInfo = field.Private;

                                if (privateInfo != null)
                                {
                                    Console.WriteLine("Private Details:");
                                    Console.WriteLine("Type: " + privateInfo.Type);
                                    Console.WriteLine("Export: " + privateInfo.Export);
                                    Console.WriteLine("Restricted: " + privateInfo.Restricted);
                                }

                                Console.WriteLine("Field DisplayLabel: " + field.DisplayLabel);
                                Console.WriteLine("Field DataType: " + field.DataType);
                                Console.WriteLine("Field ColumnName: " + field.ColumnName);
                                Console.WriteLine("Field ID: " + field.Id);
                                Console.WriteLine("Field QuickSequenceNumber: " + field.QuickSequenceNumber);

                                if (field.PickListValues != null)
                                {
                                    List<PickListValue> pickListValues = field.PickListValues;

                                    if (pickListValues.Count > 0)
                                    {
                                        foreach (PickListValue pickListValue in pickListValues)
                                        {
                                            Console.WriteLine("Field PickListValue DisplayValue: " + pickListValue.DisplayValue);
                                            Console.WriteLine("Field PickListValue SequenceNumber: " + pickListValue.SequenceNumber);
                                            Console.WriteLine("Field PickListValue ExpectedDataType: " + pickListValue.ExpectedDataType);
                                            Console.WriteLine("Field PickListValue Maps: ");

                                            if (pickListValue.Maps != null)
                                            {
                                                foreach (object map in pickListValue.Maps)
                                                {
                                                    Console.WriteLine(map);
                                                }
                                            }

                                            Console.WriteLine("Field PickListValue ActualValue: " + pickListValue.ActualValue);
                                            Console.WriteLine("Field PickListValue SysRefName: " + pickListValue.SysRefName);
                                            Console.WriteLine("Field PickListValue Type: " + pickListValue.Type);
                                            Console.WriteLine("Field PickListValue Id: " + pickListValue.Id);
                                        }
                                    }
                                }

                                AutoNumber autoNumber = field.AutoNumber61;

                                if (autoNumber != null)
                                {
                                    Console.WriteLine("Field AutoNumber Prefix: " + autoNumber.Prefix);
                                    Console.WriteLine("Field AutoNumber Suffix: " + autoNumber.Suffix);
                                    Console.WriteLine("Field AutoNumber StartNumber: " + autoNumber.StartNumber);
                                }

                                Console.WriteLine("Field DefaultValue: " + field.DefaultValue);
                                Console.WriteLine("Field APIName: " + field.APIName);
                                Console.WriteLine("Field Unique: " + field.Unique);

                                if (field.HistoryTracking != null)
                                {
                                    Console.WriteLine("Field HistoryTracking: " + field.HistoryTracking);
                                }

                                Console.WriteLine("Field ReadOnly: " + field.ReadOnly);
                                AssociationDetails associationDetails = field.AssociationDetails;

                                if (associationDetails != null)
                                {
                                    LookupField lookupField = associationDetails.LookupField;

                                    if (lookupField != null)
                                    {
                                        Console.WriteLine("Field AssociationDetails LookupField ID: " + lookupField.Id);
                                        Console.WriteLine("Field AssociationDetails LookupField Name: " + lookupField.APIName);
                                    }

                                    LookupField relatedField = associationDetails.RelatedField;

                                    if (relatedField != null)
                                    {
                                        Console.WriteLine("Field AssociationDetails RelatedField ID: " + relatedField.Id);
                                        Console.WriteLine("Field AssociationDetails RelatedField Name: " + relatedField.APIName);
                                    }
                                }

                                if (field.QuickSequenceNumber != null)
                                {
                                    Console.WriteLine("Field QuickSequenceNumber: " + field.QuickSequenceNumber);
                                }

                                Console.WriteLine("Field CustomField: " + field.CustomField);
                                Console.WriteLine("Field Visible: " + field.Visible);
                                Console.WriteLine("Field Length: " + field.Length);
                                Console.WriteLine("Field DecimalPlace: " + field.DecimalPlace);

                                ViewType viewType = field.ViewType;

                                if (viewType != null)
                                {
                                    Console.WriteLine("Field ViewType View: " + viewType.View);
                                    Console.WriteLine("Field ViewType Edit: " + viewType.Edit);
                                    Console.WriteLine("Field ViewType Create: " + viewType.Create);
                                    Console.WriteLine("Field ViewType QuickCreate: " + viewType.QuickCreate);
                                }

                                MinifiedModule subform = field.AssociatedModule;

                                if (subform != null)
                                {
                                    Console.WriteLine("Field Subform Module: " + subform.Module);
                                    Console.WriteLine("Field Subform ID: " + subform.Id);
                                }

                                Console.WriteLine("Field APIName: " + field.APIName);
                                Console.WriteLine("------------------------");
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

                        Console.WriteLine("Values : ");

                        foreach (PropertyInfo prop in props)
                        {
                            if (prop.GetIndexParameters().Length == 0)
                            {
                                Console.WriteLine("{0} : {1}", prop.Name, prop.GetValue(responseObject));
                            }
                            else
                            {
                                Console.WriteLine("{0} : {1}", prop.Name, "Indexed Property");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }

        public static void Call()
        {
            try
            {
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();
                string moduleAPIName = "Contacts";
                GetFieldsMethod(moduleAPIName);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}