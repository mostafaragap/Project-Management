﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ProjectManag.Models;

namespace ProjectManag.Controllers
{
    [Authorize]
    public class projectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: projects
        public ActionResult Index()
        {

            return View(db.projects.Where(a => a.isopen == 1).ToList());
        }

        public ActionResult CustomerIndex()
        {
            var User_id = User.Identity.GetUserId();
            return View(db.projects.Where(a => a.customerId == User_id && a.isopen == 1).ToList());
        }
        public ActionResult DeveloperIndex()
        {

            return View(db.projects.Where(a => a.isopen == 1).ToList());
        }

        public ActionResult CustomerCreate()
        {

            return View();
        }
        public ActionResult CustomerDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            Session["id"] = id; //this send project id for next page
            return View(project);
        }

        public ActionResult CustomerEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerEdit([Bind(Include = "id,title,decsription,NoOfHours,skill_need,due_deta,creation_date,customerId,isopen")] project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CustomerIndex");
            }
            return View(project);
        }

        // POST: projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,decsription,NoOfHours,skill_need,due_deta,creation_date,customerId,isopen")] project project)
        {
            if (ModelState.IsValid)
            {
                project.isopen = 1;
                db.projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        public ActionResult CustomerDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost, ActionName("CustomerDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerDeleteConfirmed(int id)
        {
            project project = db.projects.Find(id);
            db.projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("CustomerIndex");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerCreate([Bind(Include = "id,title,decsription,NoOfHours,price,skill_need,due_deta,creation_date,customerId,isopen")] project project)
        {


            if (ModelState.IsValid)
            {

                project.customerId = User.Identity.GetUserId();
                project.isopen = 1; // mean that project is not assigned yet 
                project.creation_date = DateTime.Now;
                db.projects.Add(project);
                db.SaveChanges();
                Session["id"] = project.customerId; // this send id to next page
                return RedirectToAction("CustomerIndex");
            }


            return View(project);
        }


        // GET: projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            Session["id"] = id;
            return View(project);
        }

        // GET: projects/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]


        // GET: projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            
            return View(project);
        }

        // POST: projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,decsription,NoOfHours,skill_need,due_deta,creation_date,isopen,customerId")] project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(project);
        }

        // GET: projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            project project = db.projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            project project = db.projects.Find(id);
            db.projects.Remove(project);
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