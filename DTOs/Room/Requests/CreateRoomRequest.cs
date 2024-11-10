using System.ComponentModel.DataAnnotations;
using api.Config.Enums;

namespace api.DTOs.Room.Requests
{
    public class CreateRoomRequest
    {
        [Required]
        [Range(1, 10)]
        public int Capacity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal PricePerNight { get; set; }

        [MinLength(5)]
        public string Description { get; set; } = string.Empty;
        public RoomTypes? Type { get; set; } = RoomTypes.Single;
    }
}
