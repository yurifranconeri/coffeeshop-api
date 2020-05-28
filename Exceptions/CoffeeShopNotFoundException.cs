using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopAPI.Exceptions
{
    [Serializable]
    public class CoffeeShopNotFoundException : Exception
    {
        private CoffeeShopNotFoundException()
        {

        }

        public CoffeeShopNotFoundException(string id)
            : base(String.Format("Invalid Coffee Shop Id: {0}", id))
        {

        }
    }
}
