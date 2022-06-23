using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAFE_MVC.Models;
using PagedList;
using PagedList.Mvc;

namespace CAFE_MVC.Areas.Admin.Controllers
{
    public class QLBanController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Admin/QLBan
        public ActionResult Index(string search, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if (page == null)
            {
                page = 1;
            }
            var ban = db.BANs.Where(k => k.TENBAN.Contains(search) || search == null).OrderBy(x => x.TENBAN).ToList();
            var ban1 = db.BANs.Where(x => x.KHUVUC.TENKV.Contains(search) || search == null).OrderBy(x => x.TENBAN).ToList();
            if (ban.Count==0)
            {
                return View(ban1.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(ban.ToPagedList(pageNumber, pageSize));
            }  
        }

        // GET: Ban/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAN bAN = db.BANs.Find(id);
            if (bAN == null)
            {
                return HttpNotFound();
            }
            return View(bAN);
        }

        // GET: Ban/Create
        public ActionResult Create()
        {
            ViewBag.ID_KV = new SelectList(db.KHUVUCs, "ID_KV", "TENKV");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_BAN,ID_KV,TENBAN,STAT")] BAN bn)
        {
            if (ModelState.IsValid)
            {
                db.BANs.Add(bn);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_KV = new SelectList(db.KHUVUCs.OrderBy(s => s.TENKV), "ID_KV", "TENKV", bn.ID_KV);
            return View(bn);
        }

        // GET: Ban/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAN bn = db.BANs.Find(id);
            if (bn == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_KV = new SelectList(db.KHUVUCs, "ID_KV", "TENKV", bn.ID_KV);
            return View(bn);
        }


        // POST: Ban/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_BAN,ID_KV,TENBAN,STAT")] BAN bAN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bAN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_KV = new SelectList(db.KHUVUCs, "ID_KV", "TENKV", bAN.ID_KV);
            return View(bAN);
        }

        // GET: Ban/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BAN bAN = db.BANs.Find(id);
            if (bAN == null)
            {
                return HttpNotFound();
            }
            return View(bAN);
        }

        // POST: Ban/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BAN bAN = db.BANs.Find(id);
            db.BANs.Remove(bAN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //================================================================HIỂN THỊ, THÊM, SỬA, XOÁ MÓN CHO BÀN THEO ID===================
        public ActionResult Details_part(int id)//hiển thị ds món ăn theo từng bàn riêng biệt
        {
            List<DATMON> dm = db.DATMONs.Where(s => s.ID_BAN == id).ToList();
            return View(dm);
        }
        public ActionResult Create_datmon(int id)
        {
            ViewBag.ID_BAN = new SelectList(db.BANs, "ID_BAN", "TENBAN", id);
            ViewBag.ID_SP = new SelectList(db.SANPHAMs, "ID_SP", "TENSP");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_datmon([Bind(Include = "ID_HD,ID_BAN,NGAYLAP,ID_SP,SOLUONG,ID_TK")] DATMON datmon)
        {
            if (ModelState.IsValid)
            {
                datmon.ID_TK = int.Parse(Session["ID_USER"].ToString());

                DateTime today = DateTime.Now;
                datmon.NGAYLAP = DateTime.Parse(today.ToString("yyyy-MM-dd HH:mm"));
                db.DATMONs.Add(datmon);
                db.SaveChanges();
                return RedirectToAction("Details", "QLBan", new { id = datmon.ID_BAN });
            }

            ViewBag.ID_BAN = new SelectList(db.BANs, "ID_BAN", "TENBAN", datmon.ID_BAN);
            ViewBag.ID_SP = new SelectList(db.SANPHAMs, "ID_SP", "TENSP", datmon.ID_SP);
            return View(datmon);
        }
        public ActionResult Edit_datmon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DATMON datmon = db.DATMONs.Where(x => x.ID_HD == id).FirstOrDefault();
            if (datmon == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_BAN = new SelectList(db.BANs, "ID_BAN", "TENBAN", datmon.ID_BAN);
            ViewBag.ID_SP = new SelectList(db.SANPHAMs, "ID_SP", "TENSP", datmon.ID_SP);
            return View(datmon);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_datmon([Bind(Include = "ID_HD,ID_BAN,NGAYLAP,ID_SP,SOLUONG,ID_TK")] DATMON datmon, int id)
        {
            if (ModelState.IsValid)
            {
                datmon.ID_TK = int.Parse(Session["ID_USER"].ToString());
                DateTime today = DateTime.Now;
                datmon.NGAYLAP = DateTime.Parse(today.ToString("yyyy-MM-dd HH:mm"));
                db.Entry(datmon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "QLBan", new { id = datmon.ID_BAN });
            }
            ViewBag.ID_BAN = new SelectList(db.BANs, "ID_BAN", "TENBAN", id);
            ViewBag.ID_SP = new SelectList(db.SANPHAMs, "ID_SP", "TENSP", datmon.ID_SP);
            return View(datmon);
        }
        //public ActionResult Delete_datmon(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    DATMON dATMON = db.DATMONs.Find(id);
        //    if (dATMON == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(dATMON);
        //}

        //[HttpPost, ActionName("Delete_datmon")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete_Confirmed(int id)
        //{
        //    DATMON dATMON = db.DATMONs.Find(id);
        //    dATMON.SOLUONG = 0;
        //    db.DATMONs.Save(dATMON);
        //    db.SaveChanges();
        //    return View("Details", "QLBan", new { id = dATMON.ID_BAN });
        //}
        public ActionResult payment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<DATMON> dATMON = db.DATMONs.Where(s=>s.ID_BAN==id).ToList();
            if (dATMON == null)
            {
                return HttpNotFound();
            }
            return View(dATMON);
        }
        [HttpPost, ActionName("payment")]
        [ValidateAntiForgeryToken]
        public ActionResult payment_Confirmed(int id)
        {
            List<DATMON> datmon = db.DATMONs.Where(s => s.ID_BAN == id).ToList();
            if (datmon.Count != 0)
            {
                foreach (var item in datmon)
                {
                    db.DATMONs.Remove(item);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Details", "QLBan", new { id = id });
        }
        //=========================================================================================
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
