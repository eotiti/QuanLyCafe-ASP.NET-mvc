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
    public class QLKhuvucController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Admin/QLKhuvuc
        public ActionResult Index(string search,int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            var kv= db.KHUVUCs.Where(k=>k.TENKV.Contains(search)||search==null).OrderBy(x => x.TENKV).ToList();
            
            int pageNumber = (page ?? 1);
            int pageSize = 2;
            
            return View(kv.ToPagedList(pageNumber,pageSize));
        }

        // GET: Khuvuc/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<BAN> ban = db.BANs.Where(x => x.ID_KV == id).OrderBy(x => x.TENBAN).ToList();
            if (ban == null)
            {
                return View(ban);
            }

            return View(ban);
        }

        // GET: Khuvuc/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Khuvuc/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_KV,TENKV")] KHUVUC kHUVUC)
        {
            if (ModelState.IsValid)
            {
                db.KHUVUCs.Add(kHUVUC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kHUVUC);
        }

        // GET: Khuvuc/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUVUC kHUVUC = db.KHUVUCs.Find(id);
            ;
            if (kHUVUC == null)
            {
                return HttpNotFound();
            }
            return View(kHUVUC);
        }

        
        // POST: Khuvuc/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_KV,TENKV")] KHUVUC kHUVUC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(kHUVUC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kHUVUC);
        }

        // GET: Khuvuc/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KHUVUC kHUVUC = db.KHUVUCs.Find(id);
            if (kHUVUC == null)
            {
                return HttpNotFound();
            }
            return View(kHUVUC);
        }

        // POST: Khuvuc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KHUVUC kHUVUC = db.KHUVUCs.Find(id);
            db.KHUVUCs.Remove(kHUVUC);
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
        //-======================================================================================================================================
        public ActionResult Edit_part(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<BAN> ban = db.BANs.Where(x => x.ID_KV == id).OrderBy(x=>x.TENBAN).ToList();

            if (ban == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(ban);
            }
        }


        public ActionResult Create_ban(int id)
        {
            ViewBag.ID_KV = new SelectList(db.KHUVUCs, "ID_KV", "TENKV", id);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_ban([Bind(Include = "ID_BAN,ID_KV,TENBAN,STAT")] BAN bn, int id)
        {
            if (ModelState.IsValid)
            {

                db.BANs.Add(bn);
                db.SaveChanges();
                return RedirectToAction("Edit","QLKhuvuc",new {id=id});
            }
            ViewBag.ID_KV = new SelectList(db.KHUVUCs, "ID_KV", "TENKV", bn.KHUVUC.ID_KV);
            return View(bn);
        }
        public ActionResult Edit_ban(int? id)
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
            ViewBag.ID_KV = new SelectList(db.KHUVUCs, "ID_KV", "TENKV", id);
            return View(bAN);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_ban([Bind(Include = "ID_BAN,ID_KV,TENBAN")] BAN ban,int id)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ban).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Edit", "QLKhuvuc", new { id = ban.ID_KV });
            }
            ViewBag.ID_KV = new SelectList(db.KHUVUCs, "ID_KV", "TENKV", ban.ID_KV);
            return View(ban);
        }
        public ActionResult Delete_ban(int? id)
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

        
        [HttpPost, ActionName("Delete_ban")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete_banConfirmed(int id)
        {
            BAN bAN = db.BANs.Find(id);
            db.BANs.Remove(bAN);
            db.SaveChanges();
            return View("Edit", "QLKhuvuc", new { id = id });
        }
    }
}
