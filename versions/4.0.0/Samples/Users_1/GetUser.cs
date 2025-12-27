using System;
using System.Collections.Generic;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Users;
using Com.Zoho.Crm.API.Util;
using Com.Zoho.API.Authenticator;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using Com.Zoho.Crm.API.Dc;
using Newtonsoft.Json;
using static Com.Zoho.Crm.API.Users.UsersOperations;
using System.Collections;

namespace Samples.Users_1
{
    public class GetUser
    {
        public static void GetUser_1()
        {
            try
            {
                long userId = 1055806000028632001L; // Replace with actual user ID

                UsersOperations usersOperations = new UsersOperations();

                HeaderMap headerMap = new HeaderMap();

                // Add If-Modified-Since header for conditional request
                DateTimeOffset ifModifiedSince = DateTimeOffset.Now.AddDays(-30);
                headerMap.Add(GetUserHeader.IF_MODIFIED_SINCE, ifModifiedSince);

                // Call API
                APIResponse<ResponseHandler> response = usersOperations.GetUser(userId, headerMap);

                if (response != null)
                {
                    Console.WriteLine("Status Code: " + response.StatusCode);

                    if (response.StatusCode == 304)
                    {
                        Console.WriteLine("Data not modified since " + ifModifiedSince);
                        return;
                    }

                    if (response.IsExpected)
                    {
                        ResponseHandler responseHandler = response.Object;

                        if (responseHandler is ResponseWrapper)
                        {
                            ResponseWrapper responseWrapper = (ResponseWrapper)responseHandler;

                            List<Users> users = responseWrapper.Users;

                            if (users != null && users.Count > 0)
                            {
                                Console.WriteLine($"\n=== User Details ===");

                                Users user = users[0]; // Single user response

                                Console.WriteLine("User ID: " + user.Id);
                                Console.WriteLine("Name: " + (user.FirstName ?? "") + " " + (user.LastName ?? ""));
                                Console.WriteLine("Email: " + user.Email);
                                Console.WriteLine("Language: " + user.Language);
                                Console.WriteLine("Locale: " + user.Locale);
                                Console.WriteLine("Time Zone: " + user.TimeZone);
                                Console.WriteLine("Status: " + user.Status);

                                if (user.Role != null)
                                {
                                    Console.WriteLine("Role ID: " + user.Role.Id);
                                    Console.WriteLine("Role Name: " + user.Role.Name);
                                }

                                if (user.Profile != null)
                                {
                                    Console.WriteLine("Profile ID: " + user.Profile.Id);
                                    Console.WriteLine("Profile Name: " + user.Profile.Name);
                                }

                                if (user.ReportingTo != null)
                                {
                                    Console.WriteLine("Reporting To ID: " + user.ReportingTo.Id);
                                    Console.WriteLine("Reporting To Name: " + (user.ReportingTo.Name ?? "") + " " + (user.ReportingTo.Email ?? ""));
                                }

                                Console.WriteLine("Microsoft: " + user.Microsoft);
                                Console.WriteLine("Currency: " + user.Currency);
                                Console.WriteLine("Created Time: " + user.CreatedTime);
                                Console.WriteLine("Modified Time: " + user.ModifiedTime);

                                if (user.CreatedBy != null)
                                {
                                    Console.WriteLine("Created By Id: " + user.CreatedBy.Id);
                                    Console.WriteLine("Created By Name: " + user.CreatedBy.Name);
                                }

                                if (user.ModifiedBy != null)
                                {
                                    Console.WriteLine("Modified By Id: " + user.ModifiedBy.Id);
                                    Console.WriteLine("Modified By Name: " + user.ModifiedBy.Name);
                                }

                                Console.WriteLine("Country: " + user.Country);
                                Console.WriteLine("Fax: " + user.Fax);
                                Console.WriteLine("Mobile: " + user.Mobile);
                                Console.WriteLine("Phone: " + user.Phone);
                                Console.WriteLine("Street: " + user.Street);
                                Console.WriteLine("Alias: " + user.Alias);
                                Console.WriteLine("Theme: " + user.Theme);
                                Console.WriteLine("State: " + user.State);
                                Console.WriteLine("Country Locale: " + user.CountryLocale);
                                Console.WriteLine("Signature: " + user.Signature);
                                Console.WriteLine("Sort Order Preference: " + user.SortOrderPreference);
                                Console.WriteLine("Category: " + user.Category);
                                Console.WriteLine("Date Format: " + user.DateFormat);
                                Console.WriteLine("Time Format: " + user.TimeFormat);
                                Console.WriteLine("Website: " + user.Website);
                                Console.WriteLine("Zip: " + user.Zip);
                                Console.WriteLine("Decimal Separator: " + user.DecimalSeparator);
                                Console.WriteLine("RTL Enabled: " + user.RtlEnabled);
                                Console.WriteLine("Number Separator: " + user.NumberSeparator);

                                // Custom fields
                                if (user.GetKeyValues() != null)
                                {
                                    Console.WriteLine("\nCustom Fields:");
                                    foreach (KeyValuePair<string, object> entry in user.GetKeyValues())
                                    {
                                        string keyName = entry.Key;

                                        object value = entry.Value;

                                        if (value is IList)
                                        {
                                            Console.WriteLine("Users KeyName : " + keyName);

                                            IList dataList = (IList)value;

                                            foreach (object data in dataList)
                                            {
                                                if (data is IDictionary)
                                                {
                                                    Console.WriteLine("Users KeyName : " + keyName + " - Value : ");

                                                    foreach (KeyValuePair<string, object> entry1 in (Dictionary<string, object>)data)
                                                    {
                                                        Console.WriteLine(entry1.Key + " : " + JsonConvert.SerializeObject(entry1.Value));
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine(JsonConvert.SerializeObject(data));
                                                }
                                            }
                                        }
                                        else if (value is IDictionary)
                                        {
                                            Console.WriteLine("Users KeyName : " + keyName + " - Value : ");

                                            foreach (KeyValuePair<string, object> entry1 in (Dictionary<string, object>)value)
                                            {
                                                Console.WriteLine(entry1.Key + " : " + JsonConvert.SerializeObject(entry1.Value));
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Users KeyName : " + keyName + " - Value : " + JsonConvert.SerializeObject(value));
                                        }
                                    }
                                }

                                Console.WriteLine("===================");
                            }
                            else
                            {
                                Console.WriteLine("No users found");
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

        public static void Call()
        {
            try
            {
                Environment environment = USDataCenter.PRODUCTION;
                IToken token = new OAuthToken.Builder().ClientId("Client_Id").ClientSecret("Client_Secret").RefreshToken("Refresh_Token").RedirectURL("Redirect_URL").Build();
                new Initializer.Builder().Environment(environment).Token(token).Initialize();

                // Get single user
                GetUser_1();
            }
            catch (Exception e)
            {
                Console.WriteLine(JsonConvert.SerializeObject(e));
            }
        }
    }
}