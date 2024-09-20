using AutoMais.Core.Application.Adapters.Stream;
using AutoMais.Core.Common;
using Core.Common.ActionFilters;
using Core.Common.Validation;
using FluentValidation;

namespace AutoMais.Ticket.Api.Extensions
{
    public static class StartupExtensions
    {
        public static void RegisterServices(this WebApplicationBuilder builder)
        {

            // this namespace is for Minimal APIs
            builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(opts => opts.SerializerOptions.IncludeFields = true);

            builder.Services.AddTransient(provider =>
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                const string categoryName = "Any";
                return loggerFactory.CreateLogger(categoryName);
            });

            //Register all validators founded in the Core.Application project
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(IStream));

            //Here we will map all the Mediatr files to the Dependency Injection
            builder.Services.AddMediatR(cfg =>
            {

                //Register all handlers founded in the Core.Application project
                cfg.RegisterServicesFromAssemblies(new[] {
                    typeof(IStartupRegister).Assembly,
                    typeof(IStream).Assembly
                });

                //Add the Validation Behavior to the Mediatr pipeline
                cfg.AddOpenBehavior(typeof(MediatrValidationBehavior<,>));
            });

            builder.Services.AddControllers(options => { options.Filters.Add<ResultTypeFilter>(); });

            var configuration = builder.Services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            builder.Services.RegisterDynamicClasses(configuration);
            builder.Services.RegisterStreams(configuration);
        }

        public static void RegisterEndpointDefinitions(this WebApplication app)
        {
            IEnumerable<IEndpointDefinition> endpointDefinitions = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();

            foreach (var endpointDef in endpointDefinitions)
                endpointDef.RegisterEndpoints(app);
        }
    }

}
