﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ExploreSkopjeMVC.Models
{
    public class Trending
    {

        //ovde ce gi cuvame TRENDING mestata spored presmetani review-a (oceni)
        //vo kontroler ke barame koj ima max review i ke gi sortirame po opagjacki redosled

            [Key]
        public long id { get; set; }

        public List<Restaurant> restorani { get; set; }

        //ne znam kolku imat smisla za monumenti
        public List<Monument> monumenti { get; set; }
        public List<Theatre> teatri { get; set; }
        public List<CoffeeBar> kafici { get; set; }

        public List<Restaurant> trendingRestaurants { get; set; }

        public List<Theatre> trendingTheaters { get; set; }

        public List<CoffeeBar> trendingCoffeeBars { get; set; }


    }
}