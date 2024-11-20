using Engage.Application.Filters;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using System.Reflection;

namespace Engage.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddMassTransit(settings =>
        {
            settings.SetKebabCaseEndpointNameFormatter();

            // By default, sagas are in-memory, but should be changed to a durable
            // saga repository.
            settings.SetInMemorySagaRepositoryProvider();

            //var entryAssembly = Assembly.Load("Engage.Application");

            settings.AddConsumers(Assembly.GetExecutingAssembly());
            settings.AddSagaStateMachines(Assembly.GetExecutingAssembly());
            settings.AddSagas(Assembly.GetExecutingAssembly());
            settings.AddActivities(Assembly.GetExecutingAssembly());

            settings.UsingAzureServiceBus((context, cfg) =>
            {
                cfg.Host("Endpoint=sb://engagesb.servicebus.windows.net/;SharedAccessKeyName=MassTransit;SharedAccessKey=1ECKrNQIwjoAxUU7HsnMcQB8ABD6Na1OA+ASbDbmKv0=");
                cfg.ConfigureEndpoints(context);
                cfg.UseRawJsonSerializer(RawSerializerOptions.AddTransportHeaders | RawSerializerOptions.CopyHeaders);
                cfg.DefaultContentType = new ContentType("application/json");
                cfg.UseRawJsonDeserializer();
                cfg.UseConsumeFilter(typeof(TenantConsumeFilter<>), context);
            });
        });

        return services;
    }
}
