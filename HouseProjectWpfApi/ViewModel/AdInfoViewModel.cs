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
    internal class AdInfoViewModel : INotifyPropertyChanged
    {
        private IAdServices? adServices;
        private AdInfoDTO? adInfoDTO = new();
        public AdInfoDTO AdInfoDTO
        {
            get => adInfoDTO;
            set
            {
                adInfoDTO = value;
                NotifyPropertyChanged(nameof(AdInfoDTO));
            }
        }

        public AdInfoViewModel(IAdServices? adServices, int adId)
        {
            this.adServices = adServices;
            AdInfoDTO = Task.Run(() => adServices?.GetAdById(adId)).Result;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private BaseCommand closeWindow;
        public BaseCommand CloseWindow
        {
            get
            {
                return closeWindow ?? new BaseCommand(obj =>
                {
                    Window? window = obj as Window;
                    window?.Close();
                });
            }
        }
    }
}
