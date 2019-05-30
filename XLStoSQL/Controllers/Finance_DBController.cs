using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using XLStoSQL.Models;
using EntityState = System.Data.Entity.EntityState;

namespace XLStoSQL.Controllers
{
    public class Finance_DBController : Controller
    {
        private FinanceInfoDBEntities db = new FinanceInfoDBEntities();

        // GET: Finance_DB
        public ActionResult Index()
        {
            if (db.Finance_DB != null)
                return View(db.Finance_DB.ToList());
            return RedirectToAction("Index");
        }

        // GET: Finance_DB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finance_DB finance_DB = db.Finance_DB.Find(id);
            if (finance_DB == null)
            {
                return HttpNotFound();
            }
            return View(finance_DB);
        }

        // GET: Finance_DB/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Finance_DB/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Trans,Type,Date,Name,Memo,Account,Debit,Credit,ID")] Finance_DB finance_DB)
        {
            if (ModelState.IsValid)
            {
                db.Finance_DB.Add(finance_DB);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(finance_DB);
        }

        // GET: Finance_DB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finance_DB finance_DB = db.Finance_DB.Find(id);
            if (finance_DB == null)
            {
                return HttpNotFound();
            }
            return View(finance_DB);
        }

        // POST: Finance_DB/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Trans,Type,Date,Name,Memo,Account,Debit,Credit,ID")] Finance_DB finance_DB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(finance_DB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(finance_DB);
        }

        // GET: Finance_DB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Finance_DB finance_DB = db.Finance_DB.Find(id);
            if (finance_DB == null)
            {
                return HttpNotFound();
            }
            return View(finance_DB);
        }

        // POST: Finance_DB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Finance_DB finance_DB = db.Finance_DB.Find(id);
            db.Finance_DB.Remove(finance_DB);
            db.SaveChanges();
            return RedirectToAction("Index");
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
