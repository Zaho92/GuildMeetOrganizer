using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuildMeetOrganizer.Helpers;
using GuildMeetOrganizer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.ViewModels
{
    public partial class AppShellViewModel:ObservableObject
    {
        [RelayCommand]
        public async void LogoutUser()
        {
            GlobalVariables.LoggedInUser = null;
            Shell.Current.FlyoutIsPresented = false;
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }
}
