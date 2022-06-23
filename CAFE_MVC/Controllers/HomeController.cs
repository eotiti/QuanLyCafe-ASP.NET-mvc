using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CAFE_MVC.Models;

namespace CAFE_MVC.Controllers
{
    public class HomeController : Controller
    {
        QLCAFEEntities db = new QLCAFEEntities();


        public ActionResult Index()
        {

            return View();

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(TAIKHOAN tk)
        {
            if (ModelState.IsValid)
            {
                var check = db.TAIKHOANs.FirstOrDefault(s => s.USERNAME == tk.USERNAME);
                if (check == null)
                {
                    tk.PASS = GetMD5(tk.PASS);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.TAIKHOANs.Add(tk);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Tài khoản đã được sử dụng";
                    return View();
                }
            }
            return View();


        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TAIKHOAN tk)
        {
            if (ModelState.IsValid && tk.USERNAME != null && tk.PASS!=null)
            {

                var f_password =GetMD5( tk.PASS);
                var data = db.TAIKHOANs.Where(s => s.USERNAME.Equals(tk.USERNAME) && s.PASS.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FULLNAME"] = data.FirstOrDefault().FULLNAME;
                    Session["USERNAME"] = data.FirstOrDefault().USERNAME;
                    Session["ID_USER"] = data.FirstOrDefault().ID_TK;
                    Session["TYPE"] = data.FirstOrDefault().TYPE; 
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            

        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }


        //Hàm băm mật khẩu
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
    }
}