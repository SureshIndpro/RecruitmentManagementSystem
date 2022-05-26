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
    public class CandModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CandModels
        public ActionResult Index()
        {
            var candViewerDetails = db.candidatesModels.ToList();
            //List<InterviewerSkillIDModels> viewModel = new List<InterviewerSkillIDModels>();

            // 1 , int1 , 1,2,3  | 2, int2, 3,4 | 
            // List <interViewerDetails.id == 1 | interViewerDetails.name = int1 | interViewerDetails.skillid = 1,2,3>  
            foreach (var getSkillId in candViewerDetails)
            {
                IEnumerable<int> fetchedSkillIds = getSkillId.Skill.ToString().Split(',').Select(Int32.Parse);
                var getSkillName = db.Skills.Where(x => fetchedSkillIds.Contains(x.SkillId))
                .Select(skillName => new { Name = skillName.SkillName });

                string fetchSkillName = string.Join(",", getSkillName.Select(x => x.Name));
                getSkillId.Skill = fetchSkillName;

            }


            return View(candViewerDetails);
        }

        // GET: CandModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandModels candModels = db.candidatesModels.Find(id);
            if (candModels == null)
            {
                return HttpNotFound();
            }
            return View(candModels);
        }

        // GET: CandModels/Create

        public ActionResult Create()
        {
            CandModels cand = new CandModels();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                cand.SkillList = db.Skills.ToList();
            }
            return View(cand);
        }
        //Post
        [HttpPost]
        public ActionResult Create(CandModels candModels)
        {
            candModels.Skill = string.Join(",", candModels.SelectedArray);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.candidatesModels.Add(candModels);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }


        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: CandModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Firstname,Lastname,Phone_no,Email,Experience,Skill,Current_CTC,Expected_CTC,Notice_period,Created_date,Current_address,Permanent_address")] CandModels candModels)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.candidatesModels.Add(candModels);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(candModels);
        //}

        // GET: CandModels/Edit/5
        public ActionResult Edit(int id = 0)
        {
            CandModels cand = new CandModels();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (id != 0)
                {
                    cand = db.candidatesModels.Where(x => x.Id == id).FirstOrDefault();
                    cand.SelectedArray = cand.Skill.Split(',').ToArray();
                }
                cand.SkillList = db.Skills.ToList();
            }
            return View(cand);
        }
        //Post
        [HttpPost]
        public ActionResult Edit(CandModels candModels)
        {
            candModels.Skill = string.Join(",", candModels.SelectedArray);
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (candModels.Id == 0)
                {
                    db.candidatesModels.Add(candModels);
                }
                else
                {
                    db.Entry(candModels).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: CandModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CandModels candModels = db.candidatesModels.Find(id);
            if (candModels == null)
            {
                return HttpNotFound();
            }
            return View(candModels);
        }

        // POST: CandModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CandModels candModels = db.candidatesModels.Find(id);
            db.candidatesModels.Remove(candModels);
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