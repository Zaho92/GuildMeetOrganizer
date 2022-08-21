using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.Helpers
{
    internal static class ConnectivityChecks
    {
        public static bool IsInternetConnected => Connectivity.Current.NetworkAccess == NetworkAccess.Internet;

        public static bool CheckIsInternetConnectedAndWarn()
        {
            if (IsInternetConnected) return true;
            AppShell.Current.DisplayAlert("Kein Netz...", "Alter, gib mit Internet zurück!\nICH WILL NICHT, DASS ES LÄDT!!!", "Ja ja...");
            return false;
        }
    
    }
}
