using ExploreSkopjeMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ExploreSkopjeMVC.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var model = new LikesViewModel();

            model.CoffeeBarLikes = db.CoffeeBars.OrderByDescending(o => o.likes_counter).Take(3).ToList();

            model.RestaurantLikes = db.Restaurants.OrderByDescending(o => o.likes_counter).Take(3).ToList();
            model.TheatreLikes = db.Theatres.OrderByDescending(o => o.likes_counter).Take(3).ToList();
            model.MonumentLikes = db.Monuments.OrderByDescending(o => o.likes_counter).Take(3).ToList();


            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}