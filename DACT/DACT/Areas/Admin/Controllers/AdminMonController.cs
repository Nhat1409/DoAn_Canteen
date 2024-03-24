using DACT.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace DACT.Areas.Admin.Controllers
{
    public class AdminMonController : Controller
    {
        CatinDB context = new CatinDB();
        public static string temp = "";
        // GET: Admin/AdminMon
        public static int ID = 0;
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.MonAns.ToList().ToPagedList(pageIndex, pageSize);
            ViewBag.ListDanhMuc = context.Theloais.ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.ListTheLoai = context.Theloais.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(MonAn monAn,string ImageFile)
        {
            if (monAn.ImageFile != null && monAn.ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(monAn.ImageFile.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/ImageFromFile"), fileName);
                monAn.ImageFile.SaveAs(filePath);
                monAn.Image = "/Content/ImageFromFile/" + fileName;
            }
            context.MonAns.Add(monAn);
            context.SaveChanges();
            TempData["Message"] = "Tạo mới thành công !";
            return RedirectToAction("Index");
        }
    
        public ActionResult Edit(int id)
        {
            ViewBag.ListTheLoai = context.Theloais.ToList();
            MonAn TTmonAn = context.MonAns.FirstOrDefault(p => p.MaMonAn == id);
            if (TTmonAn == null)
            {
                return HttpNotFound();
            }
            temp = TTmonAn.Image;           
            return View(TTmonAn);
        }
        [HttpPost]
        public ActionResult Edit(MonAn monAn)
        {
            MonAn editMonAn = context.MonAns.FirstOrDefault(p => p.MaMonAn == monAn.MaMonAn);
            if (editMonAn == null)
            {
                return HttpNotFound();
            }

            editMonAn.TenMon = monAn.TenMon;
            editMonAn.GiaMon = monAn.GiaMon;
            editMonAn.MoTa = monAn.MoTa;           
            editMonAn.MaTheLoai = monAn.MaTheLoai;

            //ảnh đại diện
            if (monAn.ImageFile != null && monAn.ImageFile.ContentLength > 0)
            {
                var fileName = Path.GetFileName(monAn.ImageFile.FileName);
                var filePath = Path.Combine(Server.MapPath("~/Content/ImageFromFile"), fileName);
                monAn.ImageFile.SaveAs(filePath);
                monAn.Image = "/Content/ImageFromFile/" + fileName;
                editMonAn.Image = monAn.Image;
            }
            else
            {
                editMonAn.Image = temp;
            }           
            context.SaveChanges();
            TempData["Message"] = "Chỉnh sửa thành công !";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            MonAn deMonan = context.MonAns.FirstOrDefault(p => p.MaMonAn == id);
            if (deMonan == null)
            {
                return HttpNotFound();
            }
            ID= id;
            return View(deMonan);
        }
        [HttpPost]
        public ActionResult Delete(MonAn monAn)
        {
            MonAn monAn1 = context.MonAns.FirstOrDefault(p => p.MaMonAn == ID);
            if (monAn1 == null)
            {
                return HttpNotFound();
            }
            context.MonAns.Remove(monAn1);
            context.SaveChanges();
            ID = 0;
            TempData["Message"] = "Xóa thành công !";
            return RedirectToAction("Index");
        }
    }
}