using api.Models.Booking;

namespace api.Data.Repository.Booking;

public interface IBookingRepository : IRepository<BookingModel>
{
    public Task<object> GetTopLocations(int? lastMonthCount = 3);
    public Task<object?> GetFinanceStates();
}
