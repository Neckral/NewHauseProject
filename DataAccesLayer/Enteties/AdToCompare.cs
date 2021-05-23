using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Enteties
{
    public class AdToCompare
    {
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }
        public string Region { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string HouseType { get; set; }
        public int MinAreaOfHouse { get; set; }
        public int MaxAreaOfHouse { get; set; }
        public int MinFloorAmount { get; set; }
        public int MaxFloorAmount { get; set; }
        public int MinRoomNumber { get; set; }
        public int MaxRoomNumber { get; set; }
        public int MinHouseYear { get; set; }
        public int MaxHouseYear { get; set; }
        public bool Pool { get; set; }
        public bool Balkon { get; set; }
        public bool PurchaseOportunity { get; set; }
        public bool Status { get; set; }

        public bool HouseTypeNoMatter { get; set; }
        public bool PoolNoMatter { get; set; }
        public bool BalkonNoMatter { get; set; }
        public bool PurchaseOportunityNoMatter { get; set; }
        public bool StatusNoMatter { get; set; }
    }
}
