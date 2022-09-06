using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManageShop.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "#")]
        public int CategoryID { get; set; }
        [Display(Name = "Danh mục")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
