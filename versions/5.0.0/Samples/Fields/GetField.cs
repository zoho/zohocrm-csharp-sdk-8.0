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
using ResponseHandler = Com.Zoho.Crm.API.Fields.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Fields.ResponseWrapper;
using LookupField = Com.Zoho.Crm.API.Fields.LookupField;
using APIException = Com.Zoho.Crm.API.Fields.APIException;
using Com.Zoho.Crm.API.Modules;

namespace Samples.Fields
{
    public class GetField
    {
        /// <summary>
        /// This method is used to get metadata about a specific field of a module.
        /// </summary>
        /// <param name="moduleAPIName">The API Name of the module</param>
        /// <param name="fieldId">The ID of the field</param>
        public static void GetFieldMethod(string moduleAPIName, long fieldId)
        {
            FieldsOperations fieldsOperations = new FieldsOperations();
            ParameterMap paramInstance = new ParameterMap();
            paramInstance.Add(FieldsOperations.GetFieldParam.MODULE, moduleAPIName);

            try
            {
                APIResponse<ResponseHandler> response = fieldsOperations.GetField(fieldId, paramInstance);

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
                                Console.WriteLine("Field ID: " + field.Id);
                                Console.WriteLine("Field API Name: " + field.APIName);
                                Console.WriteLine("Field DisplayLabel: " + field.DisplayLabel);
                                Console.WriteLine("Field DataType: " + field.DataType);
                                Console.WriteLine("Field ColumnName: " + field.ColumnName);
                                Console.WriteLine("Field SystemMandatory: " + field.SystemMandatory);
                                Console.WriteLine("Field CustomField: " + field.CustomField);
                                Console.WriteLine("Field Visible: " + field.Visible);
                                Console.WriteLine("Field ReadOnly: " + field.ReadOnly);
                                Console.WriteLine("Field Length: " + field.Length);
                                Console.WriteLine("Field DecimalPlace: " + field.DecimalPlace);
                                Console.WriteLine("Field JsonType: " + field.JsonType);
                                Console.WriteLine("Field DefaultValue: " + field.DefaultValue);
                                Console.WriteLine("Field QuickSequenceNumber: " + field.QuickSequenceNumber);
                                Console.WriteLine("Field Unique: " + field.Unique);
                                Console.WriteLine("Field Webhook: " + field.Webhook);

                                Private privateInfo = field.Private;

                                if (privateInfo != null)
                                {
                                    Console.WriteLine("Private Details:");
                                    Console.WriteLine("Type: " + privateInfo.Type);
                                    Console.WriteLine("Export: " + privateInfo.Export);
                                    Console.WriteLine("Restricted: " + privateInfo.Restricted);
                                }

                                Tooltip tooltip = field.Tooltip;
                                if (tooltip != null)
                                {
                                    Console.WriteLine("Tooltip Name: " + tooltip.Name);
                                    Console.WriteLine("Tooltip Value: " + tooltip.Value);
                                }

                                if (field.PickListValues != null)
                                {
                                    List<PickListValue> pickListValues = field.PickListValues;

                                    Console.WriteLine("PickList Values:");
                                    foreach (PickListValue pickListValue in pickListValues)
                                    {
                                        Console.WriteLine("  ID: " + pickListValue.Id);
                                        Console.WriteLine("  DisplayValue: " + pickListValue.DisplayValue);
                                        Console.WriteLine("  ActualValue: " + pickListValue.ActualValue);
                                        Console.WriteLine("  SequenceNumber: " + pickListValue.SequenceNumber);
                                        Console.WriteLine("  Type: " + pickListValue.Type);
                                        Console.WriteLine("  SysRefName: " + pickListValue.SysRefName);
                                        Console.WriteLine("  ExpectedDataType: " + pickListValue.ExpectedDataType);
                                        Console.WriteLine("  ---");
                                    }
                                }

                                AutoNumber autoNumber = field.AutoNumber61;
                                if (autoNumber != null)
                                {
                                    Console.WriteLine("AutoNumber Details:");
                                    Console.WriteLine("Prefix: " + autoNumber.Prefix);
                                    Console.WriteLine("Suffix: " + autoNumber.Suffix);
                                    Console.WriteLine("StartNumber: " + autoNumber.StartNumber);
                                }

                                Formula formula = field.Formula;
                                if (formula != null)
                                {
                                    Console.WriteLine("Formula Details:");
                                    Console.WriteLine("ReturnType: " + formula.ReturnType);
                                    Console.WriteLine("Expression: " + formula.Expression);
                                }

                                Currency currencyDetail = field.Currency;
                                if (currencyDetail != null)
                                {
                                    Console.WriteLine("Precision: " + currencyDetail.Precision);
                                }

                                Lookup lookupField = field.Lookup;
                                if (lookupField != null)
                                {
                                    Console.WriteLine("Lookup Details:");
                                    Console.WriteLine("ID: " + lookupField.Id);
                                    Console.WriteLine("Name: " + lookupField.APIName);
                                }

                                AssociationDetails associationDetails = field.AssociationDetails;
                                if (associationDetails != null)
                                {
                                    Console.WriteLine("Association Details:");

                                    LookupField associationLookup = associationDetails.LookupField;
                                    if (associationLookup != null)
                                    {
                                        Console.WriteLine("LookupField ID: " + associationLookup.Id);
                                        Console.WriteLine("LookupField Name: " + associationLookup.APIName);
                                    }

                                    LookupField relatedField = associationDetails.RelatedField;
                                    if (relatedField != null)
                                    {
                                        Console.WriteLine("RelatedField ID: " + relatedField.Id);
                                        Console.WriteLine("RelatedField Name: " + relatedField.APIName);
                                    }
                                }

                                ViewType viewType = field.ViewType;
                                if (viewType != null)
                                {
                                    Console.WriteLine("ViewType Details:");
                                    Console.WriteLine("View: " + viewType.View);
                                    Console.WriteLine("Edit: " + viewType.Edit);
                                    Console.WriteLine("Create: " + viewType.Create);
                                    Console.WriteLine("QuickCreate: " + viewType.QuickCreate);
                                }

                                MinifiedModule subform = field.AssociatedModule;
                                if (subform != null)
                                {
                                    Console.WriteLine("Subform Details:");
                                    Console.WriteLine("Module: " + subform.Module);
                                    Console.WriteLine("ID: " + subform.Id);
                                }

                                if (field.HistoryTracking != null)
                                {
                                    Console.WriteLine("HistoryTracking: " + field.HistoryTracking);
                                }

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
                long fieldId = 181292626l;
                GetFieldMethod(moduleAPIName, fieldId);
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}