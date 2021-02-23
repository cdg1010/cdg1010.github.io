using System;

namespace Unity.Reflect.Model
{
    public partial class SyncLight
    {
        public SyncLight(string name, SyncTransform transform, Types.Type type, SyncColor color,
            float temperature, float intensity, Types.IntensityUnit intensityUnit, float range, float spotAngle, float beamAngle,
            Types.Shape shape, float shapeDiameter, float shapeWidth, float shapeLength, bool shapeVisible, string iesProfile)
        {
            Name = name;
            Transform = transform;
            Type = type;
            Color = color;
            Temperature = temperature;
            Intensity = intensity;
            IntensityUnit = intensityUnit;
            Range = range;
            SpotAngle = spotAngle;
            BeamAngle = beamAngle;
            Shape = shape;
            ShapeDiameter = shapeDiameter;
            ShapeWidth = shapeWidth;
            ShapeLength = shapeLength;
            ShapeVisible = shapeVisible;
            IesProfile = Utils.NotNullString(iesProfile);
        }
    }
}
