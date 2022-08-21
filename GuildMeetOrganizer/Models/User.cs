using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.Models
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phonenumber { get; set; }

        public string FullDisplayName => $"{Firstname} {Lastname}";

        public virtual RightsTemplate FkRightsTemplatesNavigation { get; set; }
    }
}
