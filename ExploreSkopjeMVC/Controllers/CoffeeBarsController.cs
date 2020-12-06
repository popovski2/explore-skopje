﻿using System;
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

        // GET: CoffeeBars
        public ActionResult Index()
        {
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
                db.CoffeeBars.Add(coffeeBar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(coffeeBar);
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