using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Common;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    public class ADMINISTRATORsController : Controller
    {
        private Model1 db = new Model1();
        private static bool IsLogon = false;
        private static string AdminID = null;

        #region 这段代码返回MainPage视图
        public ActionResult MainPage()
        {
            if (IsLogon == false)
            {
                return HttpNotFound();
            }
            return View();
        }
        #endregion

        #region 这段代码返回AdminLogon视图，展示登录界面
        // GET: ADMINISTRATORs
        public ActionResult AdminLogon()
        {
            if(IsLogon == true)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        #endregion

        #region 这段代处理注销操作
        public ActionResult Logout()
        {
            IsLogon = false;
            AdminID = null;
            return RedirectToAction("Index");

        }
        #endregion

        #region 这段代码返回AdminLogon视图，验证登录请求
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogon(Logon logon)
        {
            if (ModelState.IsValid)
            {
                

                IQueryable<Models.ADMINISTRATOR> querySql = from d in db.ADMINISTRATORS
                                                            where d.STAFFNAME == logon.UserName
                                                            select d;

                List<Models.ADMINISTRATOR> query = querySql.ToList();

                if (query.Count == 0)
                {
                    ModelState.AddModelError("UserName", "账户不存在");
                }

                else if (query.First().PASSWORDS == logon.Password)
                {
                    IsLogon = true;
                    AdminID = query.First().STAFFID;
                    return RedirectToAction("Index");
                }
                else ModelState.AddModelError("Password", "密码错误");
            }
            return View();
        }
        #endregion


        #region 这段代码返回Index视图，展示主界面
        public ActionResult Index()
        {
            if (IsLogon == false)
            {
                return RedirectToAction("AdminLogon");
            }
            return View(db.ADMINISTRATORS.ToList());
        }
        #endregion

        #region 这段代码返回AdminInfo视图，展示管理员信息
        public ActionResult AdminInfo()
        {
            if (IsLogon == false)
            {
                return HttpNotFound();
            }
            var admins = from d in db.ADMINISTRATORS where d.STAFFID == AdminID select d;

            return View(admins.ToList().First());
        }
        #endregion

        #region 这段代码处理更改密码操作
        public ActionResult SetPassWord()
        {
            if (IsLogon == false)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPassWord(RePassword rePassword)
        {
            if (IsLogon == false)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                IQueryable<Models.ADMINISTRATOR> querySql = from d in db.ADMINISTRATORS
                                                            where d.STAFFID == AdminID
                                                     select d;

                List<Models.ADMINISTRATOR> query = querySql.ToList();

                ADMINISTRATOR aDMINISTRATOR = query.First();

                if (aDMINISTRATOR.PASSWORDS != rePassword.OldPassword)
                {
                    ModelState.AddModelError("OldPassword", "密码错误");
                }

                else
                {
                    db.Database.ExecuteSqlCommand("update ADMINISTRATORS set PASSWORDS='" + rePassword.NewPassword + "' where STAFFID='" + AdminID + "'");
                    db.SaveChanges();

                    return Content("<script>alert('更改成功,请重新登录!');window.location.href='/Administrators/Index';</script>");
                }
            }
            return View(rePassword);
        }

        #endregion

        #region 这段代码返回用于创建新的管理员
        // POST: ADMINISTRATORs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "STAFFID,PASSWORDS,STAFFNAME,STAFFPOSITION")] ADMINISTRATOR aDMINISTRATOR)
        {
            if (IsLogon == false)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                db.ADMINISTRATORS.Add(aDMINISTRATOR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aDMINISTRATOR);
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
