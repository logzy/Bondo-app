using Bondo.Application.Interfaces;
using Bondo.Application.Interfaces.Repositories;
using Bondo.Persistence.Contexts;
using Bondo.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Bondo.Persistence.Extensions;
public static class IServiceCollectionExtensions
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddMappings();
        services.AddDbContext(configuration);
        services.AddRepositories();
    }

    //private static void AddMappings(this IServiceCollection services)
    //{
    //    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    //}

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppSqlDbContext>(options =>
        options.UseSqlServer(
            configuration.GetConnectionString("BondoSqlConn")));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services
            .AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddTransient<IUserRepository, UserRepository>();
    }
}
