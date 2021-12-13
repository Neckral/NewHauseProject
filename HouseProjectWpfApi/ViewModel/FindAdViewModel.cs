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

        #region COMMANDS
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

        private BaseCommand viewDetailOneFromAllAdInfo;
        public BaseCommand ViewDetailOneFromAllAdInfo
        {
            get
            {
                return viewDetailOneFromAllAdInfo ?? new BaseCommand(obj =>
                {
                    OpenAdInfoWindow(SelectedAdFromAll.Id);
                });
            }
        }

        private BaseCommand adToComparasionFromAll;
        public BaseCommand AdToComparasionFromAll
        {
            get
            {
                return adToComparasionFromAll ?? new BaseCommand(obj =>
                {
                    forCompareServices?.SetForCompare(UserProfile?.Id, SelectedAdFromAll.Id);
                });
            }
        }

        private BaseCommand adToFavoriteFromAll;
        public BaseCommand AdToFavoriteFromAll
        {
            get
            {
                return adToFavoriteFromAll ?? new BaseCommand(obj =>
                {
                    favoritesServices?.SetFavorite(UserProfile?.Id, SelectedAdFromAll.Id);
                });
            }
        }
        #endregion
    }
}
