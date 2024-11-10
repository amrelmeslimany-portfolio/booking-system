namespace api.Models.Hotel
{
    public class GalleryModel
    {
        public Guid Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid? HotelId { get; set; }
        public HotelModel? Hotel { get; set; }
    }
}
