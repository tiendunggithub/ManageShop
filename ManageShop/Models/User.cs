using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManageShop.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Tên đăng nhập")]
        public string username { get; set; }
        [Required]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }
        [Display(Name = "Họ tên")]
        public string fullname { get; set; }

        [Display(Name = "Quyền")]
        public string Role { get; set; }
        /*public string LoginErrorMessage { get; set; }*/
    }
}
