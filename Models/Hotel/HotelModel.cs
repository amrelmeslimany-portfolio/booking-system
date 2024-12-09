using System;
using api.Config.Enums;
using api.Models.Common;
using api.Models.Room;
using api.Models.User;

namespace api.Models.Hotel
{
    public class HotelModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public LocationModel? Location { get; set; }
        public List<GalleryModel> Gellary { get; set; } = [];
        public HotelStatus Status { get; set; } = HotelStatus.Open;
        public string OwnerId { get; set; } = string.Empty;
        public required AppUserModel Owner { get; set; }
        public required HotelServices Services { get; set; }
        public List<RoomModel>? Rooms { get; set; }
        public string? EmailContact { get; set; }
        public string? WebsiteURL { get; set; }
    }
}
