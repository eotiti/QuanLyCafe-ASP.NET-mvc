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

namespace CAFE_MVC.Areas.Admin.Controllers
{
    public class QLSanphamController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Admin/QLSanpham
        public ActionResult Index(string search, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            var sp = db.SANPHAMs.Where(x => x.TENSP.Contains(search)||search==null ).OrderBy(x => x.TENSP).ToList();
            var sp1 = db.SANPHAMs.Where(x => x.DANHMUCSP.TENDM.Contains(search) || search == null).OrderBy(x => x.TENSP).ToList();
            if(sp.Count==0)
            {
                return View(sp1.ToPagedList(pageNumber,pageSize));
            }
            else
            {
                return View(sp.ToPagedList(pageNumber, pageSize));
            }
            
             
            //var sANPHAMs = db.SANPHAMs.Include(s => s.DANHMUCSP);
            //return View(sANPHAMs.OrderBy(s=>s.TENSP).ToList());
        }

        // GET: Admin/QLSanpham/Details/5
        public ActionResult Details(int? id)
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

        // GET: Admin/QLSanpham/Create
        public ActionResult Create()
        {
            ViewBag.ID_DM = new SelectList(db.DANHMUCSPs, "ID_DM", "TENDM");
            return View();
        }

        // POST: Admin/QLSanpham/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_SP,ID_DM,TENSP,GIA")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.SANPHAMs.Add(sANPHAM);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_DM = new SelectList(db.DANHMUCSPs, "ID_DM", "TENDM", sANPHAM.ID_DM);
            return View(sANPHAM);
        }

        // GET: Admin/QLSanpham/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.ID_DM = new SelectList(db.DANHMUCSPs, "ID_DM", "TENDM", sANPHAM.ID_DM);
            return View(sANPHAM);
        }

        // POST: Admin/QLSanpham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_SP,ID_DM,TENSP,GIA")] SANPHAM sANPHAM)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sANPHAM).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_DM = new SelectList(db.DANHMUCSPs, "ID_DM", "TENDM", sANPHAM.ID_DM);
            return View(sANPHAM);
        }

        // GET: Admin/QLSanpham/Delete/5
        public ActionResult Delete(int? id)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SANPHAM sANPHAM = db.SANPHAMs.Find(id);
            db.SANPHAMs.Remove(sANPHAM);
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
    }
}
