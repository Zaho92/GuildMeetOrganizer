using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuildMeetOrganizer.Models;

namespace GuildMeetOrganizer.ViewModels
{
    public partial class ManageUserViewModel : BasePageViewModel, IQueryAttributable
    {
        [ObservableProperty]
        public bool _inEditMode;
        [ObservableProperty]
        public bool _isNewUser;
        [ObservableProperty]
        public User _thisUser;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            ThisUser = query["user"] as User;
            IsNewUser = ThisUser == null;
            InEditMode = ThisUser == null;
            ThisUser ??= new User();
            PageName = SetPageName();
        }

        private string SetPageName()
        {
            if (IsNewUser) return "Neuer Benutzer";
            return "Benutzer " + ThisUser.Username + (InEditMode ? " bearbeiten" : "");
        }

        [RelayCommand]
        public void ActivateEditMode()
        {
            InEditMode = true;
            PageName = SetPageName();
        }

        [RelayCommand]
        public void DeactivateEditMode()
        {
            InEditMode = false;
            PageName = SetPageName();
        }

        [RelayCommand]
        public void SaveUser()
        {

        }
    }
}
