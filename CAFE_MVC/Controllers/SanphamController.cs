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
    public class SanphamController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Sanpham
        public ActionResult Index(string search,int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var sp = db.SANPHAMs.Where(s => s.TENSP.Contains(search) || search == null).OrderBy(l => l.TENSP).ToList();
            return View(sp.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Sanpham/Details/5
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

        // GET: Sanpham/Create
        
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
