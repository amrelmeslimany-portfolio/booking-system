using api.Models.Hotel;

namespace api.Data.Repository.Hotel
{
    public interface IHotelRepository : IRepository<HotelModel>
    {
        public Task<List<TopHotel>> GetTopHotels(int? count = 10);
    }
}
