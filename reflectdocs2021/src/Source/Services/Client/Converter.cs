
namespace Unity.Reflect.Services.Client
{
    static class Converter
    {

        public static Reflect.ManifestAsset ToApi(this ManifestAsset request)
        {
            return new Reflect.ManifestAsset(request.ProjectId, request.SourceId, request.Manifest);
        }

        public static Reflect.UnityProject ToApi(this UnityProject project)
        {
            return new Reflect.UnityProject(project.ProjectId, project.Id, project.Name, project.ServerName, project.ServerAddress, project.CloudOnly);
        }

        public static Reflect.UnityUser ToApi(this UnityUser user)
        {
            return new Reflect.UnityUser(user.AccessToken, user.DisplayName, user.OrganizationForeignKeys, user.PrimaryOrg, user.UserId, user.Name, user.Valid, user.Whitelisted);
        }

        public static UnitConversion ToGrpc(this Reflect.LengthUnit unitConversion)
        {
            switch (unitConversion)
            {
                case Reflect.LengthUnit.Feet:
                    return UnitConversion.UnitFeetToMeter;
                case Reflect.LengthUnit.Inches:
                    return UnitConversion.UnitInchesToMeter;
                default:
                    return UnitConversion.UnitNone;
            }
        }

        public static AxisConversion ToGrpc(this Reflect.AxisInversion axisConversion)
        {
            switch (axisConversion)
            {
                case Reflect.AxisInversion.FlipYZ:
                    return AxisConversion.AxisFlipYZ;
                default:
                    return AxisConversion.AxisNone;
            }
        }
    }
}
