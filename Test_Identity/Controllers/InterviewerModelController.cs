using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test_Identity.Models;


namespace Test_Identity.Controllers
{
    public class InterviewerModelController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //[Authorize(Roles = "Administrator, Recruiter")]
        // GET: TestInterviewerModels
        //public InterviewerController()
        //{

        //}
        public ActionResult Index()
        {
            var interViewerDetails = db.interviewerModels.ToList();
            //List<InterviewerSkillIDModels> viewModel = new List<InterviewerSkillIDModels>();

            // 1 , int1 , 1,2,3  | 2, int2, 3,4 | 
            // List <interViewerDetails.id == 1 | interViewerDetails.name = int1 | interViewerDetails.skillid = 1,2,3>  
            foreach (var getSkillId in interViewerDetails)
            {
                IEnumerable<int> fetchedSkillIds = getSkillId.SelectedSkillID.ToString().Split(',').Select(Int32.Parse);
                var getSkillName = db.Skills.Where(x => fetchedSkillIds.Contains(x.SkillId))
                .Select(skillName => new
                {
                    Name = skillName.SkillName,
                });

                string fetchSkillName = string.Join(",", getSkillName.Select(x => x.Name));
                getSkillId.SelectedSkillID = fetchSkillName;

            }


            return View(interViewerDetails);
            //    return View(db.interviewerModels.ToList());
        }



        //[Authorize(Roles = "Administrator, Recruiter")]
        // GET: TestInterviewerModels/Details/5
        public ActionResult Details(int? id)
        {
            //InterviewerViewModel interviewerViewModel = new InterviewerViewModel()
            //{

            //}
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewerModel interviewerModel = db.interviewerModels.Find(id);
            if (interviewerModel == null)
            {
                return HttpNotFound();
            }
            return View(interviewerModel);
        }

        //GET Create
        public ActionResult Create()
        {
            InterviewerModel inter = new InterviewerModel();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                inter.SkillCollection = db.Skills.ToList();
            }
            return View(inter);
        }
        //Post
        [HttpPost]
        public ActionResult Create(InterviewerModel interviewerModel)
        {
            interviewerModel.SelectedSkillID = string.Join(",", interviewerModel.SelectedIDArray);

            //interviewerModel.SelectedSkillName = string.Join(",", interviewerModel.SelectedNameArray);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.interviewerModels.Add(interviewerModel);
                
                db.SaveChanges();

                //InterviewerSkillIDModels interviewerSkillobj = new InterviewerSkillIDModels();
                //interviewerSkillobj.InterviewerID = interviewerModel.ID;
                //interviewerSkillobj.SkillID = interviewerModel.SelectedSkillID;
                //db.interviewerSkill.Add(interviewerSkillobj);

                //db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        //GET Edit
        public ActionResult Edit(int id = 0)
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
            //InterviewerModel inter = new InterviewerModel();
            //using (ApplicationDbContext db = new ApplicationDbContext())
            //{
            //    if (id !=0)
            //    {
            //        inter = db.interviewerModels.Where(x => x.ID == id).FirstOrDefault();
            //        inter.SelectedIDArray = inter.SelectedSkillID.Split(',').ToArray();
            //    }
            //    inter.SkillCollection = db.skillModels.ToList();
            //}
            //return View(inter);
        }
        //Post
        [HttpPost]
        public ActionResult Edit(InterviewerModel interviewerModel)
        {
            interviewerModel.SelectedSkillID = string.Join(",", interviewerModel.SelectedIDArray);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (interviewerModel.ID == 0)
                {
                    db.interviewerModels.Add(interviewerModel);
                }
                else
                {
                    db.Entry(interviewerModel).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        //[Authorize(Roles = "Administrator, Recruiter")]
        // GET: TestInterviewerModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterviewerModel interviewerModel = db.interviewerModels.Find(id);
            if (interviewerModel == null)
            {
                return HttpNotFound();
            }
            return View(interviewerModel);
        }

        // POST: TestInterviewerModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterviewerModel interviewerModel = db.interviewerModels.Find(id);
            db.interviewerModels.Remove(interviewerModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
