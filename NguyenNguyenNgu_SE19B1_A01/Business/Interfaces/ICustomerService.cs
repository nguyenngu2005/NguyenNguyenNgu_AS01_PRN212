using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
namespace Business.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer? GetByEmail(string email);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
        IEnumerable<Customer> Search(string keyword);
        bool ValidateLogin(string email, string password);
    }
}
