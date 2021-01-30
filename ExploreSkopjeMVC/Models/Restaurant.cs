using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExploreSkopjeMVC.Models
{
    public class Restaurant
    {
        [Key]
        public long id { get; set; }

        public string name { get; set; }

        public double longitude { get; set; }

        public double latitude { get; set; }

        public decimal TotalRating { get; set; }

        public int likes_counter { get; set; }

        public string picture_URL { get; set; }

        public string facebook_link { get; set; }

    }
}