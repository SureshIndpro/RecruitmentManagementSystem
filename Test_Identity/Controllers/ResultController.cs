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
    public class ResultController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Result
        public ActionResult Index()
        {
            //var round = db.roundInterviews.ToList();

            //foreach (var getSkillId in round)
            //{
            //    IEnumerable<int> fetchedCandidatIds = getSkillId.CandidateId.ToString().Split(',').Select(Int32.Parse);
            //    var getCandidatName = db.candidatesModels.Where(x => fetchedCandidatIds.Contains(x.Id))
            //    .Select(candidatName => new
            //    {
            //        Name = candidatName.Firstname,
            //    });
            //    string fetchSkillName = string.Join(",", getCandidatName.Select(x => x.Name));
            //    getSkillId.CandidateId = fetchSkillName;

            //    //if ()
            //    //{
            //    //    IEnumerable<int> fetchedInterviewIds = getSkillId.Interview.ToString().Split(',').Select(Int32.Parse);
            //    //    var getInterviewName = db.interviewerModels.Where(x => fetchedInterviewIds.Contains(x.ID))
            //    //    .Select(interviewName => new
            //    //    {
            //    //        Name = interviewName.Name,
            //    //    });
            //    //    string fetchInterviewName = string.Join(",", getInterviewName.Select(x => x.Name));
            //    //    getSkillId.CandidateId = fetchInterviewName;
            //    //}
            //}
            //return View(round);
            return View(db.roundInterviews.ToList());
        }

        // GET: Result/Details/5
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

       // GET: RoundInterviewModels/Update

        public ActionResult Update()
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
        public ActionResult Update(InterviewModels roundInterviewModels)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.roundInterviews.Add(roundInterviewModels);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // GET: Result/Edit/5
        public ActionResult Edit(int id=0)
        {
            InterviewerModel inter = new InterviewerModel();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != 0)
                {
                    inter = db.interviewerModels.Where(x => x.ID == id).FirstOrDefault();
                    inter.SelectedIDArray = inter.SelectedSkillID.Split(',').ToArray();
                }
                inter.SkillCollection = db.Skills.ToList();
            }
            return View(inter);
        }

        // POST: Result/Edit/5
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

        //public ActionResult chkUpdate()
        //{
        //    var UpdateDetails = db.roundInterviews.ToList();
        //    //List<InterviewerSkillIDModels> viewModel = new List<InterviewerSkillIDModels>();
 
        //    foreach (var getUpdateId in UpdateDetails)
        //    {
        //        IEnumerable<int> fetchedSkillIds = getUpdateId.isUpdateEnabled.ToString().Split(',').Select(Int32.Parse);
        //        var getSkillName = db.Skills.Where(x => fetchedSkillIds.Contains(x.SkillId))
        //        .Select(skillName => new
        //        {
        //            skillName.SkillName,
        //         });

        //         string fetchSkillName = string.Join(",", getSkillName.Select(x => x.SkillName));
        //        getUpdateId.isUpdateEnabled = fetchSkillName;

        //    }


        //    return View(UpdateDetails);
        //}

//[HttpPost]
//[ValidateAntiForgeryToken]
//public ActionResult Update([Bind(Include = "Id,Round,CandidateId,InterviewerId,ModeOfInterview,DateTime,Comments,Result,Status")] InterviewModels interviewModels)
//{
//    if (ModelState.IsValid)
//    {
//        db.Entry(interviewModels).State = EntityState.Modified;
//        db.SaveChanges();
//        return RedirectToAction("Index");
//    }
//    return View(interviewModels);
//}


//List<InterviewModels> interview = db.Meetings.Where(m.date_meeting.Day.Equals(DateTime.Now.Day) &&
//                            m.date_meeting.Month.Equals(DateTime.Now.Month) &&
//                            m.date_meeting.Year.Equals(DateTime.Now.Year) && && x.langId == lang).ToList();

//if (Convert.ToDateTime(Date_Time).Equals(DateTime.Now())){
//            do stuff
//     }
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
