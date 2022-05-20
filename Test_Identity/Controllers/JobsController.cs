using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test_Identity.Models;

namespace Test_Identity.Controllers
{
   // [Authorize(Roles = "Administrator")]
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jobs
        public ActionResult Index()
        {
            return View(db.Jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        //GET Create
        public ActionResult Create()
        {
            Job job = new Job();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                job.SkillCollection = db.Skills.ToList();
            }
            return View(job);
        }
        //Post
        [HttpPost]
        public ActionResult Create(Job job)
        {
            job.SelectedSkillID = string.Join(",", job.SelectedIDArray);
            
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.Jobs.Add(job);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        //// GET: Jobs/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Jobs/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,JobName,Experience,JobDescription,SelectedSkillID")] Job job)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Jobs.Add(job);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(job);
        //}

        // GET: Jobs/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Job job = new Job();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != 0)
                {
                    job = db.Jobs.Where(x => x.Id == id).FirstOrDefault();
                }
                job.SkillCollection = db.Skills.ToList();
            }
            return View(job);
        }
        //Post
        [HttpPost]
        public ActionResult Edit(Job job)
        {
            job.SelectedSkillID = string.Join(",", job.SelectedIDArray);

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (job.Id == 0)
                {
                    db.Jobs.Add(job);
                }
                else
                {
                    db.Entry(job).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
