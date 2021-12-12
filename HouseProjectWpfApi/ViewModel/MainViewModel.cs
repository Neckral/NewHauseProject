using BuisnesLogicLayer.DTO;
using BuisnesLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProjectWpfApi.ViewModel
{
    // user logic
    public partial class MainViewModel : INotifyPropertyChanged
    {
        protected IUserServices? userServices;
        protected IAdServices? adServices;
        protected IFavoritesServices? favoritesServices;
        protected IForCompareServices? forCompareServices;
        protected ICommentServices? commentServices;
        protected IImageServices? imageServices;

        public MainViewModel(IUserServices? userServices, IAdServices? adServices, IFavoritesServices? favoritesServices, 
            IForCompareServices? forCompareServices, ICommentServices? commentServices, IImageServices? imageServices)
        {
            this.userServices = userServices;
            this.adServices = adServices;
            this.favoritesServices = favoritesServices;
            this.forCompareServices = forCompareServices;
            this.commentServices = commentServices;
            this.imageServices = imageServices;
            UpdateData();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void UpdateData()
        {
            MyAds = Task.Run(() => adServices?.GetAdsByUserId(UserId)).Result;
            FavoritesAds = Task.Run(() => favoritesServices?.GetAllFavoritesByUserId(UserId)).Result;
            ForCompares = Task.Run(() => forCompareServices?.GetAllComparesByUserId(UserId)).Result;
        }
    }
}
