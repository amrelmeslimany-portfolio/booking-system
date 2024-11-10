using System.Linq.Expressions;
using api.Config.Enums;
using api.DTOs.Booking.Requests;
using api.Models.Booking;
using api.Models.Room;

namespace api.Services.Booking;

public class BookingService()
{
    public Expression<Func<BookingModel, bool>>? CheckParams(
        bool isCustomer,
        string userId,
        BookingFilterParams? filterParams = null,
        string? hotelName = null
    )
    {
        return (b) =>
            (isCustomer ? b.CustomerId == userId : b.Room.Hotel.OwnerId == userId)
            && (
                filterParams == null
                || filterParams.Status == null
                || filterParams.Status == b.Status
            )
            && (
                filterParams == null
                || filterParams.CheckIn == null
                || filterParams.CheckIn == b.CheckInDate
            )
            && (
                filterParams == null
                || filterParams.CheckOut == null
                || filterParams.CheckOut == b.CheckOutDate
            )
            && (
                hotelName == null
                || b.Room.Hotel.Name.Trim().ToLower().Contains(hotelName.ToLower().Trim())
            );
    }

    public bool CheckConfictRange(RoomModel room, DateTime checkIn, DateTime checkOut)
    {
        return room.Bookings.Any(b =>
            b.CheckInDate < checkOut
            && b.CheckOutDate > checkIn
            && b.Status != BookingStatus.CheckedOut
            && b.Status != BookingStatus.Cancelled
        );
    }

    public bool CustomerHasBook(RoomModel room, string customerId)
    {
        return room.Bookings.Any(b =>
            b.CustomerId == customerId
            && (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Pending)
        );
    }
}
