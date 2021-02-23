# IES Light Importer

## Description
The Unity IES Light Importer lets you import IES lights into the Unity Editor.

**Note:** IES Lights requires the use of light cookies, which is not supported in URP.

## Instructions

1. Extract the .zip file into your Unity project.
2. Drag and drop an .ies file into your project.

The importer creates an asset that you can drag and drop into your scene. This asset is a point light with a light cookie attached to it, generated from the .ies file.

IES file information is also included in this asset and can be seen in the inspector window:

![#](images/#.png)

If your project uses the High Definition Render pipeline, the light will be set to the correct Lumens value:

![#](images/#.png)

**Note:** A known issue requires you to multiply this value by a factor of 100 in HDRP. For example, a Lumens value of 1234 should be entered as 123400.

The light cookie generated from the IES light is a cubemap that uses only one channel, so the faces appear black in the Inspector:

![#](images/#.png)
