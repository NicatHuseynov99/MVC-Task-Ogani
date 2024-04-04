using Ogani.Models;

namespace Ogani.ViewModels.Account
{
    public class UserVM
    {
        public AppUser User { get; set; }
        public List<String> Roles { get; set; }
    }
}
