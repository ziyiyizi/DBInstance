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

namespace WebApplication3.Controllers
{
    public class BOOKRERSERVATIONsController : Controller
    {
        private Model1 db = new Model1();

        #region 这段代码返回主界面视图，展示预约书籍
        // GET: BOOKRERSERVATIONs
        public ActionResult Index()
        {
            var bOOKRERSERVATIONs = db.BOOKRERSERVATIONs.Include(b => b.BOOK).Include(b => b.MEMBER);
            string membername = User.Identity.GetUserName();
            var queryid = from d in db.MEMBERS where d.MEMBERNAME == membername select d.MEMBERID;
            string memberid = queryid.ToList().First();
            var query = db.BOOKRERSERVATIONs.SqlQuery("SELECT * FROM BOOKRESERVATION WHERE MEMBERID =" + memberid + "ORDER BY RESERVETIME DESC");

            return View(query.ToList());
        }
        #endregion

        #region 这段代码返回details视图，展示预约书籍的详情
        // GET: BOOKRERSERVATIONs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //BOOKRERSERVATION bOOKRERSERVATION = db.BOOKRERSERVATIONs.Find(id);
            //if (bOOKRERSERVATION == null)
            //{
            //    return HttpNotFound();
            //}
            return RedirectToRoute(new { controller = "INTERNALBOOKS", action = "Details", id });
        }
        #endregion

        #region 这段代码返回Edit视图，用于更改预约书籍或取消预约
        // GET: BOOKRERSERVATIONs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*BOOKRERSERVATION bOOKRERSERVATION = db.BOOKRERSERVATIONs.Find(id);
            if (bOOKRERSERVATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.ISBN = new SelectList(db.BOOKS, "ISBN", "BOOKNAME", bOOKRERSERVATION.ISBN);
            ViewBag.MEMBERID = new SelectList(db.MEMBERS, "MEMBERID", "PASSWORDS", bOOKRERSERVATION.MEMBERID);*/

            string query = "Select * From members Where membername ='" + User.Identity.GetUserName() + "'";
            var member = db.MEMBERS.SqlQuery(query).ToList();
            string memberID = member.First().MEMBERID;
            //找当前预约状态
            var reserve = db.BOOKRERSERVATIONs.SqlQuery("SELECT * FROM BOOKRESERVATION WHERE ISBN ='" + id + "' and reservestate!=2 and memberid='" + memberID + "'").ToList();
            if (reserve.Count() == 0)//找不到该预约记录
            {
                return Content("<script>alert(' 找不到该预约记录 ');history.go(-1);</script> "); ;//不能预约
            }
            else if(reserve.First().RESERVESTATE ==1)//已经到馆待取
            {
               
                System.DateTime reservetime = new System.DateTime();
                reservetime = reserve.First().RESERVETIME;//预约时间
                System.DateTime now = System.DateTime.Now;

                db.Database.ExecuteSqlCommand(" update BOOKRESERVATION set dealline = '" + now + "' WHERE ISBN ='" + id + "' and reservestate!=2 and memberid='" + memberID + "'");
                //预约记录会在明天被删除
                return Content("<script>alert('取消成功，预约记录会在明天被删除');history.go(-1);</script> ");

                /*db.Database.ExecuteSqlCommand(" update BOOKRESERVATION set reservestate = 2 WHERE ISBN ='" + id + "' and reservestate!=2 and memberid='" + memberID + "'");
                string query_1 = "with book_rese as (select bookid , memberid , renttime , deadline , rentstate , returntime from bookrenting join internalbooks using (BOOKID)  where ISBN = '" + id + "' and returntime> '" + reservetime + "') " +
                    " select bookid  from  (select bookid from book_rese) minus (select B1.bookid from book_rese B1, book_rese B2 where B1.returntime > B2.returntime)";
                var find_bookid = db.BOOKRENTINGs.SqlQuery(query_1).ToList();
                if(find_bookid.Count()==1)
                {
                    string bookid = find_bookid.First().BOOKID;
                    db.Database.ExecuteSqlCommand(" update internalbooks set rentstate = 0 WHERE bookid ='" + bookid + "' and rentstate=1 and memberid='" + memberID + "'");
                }*/
            }
            else//还没到馆
            {
                db.Database.ExecuteSqlCommand(" update BOOKRESERVATION set reservestate = 2 WHERE ISBN ='" + id + "' and reservestate!=2 and memberid='" + memberID + "'");
            }
                

            return Content("<script>history.go(-1);</script> ");
        }

        #endregion

        #region 这段代码返回delete视图
        // GET: BOOKRERSERVATIONs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BOOKRERSERVATION bOOKRERSERVATION = db.BOOKRERSERVATIONs.Find(id);
            if (bOOKRERSERVATION == null)
            {
                return HttpNotFound();
            }
            return View(bOOKRERSERVATION);
        }

        // POST: BOOKRERSERVATIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BOOKRERSERVATION bOOKRERSERVATION = db.BOOKRERSERVATIONs.Find(id);
            db.BOOKRERSERVATIONs.Remove(bOOKRERSERVATION);
            db.SaveChanges();
            return RedirectToAction("Index");
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
