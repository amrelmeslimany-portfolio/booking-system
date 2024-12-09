using api.Models.Common;

namespace api.Models.Hotel
{
    public class GalleryModel : BaseModel
    {
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? HotelId { get; set; }
        public HotelModel? Hotel { get; set; }
    }
}
