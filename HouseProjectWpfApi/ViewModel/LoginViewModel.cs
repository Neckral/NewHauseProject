using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BuisnesLogicLayer.DTO;
using BuisnesLogicLayer.Interfaces;

namespace HouseProjectWpfApi.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private IUserServices? userServices;
        private Window mainWindow;

        private UserLogInDTO? userLogin;
        public UserLogInDTO UserLogin
        {
            get => userLogin;
            set
            {
                userLogin = value;
                NotifyPropertyChanged(nameof(UserLogin));
            }
        }

        public LoginViewModel(IUserServices? userServices, Window mainWindow)
        {
            this.userServices = userServices;
            this.mainWindow = mainWindow;
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
