
using SpicyX.Models;
using System.ComponentModel.DataAnnotations;

namespace Be.Areas.Admin.ViewModels.Chef;

public class UpdateChefVM
{
    [Required(ErrorMessage ="required")]
    public string Name { get; set; }

    public string? Image {  get; set; }
    public IFormFile? Photo { get; set; }

    public string? FaceLink { get; set; }
    public string? TwitLink { get; set; }
    public string? LinkedLink { get; set; }
    public string? GoogleLink { get; set; }

    public int PositionId { get; set; }
    public List<Position>? Positions { get; set; }
}
