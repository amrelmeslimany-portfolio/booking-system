using api.Data.Repository.Booking;
using api.Data.Repository.Hotel;
using api.Data.Repository.Room;
using api.Data.Repository.Users;
using api.Models.Booking;
using api.Models.Hotel;

namespace api.Data.Repository
{
    internal class UnitOfWork(DataAppContext context) : IUnitOfWork
    {
        private readonly DataAppContext _context = context;

        public IHotelRepository Hotel => new HotelRepository(_context);
        public IBookingRepository Booking => new BookingRepository(_context);
        public IUsersRepository Users => new UsersRepository(_context);
        public IRoomRepository Room => new RoomRepository(_context);

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
