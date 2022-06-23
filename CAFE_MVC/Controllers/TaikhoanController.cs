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

namespace CAFE_MVC.Controllers
{
    public class TaikhoanController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Taikhoan

        // GET: Taikhoan/Details/5
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

        // GET: Taikhoan/Create


        // POST: Taikhoan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
 

        // GET: Taikhoan/Edit/5
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

        // POST: Taikhoan/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_TK,FULLNAME,USERNAME,PASS,TYPE")] TAIKHOAN tk,string repass)
        {
            if (ModelState.IsValid)
            {
                tk.USERNAME = Session["USERNAME"].ToString();
                tk.FULLNAME = Session["FULLNAME"].ToString();
                tk.TYPE = int.Parse(Session["TYPE"].ToString());
                if (repass!=null && tk.PASS!=null)
                {
                    if(repass==tk.PASS)
                    {
                        tk.PASS = GetMD5(repass);
                        db.Entry(tk).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Details", "Taikhoan", new { id = tk.ID_TK });
                    }
                    else
                    {
                        return RedirectToAction("Edit", "Taikhoan", new { id = Session["ID_USER"] });
                    }    
                }
                else
                {
                    return RedirectToAction("Edit", "Taikhoan", new { id = Session["ID_USER"] });
                }      
            }
            return View(tk);
        }

        // GET: Taikhoan/Delete/5
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

        // POST: Taikhoan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TAIKHOAN tAIKHOAN = db.TAIKHOANs.Find(id);
            db.TAIKHOANs.Remove(tAIKHOAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
    }
}
