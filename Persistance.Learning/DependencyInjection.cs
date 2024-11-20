using EntityFramework.Exceptions.MySQL.Pomelo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Learning
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceLearningServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LearningDbContext>(options =>
                options.UseMySql(configuration.GetConnectionString("LearningConnection"),
                                 new MySqlServerVersion(new Version(8, 0, 20)),
                                 mySQLOptions =>
                                 {
                                     mySQLOptions.EnableRetryOnFailure(2);
                                     mySQLOptions.MigrationsAssembly(typeof(LearningDbContext).Assembly.FullName);
                                 })
                       .EnableSensitiveDataLogging()
                       .UseExceptionProcessor());

            services.AddScoped<ILearningDbContext>(provider => provider.GetService<LearningDbContext>());

            return services;
        }
    }
}
