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
        public bool _isVisibleForAdmin;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(UserGreetingString))]
        public string _loggedInUsername;

        public string UserGreetingString => GetRandomGreeting() + LoggedInUsername;

        public AppShellViewModel()
        {
            IsVisibleForAdmin = false;
            GlobalVariables.LoggedInUserChanged += GlobalVariables_LoggedInUserChanged;
        }
        
        private void GlobalVariables_LoggedInUserChanged()
        {
            IsVisibleForAdmin = false;
            LoggedInUsername = GlobalVariables.LoggedInUser?.Username;
            RightsTemplate rights = GlobalVariables.LoggedInUser?.FkRightsTemplatesNavigation;
            if (rights!=null)
            {
                IsVisibleForAdmin = rights.IsAdmin;
            }
        }

        private string GetRandomGreeting()
        {
            var random = new Random();
            List<string> greetings = new List<string>()
            {
                "Moin",
                "Hallo",
                "Hi",
                "Tach",
                "Moinsen",
                "Hola",                
            };
            return greetings[random.Next(0, greetings.Count)] + " ";
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
