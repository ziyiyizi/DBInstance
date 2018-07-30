using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication3.Common;
using WebApplication3.Models;
using Microsoft.Owin.Security;

namespace WebApplication3.Controllers
{
    public class MemberController : Controller
    {
        private Model1 db = new Model1();
        // GET: Member

        #region 属性
        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
        #endregion

        #region 这段代码返回setPassword视图,展示改密界面，处理请求
        public ActionResult SetPassWord()
        {
            if (Request.IsAuthenticated == false)
            {
                return Content("<script>alert('请先登录!');history.go(-1);</script>");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPassWord(RePassword rePassword)
        {
            
            if (ModelState.IsValid)
            {
                IQueryable<Models.MEMBER> querySql = from d in db.MEMBERS
                                                     where d.MEMBERNAME == User.Identity.Name
                                                     select d;

                List<Models.MEMBER> query = querySql.ToList();

                MEMBER member = query.First();

                if(member.PASSWORDS!=rePassword.OldPassword)
                {
                    ModelState.AddModelError("OldPassword", "密码错误");
                }

                else
                {
                    db.Database.ExecuteSqlCommand("update MEMBERS set PASSWORDS='"+rePassword.NewPassword+ "' where MEMBERNAME='"+member.MEMBERNAME+"'");
                    db.SaveChanges();

                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    for (int i = 0; i < this.Request.Cookies.Count; i++)
                    {
                        this.Response.Cookies[this.Request.Cookies[i].Name].Expires = DateTime.Now.AddDays(-1);
                    }

                    return Content("<script>alert('更改成功,请重新登录!');window.location.href='/Home/MainPage';</script>");
                }
            }
            return View(rePassword);
        }

        #endregion

        #region 这段代码返回MemberInfo视图，展示用户资料界面
        public ActionResult MemberInfo()
        {
            if(Request.IsAuthenticated == false)
            {
                return Content("<script>alert('请先登录!');history.go(-1)</script>");
            }
            var member = from d in db.MEMBERS where d.MEMBERNAME == User.Identity.Name select d;

            return View(member.ToList().First());
        }
        #endregion

        #region 这段代码返回MainPage视图
        public ActionResult MainPage()
        {
            return View();
        }
        #endregion

        #region 这段代码返回index视图,展示收藏图书界面
        public ActionResult Index()
        {
            string membername = User.Identity.GetUserName();

            IQueryable<MEMBER> querymember = from d in db.MEMBERS
                                             where d.MEMBERNAME == membername
                                             select d;

            List<MEMBER> memberlist = querymember.ToList();
            if (memberlist.Count == 0)
            {
                return Content("<script>alert('请先登录!');history.go(-1);</script>");
            }

            var booklist = memberlist.First().BOOKS.AsEnumerable();

            var queryList = new PageHelper<BOOK>(booklist.Count(), 20, 0, booklist.OrderByDescending(m => m.ISBN));

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

            string membername = User.Identity.GetUserName();

            IQueryable<MEMBER> querymember = from d in db.MEMBERS
                                             where d.MEMBERNAME == membername
                                             select d;

            List<MEMBER> memberlist = querymember.ToList();

            var booklist = memberlist.First().BOOKS.AsEnumerable();

            int pageIndex = (int)PageIndex;

            var bookList = new PageHelper<BOOK>(booklist.Count(), 20, pageIndex, booklist.OrderByDescending(m => m.ISBN));

            ViewBag.PageIndex = bookList.PageIndex;
            ViewBag.PageCount = bookList.PageCount;

            return View(bookList.GetData().ToList());
        }

        #endregion

        #region 这段代码返回edit视图，用于处理取消收藏请求
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string membername = User.Identity.GetUserName();

            IQueryable<MEMBER> querymember = from d in db.MEMBERS
                                             where d.MEMBERNAME == membername
                                             select d;

            List<MEMBER> memberlist = querymember.ToList();
            if (memberlist.Count == 0)
            {
                return Content("<script>alert('请先登录!');history.go(-1);</script>");
            }

            IQueryable<BOOK> querybook = from d in db.BOOKS
                                           where d.ISBN == id
                                             select d;

            List<BOOK> booklist = querybook.ToList();

            memberlist.First().BOOKS.Remove(booklist.First());
            db.SaveChanges();
            return Content("<script>alert('取消收藏成功');history.go(-1);</script>");
        }
        #endregion

        #region 这段代码返回Details视图，展示收藏图书详情
        public ActionResult Details(string id)
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
                return RedirectToRoute(new { controller = "INTERNALBOOKS", action = "Details", id });
            
        }
        #endregion
    }
}