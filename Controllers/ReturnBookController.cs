using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class ReturnBookController : Controller
    {
        nitlibraryEntities db = new nitlibraryEntities();

        // GET: ReturnBook
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Save(returnbook retruns)
        {
            if (ModelState.IsValid)
            {
                db.returnbooks.Add(retruns);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(retruns);
        }



        public ActionResult GetMid(int mid)
        {
            var memberid = (from s in db.issuebooks
                            where s.m_id == mid
                            select new
                            {
                                IssueDate = s.issuedate,
                                ReturnDate = s.returndate,
                                Memberid = s.m_id,
                                BookName = s.book_id,
                                ElapsedDays = SqlFunctions.DateDiff("day", s.returndate, DateTime.Now)

                            }).ToArray();
                            
                            

            return Json(memberid, JsonRequestBehavior.AllowGet);
        }
    }
}  