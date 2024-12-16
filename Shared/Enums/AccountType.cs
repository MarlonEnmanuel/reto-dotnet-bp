using System.ComponentModel;

namespace Shared.Enums
{
    public enum AccountType : byte
    {
        [Description("Corriente")] Checking = 1,
        [Description("Ahorros")] Savings = 2,
    }
}
