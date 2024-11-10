using System.Text.Json.Serialization;
using api.Config.Filters;
using api.Data;
using api.Data.Interceptor;
using api.Models.User;
using AutoMapper;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace api.Config
{
    public static class ConfigExtensions
    {
        public static IServiceCollection AddInitInjects(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddApisServices()
                .AddAutoMapper(typeof(Program).Assembly)
                .AddRepositorys()
                .AddInitStorageAuth(configuration)
                .AddSwagger()
                .Configure<RouteOptions>(op => op.LowercaseUrls = true)
                .AddHangOptions(configuration);

            return services;
        }

        public static IServiceCollection AddApisServices(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            services.AddEndpointsApiExplorer();
            return services;
        }

        public static IServiceCollection AddInitStorageAuth(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<DataAppContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"));
                options.AddInterceptors(new TimestampInterceptor(), new BookingInterceptor());
            });
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services
                .AddIdentity<AppUserModel, IdentityRole>()
                .AddEntityFrameworkStores<DataAppContext>();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(
                            System.Text.Encoding.UTF8.GetBytes(configuration["JWT:SignInKey"] ?? "")
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });
            services.AddAuthorization();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Booking System", Version = "v1" });
                opt.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer",
                    }
                );

                opt.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer",
                                },
                            },
                            Array.Empty<string>()
                        },
                    }
                );
            });
            return services;
        }

        public static IServiceCollection AddHangOptions(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services
                .AddHangfire(config =>
                    config
                        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                        .UseSimpleAssemblyNameTypeSerializer()
                        .UseRecommendedSerializerSettings()
                        .UsePostgreSqlStorage(options =>
                            options.UseNpgsqlConnection(
                                configuration.GetConnectionString("Default")
                            )
                        )
                )
                .AddHangfireServer();

            return services;
        }
    }
}
