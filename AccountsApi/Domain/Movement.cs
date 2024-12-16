namespace AccountsApi.Domain
{
    public class Movement
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; } = string.Empty;

        // navigation properties
        public Account Account { get; set; } = default!;


        // methods

        public decimal InitialBalance => Balance - Amount;

        public bool IsReversion => Description.Contains("Anulado") ||
                                   Description.Contains("Anulación");
    }
}
