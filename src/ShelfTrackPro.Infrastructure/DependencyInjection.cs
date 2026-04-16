using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShelfTrackPro.Domain.Interfaces;
using ShelfTrackPro.Infrastructure.Data;
using ShelfTrackPro.Infrastructure.Repositories;

namespace ShelfTrackPro.Infrastructure;

/// <summary>
/// Extension method to register all Infrastructure services in the DI container.
/// Called from Program.cs: builder.Services.AddInfrastructure(builder.Configuration);
///
/// This pattern keeps Program.cs clean — instead of 15 lines of service registration,
/// it's one line. Each layer registers its own services.
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration) 
    {  
        // - DATABASE -
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                sqlOptions =>
                {
                    // Retry failed DB connections automatically (resiliência / resiliency)
                    // Useful for Azure SQL which may drop connections during scaling events
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                }));

        // ─── Repositories ───
        // Scoped = one instance per HTTP request (same as Spring's default scope)
        // Every repository in the same request shares the same DbContext instance
            
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IStockMovementRepository, StockMovementRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    
    }
    
}