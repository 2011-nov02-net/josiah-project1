using StoreApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public Location Location { get; set; }
        public DateTime Time { get; set; }
        public IEnumerable<ProductViewModel> Items { get; set; }
    }
}
