using Microsoft.AspNetCore.Identity;
namespace Ogani.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
