using GuildMeetOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.Helpers
{
    internal static class GlobalVariables
    {
        public delegate void LoggedInUserChangedHandler();
        public static event LoggedInUserChangedHandler LoggedInUserChanged;
        private static User _loggedInUser;
        public static User LoggedInUser {
            get
            {
                return _loggedInUser;
            }
            set
            {
                if (_loggedInUser != value)
                {
                    _loggedInUser = value;
                    LoggedInUserChanged?.Invoke();
                }
            }
        }
        public static string ApiToken { get; set; }
    }
}
