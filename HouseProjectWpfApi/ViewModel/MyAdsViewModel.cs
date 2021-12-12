using BuisnesLogicLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProjectWpfApi.ViewModel
{
    public partial class MainViewModel
    {
        private List<AdInfoDTO> myAds = new()
        {
            new AdInfoDTO()
            {
                Price = 200000,
                Region = "Chernivtsy",
                District = "Chernivtsy",
                City = "Chernivtsy",
                Street = "Maidan",
                HouseNumber = "47B",
                HouseType = "Дім",
                AreaOfHouse = 344,
                FloorAmount = 2,
                RoomNumber = 5,
                HouseYear = 2014,
                Pool = false,
                Balkon = true,
                PurchaseOportunity = true,
                Status = true,
                Description = "It is a house from Chernivtsy"
            },
            new AdInfoDTO()
            {
                Price = 200000,
                Region = "Chernivtsy",
                District = "Chernivtsy",
                City = "Chernivtsy",
                Street = "Maidan",
                HouseNumber = "47B",
                HouseType = "Дім",
                AreaOfHouse = 344,
                FloorAmount = 2,
                RoomNumber = 5,
                HouseYear = 2014,
                Pool = false,
                Balkon = true,
                PurchaseOportunity = true,
                Status = true,
                Description = "It is a house from Chernivtsy"
            },
            new AdInfoDTO()
            {
                Price = 200000,
                Region = "Chernivtsy",
                District = "Chernivtsy",
                City = "Chernivtsy",
                Street = "Maidan",
                HouseNumber = "47B",
                HouseType = "Дім",
                AreaOfHouse = 344,
                FloorAmount = 2,
                RoomNumber = 5,
                HouseYear = 2014,
                Pool = false,
                Balkon = true,
                PurchaseOportunity = true,
                Status = true,
                Description = "It is a house from Chernivtsy"
            }
        };

        public List<AdInfoDTO> MyAds
        {
            get { return myAds; }
            set
            {
                myAds = value;
                NotifyPropertyChanged(nameof(MyAds));
            }
        }
    }
}
