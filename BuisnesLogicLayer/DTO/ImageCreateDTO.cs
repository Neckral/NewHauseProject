using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogicLayer.DTO
{
    public class ImageCreateDTO
    {
        public byte[] ImageFile { get; set; }

        public ImageCreateDTO(byte[] img) => ImageFile = img;
        public ImageCreateDTO() { }
    }
}
