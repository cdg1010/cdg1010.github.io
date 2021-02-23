using System;

namespace Unity.Reflect.Model
{
    public partial class SyncParameter
    {
        // We might want to add other parameter (units, type, editable flag, ....)
        public SyncParameter(string value, string parameterGroup, bool visible)
        {
            Value = Utils.NotNullString(value);
            ParameterGroup = Utils.NotNullString(parameterGroup);
            Visible = visible;
        }
    }
    
}
