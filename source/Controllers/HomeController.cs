
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication3.Common;
using WebApplication3.Models;
using WebApplication3.Service;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private Model1 db = new Model1();

        #region 这段代码返回MainPage视图，展示网站主界面
        public ActionResult MainPage()
        {
            return View();
        }
        #endregion

        #region 这段代码返回Logon视图，展示登录界面，接受登录请求
        public ActionResult Logon(string returnUrl)
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Logon(Logon logonview)
        {
            if (ModelState.IsValid)
            {
                IQueryable<Models.MEMBER> querySql = from d in db.MEMBERS
                                                     where d.MEMBERNAME == logonview.UserName
                                                     select d;

                List<Models.MEMBER> query = querySql.ToList();

                //String sqlstr = "select * from MEMBERS where MEMBERNAME='" + logonview.UserName + "'";

                //List<MEMBER> query = db.Database.SqlQuery<MEMBER>(sqlstr).ToList();

                if (query.Count == 0)
                {
                    ModelState.AddModelError("UserName", "用户名不存在");
                }

                else if (query.First().PASSWORDS == logonview.Password)
                {
                    UserService userService = new UserService();
                    var _identity = userService.CreateIdentity(query.First(), DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = logonview.RememberMe }, _identity);
                    FormsAuthentication.SetAuthCookie(logonview.UserName, logonview.RememberMe);

                    return RedirectToAction("MainPage");
                }
                else ModelState.AddModelError("Password", "密码错误");
            }
            return View();
        }

        #endregion


        #region 这段代码返回Logout视图，用户注销
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            for (int i = 0; i < this.Request.Cookies.Count; i++)
            {
                this.Response.Cookies[this.Request.Cookies[i].Name].Expires = DateTime.Now.AddDays(-1);
            }
            return Redirect(Url.Content("MainPage"));
        }

        #endregion

        #region 这段代码创建验证码
        public ActionResult VerificationCode()
        {
            string verificationCode = Security.CreateVerificationText(6);
            Bitmap _img = Security.CreateVerificationImage(verificationCode, 160, 30);
            _img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            TempData["VerificationCode"] = verificationCode.ToUpper();
            return null;
        }
        #endregion

        #region 这段代码返回Register视图，接受用户注册请求
        //GET:Home/Registeruser
        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(RegisterUser register)
        {
            if (TempData["VerificationCode"] == null || TempData["VerificationCode"].ToString() != register.VerificationCode.ToUpper())
            {
                ModelState.AddModelError("VerificationCode", "验证码不正确");
                return View(register);
            }
            if (ModelState.IsValid)
            {
                IQueryable<Models.MEMBER> querySql = from d in db.MEMBERS
                                                     where d.MEMBERNAME == register.UserName
                                                     select d;

                List<Models.MEMBER> query = querySql.ToList();

                //String sqlstr = "select * from MEMBERS where MEMBERNAME='" + logonview.UserName + "'";

                //List<MEMBER> query = db.Database.SqlQuery<MEMBER>(sqlstr).ToList();

                if (query.Count != 0)
                {
                    ModelState.AddModelError("UserName", "用户名已存在");
                }

                else
                {
                    bool IsExist = true;
                    string memberid = null;
                    int count = 20;
                    while (IsExist && count-- > 0)
                    {
                        memberid = RandIdLength10();
                        IQueryable<Models.MEMBER> queryid = from d in db.MEMBERS
                                                            where d.MEMBERID == memberid
                                                            select d;

                        List<Models.MEMBER> idlist = queryid.ToList();
                        if (idlist.Count == 0)
                        {
                            IsExist = false;
                        }
                    }

                    if (IsExist == false)
                    {
                        MEMBER _user = new MEMBER()
                        {
                            MEMBERID = memberid,

                            MEMBERNAME = register.UserName,

                            //PASSWORDS = Security.Sha256(register.Password),
                            PASSWORDS = register.Password,

                            UPPERLIMIT = 10,

                            MEMBERSTATE = 0,

                            RESERVELIMIT = 10

                        };
                        db.MEMBERS.Add(_user);
                        db.SaveChanges();

                        UserService userService = new UserService();
                        var _identity = userService.CreateIdentity(_user, DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
                        AuthenticationManager.SignIn(_identity);
                        FormsAuthentication.SetAuthCookie(register.UserName, false);

                        return RedirectToAction("MainPage");

                        //AuthenticationManager.SignIn();
                    }
                    else { ModelState.AddModelError("", "注册失败！"); }
                }
            }
            return View(register);
        }

        #endregion

        #region 这段代码生成用户ID
        public string RandIdLength10()
        {
            var dateTime = System.DateTime.Now;
            string id = null;
            id += dateTime.Month.ToString().PadLeft(2, '0');
            id += dateTime.Day.ToString().PadLeft(2, '0');
            id += dateTime.Hour.ToString().PadLeft(2, '0');
            id += dateTime.Minute.ToString().PadLeft(2, '0');
            id += dateTime.Second.ToString().PadLeft(2, '0');
            return id;
        }

        #endregion

        #region 属性
        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }
        #endregion
    }
}