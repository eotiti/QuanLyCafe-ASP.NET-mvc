using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CAFE_MVC.Models;

namespace CAFE_MVC.Areas.Admin.Controllers
{
    public class QLNguyenlieuController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Admin/QLNguyenlieu
        public ActionResult Index()
        {
            return View(db.NGUYENLIEUx.ToList());
        }

        // GET: Admin/QLNguyenlieu/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUYENLIEU nGUYENLIEU = db.NGUYENLIEUx.Find(id);
            if (nGUYENLIEU == null)
            {
                return HttpNotFound();
            }
            return View(nGUYENLIEU);
        }

        // GET: Admin/QLNguyenlieu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/QLNguyenlieu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_NL,TENNL,SLTON")] NGUYENLIEU nGUYENLIEU)
        {
            if (ModelState.IsValid)
            {
                db.NGUYENLIEUx.Add(nGUYENLIEU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nGUYENLIEU);
        }

        // GET: Admin/QLNguyenlieu/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUYENLIEU nGUYENLIEU = db.NGUYENLIEUx.Find(id);
            if (nGUYENLIEU == null)
            {
                return HttpNotFound();
            }
            return View(nGUYENLIEU);
        }

        // POST: Admin/QLNguyenlieu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_NL,TENNL,SLTON")] NGUYENLIEU nGUYENLIEU)
        {
            if (ModelState.IsValid)
            {
                if (nGUYENLIEU.SLTON > 0)
                {
                    db.Entry(nGUYENLIEU).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }    
            }
            return View(nGUYENLIEU);
        }

        // GET: Admin/QLNguyenlieu/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NGUYENLIEU nGUYENLIEU = db.NGUYENLIEUx.Find(id);
            if (nGUYENLIEU == null)
            {
                return HttpNotFound();
            }
            return View(nGUYENLIEU);
        }

        // POST: Admin/QLNguyenlieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NGUYENLIEU nGUYENLIEU = db.NGUYENLIEUx.Find(id);
            db.NGUYENLIEUx.Remove(nGUYENLIEU);
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
