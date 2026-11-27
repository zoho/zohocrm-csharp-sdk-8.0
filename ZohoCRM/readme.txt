————————————————————————
Zoho CRM C# SDK 5.0.0
————————————————————————

This is the readme file for Zoho CRM’s C# SDK version 5.0.0.

This file gives a brief of the enhancements and/or bug fixes in the latest version.

----------------
Enhancements
----------------
    - Updated the file upload URLs (`GetFileUploadUrl`) of the US, EU, IN, AU, JP, and CN data centers.
        - US: https://upload-accl.zoho.com
        - EU: https://upload-accl.zoho.eu
        - IN: https://upload-accl.zoho.in
        - AU: https://upload.zoho.com.au
        - JP: https://upload.zoho.jp
        - CN: https://upload.zoho.com.cn
    - Fixed a thread-safety issue in `DataTypeConverter` where concurrent requests could fail with `InvalidOperationException` or `IndexOutOfRangeException` while the converter maps were being initialized.

You can also take a look at our GitHub page here (https://github.com/zoho/zohocrm-csharp-sdk-8.0/blob/main/README.md)