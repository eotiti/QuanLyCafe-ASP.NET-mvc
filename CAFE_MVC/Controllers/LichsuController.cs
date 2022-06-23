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

namespace CAFE_MVC.Controllers
{
    public class LichsuController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Lichsu
        public ActionResult Index(string search,int? page)
        {
            
            if(page == null)
            {
                page = 1;
            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            var ls = db.LICHSUs.Where(s=>s.TENBAN.Contains(search)||search==null).OrderBy(l => l.NGAYLAP).ToList();
            var ls1 = db.LICHSUs.Where(s => s.TENSP.Contains(search) || search == null).OrderBy(l => l.NGAYLAP).ToList();
            var ls2 = db.LICHSUs.Where(s => s.FULLNAME.Contains(search) || search == null).OrderBy(l => l.NGAYLAP).ToList();
            if(ls.Count!=0)
            {
                return View(ls.ToPagedList(pageNumber,pageSize));
            }  
            else if(ls1.Count!=0)
            {
                return View(ls1.ToPagedList(pageNumber, pageSize));
            }  
            else
            {
                return View(ls2.ToPagedList(pageNumber, pageSize));
            }    
            
        }

        // GET: Lichsu/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LICHSU lICHSU = db.LICHSUs.Find(id);
        //    if (lICHSU == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lICHSU);
        //}

        //// GET: Lichsu/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ID_BAN = new SelectList(db.BANs, "ID_BAN", "TENBAN");
        //    ViewBag.ID_SP = new SelectList(db.SANPHAMs, "ID_SP", "TENSP");
        //    ViewBag.ID_TK = new SelectList(db.TAIKHOANs, "ID_TK", "FULLNAME");
        //    return View();
        //}

        //// POST: Lichsu/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ID_LS,ID_HD,ID_BAN,ID_SP,SOLUONG,ID_TK,NGAYLAP,THANHTIEN")] LICHSU lICHSU)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.LICHSUs.Add(lICHSU);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ID_BAN = new SelectList(db.BANs, "ID_BAN", "TENBAN", lICHSU.ID_BAN);
        //    ViewBag.ID_SP = new SelectList(db.SANPHAMs, "ID_SP", "TENSP", lICHSU.ID_SP);
        //    ViewBag.ID_TK = new SelectList(db.TAIKHOANs, "ID_TK", "FULLNAME", lICHSU.ID_TK);
        //    return View(lICHSU);
        //}

        //// GET: Lichsu/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LICHSU lICHSU = db.LICHSUs.Find(id);
        //    if (lICHSU == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.ID_BAN = new SelectList(db.BANs, "ID_BAN", "TENBAN", lICHSU.ID_BAN);
        //    ViewBag.ID_SP = new SelectList(db.SANPHAMs, "ID_SP", "TENSP", lICHSU.ID_SP);
        //    ViewBag.ID_TK = new SelectList(db.TAIKHOANs, "ID_TK", "FULLNAME", lICHSU.ID_TK);
        //    return View(lICHSU);
        //}

        //// POST: Lichsu/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID_LS,ID_HD,ID_BAN,ID_SP,SOLUONG,ID_TK,NGAYLAP,THANHTIEN")] LICHSU lICHSU)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(lICHSU).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.ID_BAN = new SelectList(db.BANs, "ID_BAN", "TENBAN", lICHSU.ID_BAN);
        //    ViewBag.ID_SP = new SelectList(db.SANPHAMs, "ID_SP", "TENSP", lICHSU.ID_SP);
        //    ViewBag.ID_TK = new SelectList(db.TAIKHOANs, "ID_TK", "FULLNAME", lICHSU.ID_TK);
        //    return View(lICHSU);
        //}

        //// GET: Lichsu/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    LICHSU lICHSU = db.LICHSUs.Find(id);
        //    if (lICHSU == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(lICHSU);
        //}

        //// POST: Lichsu/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    LICHSU lICHSU = db.LICHSUs.Find(id);
        //    db.LICHSUs.Remove(lICHSU);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
