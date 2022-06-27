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
    public class InterviewModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InterviewModel
        public ActionResult Index(int id = 45)
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
            // InterviewModels interviewModels = db.roundInterviews.Find(id);
            var interviewModels = db.roundInterviews.Include(x => x.Candidate).Include(z=>z.Interview).Where(y => y.Id == id).FirstOrDefault();
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
            var jobObject0 = db.Jobs.ToList();
            foreach (var getSkillId in jobObject0)
            {
                IEnumerable<int> fetchedSkillIds1 = getSkillId.SelectedSkillID.ToString().Split(',').Select(Int32.Parse);
                var getSkillName = db.Skills.Where(x => fetchedSkillIds1.Contains(x.SkillId))
                .Select(skillName => new
                {
                    skillName.SkillName
                });

                string fetchSkillName = string.Join(",", getSkillName.Select(x => x.SkillName));
                getSkillId.SelectedSkillID = fetchSkillName;
            }
            var roundInterviews = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
            var jobObject = db.Jobs.ToList();
            InterviewModels interviewModels = db.roundInterviews.Find(id);

            if (SearchBy == "Name")
            {
                var model = db.roundInterviews.Where(x => x.Interview.Name.Contains(SearchValue) || SearchValue == null).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else if (SearchBy == "SelectedSkillID")
            {
                var roundInterview = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
                var jobObject1 = db.Jobs.ToList();
               
                var filterData = roundInterview.Where(x => x.Jobs.SelectedSkillID.ToLower().Contains(SearchValue.ToLower())).ToList();
                return Json(filterData, JsonRequestBehavior.AllowGet);

            }
            else if (SearchBy == "Results")
            { 
                var model = db.roundInterviews.Where(x => x.Results.ToString().Contains(SearchValue) || SearchValue == null).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var model = db.roundInterviews.Where(x => x.Date_Time.ToString().Contains(SearchValue) || SearchValue == null).ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Index1(string SearchBy, string SearchValue,int? id=45)
        {
            var jobObject0 = db.Jobs.ToList();
            foreach (var getSkillId in jobObject0)
            {
                IEnumerable<int> fetchedSkillIds1 = getSkillId.SelectedSkillID.ToString().Split(',').Select(Int32.Parse);
                var getSkillName = db.Skills.Where(x => fetchedSkillIds1.Contains(x.SkillId))
                .Select(skillName => new
                {
                    skillName.SkillName
                });

                string fetchSkillName = string.Join(",", getSkillName.Select(x => x.SkillName));
                getSkillId.SelectedSkillID = fetchSkillName;
            }
            var roundInterviews = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
            var jobObject = db.Jobs.ToList();
            InterviewModels interviewModels = db.roundInterviews.Find(id);

            var status = db.roundInterviews.Where(x => x.Results == 0);
            if (interviewModels.Results == 0)
            {
                if (SearchBy == "Name")
                {
                    var model = db.roundInterviews.Where(x => x.Interview.Name.Contains(SearchValue) || SearchValue == null).ToList();
                    return View(model);
                }
                else if (SearchBy == "SelectedSkillID")
                {

                    var roundInterview = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
                    var filterData = roundInterview.Where(x => x.Jobs.SelectedSkillID.ToLower().Contains(SearchValue.ToLower())).ToList();
                    return View(filterData);

                }
                else if (SearchBy == "Results")
                { 
                    var model = db.roundInterviews.Where(x => x.Results.ToString().Contains(SearchValue) || SearchValue == null).ToList();
                    return View(model);
                }
                else if (SearchBy == "Date_Time")
                {
                    var model = db.roundInterviews.Where(x => x.Date_Time.ToString().Contains(SearchValue) || SearchValue == null).ToList();
                    return View(model);
                }
                return View(status);
            }

            else
            {
                if (SearchBy == "Name")
                {
                    var model = db.roundInterviews.Where(x => x.Interview.Name.Contains(SearchValue) || SearchValue == null).ToList();
                    return View(model);
                }
                else if (SearchBy == "SelectedSkillID")
                {
                    var roundInterview = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
                    var jobObject1 = db.Jobs.ToList();
                    foreach (var getSkillId in jobObject1)
                    {
                        IEnumerable<int> fetchedSkillIds1 = getSkillId.SelectedSkillID.ToString().Split(',').Select(Int32.Parse);
                        var getSkillName = db.Skills.Where(x => fetchedSkillIds1.Contains(x.SkillId))
                        .Select(skillName => new
                        {
                            skillName.SkillName
                        });

                        string fetchSkillName = string.Join(",", getSkillName.Select(x => x.SkillName));
                        getSkillId.SelectedSkillID = fetchSkillName;
                    }
                    var filterData = roundInterview.Where(x => x.Jobs.SelectedSkillID.ToLower().Contains(SearchValue.ToLower())).ToList();
                    return View(filterData);

                }
                else if (SearchBy == "Results")
                {
                    var model = db.roundInterviews.Where(x => x.Results.ToString().Contains(SearchValue) || SearchValue == null).ToList();
                    return View(model);
                }
                else 
                {
                    var model = db.roundInterviews.Where(x => x.Date_Time.ToString().Contains(SearchValue) || SearchValue == null).ToList();
                    return View(model);
                }
                
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
