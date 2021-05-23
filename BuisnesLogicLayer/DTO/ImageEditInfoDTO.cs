using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogicLayer.DTO
{
    public class ImageEditInfoDTO
    {
        public int ID { get; set; }
        public int AdID { get; set; }
        public byte[] ImageFile { get; set; }
    }
}
