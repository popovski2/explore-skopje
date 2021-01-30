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
    public class TrendingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Trendings
        public ActionResult Index()
        {

            var model = new Trending();

            //Get the first three Coffeebars, Restaurants and Theatres from the database
            //Sorted by TotalRating in descending order
            model.trendingCoffeeBars = db.CoffeeBars.OrderByDescending(o => o.TotalRating).Take(3).ToList();
            model.trendingRestaurants = db.Restaurants.OrderByDescending(o => o.TotalRating).Take(3).ToList();
            model.trendingTheaters = db.Theatres.OrderByDescending(o => o.TotalRating).Take(3).ToList();

            return View(model);
        }

        // GET: Trendings/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trending trending = db.Trendings.Find(id);
            if (trending == null)
            {
                return HttpNotFound();
            }
            return View(trending);
        }

        // GET: Trendings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trendings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id")] Trending trending)
        {
            if (ModelState.IsValid)
            {
                db.Trendings.Add(trending);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trending);
        }

        // GET: Trendings/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trending trending = db.Trendings.Find(id);
            if (trending == null)
            {
                return HttpNotFound();
            }
            return View(trending);
        }

        // POST: Trendings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id")] Trending trending)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trending).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trending);
        }

        // GET: Trendings/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trending trending = db.Trendings.Find(id);
            if (trending == null)
            {
                return HttpNotFound();
            }
            return View(trending);
        }

        // POST: Trendings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Trending trending = db.Trendings.Find(id);
            db.Trendings.Remove(trending);
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