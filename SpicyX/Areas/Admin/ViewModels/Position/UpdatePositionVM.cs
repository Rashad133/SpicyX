using System.ComponentModel.DataAnnotations;

namespace SpicyX.Areas.Admin.ViewModels.Position
{
    public class UpdatePositionVM
    {
        [Required]
        public string Name { get; set; }
    }
}
