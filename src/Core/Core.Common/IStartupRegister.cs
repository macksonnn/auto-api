﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace AutoMais.Core.Common;

/// <summary>
/// Used to define an automatic way to register startup classes
/// </summary>
public interface IStartupRegister
{
    IServiceCollection Register(IServiceCollection services, IConfiguration configuration);
}

public static class StartupRegister
{
    public static IServiceCollection RegisterDynamicClasses(this IServiceCollection services, IConfiguration configuration)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var startupRegisterType = typeof(IStartupRegister);

        var startups = assemblies
           .SelectMany(assembly => assembly.GetTypes())
           .Where(type => startupRegisterType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
           .Select(Activator.CreateInstance)
           .Cast<IStartupRegister>()
           .ToList();

        foreach (var startup in startups)
            startup.Register(services, configuration);

        return services;
    }
}
