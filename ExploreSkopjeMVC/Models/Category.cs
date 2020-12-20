using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExploreSkopjeMVC.Models
{
    public class Category
    {

        //koi kategorii gi imame
        // bars, museums, monuments, restaurants
        public int id { get; set; }

        //ovde da stam enum najubo
        //ili pak ne
        //gi naprajv sekoj poseben model
        public List<String> kategorii { get; set; }
      
    }
}