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

        public UsersViewModel()
        {
            PageName = "Benutzer";
            LoadUsers();
        }

        [RelayCommand]
        public void LoadUsers()
        {
            UserListIsRefreshing = true;
            UserDataController userData = new UserDataController();
            var response = userData.GetUsers();
            if (!response.HasError)
            {
                Users = response.ResponseObject;
            }
            else
            {
                // Error werden noch nicht angezeigt
                Users = new List<User>();
            }
            UserListIsRefreshing = false;
        }

        [RelayCommand]
        public void AddUser()
        {
            //Navigate to Add Page
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
        public void DeleteUser(User selectedUser)
        {
            //Delete User
            LoadUsers();        
        }
    }
}
