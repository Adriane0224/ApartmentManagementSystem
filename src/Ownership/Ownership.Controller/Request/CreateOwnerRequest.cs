namespace Ownership.Controller.Request
{
    public class CreateOwnerRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
