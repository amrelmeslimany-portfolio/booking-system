using System;
using System.Linq.Expressions;
using api.Config.Enums;
using api.Config.Utils.Common;
using api.Models.Booking;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Repository.Booking;

public class BookingRepository(DataAppContext _context) : IBookingRepository
{
    public async Task<BookingModel?> Create(BookingModel model)
    {
        await _context.Bookings.AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task Delete(BookingModel model)
    {
        _context.Bookings.Remove(model);
        await _context.SaveChangesAsync();
    }

    public IQueryable<BookingModel> FindAll(FindAllParams<BookingModel> findAllParams)
    {
        IQueryable<BookingModel> query = _context
            .Bookings.AsNoTracking()
            .OrderByDescending(h => h.CreatedAt)
            .ThenByDescending(h => h.Status);

        if (findAllParams.Expression != null)
            query = query.Where(findAllParams.Expression);

        return query
            .Include(b => b.Customer)
            .Include(b => b.Room.Hotel)
            .Skip((int)(findAllParams.PageNumber - 1)! * (int)findAllParams.PageSize!)
            .Take((int)findAllParams.PageSize);
    }

    public async Task<BookingModel?> FindById(Guid id, bool? isTrack = false)
    {
        IQueryable<BookingModel> query = _context.Bookings;

        if (isTrack == null || !(bool)isTrack)
            query.AsNoTracking();

        return await query
            .Include(h => h.Customer)
            .Include(h => h.Job)
            .Include(h => h.Room)
            .ThenInclude(r => r.Hotel)
            .ThenInclude(h => h.Location)
            .Include(h => h.Room.Hotel.Owner)
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<int> FindSize(Expression<Func<BookingModel, bool>>? predicate = null)
    {
        IQueryable<BookingModel> query = _context.Bookings;

        if (predicate != null)
            query = query.Where(predicate);

        return await query.CountAsync();
    }

    public async Task<BookingModel?> FindWhere(Expression<Func<BookingModel?, bool>> predicate)
    {
        return await _context.Bookings.Where(predicate).FirstOrDefaultAsync();
    }

    public async Task Update(BookingModel model)
    {
        _context.Bookings.Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task<object> GetTopLocations(int? lastMonthCount = 3)
    {
        return await _context
            .Bookings.AsNoTracking()
            .Include(b => b.Room.Hotel.Location)
            .Where(b => b.LastUpdatedAt.Month >= DateTime.Now.Month - 3)
            .GroupBy(b => b.Room.Hotel.Location)
            .Select(element => new { Location = element.Key, BookingCount = element.Count() })
            .OrderByDescending(e => e.BookingCount)
            .ToListAsync();
    }

    public async Task<object?> GetFinanceStates()
    {
        return await _context
            .Bookings.AsNoTracking()
            .GroupBy(b => b.Status)
            .Select(group => new
            {
                Status = group.Key,
                TotalRevenue = group.Sum(b => b.TotalPrice).ToString("F3"),
                AvaragePrice = group.Average(b => b.TotalPrice).ToString("F3"),
                BookingsCount = group.Count(),
            })
            .ToListAsync();
    }
}
