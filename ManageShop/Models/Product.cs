using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ManageShop.Models
{
    public class Product
    {
        /*public Product()
        {
            Image = "~/Data/images/picture.png";
        }*/
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Tên không được dài hơn 50 ký tự.")]
        [Display(Name = "Tên sản phẩm")]
        public string NameProduct { get; set; }
        [Display(Name = "Mô tả")]
        public string Description {get; set;}
        [Required]
        [Display(Name = "Đơn giá")]
        public float Price { get; set; }
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }
        [Display(Name = "Danh mục")]
        public int? CategoryID { get; set; }
        public Category Category { get; set; }
        
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
