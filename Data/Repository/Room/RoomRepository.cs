using System.Linq.Expressions;
using api.Config.Utils.Common;
using api.Models.Room;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Repository.Room
{
    public class RoomRepository(DataAppContext _context) : IRoomRepository
    {
        public async Task<RoomModel?> Create(RoomModel model)
        {
            await _context.Rooms.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(RoomModel model)
        {
            _context.Rooms.Remove(model);
            await _context.SaveChangesAsync();
        }

        public IQueryable<RoomModel> FindAll(FindAllParams<RoomModel> findAllParams)
        {
            IQueryable<RoomModel> query = _context
                .Rooms.AsNoTracking()
                .OrderByDescending(h => h.CreatedAt);

            if (findAllParams.Expression != null)
                query = query.Where(findAllParams.Expression);

            return query
                .Include(r => r.Bookings)
                .Skip((int)(findAllParams.PageNumber - 1)! * (int)findAllParams.PageSize!)
                .Take((int)findAllParams.PageSize);
        }

        public async Task<RoomModel?> FindById(Guid id, bool? isTrack = false)
        {
            IQueryable<RoomModel> query = _context.Rooms;

            if (isTrack == null || !(bool)isTrack)
                query.AsNoTracking();

            return await query
                .Include(h => h.Bookings)
                .Include(h => h.Hotel)
                .FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<int> FindSize(Expression<Func<RoomModel, bool>>? predicate = null)
        {
            IQueryable<RoomModel> query = _context.Rooms;

            if (predicate != null)
                query = query.Where(predicate);

            return await query.CountAsync();
        }

        public async Task<RoomModel?> FindWhere(Expression<Func<RoomModel?, bool>> predicate)
        {
            return await _context.Rooms.Where(predicate).FirstOrDefaultAsync();
        }

        public async Task Update(RoomModel model)
        {
            _context.Rooms.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
