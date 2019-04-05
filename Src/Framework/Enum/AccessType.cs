using System.ComponentModel;

namespace Framework.Enum
{
    public enum AccessType
    {
        [Description("غیر مجاز")]
        Deny = 0,
        [Description(" مجاز")]
        Allow = 1
    }
}