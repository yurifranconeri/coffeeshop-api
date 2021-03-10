using CoffeeShopAPI.Exceptions;
using CoffeeShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopAPI.Repository
{
    public class CustomerAsyncRepositoryEntityFrameworkSql : ICustomerAsyncRepository
    {
        private readonly CustomerContext _context;

        public CustomerAsyncRepositoryEntityFrameworkSql(CustomerContext context)
        {
            _context = context;
        }

        public async Task AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(long cpf)
        {
            Customer customer = await GetById(cpf);

            if (customer == null)
                throw new CustomerNotFoundException(cpf.ToString());

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(long cpf)
        {
            return await _context.Customers.FindAsync(cpf);
        }

        public async Task UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(customer.CPF))
                {
                    throw new CustomerNotFoundException(customer.CPF.ToString());
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CustomerExists(long cpf)
        {
            return _context.Customers.Any(e => e.CPF == cpf);
        }
    }
}
