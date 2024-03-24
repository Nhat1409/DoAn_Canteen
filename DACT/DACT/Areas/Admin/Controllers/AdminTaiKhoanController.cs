using DACT.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACT.Areas.Admin.Controllers
{
    public class AdminTaiKhoanController : Controller
    {    
        CatinDB context = new CatinDB();
        // GET: Admin/AdminTaiKhoan
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.Taikhoans.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }
        public ActionResult Detail(int id)
        {

            var find = context.ChiTietVAs.Where(p => p.MaVe == id).ToList();
            return View(find);
        }
        public ActionResult Edit(Taikhoan taikhoan)
        {
            Taikhoan editTaikhoan = context.Taikhoans.FirstOrDefault(p => p.MaTaiKhoan == taikhoan.MaTaiKhoan);
            if (editTaikhoan == null)
            {
                return HttpNotFound();
            }

            

            //ảnh đại diện
            
            context.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công !";
            return RedirectToAction("Index");
        }
    }
}