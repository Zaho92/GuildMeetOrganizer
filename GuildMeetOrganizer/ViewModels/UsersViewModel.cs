﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuildMeetOrganizer.Controller;
using GuildMeetOrganizer.Models;

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
            UserDataController dc = new UserDataController();
            var response = dc.GetUsers();
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
    }
}