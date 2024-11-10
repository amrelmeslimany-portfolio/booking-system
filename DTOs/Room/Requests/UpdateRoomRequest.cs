using System;
using System.ComponentModel.DataAnnotations;
using api.Config.Enums;

namespace api.DTOs.Room.Requests;

public class UpdateRoomRequest
{
    [Range(1, 10)]
    public int? Capacity { get; set; } = null;

    [DataType(DataType.Currency)]
    public decimal? PricePerNight { get; set; } = null;

    [MinLength(5)]
    public string? Description { get; set; }
    public RoomTypes? Type { get; set; }
}
