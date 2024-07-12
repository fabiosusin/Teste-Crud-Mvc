using Models.Base.Output;
using Models.Customer.Entities;

namespace Teste_Crud_Mvc.Services.Interfaces
{
    public interface ICustomerService
    {
        public Customer GetById(string id);
        public BaseOutputModel Create(Customer input);
        public BaseOutputModel Update(Customer input);
        public BaseOutputModel Delete(string id);
        public BaseListOutputModel<Customer> List();
    }
}
