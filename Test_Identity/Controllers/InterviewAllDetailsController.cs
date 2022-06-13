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
            //List<CandModels> candidate = new List<CandModels>();
            var CandModels = new List<CandModels>();
            var Job = new List<Job>();
            var Interviewer = new List<InterviewerModel>();
            var interviewScheduler = new List<InterviewModels>();

            var interviewObj = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();

            foreach (var intervewData in interviewObj)
            {
                CandModels candModel = new CandModels();
                var candidatename = db.candidatesModels.Where(x => x.Id == intervewData.Candidate.Id).Select(x => x.Firstname).FirstOrDefault();
                candModel.Firstname = candidatename;    

                Job jobModel = new Job();
                var jobname = db.Jobs.Where(x => x.Id == intervewData.Jobs.Id).Select(x => x.JobName).FirstOrDefault();
                jobModel.JobName = jobname;
                //var skill = db.Jobs.Where(x => x.Id == intervewData.Jobs.Id).Select(x => x.SelectedSkillID).FirstOrDefault();
                //jobModel.SelectedIDArray = skill;

                InterviewerModel interviewerModel = new InterviewerModel();
                var interviewername = db.interviewerModels.Where(x => x.ID == intervewData.Interview.ID).Select(x => x.Name).FirstOrDefault();
                interviewerModel.Name = interviewername;

                InterviewModels interviewModels = new InterviewModels();
                var round = db.roundInterviews.Where(x => x.Id == intervewData.Id);//.Select(x => new { x.Round, x.Results ,x.Comments,x.Date_Time }).ToList();
                //interviewModels. = round;
                //var result = db.roundInterviews.Where(x => x.Id == intervewData.Id).Select(x => x.Results).FirstOrDefault();
                //interviewModels.Results = result;


                CandModels.Add(candModel);
                Job.Add(jobModel);
                Interviewer.Add(interviewerModel);
                interviewScheduler.Add(interviewModels);

            }

            List<InterviewTableViewModel> IVM = new List<InterviewTableViewModel>()
            {
                new InterviewTableViewModel{
                Cand = CandModels,
                Interviewer = Interviewer,
                InterviewDetails = interviewScheduler,
                Jobs = Job }
            };

            return View(IVM);
        }



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
        //public ActionResult Index()
        //{
        //    var roundInterviewList = db.roundInterviews.Include(i => i.Candidate).Include(i => i.Interview).Include(i => i.Jobs).ToList();
        //    var GroupedList = roundInterviewList.GroupBy(i => i.Id)
        //        .Select(d => new InterviewTableViewModel
        //        {
        //            //Interviewer = roundInterviewList.Where(y => y.Interview.ID == d.Key).Select(y => y.Interview).FirstOrDefault(),
        //            Interviewer = d.Select(x=>x.Interview),
        //            Cand = d.Select(e=>e.Candidate),
        //            Jobs = d.Select(f=>f.Jobs)
        //        }) ;


        //    return View(GroupedList);
        //}

    }

}


