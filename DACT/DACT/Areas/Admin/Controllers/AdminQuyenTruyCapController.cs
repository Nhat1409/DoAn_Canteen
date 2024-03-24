using DACT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACT.Areas.Admin.Controllers
{
    public class AdminQuyenTruyCapController : Controller
    {
        CatinDB context = new CatinDB();
        // GET: Admin/AdminQuyenTruyCap
        public ActionResult Index()
        {
            return View(context.QuyenTruyCaps.ToList());
        }
    }
}