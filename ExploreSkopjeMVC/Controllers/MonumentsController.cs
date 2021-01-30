using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExploreSkopjeMVC.Models;

namespace ExploreSkopjeMVC.Controllers
{

    public class MonumentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Enable user to like or unlike only once
        public static bool like = false;
        public static bool unlike = false;


        // GET: Monuments
        public ActionResult Index()
        {
            return View(db.Monuments.ToList());
        }

        // GET: Monuments/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monument monument = db.Monuments.Find(id);
            if (monument == null)
            {
                return HttpNotFound();
            }
            return View(monument);
        }

        // GET: Monuments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Monuments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,longitude,latitude")] Monument monument)
        {
            if (ModelState.IsValid)
            {
                monument.likes_counter = 0; //Initializing likes_counter to 0
                db.Monuments.Add(monument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(monument);
        }

        //Like functionality
        public ActionResult Like(int id)
        {
            Monument update = db.Monuments.ToList().Find(u => u.id == id);

            if (!like)
            {
                //ViewBag.flag = true;
                update.likes_counter += 1;
                like = true;
                unlike = false;
            }

            db.SaveChanges();
            return RedirectToAction("Details", new { update.id });
        }

        //Unlike functionality
        public ActionResult Unlike(int id)
        {
            Monument update = db.Monuments.ToList().Find(u => u.id == id);

            if (!unlike)
            {
                update.likes_counter -= 1;
                unlike = true;
                like = false;
            }

            db.SaveChanges();
            return RedirectToAction("Details", new { update.id });
        }

        // GET: Monuments/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monument monument = db.Monuments.Find(id);
            if (monument == null)
            {
                return HttpNotFound();
            }
            return View(monument);
        }

        // POST: Monuments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,longitude,latitude")] Monument monument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monument).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(monument);
        }

        // GET: Monuments/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Monument monument = db.Monuments.Find(id);
            if (monument == null)
            {
                return HttpNotFound();
            }
            return View(monument);
        }

        // POST: Monuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Monument monument = db.Monuments.Find(id);
            db.Monuments.Remove(monument);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        //A function to fill the database with data from the pipe (.csv file)
        //NOTE: This function is executed ONLY ONCE!
        //NOTE: Preventing multiple executions of this function by reading from absolute path
        //The path should be changed if the function is called from another PC
        public ActionResult saveToDatabase()
        {
            string filePath = "C:/Users/User/source/repos/ExploreSkopjeMVC/ExploreSkopjeMVC/Content/Tabeli/monumenti1.csv";


            //Read the contents of CSV file.
            string csvData = System.IO.File.ReadAllText(filePath);

            //Execute a loop over the rows.
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    db.Monuments.Add(new Monument
                    {
                        id = Convert.ToInt64(row.Split(',')[0]),
                        longitude = Convert.ToDouble(row.Split(',')[1]),
                        latitude = Convert.ToDouble(row.Split(',')[2]),
                        name = row.Split(',')[3]

                    });
                    db.SaveChanges();
                }
            }

            return View("zaBaza");
        }

        //In case data from the pipelines is read more than once by mistake,
        //execute this function to delete all the monuments
        //Then, call saveToDatabase() again
        public ActionResult deleteAllFromDatabase()
        {
            foreach (Monument m in db.Monuments)
            {
                db.Monuments.Remove(m);
            }
            db.SaveChanges();
            return View("zaBaza");
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
