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
    public class LoginViewModel : INotifyPropertyChanged
    {
        protected IUserServices? userServices;
        protected IAdServices? adServices;
        protected IFavoritesServices? favoritesServices;
        protected IForCompareServices? forCompareServices;
        protected ICommentServices? commentServices;
        protected IImageServices? imageServices;

        private Visibility logInFormState = Visibility.Visible;
        public Visibility LogInFormState
        {
            get => logInFormState;
            set
            {
                logInFormState = value;
                NotifyPropertyChanged(nameof(LogInFormState));
            }
        }
        private Visibility registrationFormState = Visibility.Collapsed;
        public Visibility RegistrationFormState
        {
            get => registrationFormState;
            set
            {
                registrationFormState = value;
                NotifyPropertyChanged(nameof(RegistrationFormState));
            }
        }

        private UserLogInDTO? userLogin = new UserLogInDTO();
        public UserLogInDTO UserLogin
        {
            get => userLogin;
            set
            {
                userLogin = value;
                NotifyPropertyChanged(nameof(UserLogin));
            }
        }

        private UserRegisterDTO? userRegister = new UserRegisterDTO();
        public UserRegisterDTO UserRegister
        {
            get => userRegister;
            set
            {
                userRegister = value;
                NotifyPropertyChanged(nameof(UserRegister));
            }
        }

        public LoginViewModel(IUserServices? userServices, IAdServices? adServices, IFavoritesServices? favoritesServices,
            IForCompareServices? forCompareServices, ICommentServices? commentServices, IImageServices? imageServices)
        {
            this.userServices = userServices;
            this.adServices = adServices;
            this.favoritesServices = favoritesServices;
            this.forCompareServices = forCompareServices;
            this.commentServices = commentServices;
            this.imageServices = imageServices;
        }

        #region AUTHORIZATION COMMANDS
        private BaseCommand? login;
        public BaseCommand Login
        {
            get
            {
                return login ?? new BaseCommand(async obj =>
                {
                    var result = await userServices?.LogIn(userLogin);
                    if (result == true)
                    {

                        Window window = new MainWindow();
                        window.DataContext = new MainViewModel(userServices, adServices, favoritesServices, forCompareServices, commentServices, imageServices, UserLogin.Email);
                        window.Show();
                        Window? wnd = obj as Window;
                        wnd?.Close();
                    }
                });
            }
        }

        private BaseCommand register;
        public BaseCommand Register
        {
            get
            {
                return register ?? new BaseCommand(async obj => {
                   var result = await userServices.RegisterUser(UserRegister);
                    if (result == true)
                        MessageBox.Show("Registration Succesfuly Completed.");
                    else
                        MessageBox.Show("Some problems happened, Check all fields and try again.");
                });
            }
        }
        #endregion

        #region NAVIGATION COMMANDS
        private BaseCommand? goToRegistration;
        public BaseCommand GoToRegistration
        {
            get
            {
                return goToRegistration ?? new BaseCommand(obj =>
                {
                    UserLogin = new();
                    LogInFormState = Visibility.Collapsed;
                    RegistrationFormState = Visibility.Visible;
                });
            }
        }

        private BaseCommand? goToLogin;
        public BaseCommand GoToLogin
        {
            get
            {
                return goToRegistration ?? new BaseCommand(obj =>
                {
                    UserRegister = new();
                    RegistrationFormState = Visibility.Collapsed;
                    LogInFormState = Visibility.Visible;
                });
            }
        }
        #endregion

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
