using Unity.Reflect.Services;

namespace Unity.Reflect.Model
{
    public partial class SyncMaterial : ISyncModel, ISyncSendable
    {       
        public static string Extension = ".SyncMaterial";
        
        public static SyncMaterial Default()
        {
            return new SyncMaterial
            {
                Name = "default",
        
                Tint = SyncColor.White(),
                AlbedoColor = SyncColor.White(),
                AlbedoMap = SyncMap.Empty(),
                AlbedoFade = 1,
        
                Alpha = 1.0f,
                AlphaMap = SyncMap.Empty(),
        
                NormalMap = SyncMap.Empty(),
                NormalScale = 1.0f,
        
                CutoutMap = SyncMap.Empty(),
                CutoutThreshold = 1.0f,
        
                Glossiness = 0.5f,
                GlossinessMap = SyncMap.Empty(),
        
                Metallic = 0.0f,
                MetallicMap = SyncMap.Empty(),
        
                Emission = SyncColor.Black(),
                EmissionTemperature = 0.0f,
                EmissionMap = SyncMap.Empty()
            };
        }
        
        public SyncMaterial(string name, SyncColor albedo, SyncMap albedoMap, SyncColor albedoOverlay, float albedoFade, float alpha, SyncMap alphaMap, SyncMap normalMap, float normalScale,
            SyncMap cutoutMap, float cutoutThreshold, float glossiness, SyncMap glossinessMap, float metallic, SyncMap metallicMap,
            SyncColor emission, float emissionTemperature, SyncMap emissionMap)
        {
            Name = name;
            Tint = albedo;
            AlbedoColor = albedoOverlay;
            AlbedoMap = albedoMap;
            AlbedoFade = albedoFade;
            Alpha = alpha;
            AlphaMap = alphaMap;
            NormalMap = normalMap;
            NormalScale = normalScale;
            CutoutMap = cutoutMap;
            CutoutThreshold = cutoutThreshold;
            Glossiness = glossiness;
            GlossinessMap = glossinessMap;
            Metallic = metallic;
            MetallicMap = metallicMap;
            Emission = emission;
            EmissionTemperature = emissionTemperature;
            EmissionMap = emissionMap;
        }

        void ISyncSendable.Send(SyncAgent.SyncAgentClient client, string sourceId)
        {
            client.SendMaterial(new MaterialAsset
            {
                SourceId = sourceId,
                Material = this,
                Hash = this.PersistentHash()
            });
        }
    }

    public partial class SyncMap
    {
        public static SyncMap Empty() { return new SyncMap(string.Empty, new SyncFloat2(0.0f, 0.0f), new SyncFloat2(1.0f, 1.0f)); }
        
        public SyncMap(string texture, SyncFloat2 offset, SyncFloat2 tiling, float brightness = 1, bool invert = false, float rotation = 0)
        {
            Texture = texture;
            Offset = offset;
            Tiling = tiling;
            Brightness = brightness;
            Invert = invert;
            RotationDegrees = rotation;
        }
    }
}
