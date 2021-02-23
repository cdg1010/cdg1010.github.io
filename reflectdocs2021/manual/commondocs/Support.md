# Support

Need more help with Unity Reflect? Contact [reflect-support@unity3d.com](mailto:reflect-support@unity3d.com).

This document includes some frequently requested support topics.

## Delete Reflect data (Windows)

To remove all Reflect data for the Viewer and Editor, delete all of the following folders from your home folder:

* `AppData\Local\Unity\Reflect`
* `AppData\LocalLow\Unknown Vendor`
* `AppData\LocalLow\Unity`
* `AppData\LocalLow\Unity Technologies\Unity Reflect`

> **Note:** If you don't see AppData in your home folder, go to the View tab in Windows Explorer and check **Hidden items**.

Finally, delete the following folder:

* `C:\ProgramData\Unity\Reflect`

## Select your region (iOS, Windows)

Unity Reflect 2.0 allows users in China to use Reflect on iOS and Windows.

Reflect selects its default region based on the location specified in your device settings. To change this setting, follow the instructions for your platform below.

### iOS

To change your region for Unity Reflect:

1. Go to **Settings** > **Unity Reflect** and in the Server section, tap **Region**.

3. Select your preferred region setting.

  * **Auto** lets Reflect choose based on your device localization settings.
  * **Default** connects to the GCP server for users outside China.
  * **China** connects to the server stack for users in China.

### Windows

To change your region settings on Windows, run the following application:

`Program Files/Unity/Reflect/RegionSelector/RegionSelector.exe`

This program allows you to override the previous setting.
