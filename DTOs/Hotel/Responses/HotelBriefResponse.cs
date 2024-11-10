using api.Config.Enums;

namespace api.DTOs.Hotel.Responses
{
    public class HotelBriefResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public HotelStatus? Status { get; set; }
    }
}
