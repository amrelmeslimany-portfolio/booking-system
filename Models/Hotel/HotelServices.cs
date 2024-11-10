using System.ComponentModel;

namespace api.Models.Hotel
{
    public class HotelServices
    {
        [DefaultValue(true)]
        public bool? HasWifi { get; set; }

        [DefaultValue(false)]
        public bool? HasPool { get; set; }

        [DefaultValue(false)]
        public bool? HasGYM { get; set; }

        [DefaultValue(true)]
        public bool? HasRestaurant { get; set; }

        [DefaultValue(true)]
        public bool? HasParking { get; set; }
    }
}
