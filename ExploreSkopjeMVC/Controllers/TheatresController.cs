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
    public class TheatresController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Enable user to like or unlike only once
        public static bool like = false;
        public static bool unlike = false;

        // GET: Theatres
        public ActionResult Index()
        {
            return View(db.Theatres.ToList());
        }

        // GET: Theatres/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theatre theatre = db.Theatres.Find(id);
            if (theatre == null)
            {
                return HttpNotFound();
            }


            //RatingComments functionality
            ViewBag.ObjectId = (int)id.Value;
            ViewBag.TYPE = "Theatres";


            var comments = db.RatingComments.Where(d => d.ObjectId.Equals((int)id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = db.RatingComments.Where(d => d.ObjectId.Equals((int)id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
                if (ModelState.IsValid && theatre != null)
                {
                    //Adding rating to database
                    theatre.TotalRating = ViewBag.RatingSum / ViewBag.RatingCount;
                    db.SaveChanges();
                }
            }

            //If the theatre isn't rated, initialize sum and count to 0
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
                if (ModelState.IsValid && theatre != null)
                {

                    theatre.TotalRating = decimal.Zero;
                    db.SaveChanges();
                }

            }
            return View(theatre);
        }

        // GET: Theatres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Theatres/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,longitude,latitude")] Theatre theatre)
        {
            if (ModelState.IsValid)
            {
                theatre.likes_counter = 0; //Initializing likes_counter to 0
                db.Theatres.Add(theatre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theatre);
        }

        //Like functionality
        public ActionResult Like(int id)
        {
            Theatre update = db.Theatres.ToList().Find(u => u.id == id);

            if (!like)
            {
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
            Theatre update = db.Theatres.ToList().Find(u => u.id == id);

            if (!unlike)
            {
                update.likes_counter -= 1;
                unlike = true;
                like = false;
            }

            db.SaveChanges();
            return RedirectToAction("Details", new { update.id });
        }

        // GET: Theatres/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theatre theatre = db.Theatres.Find(id);
            if (theatre == null)
            {
                return HttpNotFound();
            }
            return View(theatre);
        }

        // POST: Theatres/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,longitude,latitude")] Theatre theatre)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theatre).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theatre);
        }

        // GET: Theatres/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Theatre theatre = db.Theatres.Find(id);
            if (theatre == null)
            {
                return HttpNotFound();
            }
            return View(theatre);
        }

        // POST: Theatres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Theatre theatre = db.Theatres.Find(id);
            db.Theatres.Remove(theatre);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //A function to fill the database with data from the pipe (.csv file)
        //NOTE: This function is executed ONLY ONCE!
        //NOTE: Preventing multiple executions of this function by reading from absolute path
        //The path should be changed if the function is called from another PC
        public ActionResult saveToDatabase()
        {
            string filePath = "C:/Users/User/source/repos/ExploreSkopjeMVC/ExploreSkopjeMVC/Content/Tabeli/teatri1.csv";

            //Read the contents of CSV file.
            string csvData = System.IO.File.ReadAllText(filePath);

            //Execute a loop over the rows.
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    db.Theatres.Add(new Theatre
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
        //execute this function to delete all the theatres
        //Then, call saveToDatabase() again
        public ActionResult deleteAllFromDatabase()
        {
            foreach (Theatre t in db.Theatres)
            {
                db.Theatres.Remove(t);
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
