# Customizing the Reflect Viewer

The package folder contains the source code for the Viewer application and the scripts and resources it needs. The Reflect Prefab attaches the scripts and resources directly.

To customize the Reflect Viewer in the Unity Editor, add the Reflect Prefab to your scene.

1. From the Project view, go to **Packages** > **Unity Reflect** > **Runtime**.
2. Drag the Reflect prefab into an empty scene and press the Play button.

   ![Load the prefab](../images/EditorLoadPrefab.png)

> **Note:** Because the scripts and resources for Reflect are in the package and not in your Project, they will not show up in Visual Studio. One possible workaround to this is to copy the content into your Project, but note that this will prevent the package manager from updating the package.
