using Microsoft.Extensions.DependencyInjection;
using Services.CustomerService;
using Teste_Crud_Mvc.Services.Interfaces;

namespace Teste_Crud_Mvc.Services.DependencyInjection
{
    public class CreateServicesDependencyInjection
    {
        public static void RegisterDependencyInjection(IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
