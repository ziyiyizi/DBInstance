using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Common;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class BOOK_HOTController : Controller
    {
        private Model1 db = new Model1();

        #region 这段代码返回index视图，接受其请求并进行相关操作
        // GET: BOOK_HOT
        public ActionResult Index()
        {
            var query = db.BOOK_HOT.SqlQuery("SELECT * FROM BOOK_HOT WHERE ROWNUM < 11 ORDER BY RENTNUM DESC");

            var queryList = new PageHelper<BOOK_HOT>(query.Count(), 20, 0, query.OrderByDescending(m => m.RENTNUM));

            ViewBag.PageIndex = queryList.PageIndex;
            ViewBag.PageCount = queryList.PageCount;

            return View(queryList.GetData().ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(int? PageIndex)
        {
            if (PageIndex == null)
            {

                return View();
            }
            var books = db.BOOK_HOT.SqlQuery("SELECT * FROM BOOK_HOT WHERE ROWNUM < 11 ORDER BY RENTNUM DESC");
            int pageIndex = (int)PageIndex;

            var booklist = new PageHelper<BOOK_HOT>(books.Count(), 20, pageIndex, books.OrderByDescending(m => m.RENTNUM));

            ViewBag.PageIndex = booklist.PageIndex;
            ViewBag.PageCount = booklist.PageCount;

            return View(booklist.GetData().ToList());
        }
        #endregion

        #region 这段代码返回details视图，展示图书更多的信息
        // GET: BOOK_HOT/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToRoute(new { controller = "INTERNALBOOKS", action = "Details", id });
        }
        #endregion

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
