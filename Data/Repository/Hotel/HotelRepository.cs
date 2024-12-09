using api.Config.Enums;
using api.Models.Hotel;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Repository.Hotel
{
    public class HotelRepository(DataAppContext _context)
        : Repository<HotelModel>(_context),
            IHotelRepository
    {
        public async Task<HotelModel?> GetHotelByOwner(string ownerId)
        {
            return await _context
                .Hotels.Where(item => item.OwnerId == ownerId)
                .Include(item => item.Location)
                .Include(item => item.Gellary)
                .Include(item => item.Rooms)
                .Include(item => item.Owner)
                .FirstOrDefaultAsync();
        }

        public async Task<List<TopHotel>> GetTop(int? count = 10)
        {
            return await _context
                .Hotels.Include(h => h.Location)
                .Select(item => new TopHotel
                {
                    Hotel = item,
                    BookingsCount = item.Rooms!.Sum(r =>
                        r.Bookings.Count(b => b.Status == BookingStatus.CheckedOut)
                    ),
                })
                .Where(item => item.BookingsCount > 0)
                .OrderByDescending(i => i.BookingsCount)
                .Take(count ?? 10)
                .ToListAsync();
        }

        public async Task<object> LastMonths(int? lastMonthCount = 2)
        {
            return await _context
                .Hotels.AsNoTracking()
                .Where(h => h.CreatedAt.Month >= (DateTime.Now.Month - lastMonthCount))
                .GroupBy(b => b.CreatedAt.Date)
                .Select(h => new { CreateAt = h.Key, HotelCount = h.Count() })
                .OrderBy(h => h.CreateAt)
                .ToListAsync();
        }
    }
}
