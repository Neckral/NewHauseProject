using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DataAccesLayer.Enteties
{
    public class Comment
    {
        ////////////////////////////////////////////////
        public Ad ad { get; set; }
        public User user { get; set; }
        ////////////////////////////////////////////////

        public int ID { get; set; }
        public string UserID { get; set; }
        public int AdId { get; set; }
        public DateTime DateOfComment { get; set; }
        public string Text { get; set; }

        public Comment(string UserId, int AdId, DateTime DateOfComment, string Text)
        {
            this.UserID = UserId;
            this.AdId = AdId;
            this.DateOfComment = DateOfComment;
            this.Text = Text;
        }

        public Comment() { }
    }
}
