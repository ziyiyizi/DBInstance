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
using System.Net;
using System.IO;
using System.Xml;
using doubanapi;


namespace WebApplication3.Controllers
{
    public class RECOM_BOOK_USERController : Controller
    {
        public static string iSBN = null;
        public static string bOOKNAME = null;
        private Model1 db = new Model1();

        // GET: RECOM_BOOK_USER
        public ActionResult Index()
        {
            string membername = User.Identity.GetUserName();
            if (membername == null)
            {
                return RedirectToRoute(new { controller = "home", action = "logon", id = new { } });
            }
            ViewBag.Userid = User.Identity.GetUserId();
            
            var queryid = from d in db.MEMBERS where d.MEMBERNAME == membername select d.MEMBERID;
            string memberid = queryid.ToList().First();
            var query = db.RECOM_BOOK_USER.SqlQuery("SELECT * FROM RECOM_BOOK_USER WHERE MEMBERID = '" + memberid + "'");
            return View(query.ToList());
        }

        // GET: RECOM_BOOK_USER/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RECOM_BOOK_USER rECOM_BOOK_USER = db.RECOM_BOOK_USER.Find(id);
            if (rECOM_BOOK_USER == null)
            {
                return HttpNotFound();
            }

            return RedirectToRoute(new { controller = "INTERNALBOOKS", action = "Details", id });
        }

        public ActionResult AddBook(string id)
        {
            ViewBag.isbn = iSBN;
            ViewBag.bookname = bOOKNAME;
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBook()
        {
            var query = db.RECOMMENDEDBOOKS.SqlQuery("SELECT * FROM RECOMMENDEDBOOKS WHERE ISBN ='" + iSBN + "'");
            if(query.ToList().Count == 0)
            {
                db.Database.ExecuteSqlCommand("INSERT INTO RECOMMENDEDBOOKS VALUES('" + iSBN + "','" + bOOKNAME + "', 0)");
            }
            var query2 = db.BOOKRECOMMENDING.SqlQuery("SELECT * FROM BOOKRECOMMENDING WHERE ISBN ='" + iSBN + "' AND MEMBERID = (select MEMBERID from MEMBERS where MEMBERNAME ='" + User.Identity.GetUserName() + "')");
            if(query2.ToList().Count == 0)
            {
                db.Database.ExecuteSqlCommand("INSERT INTO BOOKRECOMMENDING VALUES('" + iSBN + "',(select MEMBERID from MEMBERS where MEMBERNAME ='" + User.Identity.GetUserName() +"'))");
            }
            return RedirectToRoute(new { controller = "RECOM_BOOK_USER", action = "index", id = User.Identity.GetUserName() });
        }

        // GET: RECOM_BOOK_USER/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RECOM_BOOK_USER/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ISBN")] RECOM_BOOK_USER rECOM_BOOK_USER)
        {
            string isbn = Request["ISBN"];
            if (isbn.Length != 13 )
            {
                ModelState.AddModelError("ISBN", "ISBN必须为13位数字编号");
                return View();
            }
            var query = db.BOOKS.SqlQuery("SELECT * FROM BOOKS WHERE ISBN ='" + isbn + "'");
            int count = query.ToList().Count;
            if (query.ToList().Count > 0)
            {
                ModelState.AddModelError("ISBN", "该书已存在书库中");
                return View();
            }

            HttpWebRequest myRequest = null;
            HttpWebResponse myHttpResponse = null;
            string doubanurl = "http://api.douban.com/book/subject/isbn/";
            string geturl = doubanurl + isbn;
            myRequest = (HttpWebRequest)WebRequest.Create(geturl);
            //myRequest.Method = "HEAD";
            //myRequest.AllowAutoRedirect = false;
            try
            {
                myHttpResponse = (HttpWebResponse)myRequest.GetResponse();
            }
            catch(WebException ex)
            {
                myHttpResponse = (HttpWebResponse)ex.Response;
            }
            

            StreamReader reader = new StreamReader(myHttpResponse.GetResponseStream());
            string xmldetail = reader.ReadToEnd();
            reader.Close();
            myHttpResponse.Close();
            myRequest.Abort();

            if (myHttpResponse != null)
            {
                if (myHttpResponse.StatusCode == HttpStatusCode.NotFound)
                {
                    ModelState.AddModelError("ISBN", "无效的ISBN");
                    return View();
                }
            }

            var books = new bookinfo();
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmldetail);
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("db", "http://www.w3.org/2005/Atom");
            XmlElement root = xml.DocumentElement;
            XmlNodeList nodes = root.SelectNodes("/db:entry", nsmgr);

            foreach (XmlNode nodeData in nodes)
            {
                foreach (XmlNode childnode in nodeData.ChildNodes)
                {
                    string str = childnode.Name;
                    switch (str)
                    {
                        case "title":
                            books.Name = childnode.InnerText;
                            break;
                        case "link":
                            if (childnode.Attributes[1].Value == "image")
                            {
                                books.Imageurl = childnode.Attributes[0].Value;
                            }
                            break;
                        case "summary":
                            books.Summary = childnode.InnerText;
                            break;
                        case "db:attribute":
                            {
                                switch (childnode.Attributes[0].Value)
                                {
                                    case "isbn13":
                                        books.Isbn = childnode.InnerText;
                                        break;
                                    case "pages":
                                        books.Pages = childnode.InnerText;
                                        break;
                                    case "author":
                                        books.Author = childnode.InnerText;
                                        break;
                                    case "price":
                                        books.Price = childnode.InnerText;
                                        break;
                                    case "publisher":
                                        books.Publisher = childnode.InnerText;
                                        break;
                                    case "pubdate":
                                        books.Pubdate = childnode.InnerText;
                                        break;
                                }//end switch
                                break;
                            }
                    }//end switch
                }//end foreach
            }//end foreach

            iSBN = books.Isbn;
            bOOKNAME = books.Name;

            return RedirectToRoute(new { controller = "RECOM_BOOK_USER", action = "AddBook" ,id = isbn});
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
