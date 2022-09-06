using ManageShop.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ManageShop.Models;
using System.Web;


namespace ManageShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly ShopContext _context;

        public LoginController(ShopContext context)
        {
            _context = context;
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Verify(User user)
        {
            
            
            /*var f_password = GetMD5(password);*/
            /*var data = _context.Users.Where(s => s.username.Equals(username) && s.password.Equals(password)).ToList();*/
            var userDetails = _context.Users.Where(s => s.username == user.username && s.password == user.password).FirstOrDefault();
            if(userDetails == null)
            {
                
                ViewBag.error = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View("Index", user);
            }
            else
            {
                HttpContext.Session.SetInt32("id", userDetails.Id);
                HttpContext.Session.SetString("Name", userDetails.username);
                HttpContext.Session.SetString("Role", userDetails.Role);
                return RedirectToAction("Index", "Home");
            }
            /*if (userDetails.Count() > 0)
            {

                //add session
                *//*Session["FullName"] = data.FirstOrDefault().Fullname;
                Session["UserName"] = data.FirstOrDefault().username;
                Session["idUser"] = data.FirstOrDefault().Id;*//*
                return RedirectToAction("Home");
                *//*return RedirectToPage("HomeController");*//*
            }
            else
            {
                
                ViewBag.error = "Login failed";
                return RedirectToAction("Login");
            }*/
            
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            /*Session.Clear();*///remove session
            return RedirectToAction("Index");
        }
    }
}
