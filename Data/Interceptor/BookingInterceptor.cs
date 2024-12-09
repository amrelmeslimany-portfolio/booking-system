using api.Config.Enums;
using api.Models.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.Data.Interceptor;

public class BookingInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        var context = eventData.Context;
        if (context == null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        foreach (var item in context.ChangeTracker.Entries<BookingModel>())
        {
            var booking = item.Entity;

            if (
                item.State == EntityState.Added
                || item.State == EntityState.Modified
                || item.State == EntityState.Deleted
            )
            {
                booking.TotalPrice =
                    booking.Room.PricePerNight * (booking.CheckOutDate - booking.CheckInDate).Days;

                if (item.Entity.Status == BookingStatus.Confirmed)
                    booking.Room!.Status = RoomStatus.Booked;
                else
                    booking.Room!.Status = RoomStatus.Available;
            }
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
