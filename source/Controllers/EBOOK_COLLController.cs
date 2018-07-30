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
    public class EBOOK_COLLController : Controller
    {
        private Model1 db = new Model1();

        #region 这段代码返回index视图，展示电子书收藏
        // GET: EBOOK_COLL
        public ActionResult Index()
        {
            string membername = User.Identity.GetUserName();
            var queryid = from d in db.MEMBERS where d.MEMBERNAME == membername select d.MEMBERID;
            string memberid = queryid.ToList().First();
            var query = db.EBOOK_COLL.SqlQuery("SELECT * FROM EBOOK_COLL WHERE MEMBERID =" + memberid);
            return View(query.ToList());
        }
        #endregion

        #region 这段代码返回details视图，展示电子书详情
        // GET: EBOOK_COLL/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*EBOOK_COLL eBOOK_COLL = db.EBOOK_COLL.Find(id);
            if (eBOOK_COLL == null)
            {
                return HttpNotFound();
            }*/
            return RedirectToRoute(new { controller = "EBOOKs", action = "Details", id });
        }
        #endregion

        #region 这段代码返回edit视图，取消电子书收藏
        // GET: EBOOK_COLL/Edit/5
        public ActionResult Edit(int? ebookid, string memberid)
        {
            if (ebookid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*EBOOK_COLL eBOOK_COLL = db.EBOOK_COLL.Find(id);
            if (eBOOK_COLL == null)
            {
                return HttpNotFound();
            }*/
            string check = "SELECT * FROM EBOOKCOLLECTING WHERE EBOOKID =" + ebookid + " AND MEMBERID ='" + memberid + "'";
            var checkquery = db.EBOOKCOLLECTINGs.SqlQuery(check);
            if (checkquery.ToList().Count > 0)
            {
                string delete = "delete from EBOOKCOLLECTING where EBOOKID =" + ebookid + " AND MEMBERID ='" + memberid + "'";
                db.Database.ExecuteSqlCommand(delete);
            }
            return Content("<script>alert('取消成功');history.go(-1);</script> ");
        }

        // POST: EBOOK_COLL/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EBOOKID,EBOOKNAME,MEMBERID,EBOOKTYPE")] EBOOK_COLL eBOOK_COLL)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eBOOK_COLL).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eBOOK_COLL);
        }


        #endregion


        #region 这段代码返回delete视图
        // GET: EBOOK_COLL/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBOOK_COLL eBOOK_COLL = db.EBOOK_COLL.Find(id);
            if (eBOOK_COLL == null)
            {
                return HttpNotFound();
            }
            return View(eBOOK_COLL);
        }

        // POST: EBOOK_COLL/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EBOOK_COLL eBOOK_COLL = db.EBOOK_COLL.Find(id);
            db.EBOOK_COLL.Remove(eBOOK_COLL);
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
