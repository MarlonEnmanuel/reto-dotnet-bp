namespace AccountsApi.Application.Dtos
{
    public record GenerateReportDto
    {
        public int ClientId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
