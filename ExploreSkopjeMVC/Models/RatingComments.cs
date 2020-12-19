using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ExploreSkopjeMVC.Models
{
    public class RatingComments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentId { get; set; }

        public string Comments { get; set; }

        public DateTime? ThisDateTime { get; set; }
        //id na objektot zavisno od klasata {CB,R,T,M}
        public int ObjectId { get; set; }

        public int? Rating { get; set; }

        public String Type { get; set; }

    }
}