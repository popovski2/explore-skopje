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

        //Enable user to like (flag) or unlike (flag1) only once
        public static bool flag = false;
        public static bool flag1 = false;

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
            return View(restaurant);
        }

        // GET: Restaurants/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,longitude,latitude")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                restaurant.likes_counter = 0;
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        public ActionResult Like(int id)
        {
            Restaurant update = db.Restaurants.ToList().Find(u => u.id == id);

            if (!flag)
            {
                //ViewBag.flag = true;
                update.likes_counter += 1;
                flag = true;
                flag1 = false;
            }

            db.SaveChanges();
            return RedirectToAction("Details", new { update.id });
        }

        public ActionResult Unlike(int id)
        {
            Restaurant update = db.Restaurants.ToList().Find(u => u.id == id);

            if (!flag1)
            {
                //ViewBag.flag = false;
                update.likes_counter -= 1;
                flag1 = true;
                flag = false;
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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

         public ActionResult zaPrototip()
         {
             return View("zaPrototip");
         }

            //ova e zatoa sto gi dodadov po greska 3 pati site restorani IHUU
            public ActionResult deleteAllFromDatabase()
        {

            foreach(Restaurant r in db.Restaurants)
            {
                db.Restaurants.Remove(r);
            }
            db.SaveChanges();
            return View("zaBaza");
        }



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
                        //ovde davat greska poso ne ni e so zapirki

                    });
                    db.SaveChanges();
                }
            }

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
