using api.Models.Hotel;

namespace api.Data.Repository.Hotel
{
    public interface IHotelRepository : IRepository<HotelModel>
    {
        public Task<List<TopHotel>> GetTop(int? count = 10);
        public Task<object> LastMonths(int? lastMonthCount);
        public Task<HotelModel?> GetHotelByOwner(string ownerId);
    }
}
