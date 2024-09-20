//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Graph;
//using Microsoft.Extensions.Options;
//using AutoMais.Services.Sharepoint.List;

//namespace AutoMais.Services.Sharepoint.Setup
//{
//    public static class SharepointStartup
//    {
//        public static IServiceCollection RegisterSharepoint(this IServiceCollection services)
//        {
//            var serviceProvider = services.BuildServiceProvider();
//            var settings = services.BuildServiceProvider().GetRequiredService<IOptions<Setup.SharepointSettings>>().Value;

//            GraphServiceClient client = serviceProvider.GetRequiredService<GraphServiceClient>();

//            services.AddKeyedScoped("MainSite", (provider, key) =>
//            {
//                var sites = client.Sites.GetAsync((requestConfiguration) =>
//                {
//                    //requestConfiguration.QueryParameters.Select = new string[] { "displayName", "id", "lists" };
//                    //requestConfiguration.QueryParameters.Filter = $"startswith(displayName,'{sharePointSiteName}')";
//                }).Result;

//                var mainSite = sites?.Value?.Where(s => s.DisplayName == settings.SiteName).FirstOrDefault();

//                mainSite.Lists = client.Sites[mainSite.Id].Lists.GetAsync()?.Result?.Value;

//                return mainSite;
//            });

//            services.AddTransient<ISharepointService, SharepointService>();

//            return services;
//        }
//    }
//}
