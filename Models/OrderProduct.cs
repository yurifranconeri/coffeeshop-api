using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopAPI.Models
{
    public class OrderProduct
    {   
        [Required]
        public string Code { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string IsGrinded { get; set; }

        [Required]
        public string UnitPrice { get; set; }

        [Required]
        public string Units { get; set; }

        [Required]
        public string Total { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}