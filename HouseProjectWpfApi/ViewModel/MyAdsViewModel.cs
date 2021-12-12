using BuisnesLogicLayer.DTO;
using HouseProjectWpfApi.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HouseProjectWpfApi.ViewModel
{
    public partial class MainViewModel
    {
        public AdShortInfoDTO SelectedMyAd { get; set; }

        private IEnumerable<AdShortInfoDTO>? myAds;

        public IEnumerable<AdShortInfoDTO>? MyAds
        {
            get => myAds;
            set
            {
                myAds = value;
                NotifyPropertyChanged(nameof(MyAds));
            }
        }

        private void UpdateMyAds() => MyAds = Task.Run(() => adServices?.GetAdsByUserId(UserProfile?.Id)).Result;

        #region COMMANDS
        private BaseCommand? refreshMyAds;
        public BaseCommand RefreshMyAds
        {
            get => refreshMyAds ?? new BaseCommand(obj => 
            {
                UpdateMyAds();
            });
        }

        private BaseCommand? deleteMyAd;
        public BaseCommand DeleteMyAd
        {
            get
            {
                return deleteMyAd ?? new BaseCommand(obj =>
                {
                    try
                    {
                        adServices?.DeleteAdById(SelectedMyAd.Id);
                        MessageBox.Show("Add Was Deleted, Refresh To View Result!");
                        // Task.Delay(1000).Wait();
                    }
                    catch
                    {
                        MessageBox.Show("This Ad Cannot be Deleted");
                    }
                    finally
                    {
                        // UpdateMyAds();
                    }
                });
            }
        }

        private BaseCommand adToFavorite;
        public BaseCommand AdToFavorite
        {
            get
            {
                return adToFavorite ?? new BaseCommand(obj =>
                {
                    favoritesServices?.SetFavorite(UserProfile?.Id, SelectedMyAd.Id);
                });
            }
        }

        private BaseCommand adToComparasion;
        public BaseCommand AdToComparasion
        {
            get 
            { 
                return adToComparasion ?? new BaseCommand(obj => 
                { 
                    forCompareServices?.SetForCompare(UserProfile?.Id, SelectedMyAd.Id);
                }); 
            }
        }
        #endregion
    }
}
