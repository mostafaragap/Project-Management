using System;
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
        public ActionResult CustomerOldProjects()
        {
            var UserId = User.Identity.GetUserId();
            var MyProjects = db.AssignJobs.Where(a => a.project.customerId == UserId && a.project.isopen == 0).ToList();
            if (MyProjects.Count == 0)
            {
                ViewBag.Result = "You don't Have a Projects";
            }


            return View(MyProjects);
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

        ////////////////////////////////////////////////
        ///
        [Authorize]
        public ActionResult ApproveProject()
        {
            return View();

        }

        [HttpPost]
        public ActionResult ApproveProject(string Message, double Price)
        {
            var UserId = User.Identity.GetUserId();
            var projectId = (int)Session["id"];
            
            var check = db.AssignJobs.Where(a => a.projectId == projectId && a.UserId == UserId).ToList();
            if (check.Count < 1)
            {

                var projectA = new AssignJob();

                projectA.UserId = UserId;
                projectA.projectId = projectId;

                projectA.Message = Message;
                projectA.Price = Price;
                projectA.State = "Pending";
                projectA.Customer_Notes = "Waiting for customer Agree";

                db.AssignJobs.Add(projectA);
                db.SaveChanges();
                ViewBag.result = "Apply";
            }
            else
            {

                ViewBag.result = "you  allready applied to this project";
            }

            return View();
        }
        // return PM applys for this job
        public ActionResult AppliedPm()
        {
            var id = (int)Session["id"];
            var pro = db.AssignJobs.Where(a => a.projectId == id).ToList();

            if (pro.Count == 0)
            {
                ViewBag.Result = "There is no one applay this project";

            }



            return View(pro);


        }
        public ActionResult AgreeApplied()
        {

            return View();
        }


        [HttpPost]
        public ActionResult AgreeApplied(int id, string Customer_Notes)
        {
            {
                var pro = db.AssignJobs.Find(id);
                var proId = pro.projectId;
                pro.State = "Agree";
                pro.Customer_Notes = Customer_Notes;
                db.Entry(pro).Property("Customer_Notes").IsModified = true;
                var project = db.projects.Find(proId);
                project.isopen = 0;
                db.Entry(project).Property("isopen").IsModified = true;
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult DeleteApplied(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignJob project = db.AssignJobs.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost, ActionName("DeleteApplied")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDelete(int id)
        {
            AssignJob project = db.AssignJobs.Find(id);
            db.AssignJobs.Remove(project);
            db.SaveChanges();
            return RedirectToAction("CustomerIndex");
        }

        [Authorize]
        public ActionResult GetProjects()
        {
            var UserId = User.Identity.GetUserId();

            var pro = db.AssignJobs.Where(a => a.UserId == UserId);


            return View(pro.ToList());
        }

        [Authorize]
        public ActionResult GetTLProjects()
        {
            var UserId = User.Identity.GetUserId();

            var pro = db.AssignJobs.Where(a => a.TeamleaderId == UserId);


            return View(pro.ToList());
        }
        public ActionResult AddTeamLeader(int id)
        {
            AssignJob TM = new AssignJob();
            TM = db.AssignJobs.Find(id);
            Session["ASID"] = TM.id;

            return View(db.TeamLeaders.ToList());


        }


        public ActionResult AddTML(string id)
        {
            int AssignId = (int)Session["ASID"];
            // var AssJob = db.AssignJobs.Find(AssignId);
            AssignJob AssJob = db.AssignJobs.Find(AssignId);
            var check = db.AssignJobs.Where(a => a.id == AssignId && a.TeamleaderId == id).ToList();
            if (check.Count < 1)
            {
                AssJob.TeamleaderId = id;
                db.Entry(AssJob).Property("TeamleaderId").IsModified = true;
                db.SaveChanges();
                ViewBag.Res = "TeamLeader Added Succecfully";
            }
            else
            {
                ViewBag.Res = "TeamLeader Allready Added";
            }

            return View();

        }

        public ActionResult Deliverd(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssignJob project = db.AssignJobs.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost, ActionName("Deliverd")]
        [ValidateAntiForgeryToken]
        public ActionResult DeliverdDelete(int id)
        {
            AssignJob project = db.AssignJobs.Find(id);
            db.AssignJobs.Remove(project);
            db.SaveChanges();
            return RedirectToAction("GetProjects");
        }

        public ActionResult TLAprrove()
        {


            return RedirectToAction("GetTLProjects");
        }

        public ActionResult TLIgnore(int id)
        {
            var Ass = db.AssignJobs.Find(id);
            Ass.TeamleaderId = null;
            db.Entry(Ass).Property("TeamleaderId").IsModified = true;
            db.SaveChanges();
            return RedirectToAction("GetTLProjects");
        }
        public ActionResult AddDevelopers(int id)
        {
            AssignJob Tm = new AssignJob();
            Team team = new Team();
            Tm = db.AssignJobs.Find(id);
            Session["ASID"] = Tm.id;

            return View(db.Developers.ToList());


        }

        public ActionResult AddDeveloper(string id)
        {
            int AssignId = (int)Session["ASID"];
            // var AssJob = db.AssignJobs.Find(AssignId);
            AssignJob AssJob = db.AssignJobs.Find(AssignId);
            Team team = new Team();
            var dev = db.Developers.Find(id);
            var check = db.Teams.Where(a => a.AssignId == AssignId && a.DeveloperId == id).ToList();
            if (check.Count < 1)
            {
                AssJob.TLState = 1;
                team.DeveloperId = id;
                team.AssignId = AssJob.id;
                team.JDSkills = dev.ApplicationUser.Skills;
                team.TeamLeaderID = AssJob.TeamleaderId;
                db.Teams.Add(team);
                db.SaveChanges();
                ViewBag.Res = "Developer Added Succecfully";
            }
            else
            {
                ViewBag.Res = "Developer Allready Added privusily";
            }

            return View();

        }


        [Authorize]
        public ActionResult GetDEVProjects()
        {
            var UserId = User.Identity.GetUserId();

            var pro = db.Teams.Where(a => a.DeveloperId == UserId);


            return View(pro.ToList());
        }
        //get the developers that work in project
        [Authorize]
        public ActionResult ViewTeam(int id)
        {
            var pro = db.Teams.Where(a => a.AssignId == id).ToList();
            if (pro.Count == 0)
            {
                ViewBag.Res = "Thers is no Members Added";
            }
            return View(pro);

        }




        public ActionResult DevDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Team project = db.Teams.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        [HttpPost, ActionName("DevDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmedDeleteDev(int id)
        {
            Team project = db.Teams.Find(id);
            db.Teams.Remove(project);
            db.SaveChanges();
            return RedirectToAction("GetProjects");
        }

        public ActionResult PMOldProjects()
        {
            var UserId = User.Identity.GetUserId();
            var MyProjects = db.AssignJobs.Where(a => a.UserId == UserId && a.project.isopen == 0).ToList();
            if (MyProjects.Count == 0)
            {
                ViewBag.Result = "You don't Have a Projects";
            }


            return View(MyProjects);
        }
        public ActionResult TLOldProjects()
        {
            var UserId = User.Identity.GetUserId();
            var MyProjects = db.AssignJobs.Where(a => a.TeamleaderId == UserId && a.project.isopen == 0).ToList();
            if (MyProjects.Count == 0)
            {
                ViewBag.Result = "You don't Have a Projects";
            }


            return View(MyProjects);
        }
        [Authorize]
        public ActionResult ViewTeamRead(int id)
        {
            var pro = db.Teams.Where(a => a.AssignId == id).ToList();
            if (pro.Count == 0)
            {
                ViewBag.Res = "Thers is no Members Added";
            }
            return View(pro);

        }


    }
}
