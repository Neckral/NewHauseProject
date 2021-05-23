using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DataAccesLayer.Enteties
{
    public class Image
    {
        
        ////////////////////////////////////////////////
        public Ad ad { get; set; }
        ////////////////////////////////////////////////
        public int ID { get; set; }
        public int AdID { get; set; }
        public byte[] ImageFile { get; set; }
        public Image(int AdsID, byte[] ImageFile)
        {
            this.AdID = AdsID;
            this.ImageFile = ImageFile;
        }
        public Image() { }
    }
}
