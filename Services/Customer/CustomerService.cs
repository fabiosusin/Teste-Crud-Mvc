using DAO.Interfaces;
using Models.Base.Output;
using Models.Customer.Entities;
using Teste_Crud_Mvc.Services.Interfaces;
using Useful.Extensions;

namespace Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerDAO _customerDAO;
        public CustomerService(ICustomerDAO customerDAO) => _customerDAO = customerDAO;

        public Customer GetById(string id) => _customerDAO.FindById(id);

        public BaseOutputModel Create(Customer input)
        {
            var validationResult = UpsertCustomerValidation(input);
            if (!validationResult.Success)
                return validationResult;

            var existing = _customerDAO.FindById(input.Id);
            if (existing != null)
                return new("Cliente já cadastrado no sistema!");

            return _customerDAO.Insert(input);
        }

        public BaseOutputModel Delete(string id) => _customerDAO.RemoveById(id);

        public BaseListOutputModel<Customer> List()
        {
            var result = _customerDAO.FindAll();
            if (!(result?.Any() ?? false))
                return new("Nenhum cliente encontrado!");

            return new(result);
        }

        public BaseOutputModel Update(Customer input)
        {
            var validationResult = UpsertCustomerValidation(input);
            if (!validationResult.Success)
                return validationResult;

            var existing = _customerDAO.FindById(input.Id);
            if (existing == null)
                return new("Cliente não encontrado no sistema!");

            return _customerDAO.Update(input);
        }

        private static BaseOutputModel UpsertCustomerValidation(Customer input)
        {
            if (input == null)
                return new("Dados não informados!");

            if (input.DateOfBirth > DateTime.Now)
                return new("Data de nascimento não pode ser maior que agora!");

            if (string.IsNullOrEmpty(input.Name))
                return new("Informe o Nome!");

            if (string.IsNullOrEmpty(input.City))
                return new("Informe a Cidade!");

            if (string.IsNullOrEmpty(input.State))
                return new("Informe o Estado!");

            if (!input.PostalCode.ZipCodeIsValid())
                return new("Informe o corretamente CEP!");

            if (!input.Phone.CellphoneIsValid())
                return new("Informe o Telefone Corretamente!");

            if (!input.Email.IsValidEmail())
                return new("Informe o Email Corretamente!");

            if (!input.CPF.IsCpf())
                return new("Informe o CPF Corretamente!");

            return new(true);
        }
    }
}
