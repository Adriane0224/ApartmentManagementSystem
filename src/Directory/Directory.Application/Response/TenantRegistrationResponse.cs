namespace Identity.Application.Response
{
    public class TenantRegistrationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public Guid? TenantId { get; set; }
    }
}
