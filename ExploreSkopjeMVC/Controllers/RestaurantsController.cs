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
    public class RestaurantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Enable user to like or unlike only once
        public static bool like = false;
        public static bool unlike = false;

        // GET: Restaurants
        public ActionResult Index()
        {
            return View(db.Restaurants.ToList());
        }

        // GET: Restaurants/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }


            //RatingComments functionality
            ViewBag.ObjectId = (int)id.Value;
            ViewBag.TYPE = "Restaurants";


            var comments = db.RatingComments.Where(d => d.ObjectId.Equals((int)id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = db.RatingComments.Where(d => d.ObjectId.Equals((int)id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;

                //Adding rating to database
                if (ModelState.IsValid && restaurant != null)
                {

                    restaurant.TotalRating = ViewBag.RatingSum / ViewBag.RatingCount;
                    db.SaveChanges();
                }
            }

            //If the restaurant isn't rated, initialize sum and count to 0
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;
                if (ModelState.IsValid && restaurant != null)
                {

                    restaurant.TotalRating = decimal.Zero;
                    db.SaveChanges();
                }
            }
            return View(restaurant);
        }


        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,longitude,latitude")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.likes_counter = 0; //Initializing likes_counter to 0
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        //Like functionality
        public ActionResult Like(int id)
        {
            Restaurant update = db.Restaurants.ToList().Find(u => u.id == id);

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
            Restaurant update = db.Restaurants.ToList().Find(u => u.id == id);

            if (!unlike)
            {
                update.likes_counter -= 1;
                unlike = true;
                like = false;
            }

            db.SaveChanges();
            return RedirectToAction("Details", new { update.id });
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,longitude,latitude")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //A function to fill the database with data from the pipe (.csv file)
        //NOTE: This function is executed ONLY ONCE!
        //NOTE: Preventing multiple executions of this function by reading from absolute path
        //The path should be changed if the function is called from another PC
        public ActionResult saveToDatabase()
        {
            string filePath = "C:/Users/User/source/repos/ExploreSkopjeMVC/ExploreSkopjeMVC/Content/Tabeli/restorani1.csv";

            //Read the contents of CSV file.
            string csvData = System.IO.File.ReadAllText(filePath);

            //Execute a loop over the rows.
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    db.Restaurants.Add(new Restaurant
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
        //execute this function to delete all the restaurants
        //Then, call saveToDatabase() again
        public ActionResult deleteAllFromDatabase()
        {
            foreach (Restaurant r in db.Restaurants)
            {
                db.Restaurants.Remove(r);
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
