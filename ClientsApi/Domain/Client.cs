namespace ClientsApi.Domain
{
    public class Client : Person
    {
        public string Password { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
