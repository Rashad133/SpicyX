using System.ComponentModel.DataAnnotations;

namespace SpicyX.Areas.Admin.ViewModels.Settings
{
    public class CreateSettingsVM
    {
        [Required(ErrorMessage ="is required")]
        public string Key { get; set; }
        [Required(ErrorMessage ="is required")]
        public string Value { get; set; }
    }
}
