using Microsoft.AspNet.Identity;
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
    public class BOOK_INFOController : Controller
    {
        private Model1 db = new Model1();

        private List<SelectListItem> GetSearchKeyList()//获取搜索列表
        {
            return new List<SelectListItem>() {
                new SelectListItem
              {
                     Text = "图书名",
                     Value = "0"
              },new SelectListItem
              {
                     Text = "isbn",
                     Value = "1"
              },new SelectListItem
              {
                     Text = "类型",
                     Value = "2"
              }
            };
        }

        #region 这段代码返回MajorClassify视图，展示分类图书
        public ActionResult MajorClassify(string id)
        {
            if(id == null)
            {
                return HttpNotFound();
            }
            var books = db.BOOK_INFO as IQueryable<BOOK_INFO>;
            books = books.Where(m => m.MAJOR.Contains(id));

            return View(books.ToList());
        }
        #endregion

        #region 这段代码返回index视图，接受其请求并进行相关操作
        // GET: BOOK_INFO
        // GET: BOOK_INFO
        //展示图书列表 并进行搜索操作
        public ActionResult Index()
        {
            // var books = db.BOOKS as IQueryable<BOOK>;
            IQueryable<BOOK_INFO> books = db.BOOK_INFO
                .SqlQuery("SELECT * FROM BOOK_INFO")
                .AsQueryable<BOOK_INFO>();

            ViewBag.SearchKeyList = GetSearchKeyList();

            var queryList = new PageHelper<BOOK_INFO>(books.Count(), 25, 0, books.OrderByDescending(m => m.ISBN));

            ViewBag.PageIndex = queryList.PageIndex;
            ViewBag.PageCount = queryList.PageCount;


            return View(queryList.GetData().ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string searchString, int? PageIndex)
        {
            var value = Request.Form["SearchKeyList"];
            var books = db.BOOK_INFO as IQueryable<BOOK_INFO>;
            if (!String.IsNullOrEmpty(searchString))
            {
                if (value == "1")
                {
                    books = books.Where(m => m.ISBN.Contains(searchString));
                }
                else if (value == "0")
                {
                    books = books.Where(m => m.BOOKNAME.Contains(searchString));
                }

                else
                {
                    books = books.Where(m => m.MAJOR.Contains(searchString));
                }

            }
            if (PageIndex == null)
            {
                ViewBag.SearchKeyList = GetSearchKeyList();
                return View(books);
            }

            int pageIndex = (int)PageIndex;

            ViewBag.SearchKeyList = GetSearchKeyList();

            var booklist = new PageHelper<BOOK_INFO>(books.Count(), 25, pageIndex, books.OrderByDescending(m => m.ISBN));

            ViewBag.PageIndex = booklist.PageIndex;
            ViewBag.PageCount = booklist.PageCount;

            return View(booklist.GetData().ToList());

        }

        #endregion

        #region 这段代码返回details视图，展示图书更多的信息
        // GET: BOOK_INFO/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return RedirectToRoute(new { controller = "INTERNALBOOKS", action = "Details", id });
        }
        #endregion

        #region 这段代码返回moredetails视图，展示图书更多的信息
        public ActionResult MoreDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BOOKCOMMERCIAL bOOK_COMMERCIAL = db.BOOKCOMMERCIALs.Find(id);
            if (bOOK_COMMERCIAL == null)
            {
                return RedirectToAction("details", "book_info", new { ID = id });
            }
            return View(bOOK_COMMERCIAL);
        }
        #endregion

        #region 这段代码返回edit视图，主要是收藏操作
        // GET: BOOK_INFO/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string MemberName = User.Identity.GetUserName();

            IQueryable<MEMBER> querymember = from d in db.MEMBERS
                                             where d.MEMBERNAME == MemberName
                                             select d;

            List<MEMBER> memberlist = querymember.ToList();
            if (memberlist.Count == 0)
            {
                return Content("<script>alert('请先登录!');history.go(-1);</script>");
            }

            if (ModelState.IsValid)
            {
                IQueryable<BOOK> querybook = from d in db.BOOKS
                                             where d.ISBN == id
                                             select d;

                List<BOOK> bookList = querybook.ToList();
                memberlist.First().BOOKS.Add(bookList.First());
                db.SaveChanges();
            }
            else
            {
                ModelState.AddModelError("", "收藏失败!");
            }

            return Content("<script>alert('收藏成功');history.go(-1);</script> ");

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
