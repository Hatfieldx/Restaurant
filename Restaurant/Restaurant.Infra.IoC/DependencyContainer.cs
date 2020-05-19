using Microsoft.Extensions.DependencyInjection;
using Restaurant.DataAccess;

namespace Restaurant.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegServices(IServiceCollection services)
        {
            //DataAccess
            services.AddScoped<RestaurantContext>();

        }
    }
}
