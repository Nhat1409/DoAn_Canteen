using DACT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACT.Controllers
{
    public class MenuController : Controller
    {
        CatinDB context = new CatinDB();
        // GET: Menu
        public ActionResult Index()
        {
            
            return View(context.MonAns.ToList());
        }
        public ActionResult Category(int id)
        {
            var theloai = context.MonAns.Where(p=>p.MaTheLoai==id).ToList();
            return View(theloai);
        }
    }
}