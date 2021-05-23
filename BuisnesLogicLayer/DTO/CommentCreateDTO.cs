using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogicLayer.DTO
{
    public class CommentCreateDTO
    {
        public string UserID { get; set; }
        public int AdId { get; set; }
        public DateTime DateOfComment { get; set; }
        public string Text { get; set; }
    }
}
