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
    public class BOOK_REGController : Controller
    {
        private Model1 db = new Model1();

        #region 这段代码返回index视图，接受其请求并进行相关操作
        // GET: BOOK_REG
        public ActionResult Index()
        {
            string time = System.DateTime.Now.AddMonths(-1).ToString();
            //var query = db.BOOK_REG.SqlQuery("SELECT * FROM BOOK_REG WHERE MAINTAINTIME > to_date('" + time + "','yyyy-mm-dd hh24:mi:ss')");
            var query = db.BOOK_REG.SqlQuery("SELECT * FROM (SELECT * FROM (select BOOKID,ISBN,BOOKNAME,MAINTAINTIME,row_number() over(partition by ISBN order by BOOKID) as new_index from BOOK_REG) s where s.new_index=1) WHERE MAINTAINTIME > to_date('" + time + "','yyyy-mm-dd hh24:mi:ss')");
            var queryList = new PageHelper<BOOK_REG>(query.Count(), 30, 0, query.OrderByDescending(m => m.MAINTAINTIME));

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
            string time = System.DateTime.Now.AddMonths(-1).ToString();
            //var books = db.BOOK_REG.SqlQuery("SELECT * FROM BOOK_REG WHERE MAINTAINTIME > to_date('" + time + "','yyyy-mm-dd hh24:mi:ss')");
            var books = db.BOOK_REG.SqlQuery("SELECT * FROM (SELECT * FROM (select BOOKID,ISBN,BOOKNAME,MAINTAINTIME,row_number() over(partition by ISBN order by BOOKID) as new_index from BOOK_REG) s where s.new_index=1) WHERE MAINTAINTIME > to_date('" + time + "','yyyy-mm-dd hh24:mi:ss')");
            int pageIndex = (int)PageIndex;

            var booklist = new PageHelper<BOOK_REG>(books.Count(), 30, pageIndex, books.OrderByDescending(m => m.MAINTAINTIME));

            ViewBag.PageIndex = booklist.PageIndex;
            ViewBag.PageCount = booklist.PageCount;

            return View(booklist.GetData().ToList());
        }
        #endregion


        #region 这段代码返回details视图，展示图书更多的信息
        // GET: BOOK_REG/Details/5
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
