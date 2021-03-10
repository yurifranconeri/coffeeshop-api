using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeShopAPI.Exceptions
{
    [Serializable]
    public class CustomerNotFoundException : Exception
    {
        private CustomerNotFoundException()
        {

        }

        public CustomerNotFoundException(string id)
            : base(String.Format("Invalid Customer Id: {0}", id))
        {

        }
    }
}
