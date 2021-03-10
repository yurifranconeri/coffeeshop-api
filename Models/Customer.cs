using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopAPI.Models
{
    public class Customer
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        [Key]
        public long CPF { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string CEP { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}