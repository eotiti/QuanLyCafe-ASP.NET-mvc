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
    public class DanhmucController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Danhmuc
        public ActionResult Index(string search, int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageNumber = (page ?? 1);
            int pageSize = 5;
            var dm = db.DANHMUCSPs.Where(s => s.TENDM.Contains(search) || search == null).OrderBy(l => l.TENDM).ToList();
            return View(dm.ToPagedList(pageNumber, pageSize));
        }

        // GET: Danhmuc/Details/5
        public ActionResult Details(int? id,string search,int? page)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (page == null)
            {
                page = 1;
            }
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            var sp = db.SANPHAMs.Where(s => s.TENSP.Contains(search) || search == null).OrderBy(l => l.TENSP).ToList();
            
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp.ToPagedList(pageNumber, pageSize));
        }

        // GET: Danhmuc/Create
        

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
