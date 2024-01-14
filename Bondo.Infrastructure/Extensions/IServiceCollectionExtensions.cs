using Bondo.Application.Interfaces;
using Bondo.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Bondo.Infrastructure.Extensions;
public static class IServiceCollectionExtensions
{
    public static void AddInfrastructureLayer(this IServiceCollection services)
    {

        services.AddServices();
    }


    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IOtpService, OtpService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IContractService, ContractService>();
        services.AddScoped<IEmailService, EmailService>();
    }
}
