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
        private string UserId = "ebce85b5-4bf4-4e17-9036-cab188083bf1";
        public UserProfileDTO? UserProfile
        {
            get { return userProfile; }
            set
            {
                userProfile = value;
                NotifyPropertyChanged(nameof(UserProfile));
            }
        }

        private BaseCommand? getUserProfile;
        public BaseCommand GetUserProfile
        {
            get
            {
                return getUserProfile ?? new BaseCommand(obj =>
                {
                    UserProfile = Task.Run(() => userServices?.GetUserProfileByEmail("user@gmail.com")).Result;
                }
                );
            }
        }
    }
}
