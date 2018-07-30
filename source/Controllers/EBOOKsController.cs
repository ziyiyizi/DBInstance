using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;
using Microsoft.AspNet.Identity;
using WebApplication3.Common;

namespace WebApplication3.Controllers
{
    public class EBOOKsController : Controller
    {
        private Model1 db = new Model1();

        #region 这段代码返回index视图，接受其请求并进行相关操作
        // GET: EBOOKs
        public ActionResult Index()
        {
            var query = db.EBOOKS.SqlQuery("SELECT * FROM EBOOKS");
            var queryList = new PageHelper<EBOOK>(query.Count(), 25, 0, query.OrderByDescending(m => m.EBOOKID));

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
            var query = db.EBOOKS.SqlQuery("SELECT * FROM EBOOKS");
            int pageIndex = (int)PageIndex;

            var booklist = new PageHelper<EBOOK>(query.Count(), 25, pageIndex, query.OrderByDescending(m => m.EBOOKID));

            ViewBag.PageIndex = booklist.PageIndex;
            ViewBag.PageCount = booklist.PageCount;

            return View(booklist.GetData().ToList());
        }
        #endregion

        #region 这段代码返回details视图，展示电子图书更多的信息
        // GET: EBOOKs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBOOK eBOOK = db.EBOOKS.Find(id);
            if (eBOOK == null)
            {
                return HttpNotFound();
            }
            if(Request.IsAuthenticated)
            {
                string time = System.DateTime.Now.ToString();
                string membername = User.Identity.GetUserName();
                var queryid = from d in db.MEMBERS where d.MEMBERNAME == membername select d.MEMBERID;
                string memberid = queryid.ToList().First();

                string check = "SELECT * FROM EBOOKHISTORY WHERE EBOOKID =" + id + " AND MEMBERID ='" + memberid + "'";
                var checkquery = db.EBOOKCOLLECTINGs.SqlQuery(check);
                if (checkquery.ToList().Count > 0)
                {
                    string update = "update EBOOKHISTORY set BROWSETIME = to_date('" + time + "','yyyy-mm-dd hh24:mi:ss')  where EBOOKID = " + id + " AND MEMBERID ='" + memberid + "'";
                    db.Database.ExecuteSqlCommand(update);
                }
                else
                {
                    string insert = "INSERT INTO EBOOKHISTORY VALUES(" + id + ",'" + memberid + "', to_date('" + time + "','yyyy-mm-dd hh24:mi:ss'))";
                    db.Database.ExecuteSqlCommand(insert);
                }
            }
    
            return View(eBOOK);
        }

        #endregion

        #region 这段代码返回edit视图，主要是收藏操作
        // GET: EBOOKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBOOK eBOOK = db.EBOOKS.Find(id);
            if (eBOOK == null)
            {
                return HttpNotFound();
            }
            if(Request.IsAuthenticated)
            {
                string username = User.Identity.GetUserName();
                var queryid = from d in db.MEMBERS where d.MEMBERNAME == username select d.MEMBERID;
                string memberid = queryid.ToList().First();
                string insert = "insert into EBOOKCOLLECTING values(" + id + ",'" + memberid + "')";

                string check = "SELECT * FROM EBOOKCOLLECTING WHERE EBOOKID =" + id + " AND MEMBERID ='" + memberid + "'";
                var checkquery = db.EBOOKCOLLECTINGs.SqlQuery(check);
                if (checkquery.ToList().Count == 0)
                {
                    db.Database.ExecuteSqlCommand(insert);
                }
                return Content("<script>alert('收藏成功');history.go(-1);</script> ");
            }
            else
            {
                return Content("<script>alert('请先登录!');history.go(-1);</script> ");
            }
            
            
            
        }

        // POST: EBOOKs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EBOOKID,EBOOKTYPE,EBOOKNAME")] EBOOK eBOOK)
        {
            


            if (ModelState.IsValid)
            {
                db.Entry(eBOOK).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eBOOK);
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
