using System.ComponentModel.DataAnnotations;

namespace Ogani.ViewModels.Account
{
    public class ChangePasswordVM
    {
        [Required, MaxLength(16), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(16), DataType(DataType.Password), Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
        public string Id {  get; set; }
    }
}
