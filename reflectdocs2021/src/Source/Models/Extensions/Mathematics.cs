using System;

namespace Unity.Reflect.Model
{
    public partial class SyncFloat4
    {
        public static SyncFloat4 Zero() { return new SyncFloat4(0.0f, 0.0f, 0.0f, 0.0f); }
        public static SyncFloat4 One() { return new SyncFloat4(1.0f, 1.0f, 1.0f, 1.0f); }

        public SyncFloat4(float x, float y, float z, float w) : this()
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        
        public static float Dot(SyncFloat4 lhs, SyncFloat4 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z + lhs.W * rhs.W;
        }
        
        public static bool operator ==(SyncFloat4 f1, SyncFloat4 f2)
        {
            if (ReferenceEquals(f1, null))
                return ReferenceEquals(f2, null);
            
            if (ReferenceEquals(f2, null))
                return false;
            
            return f1.X.Equals(f2.X) && f1.Y.Equals(f2.Y) && f1.Z.Equals(f2.Z) && f1.W.Equals(f2.W);
        }

        public static bool operator !=(SyncFloat4 f1, SyncFloat4 f2)
        {
            return !(f1 == f2);
        }
        
        public static explicit operator SyncFloat3(SyncFloat4 f)
        {
            return new SyncFloat3(f.X, f.Y, f.Z);
        }

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }
        
        public static SyncFloat4 operator+(SyncFloat4 left, SyncFloat4 right)
        {
            return new SyncFloat4
            {
                X = left.X + right.X,
                Y = left.Y + right.Y,
                Z = left.Z + right.Z,
                W = left.W + right.W
            };
        }

        public static SyncFloat4 operator-(SyncFloat4 left, SyncFloat4 right)
        {
            return left + (right * -1);
        }

        public static SyncFloat4 operator*(SyncFloat4 left, float f)
        {
            return new SyncFloat4
            {
                X = left.X * f,
                Y = left.Y * f,
                Z = left.Z * f,
                W = left.W * f
            };
        }

        public static SyncFloat4 operator*(float left, SyncFloat4 right)
        {
            return right * left;
        }
        
