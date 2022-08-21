using GuildMeetOrganizer.ViewModels;
using GuildMeetOrganizer.Views;

namespace GuildMeetOrganizer;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		BindingContext = new AppShellViewModel();

		Routing.RegisterRoute(nameof(LoginPage),typeof(LoginPage));
        Routing.RegisterRoute(nameof(UsersPage),typeof(UsersPage));
    }
}
