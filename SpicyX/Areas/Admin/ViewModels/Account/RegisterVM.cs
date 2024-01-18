using System.ComponentModel.DataAnnotations;

namespace SpicyX.Areas.Admin.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [MaxLength(30)]
        [MinLength(3)]
        public string Surname { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="pls enter correct email")]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="is required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "is required")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
