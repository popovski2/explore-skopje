using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ExploreSkopjeMVC.Models;

namespace ExploreSkopjeMVC.Controllers
{
    public class CoffeeBarsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //Enable user to like (flag) or unlike (flag1) only once
        public static bool flag = false;
        public static bool flag1 = false;

        // GET: CoffeeBars
        public ActionResult Index()
        {
            ViewBag.TYPES = "CoffeeBars";
            return View(db.CoffeeBars.ToList());
        }

        // GET: CoffeeBars/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeBar coffeeBar = db.CoffeeBars.Find(id);
            if (coffeeBar == null)
            {
                return HttpNotFound();
            }

            //dodadeno za RatingComments->
            ViewBag.ObjectId = (int)id.Value;
            ViewBag.TYPE = "CoffeeBars";
            

            var comments = db.RatingComments.Where(d => d.ObjectId.Equals((int)id.Value)).ToList();
            ViewBag.Comments = comments;

            var ratings = db.RatingComments.Where(d => d.ObjectId.Equals((int)id.Value)).ToList();
            if (ratings.Count() > 0)
            {
                var ratingSum = ratings.Sum(d => d.Rating.Value);
                ViewBag.RatingSum = ratingSum;
                var ratingCount = ratings.Count();
                ViewBag.RatingCount = ratingCount;
                //dodavanje na rating vo baza 
                if (ModelState.IsValid && coffeeBar != null)
                {

                    coffeeBar.TotalRating = ViewBag.RatingSum / ViewBag.RatingCount;
                    db.SaveChanges();
                }
              
            }
            else
            {
                ViewBag.RatingSum = 0;
                ViewBag.RatingCount = 0;

                if (ModelState.IsValid && coffeeBar != null)
                {

                    coffeeBar.TotalRating =decimal.Zero;
                    db.SaveChanges();
                }
            }
            //<-do tuka

            return View(coffeeBar);
        }

        // GET: CoffeeBars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CoffeeBars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,longitude,latitude")] CoffeeBar coffeeBar)
        {
            if (ModelState.IsValid)
            {
                coffeeBar.likes_counter = 0;
                db.CoffeeBars.Add(coffeeBar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coffeeBar);
        }

        //Like functionality
        public ActionResult Like(int id)
        {
            CoffeeBar update = db.CoffeeBars.ToList().Find(u => u.id == id);

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

        //Unlike functionality
        public ActionResult Unlike(int id)
        {
            CoffeeBar update = db.CoffeeBars.ToList().Find(u => u.id == id);

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

        // GET: CoffeeBars/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeBar coffeeBar = db.CoffeeBars.Find(id);
            if (coffeeBar == null)
            {
                return HttpNotFound();
            }
            return View(coffeeBar);
        }

        // POST: CoffeeBars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,longitude,latitude")] CoffeeBar coffeeBar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(coffeeBar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(coffeeBar);
        }

        // GET: CoffeeBars/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CoffeeBar coffeeBar = db.CoffeeBars.Find(id);
            if (coffeeBar == null)
            {
                return HttpNotFound();
            }
            return View(coffeeBar);
        }

        // POST: CoffeeBars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            CoffeeBar coffeeBar = db.CoffeeBars.Find(id);
            db.CoffeeBars.Remove(coffeeBar);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult saveToDatabase()
        {
            string filePath = "C:/Users/User/source/repos/ExploreSkopjeMVC/ExploreSkopjeMVC/Content/Tabeli/kafulinja1.csv";

            //Read the contents of CSV file.
            string csvData = System.IO.File.ReadAllText(filePath);
            
            //Execute a loop over the rows.
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    db.CoffeeBars.Add(new CoffeeBar
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

        public ActionResult saveToDatabase1()
        {
            string filePath2 = "C:/Users/Angela Madjar/Desktop/CoffeeBars_additional.csv";

            string csvData1 = System.IO.File.ReadAllText(filePath2);

            List<CoffeeBar> bars = db.CoffeeBars.ToList();

            int i = 0;

            foreach (string row in csvData1.Split('\n'))
            {

                if (!string.IsNullOrEmpty(row))
                {
                        CoffeeBar Bar = bars.ElementAt(i);
                        db.CoffeeBars.Find(Bar).picture_URL = Convert.ToString(row.Split(',')[0]);
                        db.CoffeeBars.Find(Bar).facebook_link = Convert.ToString(row.Split(',')[1]);      
                        //ovde davat greska poso ne ni e so zapirki
                        db.SaveChanges();
                }
                i++;
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
