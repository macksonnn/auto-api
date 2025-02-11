﻿namespace AutoMais.Ticket.Api.Startup
{
    public class WebApiStartupRegister : IStartupRegister
    {
        public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
        {
            // this namespace is for Minimal APIs
            services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(opts => opts.SerializerOptions.IncludeFields = true);
            //Register all validators founded in the Core.Application project
            //services.AddControllers(options => { options.Filters.Add<ResultTypeFilter>(); });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}