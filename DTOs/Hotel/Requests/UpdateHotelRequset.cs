using System.ComponentModel.DataAnnotations;
using api.Config.Enums;

namespace api.DTOs.Hotel.Requests;

public class UpdateHotelRequset
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    [RegularExpression(
        @"^\+\d{12,18}$",
        ErrorMessage = "Must be start with (+) and length range is 12 - 18 number"
    )]
    public string? PhoneNumber { get; set; }
    public UpdateLocationRequest? Location { get; set; }
    public List<CreateImageGalleryRequest>? Gellary { get; set; }
    public List<Guid>? RemoveGellary { get; set; }
    public HotelStatus? Status { get; set; }
    public UpdateHotelServicesRequest? Services { get; set; }

    [EmailAddress]
    public string? EmailContact { get; set; }

    [Url]
    public string? WebsiteURL { get; set; }
}
