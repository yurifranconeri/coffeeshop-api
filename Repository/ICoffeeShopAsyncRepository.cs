using CoffeeShopAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopAPI.Repository
{
    public interface ICoffeeShopAsyncRepository
    {
        Task<IEnumerable<CoffeeShop>> GetAll();

        Task<CoffeeShop> GetById(long id);

        Task AddCoffeeShop(CoffeeShop coffeeShop);

        Task UpdateCoffeeShop(CoffeeShop coffeeShop);

        Task DeleteCoffeeShop(long id);
    }
}
