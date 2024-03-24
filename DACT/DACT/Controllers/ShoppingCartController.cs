using DACT.Models;
using MoMo;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DACT.Controllers
{
    public class ShoppingCartController : Controller
    {
        CatinDB context = new CatinDB();
        // GET: ShoppingCart

        public List<CartItem> GetShoppingCartFromSession()
        {
            var listShoppingCart = Session["ShoppingCart"] as List<CartItem>;
            if (listShoppingCart == null)
            {
                listShoppingCart = new List<CartItem>();
                Session["ShoppingCart"] = listShoppingCart;
            }
            return listShoppingCart;
        }
        public ActionResult Index()
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            if (ShoppingCart.Count == 0)
            {
                RedirectToAction("Index", "Home");
            }
            ViewBag.TongSoLuong = ShoppingCart.Sum(p => p.SoLuong);
            ViewBag.TongTien = ShoppingCart.Sum(p => p.SoLuong * p.Gia);
            return View(ShoppingCart);
        }

        [System.Web.Mvc.Authorize]
        public RedirectToRouteResult AddToCart(int id)
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            CartItem findCartItem = ShoppingCart.FirstOrDefault(m => m.Id == id);
            if (findCartItem == null)
            {
                MonAn findSP = context.MonAns.First(m => m.MaMonAn == id);
                CartItem newItem = new CartItem();
                newItem.Id = findSP.MaMonAn;
                newItem.tenMon = findSP.TenMon;
                newItem.SoLuong = 1;
                newItem.AnhMon = findSP.Image;
                newItem.Gia = findSP.GiaMon.Value;
                ShoppingCart.Add(newItem);

            }
            else
            {
                findCartItem.SoLuong++;
            }
            //TempData["Message"] = "Đã thêm đơn hàng vào giỏ hàng!";
            return RedirectToAction("Index", "Home");
        }
        [System.Web.Mvc.Authorize]
        public RedirectToRouteResult AddToCart2(int id)
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            CartItem findCartItem = ShoppingCart.FirstOrDefault(m => m.Id == id);
            if (findCartItem == null)
            {
                MonAn findSP = context.MonAns.First(m => m.MaMonAn == id);
                CartItem newItem = new CartItem();
                newItem.Id = findSP.MaMonAn;
                newItem.tenMon = findSP.TenMon;
                newItem.SoLuong = 1;
                newItem.AnhMon = findSP.Image;
                newItem.Gia = findSP.GiaMon.Value;

                ShoppingCart.Add(newItem);
            }
            else
            {
                findCartItem.SoLuong++;
            }
            return RedirectToAction("Index", "ShoppingCart");
        }

        public RedirectToRouteResult UpdateCart(int id, int txtSoLuong)
        {
            var itemFind = GetShoppingCartFromSession().FirstOrDefault(p => p.Id == id);
            if (itemFind != null)
            {
                itemFind.SoLuong = txtSoLuong;
            }
            return RedirectToAction("Index");
        }
        public ActionResult CartSummary()
        {
            ViewBag.CartCount = GetShoppingCartFromSession().Sum(p => p.SoLuong);
            return PartialView("CartSummary");
        }
        public ActionResult SideListCart()
        {
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            ViewBag.TongTien = ShoppingCart.Sum(p => p.SoLuong * p.Gia);
            return PartialView("SideListCart", ShoppingCart);
        }
        public RedirectToRouteResult RemoveCartItem(int id)
        {
            var itemFind = GetShoppingCartFromSession().FirstOrDefault(p => p.Id == id);
            GetShoppingCartFromSession().Remove(itemFind);
            return RedirectToAction("Index");
        }
        [System.Web.Mvc.Authorize]
        public ActionResult Confirm()
        {
            var MSSV = Session["MSSV"];
            var khachHang = context.Taikhoans.FirstOrDefault(p => p.MSSV == MSSV);
            List<CartItem> ShoppingCart = GetShoppingCartFromSession();
            ViewBag.ListCartItem = ShoppingCart;
            ViewBag.TongTien = ShoppingCart.Sum(p => p.SoLuong * p.Gia);
            return View(khachHang);
        }
       
        public ActionResult Order()
        {
            //từ số SDT lấy được mã KH -> đưa vô table Đơn hàng (mã KH)
            var MSSV = Session["MSSV"];
            var khachHang = context.Taikhoans.FirstOrDefault(p => p.MSSV == MSSV);
            using (DbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    List<CartItem> listCartItems = GetShoppingCartFromSession();

                    VeAn donHang = new VeAn();
                    donHang.MaTaiKhoan = khachHang.MaTaiKhoan;
                    donHang.NgayMua = DateTime.Now;
                    donHang.NgayNhan = null;
                    donHang.TrangThai= false;
                    donHang.TongTien = (double?)listCartItems.Sum(p => p.SoLuong * p.Gia);
                    context.VeAns.Add(donHang);

                    context.SaveChanges();
                    int maVe = donHang.MaVe;

                    foreach (var item in listCartItems)
                    {
                        ChiTietVA ctdh = new ChiTietVA()
                        {
                            MaVe = maVe,
                            MaMonAn = item.Id,
                            Soluong = item.SoLuong, 
                            MaTaiKhoan= donHang.MaTaiKhoan,
                            TongTien = (double ?)item.tongTien,
                    };
                        context.ChiTietVAs.Add(ctdh);
                        context.SaveChanges();

                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //TempData["MessageInfo"] = "Gặp lỗi khi đặt hàng!";
                    return RedirectToAction("Confirm");
                }
                Session["ShoppingCart"] = null;
            }
            //TempData["Message"] = "Đơn hàng của quý khách đã được ghi nhận, Chúng tôi sẽ liên hệ quý khách trong thời gian sớm nhất!";
            return RedirectToAction("Confirm");
        }
        public ActionResult OrderMomo()
        {
            List<CartItem> listCartItems = GetShoppingCartFromSession();
            //request params need to request to MoMo system
            string endpoint = "https://test-payment.momo.vn/gw_payment/transactionProcessor";
            string partnerCode = "MOMOOJOI20210710";
            string accessKey = "iPXneGmrJH0G8FOP";
            string serectkey = "sFcbSGRSJjwGxwhhcEktCHWYUuTuPNDB";
            string orderInfo = "SmartCanTeen";
            string returnUrl = "https://localhost:44332/ShoppingCart/ConfirmPaymentClient";
            string notifyurl = "https://4c8d-2001-ee0-5045-50-58c1-b2ec-3123-740d.ap.ngrok.io/Home/SavePayment"; //lưu ý: notifyurl không được sử dụng localhost, có thể sử dụng ngrok để public localhost trong quá trình test

            string amount = listCartItems.Sum(p => p.SoLuong * p.Gia).ToString();
            string orderid = DateTime.Now.Ticks.ToString(); //mã đơn hàng
            string requestId = DateTime.Now.Ticks.ToString();
            string extraData = "";

            //Before sign HMAC SHA256 signature
            string rawHash = "partnerCode=" +
                partnerCode + "&accessKey=" +
                accessKey + "&requestId=" +
                requestId + "&amount=" +
                amount + "&orderId=" +
                orderid + "&orderInfo=" +
                orderInfo + "&returnUrl=" +
                returnUrl + "&notifyUrl=" +
                notifyurl + "&extraData=" +
                extraData;

            MoMoSecurity crypto = new MoMoSecurity();
            //sign signature SHA256
            string signature = crypto.signSHA256(rawHash, serectkey);

            //build body json request
            JObject message = new JObject
            {
                { "partnerCode", partnerCode },
                { "accessKey", accessKey },
                { "requestId", requestId },
                { "amount", amount },
                { "orderId", orderid },
                { "orderInfo", orderInfo },
                { "returnUrl", returnUrl },
                { "notifyUrl", notifyurl },
                { "extraData", extraData },
                { "requestType", "captureMoMoWallet" },
                { "signature", signature }

            };

            string responseFromMomo = PaymentRequest.sendPaymentRequest(endpoint, message.ToString());

            JObject jmessage = JObject.Parse(responseFromMomo);

            return Redirect(jmessage.GetValue("payUrl").ToString());
        }
        public ActionResult ConfirmPaymentClient(Result result)
        {
            string rMessage = result.message;
            string rOrderId = result.orderId;
            int suscces = int.Parse(result.errorCode);
            if (suscces == 0)
            {
                var MSSV = Session["MSSV"];
                var khachHang = context.Taikhoans.FirstOrDefault(p => p.MSSV == MSSV);

                List<CartItem> listCartItems = GetShoppingCartFromSession();


                VeAn donHang = new VeAn();
                donHang.MaTaiKhoan = khachHang.MaTaiKhoan;
                donHang.NgayMua = DateTime.Now;
                donHang.NgayNhan = null;
                donHang.TrangThai = false;
                donHang.TongTien = (double?)listCartItems.Sum(p => p.SoLuong * p.Gia);
                context.VeAns.Add(donHang);

                context.SaveChanges();
                int maVe = donHang.MaVe;

                foreach (var item in listCartItems)
                {
                    ChiTietVA ctdh = new ChiTietVA()
                    {
                        MaVe = maVe,
                        MaMonAn = item.Id,
                        Soluong = item.SoLuong,
                        MaTaiKhoan = donHang.MaTaiKhoan,
                        TongTien = (double?)item.tongTien,
                    };
                    context.ChiTietVAs.Add(ctdh);
                    context.SaveChanges();

                }


                //TempData["Message"] = "Giao dịch thành công, cảm ơn bạn đã ủng hộ shop chúng tôi!";
                Session["ShoppingCart"] = null;
                return RedirectToAction("Confirm");
            }

            //TempData["MessageErr"] = "Giao dịch thất bại, vui lòng thử lại!";
            return RedirectToAction("Confirm");
        }
        
    }
}