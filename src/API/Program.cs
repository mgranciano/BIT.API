namespace API;
using API.Attributes;
using Application.Services;
using Dominio.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System;

/// <summary>
/// Configuración inicial del servidor y los servicios.
/// </summary>
/// <remarks>
/// Este archivo configura los servicios esenciales para la API, incluyendo:
/// - Configuración de Serilog para logs en Consola, Archivos y Azure Application Insights.
/// - Configuración de Middleware para autenticación y autorización.
/// - Configuración de controladores, Swagger y base de datos.
/// </remarks>
public static class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console() 
            .WriteTo.File("logs/api-log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        var builder = WebApplication.CreateBuilder(args);

        builder.Host.UseSerilog(Log.Logger);

        /// <summary>
        /// Cargar configuración desde `appsettings.json`
        /// </summary>
        var configuration = builder.Configuration;

        bool useApplicationInsights = configuration.GetValue<bool>("Logging:UseApplicationInsights");

        if (useApplicationInsights)
        {
            var instrumentationKey = configuration["Logging:ApplicationInsights:InstrumentationKey"];
            if (!string.IsNullOrEmpty(instrumentationKey))
            {
                Log.Information("✅ Envío de logs a Application Insights ACTIVADO.");
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .WriteTo.File("logs/api-log-.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.ApplicationInsights(instrumentationKey, TelemetryConverter.Traces)
                    .CreateLogger();
            }
            else
            {
                Log.Warning("⚠️ Instrumentation Key no encontrada. No se enviarán logs a Application Insights.");
            }
        }
        else
        {
            Log.Information("✅ Envío de logs solo a Consola y Archivos.");
        }

        builder.Services.Configure<MvcOptions>(options =>
        {
            options.RespectBrowserAcceptHeader = false;
            options.ReturnHttpNotAcceptable = true; 
        });

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null; 
                options.JsonSerializerOptions.WriteIndented = true;
            });

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.UseCases.Usuarios.Queries.ObtenerUsuariosQuery).Assembly));
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Usuario API",
                Version = "v1",
                Description = "API para la gestión de BIT.",
                Contact = new OpenApiContact
                {
                    Name = "Soporte BIT.API",
                    Email = "xxxx@xxxx.com"
                }
            });
            c.SupportNonNullableReferenceTypes();
            c.OperationFilter<ProducesUsuarioResponseFilter>();
        });

        bool usarSql = false;

        if (usarSql)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositorySQL>();
            builder.Services.AddScoped<IUsuarioReader, UsuarioRepositorySQL>();
            builder.Services.AddScoped<IUsuarioWriter, UsuarioRepositorySQL>();
        }
        else
        {
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryJSON>();
            builder.Services.AddScoped<IUsuarioReader, UsuarioRepositoryJSON>();
            builder.Services.AddScoped<IUsuarioWriter, UsuarioRepositoryJSON>();
        }

        builder.Services.AddScoped<UsuarioService>();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseAuthorization();
        app.MapControllers();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Usuario API v1");
                c.RoutePrefix = "swagger";
            });

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger");
                    return;
                }
                await next();
            });
        }

        app.Run();
    }
}
