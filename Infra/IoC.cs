using Domain.Interfaces;
using Infra.Context;
using Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class IoC
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepository();

        services.AddDbContext<PurchaseAppDbContext>(opt => opt
            .UseSqlServer(configuration.GetConnectionString("PurchaseAppConnection")), ServiceLifetime.Singleton);

        return services;
    }

    public static IServiceCollection AddRepository(this IServiceCollection services)
    {
        services.AddTransient<ITransactionRepository, TransactionRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}