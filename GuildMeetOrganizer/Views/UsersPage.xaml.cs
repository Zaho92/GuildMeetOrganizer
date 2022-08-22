using GuildMeetOrganizer.ViewModels;
using CommunityToolkit.Maui;

namespace GuildMeetOrganizer.Views;
public partial class UsersPage : ContentPage
{
	public UsersPage(UsersViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}