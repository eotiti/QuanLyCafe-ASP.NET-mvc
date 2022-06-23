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
    public class KhuvucController : Controller
    {
        private QLCAFEEntities db = new QLCAFEEntities();

        // GET: Khuvuc
        public ActionResult Index(string search,int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            int pageNumber = (page ?? 1);
            int pageSize = 4;
            var kv = db.KHUVUCs.Where(s => s.TENKV.Contains(search) || search == null).OrderBy(l => l.TENKV).ToList();
            return View(kv.ToPagedList(pageNumber,pageSize));
        }

        // GET: Khuvuc/Details/5
        public ActionResult Details(int? id,int? page)
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
            var ban = db.BANs.Where(s=>s.ID_KV==id).OrderBy(s=>s.TENBAN).ToList();
            if (ban == null)
            {
                return HttpNotFound();
            }
            return View(ban.ToPagedList(pageNumber,pageSize));
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
