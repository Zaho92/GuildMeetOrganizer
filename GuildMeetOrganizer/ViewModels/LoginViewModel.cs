using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuildMeetOrganizer.Controller;
using GuildMeetOrganizer.Helpers;
using GuildMeetOrganizer.Models;
using GuildMeetOrganizer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.ViewModels
{
    public partial class LoginViewModel : BasePageViewModel
    {
        [ObservableProperty]
        public string _username;
        [ObservableProperty]
        public string _password;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        public string _errorMessage;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsControlsEnabled))]
        public bool _isLoading;

        public bool HasError => !String.IsNullOrEmpty(ErrorMessage);
        public bool IsControlsEnabled => !IsLoading;

        public LoginViewModel()
        {
            PageName = "Login";
        }

        [RelayCommand]
        public async void TryLogin()
        {
            IsLoading = true;
            ErrorMessage = "";
            if (String.IsNullOrWhiteSpace(Username) || String.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage += String.IsNullOrWhiteSpace(Username) ? "Benutzername" : "";
                ErrorMessage += String.IsNullOrWhiteSpace(Password) ? ((String.IsNullOrWhiteSpace(Username) ? " und " : "") + "Passwort") : "";
                ErrorMessage += (String.IsNullOrWhiteSpace(Password) && (String.IsNullOrWhiteSpace(Username)) ? " sind " : " ist ") + "leer.";
                IsLoading = false;
                return;
            }

            UserDataController userData = new UserDataController();
            var response = userData.LoginUser(Username, Password);
            if (response.HasError)
            {
                ErrorMessage += response.ErrorMessage;
                IsLoading = false;
                return;
            }
            else
            {
                GlobalVariables.LoggedInUser = response.ResponseObject;
            }

            await Shell.Current.GoToAsync($"//{nameof(UsersPage)}");
            IsLoading = false;
        }

        [RelayCommand]
        public async void NavigateLostPasswordPage()
        {
            IsLoading = true;
            await Shell.Current.GoToAsync($"//{nameof(UsersPage)}");
            IsLoading = false;
        }


        [RelayCommand]
        public async void NavigateRegisterUserPage()
        {
            IsLoading = true;
            await Shell.Current.GoToAsync($"//{nameof(UsersPage)}");
            IsLoading = false;
        }
    }
}
