using System.ComponentModel;

namespace Shared.Enums
{
    public enum Gender : byte
    {
        [Description("Hombre")] Male = 1,
        [Description("Mujer")] Female = 2,
        [Description("Otro")] Othe = 3,
    }
}
