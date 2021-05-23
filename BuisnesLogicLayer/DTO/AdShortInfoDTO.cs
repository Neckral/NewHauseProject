using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnesLogicLayer.DTO
{
    public class AdShortInfoDTO
    {
        // not displayed
        public int Id { get; set; } // id of add
        public string OwnerId { get; set; } // owner of this favorite
        // displayed
        public bool PurchaseOportunity { get; set; }
        public bool Balkon { get; set; }
        public int Price { get; set; }
        public string HouseType { get; set; }
        public int AreaOfHouse { get; set; }
        public int RoomNumber { get; set; }
        public int HouseYear { get; set; }
        public bool Status { get; set; }
        public IEnumerable<ImageEditInfoDTO> images { get; set; }
    }
}
