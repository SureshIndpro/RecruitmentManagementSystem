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
using Test_Identity.ViewModels;

namespace Test_Identity.Controllers
{
    public class InterviewAllDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var CandModels = new List<CandModels>();
            var Job = new List<Job>();
            var Interviewer = new List<InterviewerModel>();
            var interviewScheduler = new List<InterviewModels>();

            var interviewObj = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
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
            foreach (var intervewData in interviewObj)
            {
               
                CandModels.Add(new CandModels { Firstname = intervewData.Candidate.Firstname });
                interviewScheduler.Add(new InterviewModels { Round = intervewData.Round,Results= intervewData.Results, ModeOfInterview=intervewData.ModeOfInterview, Date_Time=intervewData.Date_Time, Comments=intervewData.Comments });
                Interviewer.Add(new InterviewerModel { Name = intervewData.Interview.Name });
                Job.Add(new Job {JobName = intervewData.Jobs.JobName, JobDescription= intervewData.Jobs.JobDescription, SelectedSkillID=intervewData.Jobs.SelectedSkillID });

            }

            InterviewTableViewModel IVM = new InterviewTableViewModel
            {

                Cand = CandModels,
                InterviewDetails=interviewScheduler,
                Interviewer = Interviewer,
                Jobs = Job
            };

            return View(IVM);
        }



        //2nd Approach
        //public ActionResult Index()
        //{
        //    var IVM = new List<InterviewTableViewModel>();
        //    //var interviewScheduler = new List<InterviewModels>();
        //    var interviewObj = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
        //    InterviewTableViewModel interviewModels = new InterviewTableViewModel();
        //    InterviewModels interviewDetail = new InterviewModels();
        //    CandModels candModels = new CandModels();
        //    foreach (var intervewData in interviewObj)
        //    {

        //        //var round = db.roundInterviews.Where(x => x.Id == intervewData.Id);
        //        interviewDetail.Candidate.Firstname = intervewData.Candidate.Firstname;
        //        can
        //        //interviewModels.InterviewDetails.Results = round.Select(x => x.Results).FirstOrDefault();
        //        //interviewModels.InterviewDetails.Comments = round.Select(x => x.Comments).FirstOrDefault();
        //        //interviewModels.InterviewDetails.Date_Time = round.Select(x => x.Date_Time).FirstOrDefault();
        //        //interviewModels.InterviewDetails.ModeOfInterview = round.Select(x => x.ModeOfInterview).FirstOrDefault();
        //        //interviewModels.Cand.Firstname = round.Select(x => x.Candidate.Firstname).FirstOrDefault();
        //        //interviewModels.Interview.Name = round.Select(x => x.Interview.Name).FirstOrDefault();
        //        //interviewModels.Jobs.JobName = round.Select(x => x.Jobs.JobName).FirstOrDefault();
        //        //interviewModels.Jobs.JobDescription = round.Select(x => x.Jobs.JobDescription).FirstOrDefault();
        //        //interviewModels.Jobs.SelectedSkillID = round.Select(x => x.Jobs.SelectedSkillID).FirstOrDefault();
        //        //interviewScheduler.Add(interviewModels);
        //        IVM.Add(interviewModels);
        //    }
        //    return View(IVM);
        //}









        //public ActionResult Index(InterviewTableViewModel DetailsViewModel)
        //{
        //    var candidate = new List<CandModels>();
        //    var Job = new List<Job>();
        //    var Interviewer = new List<InterviewerModel>();
        //    var interviewScheduler = new List<InterviewModels>();
        //    //InterviewModels interview = new InterviewModels();
        //    var interviewObj = db.roundInterviews.ToList();

        //    foreach (var intervewData in interviewObj)
        //    {
        //        var candidateName = db.candidatesModels.Where(x => x.Id = intervewData.CandidateId).Select(x => x.Firstname).FirstOrDefault();
        //        var addCandidateName = new List<CandModels>()
        //        {
        //        new CandModel{canidnatename= candidateName };
        //        //CandModels cand = new CandModels()
        //        //{
        //        //    Id = intervewData.Candidate.Id,
        //        //    Firstname = intervewData.Candidate.Firstname
        //        //};
        //        //Job jobs = new Job()
        //        //{
        //        //    Id = intervewData.Jobs.Id,
        //        //    JobName = intervewData.Jobs.JobName,
        //        //    SelectedSkillID = intervewData.Jobs.SelectedSkillID
        //        //};
        //        //InterviewerModel interviewers = new InterviewerModel()
        //        //{
        //        //    ID = intervewData.Interview.ID,
        //        //    Name = intervewData.Interview.Name
        //        //};
        //        //InterviewModels intervieweSchedule = new InterviewModels()
        //        //{
        //        //    Id = intervewData.Id,
        //        //    Round = intervewData.Round,
        //        //    Date_Time = intervewData.Date_Time,
        //        //    Results = intervewData.Results,
        //        //    Comments = intervewData.Comments
        //        //};
        //        //InterviewTableViewModel IVM = new InterviewTableViewModel()
        //        //{
        //        //    Cand = cand,
        //        //    Interviewer = interviewers,
        //        //    InterviewDetails = intervieweSchedule,
        //        //    Jobs = jobs
        //        //};
        //        //return View(IVM);
        //    }


        //GET: InterviewAllDetails
        //private readonly IMapper _mapper;
        //public InterviewAllDetailsController(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}
        //public ActionResult Index1()
        //{ 
        //    var roundInterviewList = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
        //    var mappedItem = _mapper.Map<List<AutoMapperVM>>(roundInterviewList);
        //    var GroupedList = roundInterviewList.GroupBy(i => i.Interview.Name)
        //        .Select(d => new AutoMapperVM
        //        {
        //            //Interviewer = roundInterviewList.Where(y => y.Interview.ID == d.Key).Select(y => y.Interview).FirstOrDefault(),
        //            InterviewerName = d.Key,
        //            CandidateName = d.Select(x=>x.Candidate.Firstname).ToList(),
        //            SelectedSkillID = d.Select(y=>y.Jobs.SelectedSkillID).ToList()
        //        });


        //    return View(GroupedList);
        //}

        public ActionResult Index1()
        {
            var CandModels = new List<CandModels>();
            var Job = new List<Job>();
            var Interviewer = new List<InterviewerModel>();
            var interviewScheduler = new List<InterviewModels>();

            var interviewObj = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
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
            foreach (var intervewData in interviewObj)
            {

                CandModels.Add(new CandModels { Firstname = intervewData.Candidate.Firstname });
                interviewScheduler.Add(new InterviewModels { Round = intervewData.Round, Results = intervewData.Results, ModeOfInterview = intervewData.ModeOfInterview, Date_Time = intervewData.Date_Time, Comments = intervewData.Comments , Id=intervewData.Id});
                Interviewer.Add(new InterviewerModel { Name = intervewData.Interview.Name });
                Job.Add(new Job { JobName = intervewData.Jobs.JobName, JobDescription = intervewData.Jobs.JobDescription, SelectedSkillID = intervewData.Jobs.SelectedSkillID });

            }
            InterviewTableViewModel IVM = new InterviewTableViewModel
            {

                Cand = CandModels,
                InterviewDetails = interviewScheduler,
                Interviewer = Interviewer,
                Jobs = Job
            };
           

            return View(IVM);
        }
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
        //Edit
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            //var interviewObj = db.roundInterviews.ToList();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(InterviewModels interviewModels)
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
        //Delete
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

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterviewModels interviewModels = db.roundInterviews.Find(id);
            db.roundInterviews.Remove(interviewModels);
            db.SaveChanges();
            return RedirectToAction("Index");
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
            var interviewModels = db.roundInterviews.Include(x => x.Candidate).Include(z => z.Interview).Where(y => y.Id == id).FirstOrDefault();
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

    }

}


