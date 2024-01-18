using SpicyX.Models;
using System.ComponentModel.DataAnnotations;

namespace Be.Areas.Admin.ViewModels
{
    public class CreateChefVM
    {
        [Required(ErrorMessage ="is required")]
        public string Name { get; set; }

        public IFormFile? Photo { get; set; }

        public string? FaceLink { get; set; }
        public string? TwitLink { get; set; }
        public string? LinkedLink { get; set; }
        public string? GoogleLink { get; set; }

        public int PositionId { get; set; }
        public List<Position>? Positions { get; set; }
    }
}
