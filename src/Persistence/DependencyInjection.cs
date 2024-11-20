using EntityFramework.Exceptions.MySQL.Pomelo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Engage.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("DefaultConnection"),
                             new MySqlServerVersion(new Version(8, 0, 20)),
                             mySQLOptions =>
                             {
                                 mySQLOptions.EnableRetryOnFailure(2);
                                 mySQLOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                                 mySQLOptions.UseMicrosoftJson();
                             })
                   .EnableSensitiveDataLogging()
                   //.EnableDetailedErrors()
                   .UseExceptionProcessor());
        ;

        services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

        return services;
    }
}
