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
    public class AdminLoaiMonController : Controller
    {
        CatinDB context = new CatinDB();
        // GET: Admin/AdminLoaiMon
        public ActionResult Index(int? page)
        {
            int pageSize = 7;
            int pageIndex = page.HasValue ? page.Value : 1;
            var result = context.Theloais.ToList().ToPagedList(pageIndex, pageSize);
            return View(result);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var context = new CatinDB();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Theloai theloai)
        {
            var context = new CatinDB();
            context.Theloais.Add(theloai);
            context.SaveChanges();
            TempData["Message"] = "Tạo mới thành công !";
            return RedirectToAction("Index");
        }



        //    [HttpGet]
        //    public ActionResult Create()
        //    {
        //        ViewBag.ListTheLoai = context.Theloais.ToList();
        //        return View();
        //    }
        //    [HttpPost]
        //    public ActionResult Create(Theloai)
        //    {
        //        if (monAn.ImageFile != null && monAn.ImageFile.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(monAn.ImageFile.FileName);
        //            var filePath = Path.Combine(Server.MapPath("~/Content/ImageFromFile"), fileName);
        //            monAn.ImageFile.SaveAs(filePath);
        //            monAn.Image = "/Content/ImageFromFile/" + fileName;
        //        }
        //        context.MonAns.Add(monAn);
        //        context.SaveChanges();
        //        TempData["Message"] = "Tạo mới thành công !";
        //        return RedirectToAction("Index");
        //    }

        //    public ActionResult Edit(int id)
        //    {
        //        ViewBag.ListTheLoai = context.Theloais.ToList();
        //        MonAn TTmonAn = context.MonAns.FirstOrDefault(p => p.MaMonAn == id);
        //        if (TTmonAn == null)
        //        {
        //            return HttpNotFound();
        //        }
        //        temp = TTmonAn.Image;
        //        return View(TTmonAn);
        //    }
        //    [HttpPost]
        //    public ActionResult Edit(MonAn monAn)
        //    {
        //        MonAn editMonAn = context.MonAns.FirstOrDefault(p => p.MaMonAn == monAn.MaMonAn);
        //        if (editMonAn == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        editMonAn.TenMon = monAn.TenMon;
        //        editMonAn.GiaMon = monAn.GiaMon;
        //        editMonAn.MoTa = monAn.MoTa;
        //        editMonAn.MaTheLoai = monAn.MaTheLoai;

        //        //ảnh đại diện
        //        if (monAn.ImageFile != null && monAn.ImageFile.ContentLength > 0)
        //        {
        //            var fileName = Path.GetFileName(monAn.ImageFile.FileName);
        //            var filePath = Path.Combine(Server.MapPath("~/Content/ImageFromFile"), fileName);
        //            monAn.ImageFile.SaveAs(filePath);
        //            monAn.Image = "/Content/ImageFromFile/" + fileName;
        //            editMonAn.Image = monAn.Image;
        //        }
        //        else
        //        {
        //            editMonAn.Image = temp;
        //        }
        //        context.SaveChanges();
        //        TempData["Message"] = "Chỉnh sửa thành công !";
        //        return RedirectToAction("Index");
        //    }
        //}

    }
}
