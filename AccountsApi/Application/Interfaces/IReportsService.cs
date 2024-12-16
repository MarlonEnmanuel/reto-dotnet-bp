using AccountsApi.Application.Dtos;

namespace AccountsApi.Application.Interfaces
{
    public interface IReportsService
    {
        Task<ReportDto> GetReport(GenerateReportDto dto);
    }
}