using System;

namespace Unity.Reflect.Model
{
    public partial class SyncColor
    {
        public static SyncColor Black() { return new SyncColor(0.0f, 0.0f, 0.0f, 1.0f); }

        public static SyncColor White() { return new SyncColor(1.0f, 1.0f, 1.0f, 1.0f); }

        public SyncColor(float r, float g, float b, float a = 1.0f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        
        public static SyncColor From256(int r, int g, int b, int a = 255)
        {
            return new SyncColor(r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f);
        }

        public static SyncColor operator *(SyncColor c1, SyncColor c)
        {
            return new SyncColor(c1.R * c.R, c1.G * c.G, c1.B * c.B, c1.A * c.A);
        }
        
        public static SyncColor operator *(float f, SyncColor c)
        {
            return new SyncColor(f * c.R, f * c.G, f * c.B, f * c.A);
        }
        
        public static SyncColor operator +(SyncColor c1, SyncColor c)
        {
            return new SyncColor(c1.R + c.R, c1.G + c.G, c1.B + c.B, c1.A + c.A);
        }

        public static SyncColor Lerp(SyncColor c1, SyncColor c2, float u)
        {
            return (1 - u) * c1 + u * c2;
        }
        
        public static bool operator ==(SyncColor c1, SyncColor c2)
        {
            if (ReferenceEquals(c1, null))
                return ReferenceEquals(c2, null);
            
            if (ReferenceEquals(c2, null))
                return false;
            
            return c1.R.Equals(c2.R) && c1.G.Equals(c2.G) && c1.B.Equals(c2.B) && c1.A.Equals(c2.A);
        }

        public static bool operator !=(SyncColor c1, SyncColor c2)
        {
            return !(c1 == c2);
        }
    }
}
