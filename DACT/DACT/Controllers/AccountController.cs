using DACT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DACT.Controllers
{
    public class AccountController : Controller
    {
        CatinDB context = new CatinDB();
        public string MH(string mh)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(mh);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }
            return (hasPass);
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashborad()
        {
            var mssv = Session["MSSV"];
            if (mssv != null)
            {
                var khachang = context.Taikhoans.FirstOrDefault(p => p.MSSV == mssv);
                if (khachang != null)
                {
                    var lsDonHang = context.VeAns.Where(p => p.MaTaiKhoan == khachang.MaTaiKhoan).ToList();
                    ViewBag.VeAns = lsDonHang;
                    return View(khachang);
                }
            }
            return RedirectToAction("DangNhap");
        }
        public ActionResult editTK()
        {
            return View();
        }
        public ActionResult Story()
        {
            var mssv = Session["MSSV"];
            if (mssv != null)
            {
                var taikhoan = context.Taikhoans.FirstOrDefault(p => p.MSSV == mssv);
                if (taikhoan != null)
                {
                    var lsDonHang = context.VeAns.Where(p => p.MaTaiKhoan == taikhoan.MaTaiKhoan).ToList();
                    ViewBag.VeAns = lsDonHang;
                    return View(taikhoan);
                }

            }
            return RedirectToAction("DangNhap");
        }

        [HttpGet]
        public ActionResult Edit()
        {
            var mssv = Session["MSSV"];
            if (mssv != null)
            {
                var taikhoan = context.Taikhoans.FirstOrDefault(p => p.MSSV == mssv);
                if (taikhoan != null)
                {
                    var lsDonHang = context.VeAns.Where(p => p.MaTaiKhoan == taikhoan.MaTaiKhoan).ToList();
                    ViewBag.VeAns = lsDonHang;
                    return View(taikhoan);
                }

            }
            return RedirectToAction("DangNhap");
        }

        [HttpPost]
        public ActionResult Edit(Taikhoan taiKhoan, string MKhienTai, string xacNhanMKmoi, string MKmoi)
        {
            Taikhoan tk = context.Taikhoans.FirstOrDefault(p => p.MaTaiKhoan == taiKhoan.MaTaiKhoan);
            Taikhoan tk1 = new Taikhoan();
            if (tk != null)
            {
                if (MKhienTai == "")
                {
                    tk.HoTen = taiKhoan.HoTen;
                  
                    context.SaveChanges();
                    TempData["Message"] = "Chỉnh sửa thành công !";
                    return RedirectToAction("Edit", "Account");
                }
                else
                {
                    if (MH(MKhienTai) == tk.MatKhau && MKmoi != "")
                    {
                        if (MH(MKmoi) != tk.MatKhau && MH(xacNhanMKmoi) == MH(MKmoi))
                        {
                            tk.HoTen = taiKhoan.HoTen;
                            tk.MatKhau = MH(MKmoi);
                            context.SaveChanges();
                            TempData["Message"] = "Chỉnh sửa thành công !";
                            return RedirectToAction("Edit", "Account");
                        }
                        else
                        {
                            TempData["MessageErr"] = "Xác nhận sai mật khẩu mới !";
                            return RedirectToAction("Edit", "Account");
                        }
                    }
                    else
                    {
                        TempData["MessageErr"] = "Mật khẩu chưa đúng !";
                        return RedirectToAction("Edit", "Account");
                    }
                }

            }
            return HttpNotFound();
        }


        //[HttpPost]
        //public ActionResult Dashborad(Taikhoan taikhoan, string MKhienTai, string xacNhanMKmoi, string MKmoi)
        //{
        //    //test
        //    Taikhoan h = context.Taikhoans.FirstOrDefault(p => p.MaTaiKhoan == taikhoan.MaTaiKhoan);
        //    if (taikhoan != null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return HttpNotFound();
        //}

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(Taikhoan taikhoan)
        {
            var MHMK = MH(taikhoan.MatKhau);
            bool userE = context.Taikhoans.Any(x => x.MSSV == taikhoan.MSSV && x.MatKhau == MHMK);
            Taikhoan t = context.Taikhoans.FirstOrDefault(x => x.MSSV == taikhoan.MSSV && x.MatKhau == MHMK);
            if (userE)
            {
                if (t.MaQuyenTruyCap == 2)
                {
                    if (t.Active == false)
                    {
                        TempData["MessageErr"] = "Tài khoản đã bị chặn!!";
                    }
                    else
                    {
                        Session["MSSV"] = taikhoan.MSSV;
                        FormsAuthentication.SetAuthCookie(t.MatKhau, false);
                        return RedirectToAction("Index","Home");
                        //"Dashborad", "Account"
                    }
                }
                else if (t.MaQuyenTruyCap == 1)
                {
                    FormsAuthentication.SetAuthCookie(t.MSSV, false);
                    return RedirectToAction("Index", "AdminMon", new { area = "Admin" });
                }
            }
            TempData["MessageErr"] = "UserName or PassWord is wrong!!";
            return View();
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(Taikhoan taikhoan)
        {
            Taikhoan tk = context.Taikhoans.FirstOrDefault(p => p.MSSV == taikhoan.MSSV);
            if (ModelState.IsValid)
            {
                if (tk == null)
                {
                    Taikhoan tk1 = new Taikhoan
                    {
                        MSSV = taikhoan.MSSV,
                        HoTen = taikhoan.HoTen,
                        MatKhau = MH(taikhoan.MatKhau),
                        MaQuyenTruyCap = 2,
                        Active = true,
                    };
                    context.Taikhoans.Add(tk1);
                    context.SaveChanges();
                    TempData["Message"] = "Đăng ký thành công !";
                    return View();
                }
                TempData["MessageErr"] = "MSSV đã tồn tại !";
                return RedirectToAction("DangKy");
            }
            else
            {
                TempData["MessageErr"] = "Đăng ký không thành công !";
                return RedirectToAction("DangKy");
            }            
        }
        public ActionResult SignOut()
        {
            Session["MSSV"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap");
        }
        //public ActionResult editTK()
    }
}