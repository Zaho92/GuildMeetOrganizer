
namespace GuildMeetOrganizer.Models
{
    public class LoginResult
    {
        public User User { get; set; }
        public string JwtToken { get; set; }
    }
}
