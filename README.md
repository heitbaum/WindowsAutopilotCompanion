# Windows Autopilot Companion

This repository contains a sample app that can be used to modify the settings of a device as part of a Windows Autopilot white glove process (introduced in Windows 10 1903).  This demonstrates how to make just-in-time configuration adjustments, before pre-provisioning the device with needed apps and settings.

## Capabilities

The app supports simple configuration adjustments:

- Add, remove, or change the user assigned to the Windows Autopilot device.
- Configure the group tag for the device.
- Configure the device name (computer name) that should be assigned to the device when it is deployed.
- Sync Autopilot Service
- Add Devices to AD Groups
- Add User search
- Added ability to press Enter on Device Search

This application leverages Xamarin in order to create a cross-platform app.  This will run on Windows 10, Android, and iOS devices.  

## Using

To use this sample app, you first need to authenticate to Microsoft Intune.  This can be done in one of two ways:

- If you are making changes to the Intune tenant associated with your Azure AD account, you can just click the "Logon" button to be prompted for your credentials.
- If you are making changes to another tenant that you have been granted access to, either via the guest access process (described here) or via Partner Center, specify the tenant ID (e.g. contoso.onmicrosoft.com) before clicking "Logon".

Once connected, choose "Device Search" from the menu.  Search for devices using the serial number of the device (all devices with serial numbers starting with the value specified will be returned).  Or alternatively, click the "Scan QR Code" button to retrieve the device associated with that QR code (retrieved using the ZtdId value embedded in the QR code).

## Building

The companion app can be built using Visual Studio 2017 or later, with the Xamarin components installed.  For debugging, choose "Debug" and "x86" which should work for Windows 10 UWP and Android emulator testing.  For use on a real device, use "Release" and "Any CPU" and perform these additional steps after the build completes:

- For Windows 10, right click on the CompanionApp.UWP project and choose Store -> Create app packages.  Complete the rest of the wizard.
- For Android, right click on the CompanionApp.Android project and choose Archive.  Create an archive, and then deploy that archive to create an APK that can be installed on an Android device.  See [this link](https://docs.microsoft.com/en-us/xamarin/android/deploy-test/signing/index?tabs=windows) for more information

If publishing the resulting packages to GitHub, create a new folder for the current date under the "Drops" folder, then create Windows and Android folders in that folder.  Copy the *.appxbundle file into the Windows folder and the *.apk file into the Android folder.

