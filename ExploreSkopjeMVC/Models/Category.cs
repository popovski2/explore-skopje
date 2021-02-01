using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExploreSkopjeMVC.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        public List<String> kategorii { get; set; }
    }
}