using Engage.Application;
using Engage.Application.Interfaces;
using Engage.Domain.Common;
using Engage.Infrastructure;
using Engage.Persistence;
using Engage.WebApi.Common.Authentication;
using Engage.WebApi.Services;
using Finbuckle.MultiTenant;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using NSwag;
using NSwag.Generation.Processors.Security;
using Okta.AspNetCore;
using System.Security.Claims;

namespace Engage.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        bool.TryParse(Configuration["FeatureSwitch:IsClerkAuthentication"], out var isClerkAuthentication);

        if (isClerkAuthentication == true)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = Configuration["Clerk:Authority"];
                        options.TokenValidationParameters = new()
                        {
                            ValidateAudience = false,
                            NameClaimType = ClaimTypes.NameIdentifier
                        };
                        options.Events = new()
                        {
                            OnTokenValidated = context =>
                            {
                                // Authorized party is the base URL of your frontend.
                                var azp = context.Principal?.FindFirstValue("azp");

                                if (string.IsNullOrWhiteSpace(azp) || !azp.Equals(Configuration["Clerk:AuthorizedParty"]))
                                {
                                    context.Fail("Authorized party claim is invalid or missing");
                                }

                                return Task.CompletedTask;
                            }
                        };
                    });
        }
        else
        {
            // Okta Authentication
            var authenticationBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            });

            var jwtProvider = Configuration["Jwt:Provider"];
            if (!string.IsNullOrWhiteSpace(jwtProvider))
            {
                jwtProvider = jwtProvider.ToLower();
                if (jwtProvider == "okta")
                {
                    authenticationBuilder.AddOktaWebApi(new OktaWebApiOptions()
                    {
                        OktaDomain = Configuration["Jwt:Authority"]
                    });
                }
            }
            else
            {
                authenticationBuilder.AddJwtBearer(options =>
                {
                    options.Authority = Configuration["Jwt:Authority"];
                    options.Audience = Configuration["Jwt:Audience"];
                });
            }
        }

        services.AddAuthorizationPolicies(isClerkAuthentication);

        services.AddCors(o => o.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .WithExposedHeaders("Content-Disposition");
        }));

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<ApiKeyAuthFilter>();

        services.AddMultiTenant<TenantAndSupplierInfo>()
                .WithHeaderStrategy()
                .WithStaticStrategy("engage")
                .WithConfigurationStore();

        services.AddTransient<IClerkHttpClient, ClerkHttpClient>();

        services.AddHttpClient();

        services.AddHttpClient("Clerk", httpClient =>
        {
            var secretKey = Configuration["Clerk:SecretKey"];
            var baseUrl = Configuration["Clerk:BaseAddress"];

            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {secretKey}");
            httpClient.BaseAddress = new Uri(baseUrl);
        });

        services.AddHttpContextAccessor();

        services.AddApplicationServices();
        services.AddInfrastructureServices(Configuration);
        services.AddPersistenceServices(Configuration);

        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();

        services.AddValidatorsFromAssemblyContaining<IAppDbContext>();

        services.AddHealthChecks()
                .AddDbContextCheck<AppDbContext>();

        services.AddControllersWithViews()
                .AddNewtonsoftJson();

        services.AddRazorPages();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // Setup Api Versioning              
        services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
        services.AddVersionedApiExplorer(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.GroupNameFormat = "VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        // Register OpenApi Documents
        services.AddOpenApiDocument(settings =>
        {
            settings.DocumentName = "v1";
            settings.ApiGroupNames = new[] { "1" };
            settings.Version = "1";
            settings.Title = "Engage API";
            settings.Description = "The Engage ASP.NET Core Web Api";
            settings.PostProcess = document => document.Info.Contact = CreateOpenApiContact();
            settings.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            settings.AddSecurity("JWT Token", Enumerable.Empty<string>(), CreateOpenApiSecurityScheme());
        });

        services.AddOpenApiDocument(settings =>
        {
            settings.DocumentName = "v2";
            settings.ApiGroupNames = new[] { "2" };
            settings.Version = "2";
            settings.Title = "Engage Api";
            settings.Description = "The Engage ASP.NET Core Web Api";
            settings.PostProcess = document => document.Info.Contact = CreateOpenApiContact();
            settings.OperationProcessors.Add(new OperationSecurityScopeProcessor("JWT Token"));
            settings.AddSecurity("JWT Token", Enumerable.Empty<string>(), CreateOpenApiSecurityScheme());
        });

        QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

        //services.AddHostedService<Worker>();

        services.Configure<JwtOptions>(options => Configuration.GetSection("Jwt").Bind(options));
        services.Configure<AzureBlobOptions>(options => Configuration.GetSection("AzureBlob").Bind(options));
        services.Configure<AzureBlobStorageOptions>(options => Configuration.GetSection("AzureBlobStorage").Bind(options));
        services.Configure<SendGridOptions>(options => Configuration.GetSection("SendGrid").Bind(options));
        services.Configure<TwilioOptions>(options => Configuration.GetSection("Twilio").Bind(options));
        services.Configure<List<SupplierClaim>>(options => Configuration.GetSection("SupplierClaims").Bind(options));
        services.Configure<List<OrganizationClaim>>(options => Configuration.GetSection("OrganizationClaims").Bind(options));
        services.Configure<ImageOptions>(options => Configuration.GetSection("Images").Bind(options));
        services.Configure<ImportFileOptions>(options => Configuration.GetSection("ImportFiles").Bind(options));
        services.Configure<OrderDefaultsOptions>(options => Configuration.GetSection("OrderDefaults").Bind(options));
        services.Configure<FeatureSwitchOptions>(options => Configuration.GetSection("FeatureSwitch").Bind(options));
        services.Configure<SmtpSettings>(options => Configuration.GetSection("SmtpSettings").Bind(options));
        services.Configure<ClaimSettings>(options => Configuration.GetSection("ClaimSettings").Bind(options));
        services.Configure<PvgSettings>(options => Configuration.GetSection("PvgSettings").Bind(options));
        services.Configure<ContactReportSettings>(options => Configuration.GetSection("ContactReportSettings").Bind(options));
        services.Configure<ClerkOptions>(options => Configuration.GetSection("Clerk").Bind(options));
        services.Configure<PosUpdateEmailOptions>(options => Configuration.GetSection("PosUpdateEmail").Bind(options));
        services.Configure<EngagementSettings>(options => Configuration.GetSection("ProjectTacOpsSettings").Bind(options));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Error")
               .UseHsts();
        }

        app.UseCustomExceptionHandler()
           .UseHealthChecks("/health")
           .UseHttpsRedirection()
           .UseStaticFiles()
           .UseOpenApi()
           .UseSwaggerUi()
           .UseMultiTenant()
           .UseAuthentication()
           .UseRouting()
           .UseCors("AllowAll")
           //.UseMiddleware<ApiKeyMiddleware>()
           .UseAuthorization()
           .UseEndpoints(endpoints =>
       {

           var allowAnonymous = Configuration.GetValue<bool>("Jwt:AllowAnonymous");
           if (allowAnonymous)
           {
               endpoints.MapControllers().AllowAnonymous();
           }

           endpoints.MapDefaultControllerRoute();
           endpoints.MapRazorPages();
       });
    }

    private static OpenApiContact CreateOpenApiContact()
    {
        return new OpenApiContact
        {
            Name = "Engage Support",
            Email = "engagesupport@insightconsulting.co.za"
        };
    }

    private static OpenApiSecurityScheme CreateOpenApiSecurityScheme()
    {
        return new OpenApiSecurityScheme()
        {
            Type = OpenApiSecuritySchemeType.ApiKey,
            Name = "Authorization",
            In = OpenApiSecurityApiKeyLocation.Header,
            Description = "Copy this into the value field: Bearer {token}"
        };
    }
}
