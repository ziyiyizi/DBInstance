using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class INTERNALBOOKSController : Controller
    {
        private Model1 db = new Model1();

        #region 这段代码返回index视图
        // GET: INTERNALBOOKS
        public ActionResult Index()
        {
            var iNTERNALBOOKS = db.INTERNALBOOKS.Include(i => i.BOOK);
            return View(iNTERNALBOOKS.ToList());
        }
        #endregion

        #region 这段代码返回Details视图
        // GET: INTERNALBOOKS/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            /*INTERNALBOOKS iNTERNALBOOK = db.INTERNALBOOKS.Find(id);
            if (iNTERNALBOOK == null)
            {
                return HttpNotFound();
            }*/
            //var query = db.INTERNALBOOKS.SqlQuery("SELECT * FROM INTERNALBOOKS WHERE ISBN = (SELECT ISBN FROM INTERNALBOOKS WHERE BOOKID =" + id + ")");
            var query = db.INTERNALBOOKS.SqlQuery("SELECT * FROM INTERNALBOOKS WHERE ISBN ='" + id + "'");
            return View(query.ToList());
        }
        #endregion

        #region 这段代码返回Reserve视图，用于处理预约图书请求
        public ActionResult Reserve(string id)//预约图书
        {
            if(id == null)
            {
                return Content("<script>alert('该图书状态异常,请稍后再试');history.go(-1);</script>");
            }
            if(Request.IsAuthenticated == false)
            {
                return Content("<script>alert('请先登录!');history.go(-1);</script>");
            }
            //找该书存量
            var stock = db.INTERNALBOOKS.SqlQuery("SELECT * FROM INTERNALBOOKS WHERE ISBN ='" + id + "' and BOOKSTATE =0").ToList();
            //找该书预约数
            var book_res_count = db.BOOKRERSERVATIONs.SqlQuery("Select * From bookreservation Where ISBN ='" + id + "' and reserveState < 2").ToList();
            //找该用户信息
            string query = "Select * From members Where membername ='" + User.Identity.GetUserName() + "'";
            var member = db.MEMBERS.SqlQuery(query).ToList();
            string memberID = member.First().MEMBERID;

            //找到该用户借书数量
            var current_reserve = db.BOOKRERSERVATIONs.SqlQuery("Select * From BOOKRESERVATION Where reserveState < 2 and memberID ='" + memberID + "'").ToList();

            if (stock.Count() != 0)//该书存量不等于0
            {
                return Content("<script>alert('预约失败，尚有可借出书籍！');history.go(-1);</script> ");;//不能预约
            }
            else if (member.First().MEMBERSTATE != 0)//该用户状态为挂失或冻结
            {
                return Content("<script>alert('预约失败，请检查您的用户状态是否为冻结或挂失！');history.go(-1);</script> ");//不能预约
            }
            else if (book_res_count.Count() != 0)//该书已经被预约
            {
                return Content("<script>alert('预约失败，该书已被预约！');history.go(-1);</script> ");//不能预约
            }
            else if (current_reserve.Count() >= member.First().RESERVELIMIT)//预约书数量超标
            {
                return Content("<script>alert('预约失败，您的预约已达上限！');history.go(-1);</script> ");//不能预约
            }
            else//开始预约
            {
                string now = System.DateTime.Now.ToString();
                string deadline = System.DateTime.Now.AddMonths(+1).ToString();
                db.Database.ExecuteSqlCommand(" Insert into BOOKRESERVATION Values( '" + id + "' , '" + memberID +
                    "' , to_date('" + now + "','yyyy-mm-dd hh24:mi:ss') , to_date('" + deadline + "','yyyy-mm-dd hh24:mi:ss'), 0)");

                //预约成功
            }

            return Content("<script>alert('预约成功');history.go(-1);</script> ");
        }

        #endregion

        #region 这段代码返回create视图
        // GET: INTERNALBOOKS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: INTERNALBOOKS/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BOOKID,BOOKSTATE,ISBN,SHELFNUMBER,FLOOR")] INTERNALBOOKS iNTERNALBOOKS)
        {
            if (ModelState.IsValid)
            {
                db.INTERNALBOOKS.Add(iNTERNALBOOKS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iNTERNALBOOKS);
        }

        #endregion

        #region 这段代码返回edit视图
        // GET: INTERNALBOOKS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INTERNALBOOKS iNTERNALBOOKS = db.INTERNALBOOKS.Find(id);
            if (iNTERNALBOOKS == null)
            {
                return HttpNotFound();
            }
            return View(iNTERNALBOOKS);
        }

        // POST: INTERNALBOOKS/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BOOKID,BOOKSTATE,ISBN,SHELFNUMBER,FLOOR")] INTERNALBOOKS iNTERNALBOOKS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNTERNALBOOKS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iNTERNALBOOKS);
        }

        #endregion

        #region 这段代码返回delete视图
        // GET: INTERNALBOOKS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INTERNALBOOKS iNTERNALBOOKS = db.INTERNALBOOKS.Find(id);
            if (iNTERNALBOOKS == null)
            {
                return HttpNotFound();
            }
            return View(iNTERNALBOOKS);
        }

        // POST: INTERNALBOOKS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            INTERNALBOOKS iNTERNALBOOKS = db.INTERNALBOOKS.Find(id);
            db.INTERNALBOOKS.Remove(iNTERNALBOOKS);
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
