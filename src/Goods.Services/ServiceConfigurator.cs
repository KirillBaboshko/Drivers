using Goods.Domain.Services;
using Goods.Services.Drivers;
using Goods.Services.Drivers.Repositories;
using Goods.Services.Products;
using Goods.Services.Products.Repositories;
using Goods.Services.TransportVechiles;
using Goods.Services.TransportVechiles.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Goods.Services;

public static class ServiceConfigurator
{
    public static IServiceCollection AddServices(this IServiceCollection collection)
    {
        collection.AddSingleton<IProductsService, ProductsService>();
        collection.AddSingleton<IProductsRepository, ProductsRepository>();

        collection.AddSingleton<IDriversService, DriversService>();
        collection.AddSingleton<IDriversRepository, DriversRepository>();

        collection.AddSingleton<ITransportVechilesService, TransportVechilesService>();
        collection.AddSingleton<ITransportVechilesRepository, TransportVechilesRepository>();

        return collection;
    }
}
