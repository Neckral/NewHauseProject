using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DataAccesLayer.Enteties
{
    [Display]
    public class User : IdentityUser
    {
        public IEnumerable<Ad> ads { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<Favorite> favorites { get; set; }
        public IEnumerable<ForCompare> forCompares { get; set; }
        //=============================================================//

        public string Name { get; set; }

        public string Surname { get; set; }

        public User() { }
    }
}
