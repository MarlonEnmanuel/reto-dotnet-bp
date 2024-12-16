namespace AccountsApi.Application.Dtos
{
    public record ReportDto
    {
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
        public string ClientIdentification { get; set; } = string.Empty;
        public string ClientName { get; set; } = string.Empty;
        public List<ReportAccountDto> Accounts { get; set; } = [];
    }

    public record ReportAccountDto
    {
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountType { get; set; } = string.Empty;
        public bool Status { get; set; }
        public List<ReportMovementDto> Movements { get; set; } = [];
    }

    public record ReportMovementDto
    {
        public int? MovementId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
