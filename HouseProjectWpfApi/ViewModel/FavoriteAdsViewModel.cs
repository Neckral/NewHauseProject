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

        public AdShortInfoDTO SelectedFavorite { get; set; }

        private void UpdateFavoriteAds() => FavoritesAds = Task.Run(() => favoritesServices?.GetAllFavoritesByUserId(UserProfile?.Id)).Result;

        private BaseCommand refreshFavorites;
        public BaseCommand RefreshFavorites
        {
            get => refreshMyAds ?? new BaseCommand(obj => 
            {
                UpdateFavoriteAds();
            });
        }

        private BaseCommand removeFromFavorites;
        public BaseCommand RemoveFromFavorites
        {
            get
            {
                return removeFromFavorites ?? new BaseCommand(obj =>
                {
                    favoritesServices?.RemoveFavoriteByUserIdAndAdId(UserProfile?.Id, SelectedFavorite.Id);
                });
            }
        }

        private BaseCommand viewDetailFavAdInfo;
        public BaseCommand ViewDetailFavAdInfo
        {
            get
            {
                return viewDetailFavAdInfo ?? new BaseCommand(obj =>
                {
                    OpenAdInfoWindow(SelectedFavorite.Id);
                });
            }
        }

    }
}
