
namespace AutoMais.Services.ActiveDirectory.Setup
{
    public class ActiveDirectorySettings
    {
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? TenantId { get; set; }
        public string[] Scopes { get; set; } = new[] { "https://graph.microsoft.com/.default" };
    }
}
