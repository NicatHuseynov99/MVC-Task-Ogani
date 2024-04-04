using System.ComponentModel.DataAnnotations;

namespace Ogani.ViewModels.Account
{
    public class LoginVM
    {
        [Required, MaxLength(255), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(255), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
