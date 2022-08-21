using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.Models
{
    public partial class RightsTemplate
    {
        public int IdRightsTemplate { get; set; }
        public string Description { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsInitialUser { get; set; }
    }
}
