using System;
using System.Linq.Expressions;
using api.Config.Enums;
using api.Config.Utils.Common;
using api.Models.Booking;
using Microsoft.EntityFrameworkCore;

namespace api.Data.Repository.Booking;

public class BookingRepository(DataAppContext _context)
    : Repository<BookingModel>(_context),
        IBookingRepository
{
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
