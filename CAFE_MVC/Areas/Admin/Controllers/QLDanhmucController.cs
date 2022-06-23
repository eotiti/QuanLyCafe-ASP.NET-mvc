using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAFE_MVC.Models;
using CAFE_MVC.Areas.Admin;

namespace CAFE_MVC.Areas.Admin.Controllers
{
    public class QLDanhmucController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Admin/QLDanhmuc
        public ActionResult Index()
        {
            return View(db.DANHMUCSPs.ToList());
        }

        // GET: Admin/QLDanhmuc/Details/5
        

        // GET: Admin/QLDanhmuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLDanhmuc/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DM,TENDM")] DANHMUCSP dANHMUCSP)
        {
            if (ModelState.IsValid)
            {
                db.DANHMUCSPs.Add(dANHMUCSP);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dANHMUCSP);
        }

        // GET: Admin/QLDanhmuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUCSP dANHMUCSP = db.DANHMUCSPs.Find(id);
            if (dANHMUCSP == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUCSP);
        }

        // POST: Admin/QLDanhmuc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DM,TENDM")] DANHMUCSP dANHMUCSP)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dANHMUCSP).State = EntityState.Modified;
                db.SaveChanges();
                return View();
            }
            return View(dANHMUCSP);
        }

        // GET: Admin/QLDanhmuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DANHMUCSP dANHMUCSP = db.DANHMUCSPs.Find(id);
            if (dANHMUCSP == null)
            {
                return HttpNotFound();
            }
            return View(dANHMUCSP);
        }

        // POST: Admin/QLDanhmuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DANHMUCSP dANHMUCSP = db.DANHMUCSPs.Find(id);
            db.DANHMUCSPs.Remove(dANHMUCSP);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //============================================Thêm, sửa, xoá món cho danh mục========================

        public ActionResult Edit_part(int id)//hiển thị ds món ăn theo từng danh mục riêng biệt
        {
            List<SANPHAM> sp = db.SANPHAMs.Where(s => s.ID_DM == id).ToList();
            return View(sp);
        }
        public ActionResult Create_sp(int id)
        {
            ViewBag.ID_DM = new SelectList(db.DANHMUCSPs, "ID_DM", "TENDM", id);
            return View();
        }

        // POST: Admin/QLSanpham/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_sp([Bind(Include = "ID_SP,ID_DM,TENSP,GIA")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Edit", "QLDanhmuc", new { id = sANPHAM.ID_DM });
            }

            ViewBag.ID_DM = new SelectList(db.DANHMUCSPs, "ID_DM", "TENDM", sANPHAM.ID_DM);
            return View(sANPHAM);
        }
        public ActionResult Edit_sp(int? id)// edit sanpham
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sanpham = db.SANPHAMs.Find(id);
            if (sanpham == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_DM = new SelectList(db.DANHMUCSPs, "ID_DM", "TENDM", sanpham.ID_DM); 
            return View(sanpham);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_sp([Bind(Include = "ID_SP,ID_DM,TENSP,GIA")] SANPHAM sanpham, int id)
        {
            if (ModelState.IsValid)
            {
                
                db.Entry(sanpham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "QLDanhmuc", new { id = sanpham.ID_DM });
            }
            ViewBag.ID_DM = new SelectList(db.DANHMUCSPs, "ID_DM", "TENDM", id);
         
            return View(sanpham);
        }
        public ActionResult Delete_sp(int? id)//xoá món khỏi danh mục
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            if (sANPHAM == null)
            {
                return HttpNotFound();
            }
            return View(sANPHAM);
        }

        // POST: Admin/QLSanpham/Delete/5
        [HttpPost, ActionName("Delete_sp")]//confirm delete
        [ValidateAntiForgeryToken]
        public ActionResult Delete_Confirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
            db.SaveChanges();
            return RedirectToAction("Edit", "QLDanhmuc", new {id=sANPHAM.ID_DM});
        }
        //===================================================================================================
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
