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

        private BaseCommand refreshForCompares;
        public BaseCommand RefreshForCompares
        {
            get => refreshMyAds ?? new BaseCommand(obj =>
            {
                ForCompares = Task.Run(() => forCompareServices?.GetAllComparesByUserId(UserProfile.Id)).Result;
            });
        }
    }
}
