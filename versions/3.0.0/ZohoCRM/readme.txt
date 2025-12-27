————————————————————————
Zoho CRM C# SDK 3.0.0
————————————————————————

This is the readme file for Zoho CRM’s C# SDK version 3.0.0.

This file gives a brief of the enhancements and/or bug fixes in the latest version.

----------------
Enhancements
----------------
    - Updated dependencies to their latest versions.
    - Handled SDK utils and API connectors.
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

You can also take a look at our GitHub page here (https://github.com/zoho/zohocrm-csharp-sdk-8.0/blob/main/README.md)