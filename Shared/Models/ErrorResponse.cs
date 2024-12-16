namespace Shared.Models
{
    public class ErrorResponse
    {
        public string Error { get; set; } = string.Empty;
        public string[]? ErrorDetails { get; set; }
        public string? Exception { get; set; }
    }
}
