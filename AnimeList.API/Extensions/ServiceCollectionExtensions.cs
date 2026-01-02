using System.Text.Json.Serialization;
using AnimeList.API.Options;
using AnimeList.Application.Handlers.Anime.Query;
using AnimeList.Application.Handlers.User;
using AnimeList.Application.Handlers.UserAnimeEntry;
using AnimeList.Application.Interfaces.Anime;
using AnimeList.Application.Mapping.Anime;
using AnimeList.Application.Mapping.User;
using AnimeList.Application.Mapping.UserAnimeEntries;
using AnimeList.Application.RepoInterfaces.Anime;
using AnimeList.Application.RepoInterfaces.AnimeRatings;
using AnimeList.Application.RepoInterfaces.AnimeRecommendations;
using AnimeList.Application.RepoInterfaces.AnimeStats;
using AnimeList.Application.RepoInterfaces.User;
using AnimeList.Application.RepoInterfaces.UserAnimeEntry;
using AnimeList.Persistence.CSV.Anime;
using AnimeList.Persistence.CSV.AnimeRatings;
using AnimeList.Persistence.CSV.AnimeRecommendations;
using AnimeList.Persistence.CSV.AnimeStats;
using AnimeList.Persistence.Database;
using AnimeList.Persistence.Database.Orchestrator;
using AnimeList.Persistence.Repositories.Anime;
using AnimeList.Persistence.Repositories.AnimeRatings;
using AnimeList.Persistence.Repositories.AnimeRecommendations;
using AnimeList.Persistence.Repositories.AnimeStats;
using AnimeList.Persistence.Repositories.User;
using AnimeList.Persistence.Repositories.UserAnimeEntries;
using Microsoft.Extensions.Options;
// Split this file into 3... eventually

namespace AnimeList.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                    JsonIgnoreCondition.WhenWritingNull;
            });
        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration config)
    {
        // Bind CsvSettings from appsettings.json into CsvSettings object
        services.Configure<CsvSettings>(config.GetSection("CsvSettings"));

        // Connection string from appsettings.json
        var connectionString =
            config.GetConnectionString("AnimeListDatabase")
            ?? throw new InvalidOperationException("Connection string not found");


        // DB Connection factory
        services.AddSingleton<IDbConnectionFactory>(_ =>
            new SqliteConnectionFactory(connectionString));

        // Repos
        // Use AddScoped for repos, they are created each time needed
        // Singletons are created once and used for the whole lifetime, they're more expensive though
        services.AddScoped<IAnimeLoadRepository, AnimeLoadRepository>();
        services.AddScoped<IAnimeRepository, AnimeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserAnimeEntryRepository, UserAnimeEntryRepository>();
        services.AddScoped<IAnimeStatsLoadRepository, AnimeStatsLoadRepository>();
        services.AddScoped<IAnimeRecommendationsLoadRepository, AnimeRecommendationsLoadRepository>();
        services.AddScoped<IAnimeRatingsLoadRepository, AnimeRatingsLoadRepository>();


        // CSV parsers
        // Get paths from Options/CsvSettings
        // When app needs a CSVAnimeParser it calls this and pulls these dependencies
        services.AddSingleton<CsvAnimeParser>(sp =>
        {
            var csv = sp.GetRequiredService<IOptions<CsvSettings>>().Value;
            return new CsvAnimeParser(csv.DetailsPath);
        });

        services.AddSingleton<CsvAnimeStatsParser>(sp =>
        {
            var csv = sp.GetRequiredService<IOptions<CsvSettings>>().Value;
            return new CsvAnimeStatsParser(csv.StatsPath);
        });

        services.AddSingleton<CsvAnimeRecommendationsParser>(sp =>
        {
            var csv = sp.GetRequiredService<IOptions<CsvSettings>>().Value;
            return new CsvAnimeRecommendationsParser(csv.RecommendationsPath);
        });

        services.AddSingleton<CsvAnimeRatingsParser>(sp =>
        {
            var csv = sp.GetRequiredService<IOptions<CsvSettings>>().Value;
            return new CsvAnimeRatingsParser(csv.RatingsPath);
        });

    // Raw CSV -> Domain mapping
        services.AddSingleton<RawAnimeMapper>();
        services.AddSingleton<RawAnimeStatsMapper>();
        services.AddSingleton<RawAnimeRecommendationsMapper>();
        services.AddSingleton<RawAnimeRatingsMapper>();
        
        // Aggregation
        services.AddSingleton<RawAnimeRecommendationsAggregator>();

        // DB Init / orchestration
        services.AddSingleton<DbInitializer>();
        services.AddScoped<AnimeDbOrchestrator>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<AnimeQueryHandler>();
        services.AddSingleton<IAnimeMapper, AnimeMapper>();
        
        services.AddScoped<UserQueryHandler>();
        services.AddScoped<UserCommandHandler>();
        services.AddSingleton<IUserMapper, UserMapper>();
        
        services.AddScoped<UserAnimeEntryCommandHandler>();
        services.AddSingleton<IUserAnimeEntryMapper, UserAnimeEntryMapper>();
        
        return services;
    }
}