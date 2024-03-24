using DACT.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DACT.Areas.Admin.Controllers
{
    public class SearchController : Controller
    {
        // GET: Admin/Search
        CatinDB context = new CatinDB();
        [HttpPost]
        //public ActionResult FindProduct(string keyword)
        //{
        //    List<MonAn> ls = new List<MonAn>();
        //    if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
        //    {
        //        int count = 0;
        //        List<MonAn> ls2 = new List<MonAn>();
        //        foreach (var item in context.MonAns.ToList())
        //        {
        //            ls2.Add(item);
        //            count++;
        //            if (count == 7) 
        //                break;
        //        }
        //        return PartialView("ListProductsSearchPartial", ls2);

        //    }
        //    ls = context.MonAns.AsNoTracking()
        //                          .Where(x => x.TenMon.Contains(keyword) || x.Theloai.TenTheLoai.Contains(keyword))
        //                          .OrderByDescending(x => x.MaMonAn)
        //                          .Take(10)
        //                          .ToList();

        //    if (ls == null)
        //    {
        //        return PartialView("ListProductsSearchPartial", null);
        //    }
        //    else
        //    {
        //        return PartialView("ListProductsSearchPartial", ls);
        //    }
        //}
        //public ActionResult TimSP(string keyword)
        //{
        //    List<MonAn> ls = new List<MonAn>();
        //    if (string.IsNullOrEmpty(keyword) || keyword.Length < 1)
        //    {
        //        return PartialView("DSSPSearchPartial", context.MonAns.ToList());

        //    }
        //    ls = context.MonAns.AsNoTracking()

        //                          .Where(x => x.TenMon.Contains(keyword) || x.Theloai.T.Contains(keyword))
        //                          .Take(10)
        //                          .ToList();

        //    if (ls == null)
        //    {
        //        return PartialView("DSSPSearchPartial", null);
        //    }
        //    else
        //    {
        //        return PartialView("DSSPSearchPartial", ls);
        //    }
        //}
        public static string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public ActionResult SearchMonAn(string keyword)
        {
            //Info info = new Info();
            //info.ngayDen = ngayden;
            //info.ngayTra = ngaydi;
            //Session["Info"] = info;
            if (!string.IsNullOrEmpty(keyword))
            {

                string searchKeyword = RemoveDiacritics(keyword);
                var findMA = context.MonAns.ToList().Where(p => RemoveDiacritics(p.TenMon).ToLower().Contains(searchKeyword.ToLower())).ToList();
                if (findMA != null)
                {
                    //return RedirectToAction("Index", "MonAn", new { id = findMA.MaMonAn });
                    return View(findMA);
                }
            }


            return PartialView("NotFound");
        }
        public ActionResult SearchMaVe(int mave)
        {
            //Info info = new Info();
            //info.ngayDen = ngayden;
            //info.ngayTra = ngaydi;
            //Session["Info"] = info;
            if (mave>0)
            {

               
                var findMv = context.VeAns.ToList().Where(p => p.MaVe == mave).ToList();
                if (findMv != null)
                {
                    //return RedirectToAction("Index", "MonAn", new { id = findMA.MaMonAn });
                    return View(findMv);
                }
            }


            return PartialView("NotFound");
        }
    }
}



