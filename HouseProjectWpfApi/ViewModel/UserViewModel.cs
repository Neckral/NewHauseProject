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
        private UserProfileDTO? userProfile = null;
        public UserProfileDTO? UserProfile
        {
            get { return userProfile; }
            set
            {
                userProfile = value;
                NotifyPropertyChanged(nameof(UserProfile));
            }
        }
    }
}
