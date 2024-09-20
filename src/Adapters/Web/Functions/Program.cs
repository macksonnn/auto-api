using AutoMais.Functions.Middleware;
using AutoMais.Integrations.Startup;
using AutoMais.Services.ActiveDirectory.Setup;
using Core.Application.Ticket.Commands;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    //.ConfigureFunctionsWebApplication()
    .ConfigureFunctionsWebApplication(workerApplication =>
    {
        workerApplication.UseMiddleware<ExceptionHandlingMiddleware>();
        workerApplication.UseMiddleware<LoggingMiddleware>();
    })
    
    .ConfigureServices((hostbuilder, services) =>
    {
        services.AddSingleton<LoggingMiddleware>();
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        // Register all the libraries used by this project
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(new[] { 
            typeof(IStartupRegister).Assembly,
            typeof(CreateTicketCommandHandler).Assembly
        }));

        //TODO: Create a way to automatically load the Startup classes and call register

        //This will register EventHub or ServiceBus, depending on the configuration value
        services.RegisterStream(hostbuilder.Configuration);
        services.RegisterActiveDirectory(hostbuilder.Configuration);
        //services.RegisterSharepoint();
        //services.RegisterSendgrid(hostbuilder.Configuration);

    })
    .Build();

host.Run();


