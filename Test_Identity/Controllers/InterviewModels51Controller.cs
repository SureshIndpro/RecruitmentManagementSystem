using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test_Identity.Models;

namespace Test_Identity.Controllers
{
    public class InterviewModels51Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InterviewModels51
        public ActionResult Index()
        {
            return View(db.roundInterviews.ToList());
        }

        // GET: InterviewModels51/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewModels interviewModels = db.roundInterviews.Find(id);
            if (interviewModels == null)
            {
                return HttpNotFound();
            }
            return View(interviewModels);
        }

        // GET: InterviewModels51/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InterviewModels51/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Round,CandidateId,InterviewerId,ModeOfInterview,Date_Time,Comments,Result,isUpdateEnabled,RecruitmentStatus")] InterviewModels interviewModels)
        {
            if (ModelState.IsValid)
            {
                db.roundInterviews.Add(interviewModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(interviewModels);
        }
        public ActionResult Update(int? id)
        {

            DateTime currentDate = DateTime.Now;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewModels interviewModels = db.roundInterviews.Find(id);
            DateTime InterviewDate = (DateTime)interviewModels.Date_Time;

            int result = DateTime.Compare(currentDate, InterviewDate);

            if (interviewModels == null)
            {
                return HttpNotFound();
            }

            if (InterviewDate <= currentDate )
            {
                return View(interviewModels);
            }
            //TempData["message"] = "Cannot be edit.";
            return RedirectToAction("Index", "InterviewModels51");
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateResult(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewModels interviewModels = db.roundInterviews.Find(id);
            var resultToUpdate = db.roundInterviews.Find(id);
            if (TryUpdateModel(resultToUpdate, "",
               new string[] { "Results", "Comments" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(interviewModels);
        }
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewModels interviewModels = db.roundInterviews.Find(id);

            return View(interviewModels);
        }

        // POST: InterviewModels51/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Round,CandidateId,InterviewerId,ModeOfInterview,Date_Time,Comments,Result,isUpdateEnabled,RecruitmentStatus")] InterviewModels interviewModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(interviewModels);
        }
        // GET: InterviewModels51/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewModels interviewModels = db.roundInterviews.Find(id);
            if (interviewModels == null)
            {
                return HttpNotFound();
            }
            return View(interviewModels);
        }

        // POST: InterviewModels51/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterviewModels interviewModels = db.roundInterviews.Find(id);
            db.roundInterviews.Remove(interviewModels);
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
