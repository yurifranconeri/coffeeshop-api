using CoffeeShopAPI.Exceptions;
using CoffeeShopAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopAPI.Repository
{
    public class CoffeeShopAsyncRepositoryEntityFrameworkSQL : ICoffeeShopAsyncRepository
    {
        private readonly CoffeeShopContext _context;

        public CoffeeShopAsyncRepositoryEntityFrameworkSQL(CoffeeShopContext context)
        {
            _context = context;
        }

        public async Task AddCoffeeShop(CoffeeShop coffeeShop)
        {
            _context.CoffeeShops.Add(coffeeShop);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCoffeeShop(long id)
        {
            CoffeeShop coffeeShop = await GetById(id);

            if (coffeeShop == null)
                throw new CoffeeShopNotFoundException(id.ToString());

            _context.CoffeeShops.Remove(coffeeShop);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CoffeeShop>> GetAll()
        {
            return await _context.CoffeeShops.ToListAsync();
        }

        public async Task<CoffeeShop> GetById(long id)
        {
            return await _context.CoffeeShops.FindAsync(id);
        }

        public async Task UpdateCoffeeShop(CoffeeShop coffeeShop)
        {
            _context.Entry(coffeeShop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoffeeShopExists(coffeeShop.Id))
                {
                    throw new CoffeeShopNotFoundException(coffeeShop.Id.ToString());
                }
                else
                {
                    throw;
                }
            }
        }

        private bool CoffeeShopExists(long id)
        {
            return _context.CoffeeShops.Any(e => e.Id == id);
        }
    }
}
