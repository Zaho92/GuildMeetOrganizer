using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildMeetOrganizer.ViewModels
{
    public partial class BasePageViewModel : ObservableObject
    {
        [ObservableProperty]
        public string _pageName;

        public BasePageViewModel()
        {
            PageName = "";
        }
    }
}
