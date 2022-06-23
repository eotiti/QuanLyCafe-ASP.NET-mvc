using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CAFE_MVC.Models;

namespace CAFE_MVC.Areas.Admin.Controllers
{
    public class QLTaikhoanController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Admin/QLTaikhoan
        public ActionResult Index()
        {
            return View(db.TAIKHOANs.ToList());
        }

        // GET: Admin/QLTaikhoan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // GET: Admin/QLTaikhoan
        
        //================================tao tk============
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
                    if(tk.TYPE > -1 && tk.TYPE < 2)
                    {
                        tk.PASS = GetMD5(tk.PASS);
                        db.Configuration.ValidateOnSaveEnabled = false;
                        db.TAIKHOANs.Add(tk);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }  
                    else
                    {
                        ModelState.Clear();
                        ViewBag.Loi = "Phải là 0 hoặc 1";
                        return View();
                    }    
                    
                }
                else
                {
                    ViewBag.error = "Tài khoản đã được sử dụng";
                    return View();
                }
            }
            return View();

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
        //=====================

        // GET: Admin/QLTaikhoan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // POST: Admin/QLTaikhoan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TK,FULLNAME,USERNAME,PASS,TYPE")] TAIKHOAN tAIKHOAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tAIKHOAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tAIKHOAN);
        }

        // GET: Admin/QLTaikhoan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // POST: Admin/QLTaikhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            db.TAIKHOANs.Remove(tAIKHOAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //============================Đặt lại pass=================
        public ActionResult Resetpass(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            if (tAIKHOAN == null)
            {
                return HttpNotFound();
            }
            return View(tAIKHOAN);
        }

        // POST: Admin/QLTaikhoan/Delete/5
        [HttpPost, ActionName("Resetpass")]
        [ValidateAntiForgeryToken]
        public ActionResult ResetConfirmed(int id)
        {
            TAIKHOAN tk = db.TAIKHOANs.Find(id);
            string reset = "123456";
            ViewBag.pass = reset;
            if (ModelState.IsValid)
            {
                tk.PASS = GetMD5(reset);
                db.Entry(tk).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tk);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
