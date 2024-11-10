using api.Config.Enums;
using api.DTOs.Room.Responses;
using api.Models.Common;
using api.Models.Hotel;

namespace api.DTOs.Hotel.Responses
{
    public class HotelResponse : BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public LocationResponse? Location { get; set; }
        public List<GellaryResponse>? Gellary { get; set; } = [];
        public HotelStatus? Status { get; set; }
        public required OwnerResponse Owner { get; set; }
        public required HotelServices Services { get; set; }
        public List<RoomResponse>? Rooms { get; set; }
        public string? EmailContact { get; set; }
        public string? WebsiteURL { get; set; }
    }

    public class HotelListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public LocationResponse? Location { get; set; }
        public virtual List<GellaryResponse>? Gellary { get; set; } = [];
        public HotelStatus? Status { get; set; }
        public required OwnerResponse Owner { get; set; }
    }
}
