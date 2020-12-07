using StoreApp.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreApp.WebApp.Models
{
    public class LocationViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Display(Name = "Location name")]
        public string Name { get; set; }
        public IDictionary<Product, int> Inventory { get; set; }
    }
}
