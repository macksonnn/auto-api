using Becape.Core.Common.Startup;
using Core.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace States.Mongo.Startup
{
    public class MongoSettings : IApplication
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class MongoStartupRegister : IStartupRegister
    {
        public IServiceCollection Register(IServiceCollection services, IConfiguration configuration)
        {
            // Bind MongoSettings from configuration
            var settings = configuration.GetSection("StateSettings:MongoDB").Get<MongoSettings>();
            services.AddSingleton<MongoSettings>(settings ?? new MongoSettings());

            // Register MongoClient
            services.AddSingleton<IMongoClient>(sp =>
            {
                var mongoSettings = sp.GetRequiredService<MongoSettings>();
                return new MongoClient(mongoSettings.ConnectionString);
            });

            // Register IMongoDatabase
            services.AddSingleton(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var mongoSettings = sp.GetRequiredService<MongoSettings>();
                return client.GetDatabase(mongoSettings.DatabaseName);
            });

            var startupRegisterType = typeof(IState);

            var assembly = Assembly.GetExecutingAssembly();
            var repositoryTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && startupRegisterType.IsAssignableFrom(t))
                .ToList();
            
            foreach (var repositoryType in repositoryTypes)
            {
                var interfaces = repositoryType.GetInterfaces()
                    .Where(@interface => @interface != startupRegisterType)
                    .ToList();

                if (interfaces.Count != 1)
                {
                    throw new InvalidOperationException($"Repository '{repositoryType.Name}' must implement only one interface that implements IState<>");
                }

                services.AddScoped(interfaces[0], repositoryType);
            }


            return services;
        }

        private IServiceCollection RegisterStates(IServiceCollection services, IConfiguration configuration)
        {
            //var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            //var startupRegisterType = typeof(IState);

            //var startups = assemblies
            //   .SelectMany(assembly => assembly.GetTypes())
            //   .Where(type => startupRegisterType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            //   .Select(Activator.CreateInstance)
            //   .Cast<IStartupRegister>()
            //   .ToList();

            //foreach (var startup in startups)
            //    startup.Register(services, configuration);

            return services;
        }
    }
}
