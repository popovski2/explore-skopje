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
    public class RatingCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RatingComments
        public ActionResult Index()
        {
            return View(db.RatingComments.ToList());
        }

        // GET: RatingComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RatingComments ratingComments = db.RatingComments.Find(id);
            if (ratingComments == null)
            {
                return HttpNotFound();
            }
            return View(ratingComments);
        }

        // GET: RatingComments/Create
        public ActionResult Create()
        {
            return View();
        }

        //Adding Rating and Comment in database
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(FormCollection form)
        {
            var comment = form["Comment"].ToString();
            var objectId = form["ObjectId"];
            var rating = int.Parse(form["Rating"]);
            
            RatingComments artComment = new RatingComments()
            {
                ObjectId = int.Parse(objectId),
                Comments = comment,
                Rating = rating,
                ThisDateTime = DateTime.Now, 
            };

            db.RatingComments.Add(artComment);
            db.SaveChanges();

            IEnumerable<CoffeeBar> cb = db.CoffeeBars.ToList();
            IEnumerable<Restaurant> res = db.Restaurants.ToList();
            IEnumerable<Theatre> the = db.Theatres.ToList();

            foreach (CoffeeBar c in cb)
            {
                if (int.Parse(objectId).Equals((int)c.id))
                {
                    return RedirectToAction("Details", "CoffeeBars", new { id = objectId });
                }
            }
            
            foreach (Theatre t in the)
            {
                if (int.Parse(objectId).Equals((int)t.id))
                {
                    return RedirectToAction("Details", "Theatres", new { id = objectId });
                }
            }

            foreach (Restaurant r in res)
            {
                if (int.Parse(objectId).Equals((int)r.id))
                {
                    return RedirectToAction("Details", "Restaurants", new { id = objectId });
                }
            }          

            return RedirectToAction("Index", "Categories");
  
        }

        // POST: RatingComments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommentId,Comments,ThisDateTime,ObjectId,Rating")] RatingComments ratingComments)
        {
            if (ModelState.IsValid)
            {
                db.RatingComments.Add(ratingComments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ratingComments);
        }

        // GET: RatingComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RatingComments ratingComments = db.RatingComments.Find(id);
            if (ratingComments == null)
            {
                return HttpNotFound();
            }
            return View(ratingComments);
        }

        // POST: RatingComments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentId,Comments,ThisDateTime,ObjectId,Rating")] RatingComments ratingComments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ratingComments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ratingComments);
        }

        // GET: RatingComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RatingComments ratingComments = db.RatingComments.Find(id);
            if (ratingComments == null)
            {
                return HttpNotFound();
            }
            return View(ratingComments);
        }

        // POST: RatingComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RatingComments ratingComments = db.RatingComments.Find(id);
            db.RatingComments.Remove(ratingComments);
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