        public static SyncFloat4 operator/(SyncFloat4 left, float right)
        {
            return left * (1 / right);
        }
    }

    public partial class SyncFloat3
    {
        public static SyncFloat3 Zero() { return new SyncFloat3(0.0f, 0.0f, 0.0f); }
        public static SyncFloat3 One() { return new SyncFloat3(1.0f, 1.0f, 1.0f); }

        public SyncFloat3(float x, float y, float z) : this()
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public static SyncFloat3 operator+(SyncFloat3 left, SyncFloat3 right)
        {
            return new SyncFloat3
            {
                X = left.X + right.X,
                Y = left.Y + right.Y,
                Z = left.Z + right.Z
            };
        }

        public static SyncFloat3 operator-(SyncFloat3 left, SyncFloat3 right)
        {
            return left + (right * -1);
        }

        public static SyncFloat3 operator*(SyncFloat3 left, float right)
        {
            return new SyncFloat3
            {
                X = left.X * right,
                Y = left.Y * right,
                Z = left.Z * right
            };
        }

        public static SyncFloat3 operator*(float left, SyncFloat3 right)
        {
            return right * left;
        }

        public static SyncFloat3 operator*(SyncFloat3 left, SyncFloat3 right)
        {
            return new SyncFloat3
            {
                X = left.X * right.X,
                Y = left.Y * right.Y,
                Z = left.Z * right.Z
            };
        }

        public static SyncFloat3 operator/(SyncFloat3 left, float right)
        {
            return left * (1 / right);
        }

        public static float Dot(SyncFloat3 lhs, SyncFloat3 rhs)
        {
            return lhs.X * rhs.X + lhs.Y * rhs.Y + lhs.Z * rhs.Z;
        }

        public static SyncFloat3 Cross(SyncFloat3 lhs, SyncFloat3 rhs)
        {
            return new SyncFloat3(
                (float) (lhs.Y * (double) rhs.Z - lhs.Z * (double) rhs.Y),
                (float) (lhs.Z * (double) rhs.X - lhs.X * (double) rhs.Z),
                (float) (lhs.X * (double) rhs.Y - lhs.Y * (double) rhs.X));
        }

        public SyncFloat3 FlipYZ()
        {
            return new SyncFloat3(X, Z, Y);
        }
        
        public static bool operator ==(SyncFloat3 f1, SyncFloat3 f2)
        {
            if (ReferenceEquals(f1, null))
                return ReferenceEquals(f2, null);
            
            if (ReferenceEquals(f2, null))
                return false;
            
            return f1.X.Equals(f2.X) && f1.Y.Equals(f2.Y) && f1.Z.Equals(f2.Z);
        }

        public static bool operator !=(SyncFloat3 f1, SyncFloat3 f2)
        {
            return !(f1 == f2);
        }
    }

    public partial class SyncFloat2
    {
        public static SyncFloat2 Zero() { return new SyncFloat2(0.0f, 0.0f); }
        public static SyncFloat2 One() { return new SyncFloat2(1.0f, 1.0f); }

        public static SyncFloat2 operator+(SyncFloat2 left, SyncFloat2 right)
        {
            return new SyncFloat2
            {
                X = left.X + right.X,
                Y = left.Y + right.Y
            };
        }

        public static SyncFloat2 operator-(SyncFloat2 left, SyncFloat2 right)
        {
            return left + (right * -1);
        }

        public static SyncFloat2 operator*(SyncFloat2 left, float right)
        {
            return new SyncFloat2
            {
                X = left.X * right,
                Y = left.Y * right
            };
        }

        public static SyncFloat2 operator*(float left, SyncFloat2 right)
        {
            return right * left;
        }

        public static SyncFloat2 operator*(SyncFloat2 left, SyncFloat2 right)
        {
            return new SyncFloat2
            {
                X = left.X * right.X,
                Y = left.Y * right.Y
            };
        }

        public static SyncFloat2 operator/(SyncFloat2 left, float right)
        {
            return left * (1 / right);
        }

        public float Dot(SyncFloat2 other)
        {
            return X * other.X + Y * other.Y;
        }

        public SyncFloat2(float x, float y) : this()
        {
            X = x;
            Y = y;
        }
        
        public static bool operator ==(SyncFloat2 f1, SyncFloat2 f2)
        {
            if (ReferenceEquals(f1, null))
                return ReferenceEquals(f2, null);
            
            if (ReferenceEquals(f2, null))
                return false;
            
            return f1.X.Equals(f2.X) && f1.Y.Equals(f2.Y);
        }

        public static bool operator !=(SyncFloat2 f1, SyncFloat2 f2)
        {
            return !(f1 == f2);
        }
    }

    public partial class SyncQuaternion
    {
        public static SyncQuaternion Identity() { return new SyncQuaternion(0.0f, 0.0f, 0.0f, 1.0f); }

        public static SyncQuaternion RotateX(float angleRadian)
        {
            var sinA = (float) Math.Sin(0.5 * angleRadian);
            var cosA = (float) Math.Cos(0.5 * angleRadian);
            return new SyncQuaternion(sinA, 0, 0, cosA);
        }
        
        public SyncQuaternion(float x, float y, float z, float w) : this()
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        
        public static SyncQuaternion operator*(SyncQuaternion left, SyncQuaternion right)
        {
            return new SyncQuaternion(
                left.W * right.X + left.X * right.W + left.Y * right.Z - left.Z * right.Y,
                left.W * right.Y + left.Y * right.W + left.Z * right.X - left.X * right.Z,
                left.W * right.Z + left.Z * right.W + left.X * right.Y - left.Y * right.X,
                left.W * right.W - left.X * right.X - left.Y * right.Y - left.Z * right.Z
            );
        }

        public static SyncQuaternion Inverse(SyncQuaternion q)
        {
            return new SyncQuaternion(- q.X, - q.Y, - q.Z,  q.W);
        }
        
        public static SyncFloat3 operator*(SyncQuaternion q, SyncFloat3 v)
        {
            var qXyz = new SyncFloat3(q.X, q.Y, q.Z);
            var t = 2f * SyncFloat3.Cross(qXyz, v);
            return v + q.W * t + SyncFloat3.Cross(qXyz, t);
        }

        public static SyncQuaternion Normalize(SyncQuaternion q)
        {
            var num = Math.Sqrt(Dot(q, q));
            return num < double.Epsilon ? Identity() : new SyncQuaternion((float) (q.X / num), (float) (q.Y / num), (float) (q.Z / num), (float) (q.W / num));
        }

        public static float Dot(SyncQuaternion a, SyncQuaternion b)
        {
            return (float) (a.X * (double) b.X + a.Y * (double) b.Y + a.Z * (double) b.Z + a.W * (double) b.W);
        }
        
        public static bool operator ==(SyncQuaternion q1, SyncQuaternion q2)
        {
            if (ReferenceEquals(q1, null))
                return ReferenceEquals(q2, null);
            
            if (ReferenceEquals(q2, null))
                return false;
            
            return q1.X.Equals(q2.X) && q1.Y.Equals(q2.Y) && q1.Z.Equals(q2.Z) && q1.W.Equals(q2.W);
        }

        public static bool operator !=(SyncQuaternion q1, SyncQuaternion q2)
        {
            return !(q1 == q2);
        }
    }

    public partial class SyncMatrix
    {
        public static SyncMatrix Identity()
        {
            return new SyncMatrix(
                new SyncFloat4(1.0f, 0.0f, 0.0f, 0.0f),
                new SyncFloat4(0.0f, 1.0f, 0.0f, 0.0f),
                new SyncFloat4(0.0f, 0.0f, 1.0f, 0.0f),
                new SyncFloat4(0.0f, 0.0f, 0.0f, 1.0f));
        }

        public SyncMatrix(SyncFloat4 c0, SyncFloat4 c1, SyncFloat4 c2, SyncFloat4 c3) : this()
        {
            C0 = c0;
            C1 = c1;
            C2 = c2;
            C3 = c3;
        }

        public SyncTRS ToTRS()
        {
            var trs = new SyncTRS { Scale = Scale() };


            if (HasReflection())
            {
                trs.Scale = - 1.0f * trs.Scale;
            }
            
            trs.Position = Origin();
            trs.Rotation = Rotation(trs.Scale);

            return trs;
        }

        SyncFloat3 Origin()
        {
            return (SyncFloat3)C3;
        }
        
        SyncQuaternion Rotation(SyncFloat3 scale)
        {
            var u = C0 / scale.X;
            var v = C1 / scale.Y;
            var w = C2 / scale.Z;

            SyncQuaternion q;

            if (u.X >= 0f)
            {
                var t = v.Y + w.Z;
                q = t >= 0 ?
                    new SyncQuaternion(v.Z - w.Y, w.X - u.Z, u.Y - v.X, 1.0f + u.X + t) :
                    new SyncQuaternion(1.0f + u.X - t, u.Y + v.X, w.X + u.Z, v.Z - w.Y);
            }
            else
            {
                var t = v.Y - w.Z;
                q = t >= 0f ?
                    new SyncQuaternion(u.Y + v.X, 1.0f - u.X + t, v.Z + w.Y, w.X - u.Z) :
                    new SyncQuaternion(w.X + u.Z, v.Z + w.Y, 1.0f - u.X - t, u.Y - v.X);
            }

            return SyncQuaternion.Normalize(q);
        }

        SyncFloat3 Scale()
        {
            return new SyncFloat3(C0.Length(), C1.Length(), C2.Length());
        }
        
        bool HasReflection()
        {
            var c = SyncFloat3.Cross((SyncFloat3)C0, (SyncFloat3)C1);
            return SyncFloat3.Dot(c, (SyncFloat3)C2) < 0.0f;
        }
    }
    
    public partial class SyncTRS
    {
        public static SyncTRS Identity() { return new SyncTRS(SyncFloat3.Zero(), SyncQuaternion.Identity(), SyncFloat3.One()); }

        public SyncTRS(SyncFloat3 position, SyncQuaternion rotation, SyncFloat3 scale) : this()
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
        
        public SyncFloat3 TransformPoint(SyncFloat3 pt)
        {
            return Position + Rotation * (pt * Scale);
        }
        
        public SyncFloat3 TransformVector(SyncFloat3 vec)
        {
            return Rotation * (vec * Scale);
        }
        
        public SyncTRS Inverse()
        {
            var invRot = SyncQuaternion.Inverse(Rotation);
            var t = new SyncTRS
            {
                Position = invRot * (-1.0f * Position),
                Rotation = invRot,
                Scale = new SyncFloat3(1.0f / Scale.X, 1.0f / Scale.Y, 1.0f / Scale.Z)
            };

            return t;
        }
        
        public static SyncTRS operator *(SyncTRS left, SyncTRS right)
        {
            var t = new SyncTRS
            {
                Position = left.Position + left.Rotation * right.Position,
                Rotation = left.Rotation * right.Rotation,
                Scale =  left.Scale * right.Scale
            };

            return t;
        }
        
        public static bool operator ==(SyncTRS t1, SyncTRS t2)
        {
            if (ReferenceEquals(t1, null))
                return ReferenceEquals(t2, null);
            
            if (ReferenceEquals(t2, null))
                return false;
            
            return t1.Position.Equals(t2.Position) && t1.Rotation.Equals(t2.Rotation) && t1.Scale.Equals(t2.Scale);
        }

        public static bool operator !=(SyncTRS t1, SyncTRS t2)
        {
            return !(t1 == t2);
        }
        
        public SyncTRS FlipYZ()
        {
            var t = new SyncTRS
            {
                Position = Position.FlipYZ(),
                Rotation = new SyncQuaternion(Rotation.X, Rotation.Z, Rotation.Y, -Rotation.W),
                Scale = Scale.FlipYZ()
            };

            return t;
        }
    }

    public partial class SyncTransform
    {
        public static SyncTransform Identity() { return new SyncTransform(SyncFloat3.Zero(), SyncQuaternion.Identity(), SyncFloat3.One()); }

        public SyncTransform(SyncFloat3 position, SyncQuaternion rotation, SyncFloat3 scale) : this()
        {
            Trs = new SyncTRS(position, rotation, scale);
        }
        
        public SyncTransform(SyncMatrix matrix) : this()
        {
            Matrix = matrix;
            EnsureTRS();
        }
        
        public SyncTransform(SyncTRS trs) : this()
        {
            Trs = trs;
        }

        public SyncFloat3 Position
        {
            get { EnsureTRS(); return Trs.Position; }
            set { EnsureTRS(); Trs.Position = value; }
        }
        
        public SyncQuaternion Rotation
        {
            get { EnsureTRS(); return Trs.Rotation; }
            set { EnsureTRS(); Trs.Rotation = value; }
        }
        
        public SyncFloat3 Scale
        {
            get { EnsureTRS(); return Trs.Scale; }
            set { EnsureTRS(); Trs.Scale = value; }
        }
        
        public SyncFloat3 TransformPoint(SyncFloat3 pt)
        {
            EnsureTRS();
            return Trs.TransformPoint(pt);
        }
        
        public SyncFloat3 TransformVector(SyncFloat3 vec)
        {
            EnsureTRS();
            return Trs.TransformVector(vec);
        }
        
        public SyncTransform Inverse()
        {
            EnsureTRS();
            return new SyncTransform(Trs.Inverse());
        }
        
        public static SyncTransform operator *(SyncTransform t1, SyncTransform t2)
        {
            t1.EnsureTRS();
            t2.EnsureTRS();
            
            var trs1 = t1.Trs;
            var trs2 = t2.Trs;
            
            return new SyncTransform(trs1 * trs2);
        }
        
        public static bool operator ==(SyncTransform t1, SyncTransform t2)
        {
            if (ReferenceEquals(t1, null))
                return ReferenceEquals(t2, null);
            
            if (ReferenceEquals(t2, null))
                return false;

            t1.EnsureTRS();
            t2.EnsureTRS();
            
            var trs1 = t1.Trs;
            var trs2 = t2.Trs;

            return trs1 == trs2;
        }

        public static bool operator !=(SyncTransform t1, SyncTransform t2)
        {
            return !(t1 == t2);
        }
        
        public SyncTransform FlipYZ()
        {
            return new SyncTransform(Trs.FlipYZ());
        }

        void EnsureTRS()
        {
            if (Trs == null)
            {
                Trs = Matrix != null ? Matrix.ToTRS() : SyncTRS.Identity();
            }
        }
    }

    public partial class SyncBoundingBox
    {
        public bool IsInitialized()
        {
            return Min != null;
        }

        public void Encapsulate(SyncFloat3 pt)
        {
            if (!IsInitialized())
            {
                Min = new SyncFloat3(pt);
                Max = new SyncFloat3(pt);
            }
            else
            {
                Min.X = Math.Min(Min.X, pt.X);
                Min.Y = Math.Min(Min.Y, pt.Y);
                Min.Z = Math.Min(Min.Z, pt.Z);

                Max.X = Math.Max(Max.X, pt.X);
                Max.Y = Math.Max(Max.Y, pt.Y);
                Max.Z = Math.Max(Max.Z, pt.Z);
            }
        }

        public void Encapsulate(SyncBoundingBox bbox)
        {
            Encapsulate(bbox.Min);
            Encapsulate(bbox.Max);
        }

        public SyncBoundingBox Transform(SyncTransform transform)
        {
            var bbox = new SyncBoundingBox();
            var corners = GetCorners();
            foreach (var corner in corners)
            {
                var point = transform.TransformPoint(corner);
                bbox.Encapsulate(point);
            }

            return bbox;
        }

        public SyncFloat3[] GetCorners()
        {
            var corners = new SyncFloat3[8];
            corners[0] = new SyncFloat3(Min);
            corners[1] = new SyncFloat3(Min.X, Min.Y, Max.Z);
            corners[2] = new SyncFloat3(Min.X, Max.Y, Min.Z);
            corners[3] = new SyncFloat3(Max.X, Min.Y, Min.Z);
            corners[4] = new SyncFloat3(Min.X, Max.Y, Max.Z);
            corners[5] = new SyncFloat3(Max.X, Min.Y, Max.Z);
            corners[6] = new SyncFloat3(Max.X, Max.Y, Min.Z);
            corners[7] = new SyncFloat3(Max);
            return corners;
        }
    }
}
