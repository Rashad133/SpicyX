using Microsoft.Build.Framework;

namespace SpicyX.Areas.Admin.ViewModels.Position
{
    public class CreatePositionVM
    {
        [Required]
        public string Name { get; set; }
    }
}
