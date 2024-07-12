using DAO.Connections;
using DAO.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DAO.DependencyInjection
{
    public class CreateDaoDependencyInjection
    {
        public static void RegisterDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<ICustomerDAO, CustomerDAO>();
        }
    }
}
