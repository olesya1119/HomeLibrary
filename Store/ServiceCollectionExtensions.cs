using Microsoft.Extensions.DependencyInjection;
using Store.Repositories;

namespace Store;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHomeLibraryStore(
        this IServiceCollection services)
    {
        services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();

        services.AddTransient<IPublisherRepository, PublisherRepository>();
        services.AddTransient<IAuthorRepository, AuthorRepository>();
        services.AddTransient<IGenreRepository, GenreRepository>();
        services.AddTransient<IBookRepository, BookRepository>();

        return services;
    }
}
