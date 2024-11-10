using System.ComponentModel.DataAnnotations;
using api.Config.Filters;

namespace api.DTOs.Hotel.Requests;

public class CreateImageGalleryRequest
{
    [Required]
    [ValidateFile(
        MaxSizeInBytes = 800000,
        ErrorMessage = "File types allowed are {1} and size is {2}"
    )]
    public IFormFile File { get; set; } = null!;

    [Required]
    [MinLength(5)]
    public string Description { get; set; } = string.Empty;
}
