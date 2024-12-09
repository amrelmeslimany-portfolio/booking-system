using api.Data.Repository.Booking;
using api.Data.Repository.Hotel;
using api.Data.Repository.Room;
using api.Data.Repository.Users;

namespace api.Data.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        public IHotelRepository Hotel { get; }
        public IBookingRepository Booking { get; }
        public IUsersRepository Users { get; }
        public IRoomRepository Room { get; }
        public Task SaveChanges();
    }
}
