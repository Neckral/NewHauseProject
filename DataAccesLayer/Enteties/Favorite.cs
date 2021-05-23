using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccesLayer.Enteties
{
    public class Favorite
    {
        public Ad ad { get; set; }
        public User user { get; set; }
       //[Required]
       //public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [Required]
        public int AdID { get; set; }
        public Favorite(string UserID, int AdsID)
        {
            this.UserID = UserID;
            this.AdID = AdsID;
        }

        public Favorite() { }
    }
}
