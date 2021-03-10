using CoffeeShopAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeShopAPI.Repository
{
    public interface ICustomerAsyncRepository
    {
        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> GetById(long cpf);

        Task AddCustomer(Customer customer);

        Task UpdateCustomer(Customer customer);

        Task DeleteCustomer(long cpf);
    }
}
