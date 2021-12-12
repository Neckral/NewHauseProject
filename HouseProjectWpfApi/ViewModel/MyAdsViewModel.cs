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
        private IEnumerable<AdShortInfoDTO>? myAds;

        public IEnumerable<AdShortInfoDTO> MyAds
        {
            get => myAds;
            set
            {
                myAds = value;
                NotifyPropertyChanged(nameof(MyAds));
            }
        }
    }
}
