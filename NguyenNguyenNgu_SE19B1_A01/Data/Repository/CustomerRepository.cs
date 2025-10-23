using Data.Database;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class CustomerRepository : IGenericRepository<Customer>
    {
        private readonly InMemoryDatabase _db = InMemoryDatabase.Instance;

        public IEnumerable<Customer> GetAll() => _db.Customers;

        public Customer? GetById(Func<Customer, bool> predicate)
            => _db.Customers.FirstOrDefault(predicate);

        public void Add(Customer entity)
        {
            entity.CustomerID = _db.Customers.Max(c => c.CustomerID) + 1;
            _db.Customers.Add(entity);
        }

        public void Update(Customer entity)
        {
            var existing = _db.Customers.FirstOrDefault(c => c.CustomerID == entity.CustomerID);
            if (existing == null) return;
            existing.CustomerFullName = entity.CustomerFullName;
            existing.Telephone = entity.Telephone;
            existing.EmailAddress = entity.EmailAddress;
            existing.CustomerBirthday = entity.CustomerBirthday;
            existing.CustomerStatus = entity.CustomerStatus;
            existing.Password = entity.Password;
        }

        public void Delete(Func<Customer, bool> predicate)
        {
            var item = _db.Customers.FirstOrDefault(predicate);
            if (item != null)
                _db.Customers.Remove(item);
        }

        public IEnumerable<Customer> Search(Expression<Func<Customer, bool>> predicate)
            => _db.Customers.AsQueryable().Where(predicate);
    }
}
