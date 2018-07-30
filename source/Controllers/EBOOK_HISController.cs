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
    public class EBOOK_HISController : Controller
    {
        private Model1 db = new Model1();

        #region 这段代码返回index视图，展示电子浏览记录
        // GET: EBOOK_HIS
        public ActionResult Index()
        {
            string membername = User.Identity.GetUserName();
            var queryid = from d in db.MEMBERS where d.MEMBERNAME == membername select d.MEMBERID;
            string memberid = queryid.ToList().First();
            var query = db.EBOOK_HIS.SqlQuery("SELECT * FROM EBOOK_HIS WHERE MEMBERID =" + memberid + "ORDER BY BROWSETIME DESC");
            return View(query.ToList());
        }
        #endregion

        #region 这段代码返回details视图，展示电子书详情
        // GET: EBOOK_HIS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*EBOOK_HIS eBOOK_HIS = db.EBOOK_HIS.Find(id);
            if (eBOOK_HIS == null)
            {
                return HttpNotFound();
            }*/
            return RedirectToRoute(new { controller = "EBOOKs", action = "Details", id });
        }
        #endregion


        #region 这段代码返回edit视图，删除电子书记录
        // GET: EBOOK_HIS/Edit/5
        public ActionResult Edit(int? ebookid, string memberid)
        {
            if (ebookid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*EBOOK_HIS eBOOK_HIS = db.EBOOK_HIS.Find(id);
            if (eBOOK_HIS == null)
            {
                return HttpNotFound();
            }*/
            string check = "SELECT * FROM EBOOKHISTORY WHERE EBOOKID =" + ebookid + " AND MEMBERID ='" + memberid + "'";
            var checkquery = db.EBOOKCOLLECTINGs.SqlQuery(check);
            if (checkquery.ToList().Count > 0)
            {
                string delete = "delete from EBOOKHISTORY where EBOOKID =" + ebookid + " AND MEMBERID ='" + memberid + "'";
                db.Database.ExecuteSqlCommand(delete);
            }
            return Content("<script>alert('删除成功');history.go(-1);</script> ");
        }

        // POST: EBOOK_HIS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EBOOKID,EBOOKNAME,MEMBERID,BROWSETIME,EBOOKTYPE")] EBOOK_HIS eBOOK_HIS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eBOOK_HIS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eBOOK_HIS);
        }
        #endregion

        #region 这段代码返回delete视图
        // GET: EBOOK_HIS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EBOOK_HIS eBOOK_HIS = db.EBOOK_HIS.Find(id);
            if (eBOOK_HIS == null)
            {
                return HttpNotFound();
            }
            return View(eBOOK_HIS);
        }

        // POST: EBOOK_HIS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EBOOK_HIS eBOOK_HIS = db.EBOOK_HIS.Find(id);
            db.EBOOK_HIS.Remove(eBOOK_HIS);
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
