using GuildMeetOrganizer.ViewModels;
using GuildMeetOrganizer.Views;

namespace GuildMeetOrganizer;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("Roboto-Medium.ttf", "RobotoMedium");
				fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
                fonts.AddFont("Roboto-Thin.ttf", "RobotoThin");
            });

		builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        builder.Services.AddSingleton<UsersViewModel>();
		builder.Services.AddSingleton<UsersPage>();

        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoginPage>();

		var app = builder.Build();

		app.Services.GetService<UsersViewModel>();

        return builder.Build();
	}
}
