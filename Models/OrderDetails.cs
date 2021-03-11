using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShopAPI.Models
{
    public class OrderDetails
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public string CPF { get; set; }

        [Required]
        public string AddressLine1 { get; set; }
        
        [Required]
        public string AddressLine2 { get; set; }


        [Required]
        public string ShipType { get; set; }

        [Required]
        public string CEP { get; set; }

        [Required]
        public string Payment { get; set; }

        [Required]
        public List<OrderProduct> OrderProducts { get; set; }

        [Required]
        public string Subtotal { get; set; }

        [Required]
        public string Discount { get; set; }

        [Required]
        public string ShipPrice { get; set; }

        [Required]
        public string Total { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}