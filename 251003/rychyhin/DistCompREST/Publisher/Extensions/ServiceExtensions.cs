﻿using Microsoft.EntityFrameworkCore;
using Publisher.Data;
using Publisher.HttpClients;
using Publisher.HttpClients.Implementations;
using Publisher.HttpClients.Interfaces;
using Publisher.Repositories;
using Publisher.Repositories.Implementations;
using Publisher.Repositories.Interfaces;
using Publisher.Services.Implementations;
using Publisher.Services.Interfaces;

namespace Publisher.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEditorRepository, DatabaseEditorRepository>();
        services.AddScoped<INewsRepository, DatabaseNewsRepository>();
        services.AddScoped<ILabelRepository, DatabaseLabelRepository>();
        
        services.Decorate<IEditorRepository, CachedEditorRepository>();
        services.Decorate<INewsRepository, CachedNewsRepository>();
        services.Decorate<ILabelRepository, CachedLabelRepository>();
        
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IEditorService, EditorService>();
        services.AddScoped<INewservice, Newservice>();
        services.AddScoped<ILabelService, LabelService>();
        
        services.AddScoped<IDiscussionClient, DiscussionClient>();
        services.Decorate<IDiscussionClient, CachedDiscussionClient>();

        return services;
    }
    
    public static IServiceCollection AddDiscussionClient(this IServiceCollection services)
    {
        services
            .AddHttpClient(nameof(DiscussionClient), 
                client => client.BaseAddress = new Uri
                    ($"http://{Environment.GetEnvironmentVariable("DISCUSSION_HOST")}:24130/api/v1.0/"));

        return services;
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = $"Host={Environment.GetEnvironmentVariable("POSTGRES_HOST")};" +
                               $"Port={Environment.GetEnvironmentVariable("POSTGRES_PORT")};" +
                               $"Database=distcomp;" +
                               $"Username=postgres;" +
                               $"Password=postgres";
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        return services;
    }
    
    public static void ApplyMigrations(this IApplicationBuilder app, IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var migrations = db.Database.GetPendingMigrations();

        if (migrations.Any())
        {
            db.Database.Migrate();
        }
    }
}