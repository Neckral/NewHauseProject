using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuisnesLogicLayer.DTO;
using HouseProjectWpfApi.Commands;

namespace HouseProjectWpfApi.ViewModel
{
    public partial class MainViewModel
    {
        private IEnumerable<AdShortInfoDTO> allAds;
        public IEnumerable<AdShortInfoDTO> AllAds
        {
            get => allAds;
            set
            {
                allAds = value;
                NotifyPropertyChanged(nameof(AllAds));
            }
        }

        public AdShortInfoDTO SelectedAdFromAll { get; set; }
        private BaseCommand getAllAds;
        public BaseCommand GetAllAds
        {
            get
            {
                return getAllAds ?? new BaseCommand(obj =>
                {
                    AllAds = Task.Run(() => adServices?.GetAllAds()).Result;
                });
            }
        }
    }
}
