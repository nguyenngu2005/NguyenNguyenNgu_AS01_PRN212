using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interfaces;
using Data.Entities;
using Data.Repository;
using System.Linq;

    namespace Business.Services
    {
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _repo = new();

        public IEnumerable<Customer> GetAll() => _repo.GetAll();

        public Customer? GetByEmail(string email)
            => _repo.GetAll().FirstOrDefault(c => c.EmailAddress.Equals(email, StringComparison.OrdinalIgnoreCase));

        public void Add(Customer customer) => _repo.Add(customer);

        public void Update(Customer customer) => _repo.Update(customer);

        public void Delete(int id) => _repo.Delete(c => c.CustomerID == id);

        public IEnumerable<Customer> Search(string keyword)
        {
            keyword = keyword.ToLower();
            return _repo.GetAll().Where(c =>
                c.CustomerFullName.ToLower().Contains(keyword) ||
                c.EmailAddress.ToLower().Contains(keyword));
        }

        public bool ValidateLogin(string email, string password)
        {
            var acc = GetByEmail(email);
            return acc != null && acc.Password == password;
        }
    }
    }

