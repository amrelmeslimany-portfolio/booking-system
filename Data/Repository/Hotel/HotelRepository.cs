using System.Linq.Expressions;
using api.Config.Enums;
using api.Config.Utils.Common;
using api.Models.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Data.Repository.Hotel
{
    public class HotelRepository(DataAppContext _context) : IHotelRepository
    {
        public async Task<HotelModel?> Create(HotelModel Hotel)
        {
            await _context.Hotels.AddAsync(Hotel);
            await _context.SaveChangesAsync();
            return Hotel;
        }

        public IQueryable<HotelModel> FindAll(FindAllParams<HotelModel> requestParams)
        {
            IQueryable<HotelModel> query = _context
                .Hotels.AsNoTracking()
                .OrderByDescending(h => h.CreatedAt);

            if (requestParams.Expression != null)
                query = query.Where(requestParams.Expression);

            return query
                .Include(h => h.Owner)
                .Include(h => h.Gellary)
                .Include(h => h.Location)
                .Skip((int)(requestParams.PageNumber - 1)! * (int)requestParams.PageSize!)
                .Take((int)requestParams.PageSize);
        }

        public async Task<HotelModel?> FindById(Guid Id, bool? isTrack = false)
        {
            IQueryable<HotelModel> query = _context.Hotels;
            if (isTrack == null || !(bool)isTrack)
                query.AsNoTracking();
            return await query
                .Include(h => h.Gellary)
                .Include(h => h.Owner)
                .Include(h => h.Location)
                .Include(h => h.Rooms)
                .FirstOrDefaultAsync(h => h.Id == Id || h.OwnerId == Id.ToString());
        }

        public Task<int> FindSize(Expression<Func<HotelModel, bool>>? predicate = null)
        {
            IQueryable<HotelModel> query = _context.Hotels;
            if (predicate != null)
                query = query.Where(predicate);
            return query.CountAsync();
        }

        public async Task<HotelModel?> FindWhere(Expression<Func<HotelModel?, bool>> predicate)
        {
            return await _context.Hotels.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task Update(HotelModel current)
        {
            _context.Locations.Update(current.Location!);
            _context.Hotels.Update(current);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(HotelModel current)
        {
            _context.Hotels.Remove(current);
            await _context.SaveChangesAsync();
        }

        public async Task<List<TopHotel>> GetTop(int? count = 10)
        {
            return await _context
                .Hotels.Select(item => new TopHotel
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
                .Select(h => new { h.Key, HotelCount = h.Count() })
                .OrderBy(h => h.Key)
                .ToListAsync();
        }
    }
}
