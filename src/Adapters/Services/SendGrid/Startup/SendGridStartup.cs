//using Microsoft.Extensions.Options;
//using Microsoft.Extensions.Configuration;
//using AutoMais.Services.SendGrid.Service;

//namespace AutoMais.Services.SendGrid.Startup
//{
//    public static class SendGridStartup
//    {
//        public static IServiceCollection RegisterSendgrid(this IServiceCollection services, IConfiguration configuration)
//        {
//            services.Configure<SendGridSettings>((c) => configuration.GetSection("SendGridSettings"));

//            var settings = services.BuildServiceProvider().GetRequiredService<IOptions<SendGridSettings>>().Value;

//            services.AddScoped(provider => new SendGridClient(settings.Apikey));

//            services.AddKeyedSingleton("FromAddress", (provider, key) =>
//            {
//                return new MailAddress(settings.SenderEmail, settings.SenderName);
//            });

//            services.AddTransient<SendGridMailService>();

//            return services;
//        }
//    }
//}
