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
    public class RECOM_BOOK_INFOController : Controller
    {
        private Model1 db = new Model1();

        // GET: RECOM_BOOK_INFO
        public ActionResult Index()
        {
            var query = db.RECOM_BOOK_INFO.SqlQuery("SELECT * FROM RECOM_BOOK_INFO ORDER BY RECOM_NUM DESC");
            return View(query.ToList());
        }

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
