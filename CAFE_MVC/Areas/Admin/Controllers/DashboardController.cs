using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CAFE_MVC.Models;
using CAFE_MVC.Areas;
namespace CAFE_MVC.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        QLCAFEEntities db = new QLCAFEEntities();
        public ActionResult Index()
        {
            if(Session["ID_USER"]!=null && Session["TYPE"].ToString() =="0")
            {
                return View();
            }
            else
            {
                RedirectToAction("Login");
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
            if (ModelState.IsValid)
            {

                var f_password = GetMD5(tk.PASS);
                var data = db.TAIKHOANs.Where(s => s.USERNAME.Equals(tk.USERNAME) && s.PASS.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FULLNAME"] = data.FirstOrDefault().FULLNAME;
                    Session["USERNAME"] = data.FirstOrDefault().USERNAME;
                    Session["ID_USER"] = data.FirstOrDefault().ID_TK;
                    Session["TYPE"] = data.FirstOrDefault().TYPE;
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login","Dashboard");
                }
            }
            return View("Index","Dashboard");

        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login","Dashboard");
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