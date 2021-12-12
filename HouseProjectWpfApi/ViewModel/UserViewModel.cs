using BuisnesLogicLayer.DTO;
using HouseProjectWpfApi.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseProjectWpfApi.ViewModel
{
    public partial class MainViewModel
    {
        private UserProfileDTO? userProfile = new()
        {
            Name = "Jony",
            Surname = "Smith",
            PhoneNumber = "0441122656",
            Email = "jsmith@gmail.com",
            AdsAmount = 4,
            ComentsAmount = 5
        };

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
