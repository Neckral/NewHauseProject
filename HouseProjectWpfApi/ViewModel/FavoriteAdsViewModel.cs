using BuisnesLogicLayer.DTO;
using HouseProjectWpfApi.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProjectWpfApi.ViewModel
{
    partial class MainViewModel
    {
        private IEnumerable<AdShortInfoDTO>? favoritesAds;
        public IEnumerable<AdShortInfoDTO> FavoritesAds
        {
            get => favoritesAds;
            set
            {
                favoritesAds = value;
                NotifyPropertyChanged(nameof(FavoritesAds));
            }
        }


        private BaseCommand refreshFavorites;
        public BaseCommand RefreshFavorites
        {
            get => refreshMyAds ?? new BaseCommand(obj => 
            { 
                FavoritesAds = Task.Run(() => favoritesServices?.GetAllFavoritesByUserId(UserProfile.Id)).Result; 
            });
        }
    }
}
