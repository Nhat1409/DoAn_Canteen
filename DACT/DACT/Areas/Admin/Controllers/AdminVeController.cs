using DACT.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace DACT.Areas.Admin.Controllers
{
    public class AdminVeController : Controller
    {
        CatinDB context = new CatinDB();
        // GET: Admin/AdminVe
        public static int ID = 0;
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.VeAns.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }

        public ActionResult Detail(int id)
        {

            var find = context.ChiTietVAs.Where(p => p.MaVe == id).ToList();
            return View(find);
        }
        [HttpGet]
        public ActionResult Check(int id)
        {
            VeAn vean = context.VeAns.FirstOrDefault(p=>p.MaVe == id);
            ID = id;
            return View(vean);

        }
        [HttpPost]
        public ActionResult Check(VeAn vean)
        {
            VeAn findVeAn = context.VeAns.FirstOrDefault(p=>p.MaVe==ID);
            if(findVeAn.TrangThai == false)
            {
                findVeAn.TrangThai = true;
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}