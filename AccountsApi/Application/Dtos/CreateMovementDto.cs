namespace AccountsApi.Application.Dtos
{
    public class CreateMovementDto
    {
        public string AccountNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
