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
    public class InterviewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RoundInterviewModels
        public ActionResult Index()
        {
            var round = db.roundInterviews.ToList();

            foreach (var getSkillId in round)
            {
                IEnumerable<int> fetchedCandidatIds = getSkillId.Candidate.ToString().Split(',').Select(Int32.Parse);
                var getCandidatName = db.candidatesModels.Where(x => fetchedCandidatIds.Contains(x.Id))
                .Select(candidatName => new
                {
                    Name = candidatName.Firstname,
                });
                string fetchSkillName = string.Join(",", getCandidatName.Select(x => x.Name));
                getSkillId.CandidateId = fetchSkillName;

                //if ()
                //{
                //    IEnumerable<int> fetchedInterviewIds = getSkillId.Interview.ToString().Split(',').Select(Int32.Parse);
                //    var getInterviewName = db.interviewerModels.Where(x => fetchedInterviewIds.Contains(x.ID))
                //    .Select(interviewName => new
                //    {
                //        Name = interviewName.Name,
                //    });
                //    string fetchInterviewName = string.Join(",", getInterviewName.Select(x => x.Name));
                //    getSkillId.CandidateId = fetchInterviewName;
                //}
            }

            //foreach (var getCandId in round)
            //{
            //    IEnumerable<int> fetchedCandidatIds = getCandId.CandidateId.ToString().Split(',').Select(Int32.Parse);
            //    var getCandidatName = db.candidatesModels.Where(x => fetchedCandidatIds.Contains(x.Id))
            //    .Select(candidatName => new
            //    {
            //        Name = candidatName.Firstname,
            //    });
            //    string fetchSkillName = string.Join(",", getCandidatName.Select(x => x.Name));
            //    getCandId.CandidateId = fetchSkillName;
            //}

            //foreach (var getInterId in round)
            //{
            //    IEnumerable<int> fetchedInterviewIds = getInterId.CandidateId.ToString().Split(',').Select(Int32.Parse);
            //    var getInterviewName = db.interviewerModels.Where(x => fetchedInterviewIds.Contains(x.ID))
            //    .Select(interviewName => new
            //    {
            //        Name = interviewName.Name,
            //    });
            //    string fetchInterviewName = string.Join(",", getInterviewName.Select(x => x.Name));
            //    getInterId.CandidateId = fetchInterviewName;
            //}
            return View(round);
        }

        // GET: RoundInterviewModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewModels roundInterviewModels = db.roundInterviews.Find(id);
            if (roundInterviewModels == null)
            {
                return HttpNotFound();
            }
            return View(roundInterviewModels);
        }

        // GET: RoundInterviewModels/Create

        public ActionResult Create()
        {
            InterviewModels model = new InterviewModels();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                model.Candidate = db.candidatesModels.ToList();
                model.Interview = db.interviewerModels.ToList();
            }
            return View(model);
        }

        // POST: RoundInterviewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InterviewModels roundInterviewModels)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.roundInterviews.Add(roundInterviewModels);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: RoundInterviewModels/Edit/5
        public ActionResult Edit(int id = 0)
        {
            InterviewModels model = new InterviewModels();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != 0)
                {
                    model = db.roundInterviews.Where(x => x.Id == id).FirstOrDefault();
                }
                model.Candidate = db.candidatesModels.ToList();
                model.Interview = db.interviewerModels.ToList();
            }
            return View(model);
        }

        // POST: RoundInterviewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InterviewModels roundInterviewModels)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (roundInterviewModels.Id == 0)
                {
                    db.roundInterviews.Add(roundInterviewModels);
                }
                else
                {
                    db.Entry(roundInterviewModels).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: RoundInterviewModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewModels roundInterviewModels = db.roundInterviews.Find(id);
            if (roundInterviewModels == null)
            {
                return HttpNotFound();
            }
            return View(roundInterviewModels);
        }

        // POST: RoundInterviewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterviewModels roundInterviewModels = db.roundInterviews.Find(id);
            db.roundInterviews.Remove(roundInterviewModels);
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
