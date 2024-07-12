using DAO.DBConnection;
using DAO.Interfaces;
using Models.Base.Output;
using Models.Customer.Entities;
using System.Linq.Expressions;

namespace DAO.Connections
{
    public class CustomerDAO : ICustomerDAO
    {
        internal RepositorySqlServer<Customer> Repository;
        public CustomerDAO(IDatabaseSettings settings) => Repository = new(settings?.SqlServerSettings);
        public DAOActionResultOutput Insert(Customer obj)
        {
            var result = Repository.Insert(obj);
            if (string.IsNullOrEmpty(result?.Id))
                return new("Não foi possível salvar o registro");

            return new(result);
        }

        public DAOActionResultOutput Update(Customer obj)
        {
            var result = Repository.Update(obj);
            if (string.IsNullOrEmpty(result?.Id))
                return new("Não foi possível salvar o registro");

            return new(result);
        }

        public DAOActionResultOutput Upsert(Customer obj) => string.IsNullOrEmpty(obj.Id) ? Insert(obj) : Update(obj);

        public DAOActionResultOutput Remove(Customer obj)
        {
            Repository.RemoveById(obj.Id);
            return new(true);
        }

        public DAOActionResultOutput RemoveById(string id)
        {
            Repository.RemoveById(id);
            return new(true);
        }

        public Customer FindOne() => Repository.FindOne();

        public Customer FindOne(Expression<Func<Customer, bool>> predicate) => Repository.FindOne(predicate);

        public Customer FindById(string id) => string.IsNullOrEmpty(id) ? null : Repository.FindById(id);

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate) => Repository.Find(predicate);

        public IEnumerable<Customer> FindAll() => Repository.FindAll();

    }
}
