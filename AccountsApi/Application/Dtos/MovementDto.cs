namespace AccountsApi.Application.Dtos
{
    public class MovementDto
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
