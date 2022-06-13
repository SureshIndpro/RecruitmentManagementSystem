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
    //[Authorize(Roles = "Administrator")]
    public class RecruiterController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View(db.recruiters.ToList());
        }
        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RecruiterModel recruiterModel)
        {
            if (ModelState.IsValid)
            {
                var isEmailAlreadyExists = db.recruiters.Any(x => x.Email ==recruiterModel.Email);
                var isPhoneAlreadyExists = db.recruiters.Any(x => x.Phone == recruiterModel.Phone);
                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "User with this email already exists");
                    return View(recruiterModel);
                }
                if (isPhoneAlreadyExists)
                {
                    ModelState.AddModelError("Phone Number", "User with this phone number already exists");
                    return View(recruiterModel);
                }
                db.recruiters.Add(recruiterModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(recruiterModel);
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecruiterModel recruiterModel = db.recruiters.Find(id);
            if (recruiterModel == null)
            {
                return HttpNotFound();
            }
            return View(recruiterModel);
        }

        [HttpPost]
        public ActionResult Edit(RecruiterModel recruiterModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recruiterModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(recruiterModel);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecruiterModel recruiterModel = db.recruiters.Find(id);
            if (recruiterModel == null)
            {
                return HttpNotFound();
            }
            return View(recruiterModel);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecruiterModel recruiterModel = db.recruiters.Find(id);
            if (recruiterModel == null)
            {
                return HttpNotFound();
            }
            return View(recruiterModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            RecruiterModel recruiterModel = db.recruiters.Find(id);
            db.recruiters.Remove(recruiterModel);
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
