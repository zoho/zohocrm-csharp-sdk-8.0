# ZOHO CRM CSHARP SDK 8.0 for API version 8

The C# SDK for Zoho CRM allows developers to easily create C# applications that can be integrated with Zoho CRM. This SDK serves as a wrapper for the REST APIs, making it easier to access and utilize the services of Zoho CRM. 
Authentication to access the CRM APIs is done through OAuth2.0, and the authentication process is streamlined through the use of the C# SDK. The grant and access/refresh tokens are generated and managed within the SDK code, eliminating the need for manual handling during data synchronization between Zoho CRM and the client application.

This repository includes the C# SDK for API v8 of Zoho CRM. Check [Versions](https://github.com/zoho/zohocrm-csharp-sdk-8.0/releases) for more details on the versions of SDK released for this API version.

License
=======

    Copyright (c) 2021, ZOHO CORPORATION PRIVATE LIMITED 
    All rights reserved. 

    Licensed under the Apache License, Version 2.0 (the "License"); 
    you may not use this file except in compliance with the License. 
    You may obtain a copy of the License at 
    
        http://www.apache.org/licenses/LICENSE-2.0 
    
    Unless required by applicable law or agreed to in writing, software 
    distributed under the License is distributed on an "AS IS" BASIS, 
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
    See the License for the specific language governing permissions and 
    limitations under the License.

## Latest Version

- [4.0.0](/versions/4.0.0/README.md)
    - Updated dependencies to their latest versions.
    - Handled SDK utils.
    - Improved DB store query handling.
    - Fixed an issue with the Blueprint update API response.
    - Blueprint Field class `validationRule` field datatype changed (String to Object).
    - BulkWrite `BodyWrapper` class `fileType` field datatype changed (Choice to String).
    - BulkWrite `JobDetail` class `fileType` field datatype changed (Choice to String).
    - BulkRead `Query` class `fileType` field datatype changed (Choice to String).
    - Modules `GetModulesParam` class `STATUS` field datatype changed (Choice to String).
    - Added new `trigger` field in Notes `BodyWrapper` class.
    - Notifications class `deleteEvents` field datatype changed (Choice to String).
    - Modules `DeleteNotificationParam` class `CHANNEL_IDS` field datatype changed (Long to String).
    - Profiles and `MinifiedProfile` class `delete` field datatype changed (Boolean to `Delete` class).
    - Added new `applyFeatureExecution`, `applyValidationRule`, `applyFunctionValidationRule`, and `skipFeatureExecution` fields in Record `BodyWrapper` class.
    - Tags class `colorCode` field datatype changed (Choice to String).
    - Users `CountWrapper` class `count` field datatype changed (Long to Integer).
    - Users `GetUsersParam` class `TYPE` field datatype changed (Choice to String).
    - Webforms `Abtesting` class `id` field datatype changed (Long to String).
    - Webforms `AcknowledgeVisitor` class `templateId` field datatype changed (Long to String).
    - Webforms `AssignmentRule` class `id` field datatype changed (Long to String).
    - Webforms `Layout` class `id` field datatype changed (Long to String).
    - Webforms `Module` class `id` field datatype changed (Long to String).
    - Webforms `Owner` class `id` field datatype changed (Long to String).
    - Webforms `Tags` class `id` field datatype changed (Long to String).
    - Webforms `Users` class `id` field datatype changed (Long to String).
    - Webforms `Users` `acknowledgeVisitor` field datatype changed (AcknowledgeVisitors to AcknowledgeVisitor).
    - Removed `updateWebForms` method from the `WebformsOperations` class.

- [2.0.0](/versions/2.0.0/README.md)
    - New fields have been added to the History Tracking Fields API. 
    - Fixed the issue with the inventory mass conversion status API.

- [1.0.0](/versions/1.0.0/README.md)

    - C# SDK upgraded to support v8 APIs.

    - C# SDK improved to support the following new APIs

      - [Get Related Records Count of a Record](https://www.zoho.com/crm/developer/docs/api/v8/get-related-records-count.html)
      - [Calls Preference](https://www.zoho.com/crm/developer/docs/api/v8/get-calls-preferences.html)
      - [Record-Level Sharing of Emails](https://www.zoho.com/crm/developer/docs/api/v8/share-emails.html)
      - [Data Sharing Rules API](https://www.zoho.com/crm/developer/docs/api/v8/data-sharing-rules.html)
      - [Get Rich Text Fields API](https://www.zoho.com/crm/developer/docs/api/v8/get-rich-text-fields.html)


For older versions, please [refer](https://github.com/zoho/zohocrm-csharp-sdk-8.0/releases).

## Environmental Setup

C# SDK requires .NET Framework 4.6.1 or above to be set up in your development environment.

## Including the SDK in your project

You can include the SDK to your project using:

1. Install Visual Studio IDE from [Visual Studio](https://visualstudio.microsoft.com/downloads/) (if not installed).

2. C# SDK is available as a Nuget package. The ZOHOCRMSDK-8.0 assembly can be installed through the Nuget Package Manager or through the following options:

    - Package Manager

        ```sh
        Install-Package ZOHOCRMSDK-8.0 -Version 4.0.0
        Install-Package MySql.Data -Version 9.5.0
        Install-Package Newtonsoft.Json -Version 13.0.1
        ```

    - .NET  CLI

        ```sh
        dotnet add package ZOHOCRMSDK-8.0 --version 4.0.0
        dotnet add package Newtonsoft.Json --version 13.0.1
        dotnet add package MySql.Data --version 9.5.0
        ```

    - PackageReference

        For projects that support PackageReference, copy this XML node into the project file to refer the package.

        ```sh
        <ItemGroup>
            <PackageReference Include="ZOHOCRMSDK-8.0" Version="4.0.0" />
            <PackageReference Include="Newtonsoft.Json" Version="13.0.1" />
            <PackageReference Include="MySql.Data" Version="9.5.0" />
        </ItemGroup>
        ```
---

**NOTE** 

> - The **access and refresh tokens are environment-specific and domain-specific**. When you handle various environments and domains such as **Production**, **Sandbox**, or **Developer** and **IN**, **CN**, **US**, **EU**, **JP**, or **AU**, respectively, you must use the access token and refresh token generated only in those respective environments and domains. The SDK throws an error, otherwise.
For example, if you generate the tokens for your Sandbox environment in the CN domain, you must use only those tokens for that domain and environment. You cannot use the tokens generated for a different environment or a domain.

> - For **Deal Contact Roles API and Records API**, you will need to provide the **ZohoCRM.settings.fields.ALL** scope along with the **ZohoCRM.modules.ALL** scope while generating the OAuthtoken. Otherwise, the system returns the **OAUTH-SCOPE-MISMATCH** error.

> - For **Related Records API**, the scopes required for generating OAuthtoken are **ZohoCRM.modules.ALL**, **ZohoCRM.settings.fields.ALL** and **ZohoCRM.settings.related_lists.ALL**. Otherwise, the system returns the **OAUTH-SCOPE-MISMATCH** error.

> - For **Mass Convert API**, you will need to provide the **ZohoCRM.settings.fields.ALL** scope along with the **ZohoCRM.mass_convert.leads.CREATE** and **ZohoCRM.mass_convert.leads.READ** scope while generating the OAuthtoken. Otherwise, the system returns the **OAUTH-SCOPE-MISMATCH** error.

---

For more details, kindly refer [here](/versions/4.0.0/ZohoCRM/README.md).