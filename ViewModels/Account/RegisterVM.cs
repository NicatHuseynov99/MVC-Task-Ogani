using System.ComponentModel.DataAnnotations;
namespace Ogani.ViewModels.Account
{
    public class RegisterVM
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required, MaxLength(20)]
        public string Username { get; set; }
        [Required, MaxLength(255), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, MaxLength(16), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(16), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
