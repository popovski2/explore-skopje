using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExploreSkopjeMVC.Models
{
    public class Theatre
    {
        //ovie se  site od excel fajlot

        //od ko ke se naprajt bazata i sve ko ke e okej za drugata domasna da dodajme
        //REVIEW (int) i KOMENTARI List<String>
        
            //id-vo e long zs vo .csv fajlot e long (od tamu go zemame direktno)

            [Key]
        public long id { get; set; }

        public string name { get; set; }

        public double longitude { get; set; }

        public double latitude { get; set; }
    }
}