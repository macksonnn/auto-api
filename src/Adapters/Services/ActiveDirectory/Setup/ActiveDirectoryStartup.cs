using Azure.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Becape.Services.ActiveDirectory.Setup
{
    public static class ActiveDirectoryStartup
    {
        public static IServiceCollection RegisterActiveDirectory(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ActiveDirectorySettings>((c) => configuration.GetSection("ActiveDirectorySettings"));

            var settings = services.BuildServiceProvider().GetRequiredService<IOptions<ActiveDirectorySettings>>().Value;

            var options = new ClientSecretCredentialOptions { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud };

            ClientSecretCredential clientSecretCredential = new(settings.TenantId, settings.ClientId, settings.ClientSecret, options);

            services.AddSingleton(new GraphServiceClient(clientSecretCredential, settings.Scopes));

            //services.AddSingleton<IUserManagement, UserManagement>();

            return services;
        }
    }
}
