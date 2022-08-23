using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuildMeetOrganizer.Controller;
using GuildMeetOrganizer.Models;
using GuildMeetOrganizer.Views;

namespace GuildMeetOrganizer.ViewModels
{
    public partial class UsersViewModel : BasePageViewModel
    {
        [ObservableProperty]
        public List<User> _users;
        [ObservableProperty]
        public bool _userListIsRefreshing;
        [ObservableProperty]
        public bool _hasError;
        [ObservableProperty]
        public string _errorMessage;

        public UsersViewModel()
        {
            PageName = "Benutzer";
        }

        [RelayCommand]
        public async void LoadUsers()
        {
            HasError = false;
            UserListIsRefreshing = true;
            var response = await new UserDataController().GetUsersAsync();
            if (response == null || response.HasError)
            {
                HasError = true;
                ErrorMessage = "Fehler beim laden der Benutzer";
                Users = new List<User>();
            }
            else
            {
                Users = response.Response;
            }
            UserListIsRefreshing = false;
        }

        [RelayCommand]
        public async void AddUser()
        {
            await Shell.Current.GoToAsync(nameof(MangeUserPage));
        }

        [RelayCommand]
        public async void ShowUser(User selectedUser)
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "user", selectedUser }
            };
            await Shell.Current.GoToAsync(nameof(MangeUserPage), navigationParameter);
        }

        [RelayCommand]
        public async void DeleteUser(User selectedUser)
        {
            var response = await new UserDataController().DeleteUserAsync(selectedUser.IdUser);
            if (response == null || response.HasError || response.Response == false)
            {
                HasError = true;
                ErrorMessage = "Fehler beim löschen des Benutzers " + selectedUser.Username;
                return;
            }
            else
            {
                LoadUsers();        
            }
        }
    }
}
