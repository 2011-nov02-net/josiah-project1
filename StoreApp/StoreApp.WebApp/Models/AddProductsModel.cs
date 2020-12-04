using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApp.WebApp.Models
{
    public class AddProductsModel
    {
        public AddProductsModel() =>
            Names = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Names { get; set; }
        public int Amount { get; set; }
    }
}
