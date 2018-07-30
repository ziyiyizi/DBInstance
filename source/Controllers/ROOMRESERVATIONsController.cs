using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using WebApplication3.Common;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ROOMRESERVATIONsController : Controller
    {
        private Model1 db = new Model1();

        #region 这段代码返回myreserve视图，主要是查看用户预约操作
        public ActionResult myReserve()
        {
            string membername = User.Identity.GetUserName();
            var queryid = from d in db.MEMBERS where d.MEMBERNAME == membername select d.MEMBERID;
            string memberid = queryid.ToList().First();
            var query = db.ROOMRESERVATIONs.SqlQuery("SELECT * FROM ROOMRESERVATION WHERE MEMBERID = '" + memberid + "'");

            return View(query.ToList());
        }
        #endregion

        #region 这段代码返回index视图，是预约主界面的相关操作
        // GET: ROOMRESERVATIONs
        public ActionResult Index()
        {
            ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID");
            var rOOMRESERVATIONs = db.ROOMRESERVATIONs.Include(r => r.MEMBER).Include(r => r.STUDYROOM);

            return View(rOOMRESERVATIONs.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//将此处改为用户按了按钮才筛选，点击换页不筛选，或者去掉换页
        public ActionResult Index([Bind(Include = "MEMBERID,ROOMID,RESERVETIME,DEADLINE,NUMOFPEOPLE")] ROOMRESERVATION rOOMRESERVATION)
        {
            var roomreservation = db.ROOMRESERVATIONs.SqlQuery("select * from ROOMRESERVATION where ROOMID = '" + rOOMRESERVATION.ROOMID + "'") as IEnumerable<ROOMRESERVATION>;


            ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID");
            return View(roomreservation.ToList());

        }
        #endregion

        #region 这段代码返回details视图，主要是展示图书室相关信息
        // GET: ROOMRESERVATIONs/Details/5
        public ActionResult Details(string roomid, DateTime reservetime)
        {
            if (roomid == null || reservetime == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ROOMRESERVATION rOOMRESERVATION = db.ROOMRESERVATIONs.Find("0000100002", roomid, reservetime);

            //var query = db.ROOMRESERVATIONs.SqlQuery("select * from ROOMRESERVATION where ROOMID = '" + roomid + "'");
            return View(rOOMRESERVATION);
        }
        #endregion

        #region 这段代码返回create视图，主要是预约图书室操作
        // GET: ROOMRESERVATIONs/Create
        public ActionResult Create()
        {
            ViewBag.MEMBERID = new SelectList(db.MEMBERS, "MEMBERID", "PASSWORDS");
            ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID");
            return View();
        }

        // POST: ROOMRESERVATIONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MEMBERID,ROOMID,RESERVETIME,DEADLINE,NUMOFPEOPLE")] ROOMRESERVATION rOOMRESERVATION)
        {
            if (ModelState.IsValid)
            {
                var time = DateTime.Now;
                if (rOOMRESERVATION.RESERVETIME < time)
                {
                    ModelState.AddModelError("RESERVETIME", "输入时间非法");
                    ViewBag.MEMBERID = new SelectList(db.MEMBERS, "MEMBERID", "PASSWORDS");
                    ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID");
                    return View();
                }
                var capacity = db.STUDYROOMS.SqlQuery("select * from STUDYROOMS where ROOMID = '" + rOOMRESERVATION.ROOMID + "'").ToList();
                var roomcapacity = capacity.First();
                if (rOOMRESERVATION.NUMOFPEOPLE <= 0 || rOOMRESERVATION.NUMOFPEOPLE > roomcapacity.ROOMCAPACITY)
                {
                    ModelState.AddModelError("NUMOFPEOPLE", "输入人数非法");
                    ViewBag.MEMBERID = new SelectList(db.MEMBERS, "MEMBERID", "PASSWORDS");
                    ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID");
                    return View();
                }
                DateTime dt = rOOMRESERVATION.RESERVETIME;
                rOOMRESERVATION.DEADLINE = dt.AddHours(1);
                var query = db.ROOMRESERVATIONs.SqlQuery("select * from ROOMRESERVATION where ROOMID = '" + rOOMRESERVATION.ROOMID + "'");
                foreach (var item in query)
                {
                    if (rOOMRESERVATION.RESERVETIME < item.DEADLINE && rOOMRESERVATION.RESERVETIME >= item.RESERVETIME)
                    {
                        ModelState.AddModelError("RESERVETIME", "这个房间要在" + item.DEADLINE.ToString() + "后才有空");
                        ViewBag.MEMBERID = new SelectList(db.MEMBERS, "MEMBERID", "PASSWORDS");
                        ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID");
                        return View();
                    }
                    if (rOOMRESERVATION.DEADLINE <= item.DEADLINE && rOOMRESERVATION.RESERVETIME > item.RESERVETIME)
                    {
                        ModelState.AddModelError("DEADLINE", "这个房间在" + item.RESERVETIME.ToString() + "后被占用");
                        ViewBag.MEMBERID = new SelectList(db.MEMBERS, "MEMBERID", "PASSWORDS");
                        ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID");
                        return View();
                    }
                }
                string temp1 = rOOMRESERVATION.RESERVETIME.ToString();
                string temp2 = rOOMRESERVATION.DEADLINE.ToString();
                string membername = User.Identity.GetUserName();
                var queryid = from d in db.MEMBERS where d.MEMBERNAME == membername select d.MEMBERID;
                string memberid = queryid.ToList().First();
                string temp3 = "insert into ROOMRESERVATION values('" + memberid + "','" + rOOMRESERVATION.ROOMID + "', to_date('" + temp1 + "','yyyy-mm-dd hh24:mi:ss'), to_date('" + temp2 + "','yyyy-mm-dd hh24:mi:ss')," + rOOMRESERVATION.NUMOFPEOPLE + ")";
                db.Database.ExecuteSqlCommand(temp3);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MEMBERID = new SelectList(db.MEMBERS, "MEMBERID", "PASSWORDS", rOOMRESERVATION.MEMBERID);
            ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID", rOOMRESERVATION.ROOMID);
            return View(rOOMRESERVATION);
        }

        #endregion

        #region 这段代码返回edit视图，主要是修改操作
        // GET: ROOMRESERVATIONs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROOMRESERVATION rOOMRESERVATION = db.ROOMRESERVATIONs.Find(id);
            if (rOOMRESERVATION == null)
            {
                return HttpNotFound();
            }
            ViewBag.MEMBERID = new SelectList(db.MEMBERS, "MEMBERID", "PASSWORDS", rOOMRESERVATION.MEMBERID);
            ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID", rOOMRESERVATION.ROOMID);
            return View(rOOMRESERVATION);
        }

        // POST: ROOMRESERVATIONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MEMBERID,ROOMID,RESERVETIME,DEADLINE,NUMOFPEOPLE")] ROOMRESERVATION rOOMRESERVATION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOOMRESERVATION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MEMBERID = new SelectList(db.MEMBERS, "MEMBERID", "PASSWORDS", rOOMRESERVATION.MEMBERID);
            ViewBag.ROOMID = new SelectList(db.STUDYROOMS, "ROOMID", "ROOMID", rOOMRESERVATION.ROOMID);
            return View(rOOMRESERVATION);
        }

        #endregion

        #region 这段代码返回delete视图，主要是删除操作
        // GET: ROOMRESERVATIONs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROOMRESERVATION rOOMRESERVATION = db.ROOMRESERVATIONs.Find(id);
            if (rOOMRESERVATION == null)
            {
                return HttpNotFound();
            }
            return View(rOOMRESERVATION);
        }

        // POST: ROOMRESERVATIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ROOMRESERVATION rOOMRESERVATION = db.ROOMRESERVATIONs.Find(id);
            db.ROOMRESERVATIONs.Remove(rOOMRESERVATION);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region 这段代码返回return视图，主要是归还图书室操作
        public ActionResult Return()
        {
            string membername = User.Identity.GetUserName();
            var queryid = from d in db.MEMBERS where d.MEMBERNAME == membername select d.MEMBERID;
            string memberid = queryid.ToList().First();
            var rOOMRESERVATIONs = db.ROOMRESERVATIONs.SqlQuery("select * from ROOMRESERVATION where MEMBERID = '"+ memberid +"'");
            return View(rOOMRESERVATIONs.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnRoom([Bind(Include = "MEMBERID,ROOMID,RESERVETIME,DEADLINE,NUMOFPEOPLE")] ROOMRESERVATION rOOMRESERVATION)
        {
            var reserve = db.ROOMRESERVATIONs.Find(rOOMRESERVATION.MEMBERID, rOOMRESERVATION.ROOMID, rOOMRESERVATION.RESERVETIME);
            db.ROOMRESERVATIONs.Remove(reserve);
            var time = DateTime.Now;
            var occupation = new ROOMOCCUPATION();
            occupation.MEMBERID = rOOMRESERVATION.MEMBERID;
            occupation.ROOMID = rOOMRESERVATION.ROOMID;
            occupation.STARTTIME = rOOMRESERVATION.RESERVETIME;
            occupation.DEADLINE = rOOMRESERVATION.DEADLINE;
            occupation.RETURNTIME = time;
            db.ROOMOCCUPATIONs.Add(occupation);
            return View();
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
