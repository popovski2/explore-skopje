using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExploreSkopjeMVC.Models
{
    public class Monument
    {

        //ovie se  site od excel fajlot

        //od ko ke se naprajt bazata i sve ko ke e okej za drugata domasna da dodajme
        //REVIEW (int) i KOMENTARI List<String>

        //ovde mozda ne review -> da vidime koi ni se monumenti dan nemat smisla da stavame review ili komentar

        //id-vo e long zs vo .csv fajlot e long (od tamu go zemame direktno)

         
        [Key]
        public long id { get; set; }

        public string name { get; set; }

        public double longitude { get; set; }

        public double latitude { get; set; }

        public int likes_counter { get; set; }


    }
}