using GuildMeetOrganizer.Models;
using GuildMeetOrganizer.ViewModels;

namespace GuildMeetOrganizer.Views;

public partial class MangeUserPage : ContentPage
{
    public MangeUserPage(ManageUserViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

}