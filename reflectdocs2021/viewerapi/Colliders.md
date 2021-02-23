# Colliders <span class="red">WIP</span>

## Overview

## Key points

 <div style="display: inline-block; border: 1px solid red; padding: 1em; width: 100%;">

`highlighted example`

</div>

## Annotated sample code

```
public class AddColliderNode : ReflectNode<AddCollider>
{
    public GameObjectInput input = new GameObjectInput();

    protected override AddCollider Create(ISyncModelProvider provider, IExposedPropertyTable resolver)
    {
        var node = new AddCollider();
        input.streamEvent = node.OnGameObjectEvent;
        return node;
    }
}

public class AddCollider : IReflectNodeProcessor
{
    public void OnGameObjectEvent(SyncedData<GameObject> stream, StreamEvent streamEvent)
    {
        if (streamEvent == StreamEvent.Added)
        {
            var gameObject = stream.data;
            if (!gameObject.TryGetComponent(out MeshFilter meshFilter))
                return;

            // We add this safety check in case the object already has its own collider
            if (gameObject.TryGetComponent(out MeshCollider _))
                return;

            var collider = gameObject.AddComponent<MeshCollider>();
            collider.sharedMesh = meshFilter.sharedMesh;
        }
    }

    public void OnPipelineInitialized()
    {
        // not needed
    }

    public void OnPipelineShutdown()
    {
        // not needed
    }
}

public class AddingCollidersSample : MonoBehaviour
{
    void Start()
    {
        // Create a pipeline asset

        var pipelineAsset = ScriptableObject.CreateInstance<PipelineAsset>();

        // Create the nodes required for this sample

        var addColliderNode = pipelineAsset.CreateNode<AddColliderNode>();

        // Create the rest of the pipeline

        var projectStreamer = pipelineAsset.CreateNode<ProjectStreamerNode>();
        var instanceProvider = pipelineAsset.CreateNode<SyncObjectInstanceProviderNode>();
        var dataProvider = pipelineAsset.CreateNode<DataProviderNode>();
        var meshConverter = pipelineAsset.CreateNode<MeshConverterNode>();
        var materialConverter = pipelineAsset.CreateNode<MaterialConverterNode>();
        var textureConverter = pipelineAsset.CreateNode<TextureConverterNode>();
        var instanceConverter = pipelineAsset.CreateNode<InstanceConverterNode>();

        // Inputs / Outputs

        pipelineAsset.CreateConnection(projectStreamer.assetOutput, instanceProvider.input);
        pipelineAsset.CreateConnection(instanceProvider.output, dataProvider.instanceInput);
        pipelineAsset.CreateConnection(dataProvider.syncMeshOutput, meshConverter.input);
        pipelineAsset.CreateConnection(dataProvider.syncMaterialOutput, materialConverter.input);
        pipelineAsset.CreateConnection(dataProvider.syncTextureOutput, textureConverter.input);
        pipelineAsset.CreateConnection(dataProvider.instanceDataOutput, instanceConverter.input);
        pipelineAsset.CreateConnection(instanceConverter.output, addColliderNode.input);

        // Params

        pipelineAsset.SetParam(dataProvider.hashCacheParam, projectStreamer);
        pipelineAsset.SetParam(materialConverter.textureCacheParam, textureConverter);
        pipelineAsset.SetParam(instanceConverter.materialCacheParam, materialConverter);
        pipelineAsset.SetParam(instanceConverter.meshCacheParam, meshConverter);

        // Add a ReflectPipeline node and start the pipeline

        var reflectBehaviour = gameObject.AddComponent<ReflectPipeline>();

        reflectBehaviour.pipelineAsset = pipelineAsset;
        reflectBehaviour.InitializeAndRefreshPipeline(new SampleSyncModelProvider());
    }
}

```
