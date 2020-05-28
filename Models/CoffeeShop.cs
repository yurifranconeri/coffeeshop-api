using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopAPI.Models
{
    public class CoffeeShop
    {
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        public string Address { get; set; }

        public string WebSiteURL { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}