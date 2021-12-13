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
        private IEnumerable<ForCompareDTO>? forCompares;
        public IEnumerable<ForCompareDTO> ForCompares
        {
            get=> forCompares;
            set
            {
                forCompares = value;
                NotifyPropertyChanged(nameof(ForCompares));
            }
        }

        public ForCompareDTO SelectedForCompare { get; set; }

        private void UpdateForCompares() => ForCompares = Task.Run(() => forCompareServices?.GetAllComparesByUserId(UserProfile.Id)).Result;

        private BaseCommand refreshForCompares;
        public BaseCommand RefreshForCompares
        {
            get => refreshMyAds ?? new BaseCommand(obj =>
            {
                UpdateForCompares();
            });
        }

        private BaseCommand removeFromComparasion;
        public BaseCommand RemoveFromComparasion
        {
            get
            {
                return removeFromComparasion ?? new BaseCommand(obj =>
                {
                    forCompareServices?.RemoveCopareByUserIdAndAdId(UserProfile?.Id, SelectedForCompare.Id);
                });
            }
        }

        private BaseCommand viewDetailCompareAdInfo;
        public BaseCommand ViewDetailCompareAdInfo
        {
            get
            {
                return viewDetailCompareAdInfo ?? new BaseCommand(obj =>
                {
                    OpenAdInfoWindow(SelectedForCompare.Id);
                });
            }
        }
    }
}
