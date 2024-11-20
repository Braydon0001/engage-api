using Engage.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Engage.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTimeService, DateTimeService>();
        services.AddTransient<IImageService, PhysicalFileImageService>();
        services.AddTransient<IBlobService, AzureBlobService>();
        services.AddTransient<ICsvService, CsvService>();
        services.AddTransient<IExcelService, ExcelService>();
        services.AddTransient<IFileService, AzureBlobStorageService>();
        services.AddTransient<ITargetingService, TargetingService>();
        services.AddTransient<IOktaService, OktaService>();
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IEmailService2, EmailService2>();
        services.AddTransient<IWhatsAppService, WhatsAppService>();
        services.AddTransient<IHandlebarsService, HandlebarsService>();
        services.AddTransient<IPdfService, PdfService>();
        services.AddTransient<IHttpClientUtilService, HttpClientUtilService>();

        return services;
    }
}
