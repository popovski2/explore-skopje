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

        //Enable user to like (flag) or unlike (flag1) only once
        public static bool flag = false;
        public static bool flag1 = false;

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
            return View(theatre);
        }

        // GET: Theatres/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Theatres/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,longitude,latitude")] Theatre theatre)
        {
            if (ModelState.IsValid)
            {
                theatre.likes_counter = 0;
                db.Theatres.Add(theatre);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theatre);
        }

        public ActionResult Like(int id)
        {
            Theatre update = db.Theatres.ToList().Find(u => u.id == id);

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
            Theatre update = db.Theatres.ToList().Find(u => u.id == id);

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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
