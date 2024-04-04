using System.ComponentModel.DataAnnotations;

namespace Ogani.ViewModels.Account
{
    public class ForgotPasswordVM
    {
        [Required, MaxLength(255), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
