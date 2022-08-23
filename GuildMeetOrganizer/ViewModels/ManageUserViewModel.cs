using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuildMeetOrganizer.Controller;
using GuildMeetOrganizer.Helpers;
using GuildMeetOrganizer.Models;
using GuildMeetOrganizer.Models.ApiHelper;

namespace GuildMeetOrganizer.ViewModels
{
    public partial class ManageUserViewModel : BasePageViewModel, IQueryAttributable
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanTemplateBeChanged))]
        public bool _inEditMode;
        [ObservableProperty]
        public bool _isNewUser;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SelectedTemplate))]
        public User _thisUser;
        [ObservableProperty]
        public List<RightsTemplate> _rightsTemplates;
        [ObservableProperty]
        public string _checkPassword;

        public bool CanTemplateBeChanged => InEditMode && (GlobalVariables.LoggedInUser?.FkRightsTemplatesNavigation?.IsAdmin ?? false);
        public RightsTemplate SelectedTemplate
        {
            get
            {
                return ThisUser?.FkRightsTemplatesNavigation;
            }
            set
            {
                if (ThisUser == null) return;
                if (value == null) return; 
                if (ThisUser.FkRightsTemplatesNavigation == null || !ThisUser.FkRightsTemplatesNavigation.Equals((value as RightsTemplate)))
                {
                    ThisUser.FkRightsTemplatesNavigation = value;
                    OnPropertyChanged(nameof(SelectedTemplate));
                }
            }
        }
        private User BackupUser { get; set; }

        public ManageUserViewModel()
        {
            GetRightsTemplates();
        }

        private async void GetRightsTemplates()
        {
            var response = await new RightsTemplateDataController().GetRightsTemplatesAsync();
            if (response != null && !response.HasError)
            {
                var t = ThisUser.FkRightsTemplatesNavigation;
                RightsTemplates = response.Response;
                if (IsNewUser)
                {
                    SelectedTemplate = RightsTemplates.FirstOrDefault(rt => rt.IsInitialUser);
                }
                else
                {
                    SelectedTemplate = RightsTemplates.FirstOrDefault(rt => rt.IdRightsTemplate == ThisUser.FkRightsTemplatesNavigation.IdRightsTemplate);
                }
            }
            else
            {
                RightsTemplates = new List<RightsTemplate>();
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            ThisUser = (query == null || query.Count > 0) ? (query["user"] as User).GetCopy() : null;
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
            BackupUser = ThisUser.GetCopy();
            PageName = SetPageName();
        }

        [RelayCommand]
        public async void DeactivateEditMode()
        {
            if (IsNewUser)
            {
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                InEditMode = false;
                ThisUser = BackupUser?.GetCopy();
                SelectedTemplate = RightsTemplates.FirstOrDefault(rt => rt.IdRightsTemplate == ThisUser.FkRightsTemplatesNavigation.IdRightsTemplate);
                BackupUser = null;
                PageName = SetPageName();
            }
        }

        [RelayCommand]
        public async void SaveUser()
        {
            InEditMode = false;
            ApiResponseObject<User> response = null;
            if (IsNewUser)
            {
                if (ThisUser.Password != CheckPassword)
                {
                    //Fehler Passort stimmt nicht
                    InEditMode = true;
                    return;
                }
                response = await new UserDataController().AddUserAsync(ThisUser);
            }
            else
            {
                response = await new UserDataController().EditUserAsync(ThisUser);
            }
            if (response != null && !response.HasError)
            {
                await Shell.Current.Navigation.PopAsync();
            }
            else
            {
                // Fehler beim Speichern
                InEditMode = true;
            }
        }
    }
}
