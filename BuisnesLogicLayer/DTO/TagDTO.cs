using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogicLayer.DTO
{
    public class TagDTO
    {
        public string Tag_ { get; set; }

        public TagDTO(string tag) => Tag_= tag;
        public TagDTO() { }
    }
}
