using System;
using System.Collections.Generic;
using Com.Zoho.API.Authenticator;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Profiles;
using Com.Zoho.Crm.API.Util;
using APIException = Com.Zoho.Crm.API.Profiles.APIException;
using ResponseHandler = Com.Zoho.Crm.API.Profiles.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Profiles.ResponseWrapper;
using GetProfilesParam = Com.Zoho.Crm.API.Profiles.ProfilesOperations.GetProfilesParam;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;

namespace Samples.Profile
{
    public class GetProfiles
    {
        public static void GetProfiles_1()
        {
            try
            {
                ProfilesOperations profilesOperations = new ProfilesOperations();
                ParameterMap paramInstance = new ParameterMap();
                paramInstance.Add(GetProfilesParam.INCLUDE_LITE_PROFILE, true);

                APIResponse<ResponseHandler> response = profilesOperations.GetProfiles(paramInstance);

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
                            List<Com.Zoho.Crm.API.Profiles.Profile> profiles = responseWrapper.Profiles;

                            if (profiles != null)
                            {
                                foreach (Com.Zoho.Crm.API.Profiles.Profile profile in profiles)
                                {
                                    Console.WriteLine("Profile DisplayLabel: " + profile.DisplayLabel);
                                    Console.WriteLine("Profile CreatedTime: " + profile.CreatedTime);
                                    Console.WriteLine("Profile ModifiedTime: " + profile.ModifiedTime);
                                    Console.WriteLine("Profile Name: " + profile.Name);
                                    Console.WriteLine("Profile ModifiedBy: " + profile.ModifiedBy);
                                    Console.WriteLine("Profile Description: " + profile.Description);
                                    Console.WriteLine("Profile Id: " + profile.Id);
                                    Console.WriteLine("Profile Category: " + profile.Category);
                                    Console.WriteLine("Profile CreatedBy: " + profile.CreatedBy);

                                    List<Com.Zoho.Crm.API.Profiles.PermissionDetail> permissionsList = profile.PermissionsDetails;
                                    if (permissionsList != null)
                                    {
                                        foreach (Com.Zoho.Crm.API.Profiles.PermissionDetail permission in permissionsList)
                                        {
                                            Console.WriteLine("Permission DisplayLabel: " + permission.DisplayLabel);
                                            Console.WriteLine("Permission Module: " + permission.Module);
                                            Console.WriteLine("Permission Name: " + permission.Name);
                                            Console.WriteLine("Permission Id: " + permission.Id);
                                            Console.WriteLine("Permission Enabled: " + permission.Enabled);
                                        }
                                    }

                                    List<Com.Zoho.Crm.API.Profiles.Section> sections = profile.Sections;
                                    if (sections != null)
                                    {
                                        foreach (Com.Zoho.Crm.API.Profiles.Section section in sections)
                                        {
                                            Console.WriteLine("Section Name: " + section.Name);
                                            Console.WriteLine("Section Categories: " + section.Categories);
                                        }
                                    }

                                    Console.WriteLine("Profile Default: " + profile.Default);
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

                            Console.WriteLine("Message: " + exception.Message.Value);
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

                GetProfiles_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}