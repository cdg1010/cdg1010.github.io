# Unity Reflect Viewer API

## Pipeline Overview

<!--
<span class="red">[Revit/Naviswork/Sketchup] -> [Reflect Publisher API] -> [SyncService] -> [Reflect Pipeline API] -> [GameObjects] <br> *to replace with image*</span>
-->

The Pipeline API is used to convert Reflect models into Unity GameObjects at runtime. It's also used to sync any changes happening at the same time to the original CAD model source.

Reflect models are represented by different types of ISyncModel:

* **SyncObjectInstance** represents an object in the model with its transform and metadata.
* **SyncMesh**, **SyncTexture** and **SyncMaterials** represent the different types of asset that can be used by a SyncObjectInstance.

<!--**Note:** For more about the Unity Reflect classes, see <span class="red">tbd</span>-->

The Pipeline API uses a node-based approach where each node manipulates Reflect models in order to filter or convert them. As in any node-based API, the pipeline can be extended and customized using new nodes or by changing the pipeline connection structure.

Pipelines can be saved into Assets and reused across multiple Scenes and Projects.

Nodes communicate between each other using Stream Events:

* **StreamBegin**: Beginning of a batch of StreamEvents.
* **StreamEvents**: Add/Changed/Removed
* **StreamEnd**: End of the batch

<img src="../manual/images/1.3/Pipeline.png" style="width:400px; border: 1px solid #333; margin-top: 20px;">

*Pipeline diagram*

### Nodes

| Node | Description |
|--|--|
| **ProjectStreamer** | Reads a Reflect Project content and streams the data as lightweight entries (Type, BoundingBox); reacts to live changes in the Reflect Project and streams the deltas. |
| **SyncInstanceProvider** | Gets a more detailed description of the instance (Transform, Metadata, Dependencies).
| **DataProvider** | Loads all the dependencies of a given SyncInstance and streams them to the rest of the pipeline; manages the data so no duplicate information is loaded.<br><br>The dependencies are SyncObjects, SyncMeshes, SyncTextures and SyncMaterials. |
| **MeshConverter, MaterialConverter, TextureConverter** | Convert the Reflect data into a Unity Object. |
| **InstanceConverter**   |  The last node that generates the final GameObject representing the SyncInstance. |

<!--### Example

#### Adding a chair (in Revit)

<div class="red">

1. SendBegin is propagated to all Nodes

2. Stream entry : { Id : 123, Name : Stacking_Chair, BBox }

3. Stream instance : { Id : 123, Name : Stacking_Chair, BBox, Transform, Metadata, ObjectId }

4. SyncInstance dependencies are sent to the Converter nodes

</div>

#### Moving the chair

<div class="red">
pending
</div>

## Pipeline settings

<span class="red">tbd</span>

## Scripting API

<span class="red">tbd</span>

### Connections

Strongly typed Input / Output classes

### Pipeline Params

* Ability to send additional
Data to nodes
* Share values between nodes

-->

### Running a Reflect Pipeline

**PipelineAsset** is a ScriptableObject that contains the ReflectNodes and connections that describe the Reflect pipeline.

**ReflectPipeline** is a MonoBehaviour that takes a PipelineAsset. When assigning a PipelineAsset to the ReflectPipeline, the inspector displays all public and serializable fields found in the pipeline nodes.

The method **ReflectPipeline.InitializeAndRefreshPipeline** can start the Pipeline.

If there are any changes to the data after the PipelineAsset is started, **ReflectPipeline.RefreshPipeline** can trigger a refresh.
