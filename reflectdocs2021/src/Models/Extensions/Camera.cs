using System;

namespace Unity.Reflect.Model
{
    public partial class SyncCamera
    {
        public static SyncCamera FromOrthographic(string name, SyncTransform transform, float aspect, float size, float near, float far)
        {
            return new SyncCamera
            {
                Name = name,
                Transform = transform,
                Aspect = aspect,
                Orthographic = true,
                Size = size,
                Near = near,
                Far = far
            };
        }
        
        public static SyncCamera FromPerspective(string name, SyncTransform transform, float aspect, float fov, float near, float far)
        {
            return new SyncCamera
            {
                Name = name,
                Transform = transform,
                Aspect = aspect,
                Orthographic = false,
                Fov = fov,
                Near = near,
                Far = far
            };
        }
        
        public static SyncCamera FromOrthographicFrustum(string name, SyncTransform transform,
            float left, float right, float bottom, float top, float near, float far)
        {
            return new SyncCamera
            {
                Name = name,
                Transform = transform,
                Aspect = 0.0f, // aspect == 0.0f means the camera uses a custom Frustum
                Orthographic = true,
                Left = left,
                Right = right,
                Bottom = bottom,
                Top = top,
                Near = near,
                Far = far
            };
        }
        
        public static SyncCamera FromPerspectiveFrustum(string name, SyncTransform transform,
            float left, float right, float bottom, float top, float near, float far)
        {
            return new SyncCamera
            {
                Name = name,
                Transform = transform,
                Aspect = 0.0f, // aspect == 0.0f means the camera uses a custom Frustum
                Orthographic = false,
                Left = left,
                Right = right,
                Bottom = bottom,
                Top = top,
                Near = near,
                Far = far
            };
        }
    }
}
