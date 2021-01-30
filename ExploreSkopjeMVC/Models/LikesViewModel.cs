using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExploreSkopjeMVC.Models
{
    public class LikesViewModel
    {
        [Key]
        public long id { get; set; }
        public List<CoffeeBar> CoffeeBarLikes { get; set; }
        public List<Restaurant> RestaurantLikes { get; set; }
        public List<Theatre> TheatreLikes { get; set; }
        public List<Monument> MonumentLikes { get; set; }
    }
}