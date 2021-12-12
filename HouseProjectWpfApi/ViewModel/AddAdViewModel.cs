using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BuisnesLogicLayer.DTO;
using BuisnesLogicLayer.Interfaces;
using HouseProjectWpfApi.Commands;

namespace HouseProjectWpfApi.ViewModel
{
    public class AddAdViewModel : INotifyPropertyChanged
    {
        private IAdServices? adServices;
        private AdCreateDTO? adCreateDTO = new();
        public AdCreateDTO? AdCreateDTO
        {
            get => adCreateDTO;
            set
            {
                adCreateDTO = value;
                NotifyPropertyChanged(nameof(AdCreateDTO));
            }
        }

        public AddAdViewModel(IAdServices? adServices, string? userId)
        {
            this.adServices = adServices;
            AdCreateDTO.OwnerId = userId;
        }

        private BaseCommand addAd;
        public BaseCommand AddAd
        {
            get
            {
                return addAd ?? new BaseCommand(obj =>
                {
                    try
                    {
                        Task.Run(() => adServices?.AddNewAd(AdCreateDTO));
                        MessageBox.Show("Ad succesfuly published");
                    }
                    catch
                    {
                        MessageBox.Show("Some problems occured, check the fields and try again.");
                    }
                });
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
