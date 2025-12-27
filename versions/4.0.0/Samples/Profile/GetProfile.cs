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
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using System.Management.Instrumentation;

namespace Samples.Profile
{
    public class GetProfile
    {
        public static void GetProfile_1()
        {
            try
            {
                ProfilesOperations profilesOperations = new ProfilesOperations();
                long profileId = 1055806000024277005L; // Replace with actual profile ID

                APIResponse<ResponseHandler> response = profilesOperations.GetProfile(profileId);

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

                                    Com.Zoho.Crm.API.Users.MinifiedUser modifiedBy = profile.ModifiedBy;
                                    if (modifiedBy != null)
                                    {
                                        Console.WriteLine("Profile Modified By Id: " + modifiedBy.Id);
                                        Console.WriteLine("Profile Modified By Name: " + modifiedBy.Name);
                                        Console.WriteLine("Profile Modified By Email: " + modifiedBy.Email);
                                    }

                                    Console.WriteLine("Profile Description: " + profile.Description);
                                    Console.WriteLine("Profile Id: " + profile.Id);
                                    Console.WriteLine("Profile Category: " + profile.Category);

                                    Com.Zoho.Crm.API.Users.MinifiedUser createdBy = profile.CreatedBy;
                                    if (createdBy != null)
                                    {
                                        Console.WriteLine("Profile Created By Id: " + createdBy.Id);
                                        Console.WriteLine("Profile Created By Name: " + createdBy.Name);
                                        Console.WriteLine("Profile Created By Email: " + createdBy.Email);
                                    }

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

                                            List<Com.Zoho.Crm.API.Profiles.Category> categories = section.Categories;
                                            if (categories != null)
                                            {
                                                foreach (Com.Zoho.Crm.API.Profiles.Category category in categories)
                                                {
                                                    if (category is CategoryOthers)
									                {
                                                        CategoryOthers category1 = (CategoryOthers)category;
                                                        Console.WriteLine("Profile Section Category DisplayLabel: " + category1.DisplayLabel);
                                                        List<String> categoryPermissionsDetails = category1.PermissionsDetails;
                                                        if (categoryPermissionsDetails != null)
                                                        {
                                                            foreach (Object permissionsDetailID in categoryPermissionsDetails)
                                                            {
                                                                Console.WriteLine("Profile Section Category permissionsDetailID: " + permissionsDetailID);
                                                            }
                                                        }
                                                        Console.WriteLine("Profile Section Category Name: " + category1.Name);
                                                    }

                                                    else if (category is CategoryModule)
									                {
                                                        CategoryModule category1 = (CategoryModule)category;
                                                        Console.WriteLine("Profile Section Category DisplayLabel: " + category1.DisplayLabel);
                                                        List<String> categoryPermissionsDetails = category1.PermissionsDetails;
                                                        if (categoryPermissionsDetails != null)
                                                        {
                                                            foreach (Object permissionsDetailID in categoryPermissionsDetails)
                                                            {
                                                                Console.WriteLine("Profile Section Category permissionsDetailID: " + permissionsDetailID);
                                                            }
                                                        }
                                                        Console.WriteLine("Profile Section Category Module: " + category1.Module);
                                                        Console.WriteLine("Profile Section Category Name: " + category1.Name);
                                                    }
                                                }
                                            }
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

                GetProfile_1();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}