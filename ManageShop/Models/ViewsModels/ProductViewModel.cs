using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageShop.Models.ViewsModels
{
    public class ProductViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categorys { get; set; }
    }
}
