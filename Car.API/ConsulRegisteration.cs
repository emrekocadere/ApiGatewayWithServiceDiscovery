﻿
using Consul;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;

namespace Car.API;

public static class ConsulRegisteration
{   
    public static string Url;
    public static IServiceCollection ConfigurationConsul(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(consulConfig =>
        {

            var address = configuration["ConsulConfig:Address"];
            consulConfig.Address = new Uri(address);
             var JsonConfig=configuration["Url"];
             Url=JsonConfig;
        }));
        return services;
    }

    public static IApplicationBuilder RegisterWithConsul(this WebApplication app, IHostApplicationLifetime lifetime)
    {
        //11.satırla bir bağlantısı var
        var consulClient = app.Services.GetRequiredService<IConsulClient>();

        //Register service with consul

        var uri = new Uri(Url);
        var registration = new AgentServiceRegistration()
        {
            ID = $"Car",
            Name = "Car",
            Address = $"{uri.Host}",
            Port = uri.Port,
            Check = new AgentCheckRegistration()
            {
                HTTP = "http://localhost:5169/api/car/health",
                Interval = TimeSpan.FromSeconds(10)
            },
            Tags = new[] { "Car Service", "Car" }
        };
        


        consulClient.Agent.ServiceDeregister(registration.ID).Wait();
        consulClient.Agent.ServiceRegister(registration).Wait();


        return app;
    }


}
