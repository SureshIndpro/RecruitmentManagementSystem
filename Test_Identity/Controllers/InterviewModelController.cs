﻿using System;
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
    public class InterviewModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InterviewModel
        public ActionResult Index(int id= 45)
        {
            var roundInterview = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
            var jobObject = db.Jobs.ToList();
            foreach (var getSkillId in jobObject)
            {
                IEnumerable<int> fetchedSkillIds = getSkillId.SelectedSkillID.ToString().Split(',').Select(Int32.Parse);
                var getSkillName = db.Skills.Where(x => fetchedSkillIds.Contains(x.SkillId))
                .Select(skillName => new
                {
                    skillName.SkillName
                });

                string fetchSkillName = string.Join(",", getSkillName.Select(x => x.SkillName));
                getSkillId.SelectedSkillID = fetchSkillName;
            }
            InterviewModels interviewModels = db.roundInterviews.Find(id);

            var status = db.roundInterviews.Where(x => x.Results == 0);
            if (interviewModels.Results == 0)
            {
                return View(status);
            }
            return View(roundInterview);
            
        }
        public ActionResult ShowSchedule()
        {
            var roundInterview = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
            var jobObject = db.Jobs.ToList();
            foreach (var getSkillId in jobObject)
            {
                IEnumerable<int> fetchedSkillIds = getSkillId.SelectedSkillID.ToString().Split(',').Select(Int32.Parse);
                var getSkillName = db.Skills.Where(x => fetchedSkillIds.Contains(x.SkillId))
                .Select(skillName => new
                {
                    skillName.SkillName
                });

                string fetchSkillName = string.Join(",", getSkillName.Select(x => x.SkillName));
                getSkillId.SelectedSkillID = fetchSkillName;
            }
            return View(roundInterview);
        }

        // GET: InterviewModel/Details/5
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

        public ActionResult Create()
        {
            ViewBag.CandidateId = new SelectList(db.candidatesModels, "Id", "Firstname");
            ViewBag.InterviewerId = new SelectList(db.interviewerModels, "ID", "Name");
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Round,CandidateId,InterviewerId,JobId,ModeOfInterview,Date_Time,Comments,Results")] InterviewModels interviewModels)
        {
            if (ModelState.IsValid)
            {
                db.roundInterviews.Add(interviewModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CandidateId = new SelectList(db.candidatesModels, "Id", "Firstname", interviewModels.CandidateId);
            ViewBag.InterviewerId = new SelectList(db.interviewerModels, "ID", "Name", interviewModels.InterviewerId);
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", interviewModels.JobId);
            //return View(interviewModels);
            return RedirectToAction("Index", "InterviewModel");
        }
        //Update Results
        public ActionResult Update(int? id)
        {
            //var roundInterviews = db.roundInterviews.Find(id);
            DateTime currentDate = DateTime.Now;
           // var roundInterview = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
            var jobObject = db.Jobs.ToList();
            foreach (var getSkillId in jobObject)
            {
                IEnumerable<int> fetchedSkillIds = getSkillId.SelectedSkillID.ToString().Split(',').Select(Int32.Parse);
                var getSkillName = db.Skills.Where(x => fetchedSkillIds.Contains(x.SkillId))
                .Select(skillName => new
                {
                    skillName.SkillName
                });

                string fetchSkillName = string.Join(",", getSkillName.Select(x => x.SkillName));
                getSkillId.SelectedSkillID = fetchSkillName;
            }


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

            if (InterviewDate <= currentDate)
            {
                return View(interviewModels);
            }
            //TempData["message"] = "Update is disable , please wait till interview get over.";

            return RedirectToAction("Index", "InterviewModel");
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
            // var resultToUpdate = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
            if (TryUpdateModel(interviewModels, "",
               new string[] { "Results", "Comments" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(interviewModels);
        }
        // GET: InterviewModel/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CandidateId = new SelectList(db.candidatesModels, "Id", "Firstname", interviewModels.CandidateId);
            ViewBag.InterviewerId = new SelectList(db.interviewerModels, "ID", "Name", interviewModels.InterviewerId);
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", interviewModels.JobId);
            return View(interviewModels);
        }

        // POST: InterviewModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Round,CandidateId,InterviewerId,JobId,ModeOfInterview,Date_Time,Comments,Results")] InterviewModels interviewModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interviewModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CandidateId = new SelectList(db.candidatesModels, "Id", "Firstname", interviewModels.CandidateId);
            ViewBag.InterviewerId = new SelectList(db.interviewerModels, "ID", "Name", interviewModels.InterviewerId);
            ViewBag.JobId = new SelectList(db.Jobs, "Id", "JobName", interviewModels.JobId);
            return View(interviewModels);
        }

        // GET: InterviewModel/Delete/5
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

        // POST: InterviewModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterviewModels interviewModels = db.roundInterviews.Find(id);
            db.roundInterviews.Remove(interviewModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Search and Filter
        public JsonResult GetSearchingData(string SearchBy, string SearchValue, int? id)
        {
            //var interviewModels = db.roundInterviews.ToList();
            var interviewModels = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
            // var res = db.roundInterviews.Where(a => a.Interview.Name.ToLower().Contains(SearchValue) || a.Date_Time.ToString().Contains(SearchValue));
            var jobObject = db.Jobs.ToList();
            foreach (var getSkillId in jobObject)
            {
                IEnumerable<int> fetchedSkillIds = getSkillId.SelectedSkillID.ToString().Split(',').Select(Int32.Parse);
                var getSkillName = db.Skills.Where(x => fetchedSkillIds.Contains(x.SkillId))
                .Select(skillName => new
                {
                    skillName.SkillName
                });

                string fetchSkillName = string.Join(",", getSkillName.Select(x => x.SkillName));
                getSkillId.SelectedSkillID = fetchSkillName;
            }

            if (SearchBy == "Id")
            {
                try
                {
                    int ID = Convert.ToInt32(SearchValue);
                    interviewModels = db.roundInterviews.Where(x => x.Id == ID || SearchValue == null).ToList();
                }
                catch (FormatException)
                {
                    Console.WriteLine("{0} is not an Id", SearchValue);
                }

                return Json(interviewModels, JsonRequestBehavior.AllowGet);
            }
            //else if(SearchBy =="Interview.Name")
            //{
            //    interviewModels = db.roundInterviews.Where(x => x.Interview.Name.Contains(SearchValue) || SearchValue == null).ToList();
            //    return Json(interviewModels, JsonRequestBehavior.AllowGet);
            //}
            //else if (SearchBy == "Jobs.SelectedSkillID")
            //{
            //    interviewModels = db.roundInterviews.Where(x => x.Jobs.SelectedSkillID.Contains(SearchValue) || SearchValue == null).ToList();
            //    return Json(interviewModels, JsonRequestBehavior.AllowGet);
            //}
            else
            {
                //interviewModels = db.roundInterviews.Where(x => x.Interview.Name.Contains(SearchValue) || x.Date_Time.ToString().Contains(SearchValue) || x.Results.ToString().Contains(SearchValue) || x.Jobs.SelectedSkillID.Contains(SearchValue) || SearchValue == null).ToList();
                interviewModels = db.roundInterviews.Where(x => x.Jobs.SelectedSkillID.Contains(SearchValue)).Where(x => x.Interview.Name.Contains(SearchValue)).Where(x=> x.Date_Time.ToString().Contains(SearchValue)).ToList();
                return Json(interviewModels, JsonRequestBehavior.AllowGet);
            }

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
