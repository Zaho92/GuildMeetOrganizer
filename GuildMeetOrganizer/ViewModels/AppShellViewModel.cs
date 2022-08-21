using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuildMeetOrganizer.Helpers;
using GuildMeetOrganizer.Models;
using GuildMeetOrganizer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        [ObservableProperty]
        public bool isVisibleForAdmin;

        public AppShellViewModel()
        {
            IsVisibleForAdmin = false;
            GlobalVariables.LoggedInUserChanged += GlobalVariables_LoggedInUserChanged;
        }

        private void GlobalVariables_LoggedInUserChanged()
        {
            IsVisibleForAdmin = false;
            RightsTemplate rights = GlobalVariables.LoggedInUser?.FkRightsTemplatesNavigation;
            if (rights!=null)
            {
                IsVisibleForAdmin = rights.IsAdmin;
            }
        }

        [RelayCommand]
        public async void LogoutUser()
        {
            GlobalVariables.LoggedInUser = null;
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
