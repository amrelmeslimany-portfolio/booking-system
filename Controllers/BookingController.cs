using System.Linq.Expressions;
using System.Security.Claims;
using api.Config.Enums;
using api.Config.Utils.Common;
using api.Data;
using api.Data.Repository;
using api.DTOs.Booking.Requests;
using api.DTOs.Booking.Responses;
using api.Models.Booking;
using api.Models.Room;
using api.Models.User;
using api.Services.Booking;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BookingController(
        IMapper mapper,
        DataAppContext context,
        IUnitOfWork unitOfWork,
        UserManager<AppUserModel> userManager,
        BookingService bookingService
    ) : ControllerBase
    {
        [HttpPost("book-room/{roomId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(Guid roomId, [FromBody] CreateBookingRequest body)
        {
            using var transication = await context.Database.BeginTransactionAsync();

            try
            {
                string customerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                AppUserModel? owner = await userManager.FindByIdAsync(customerId);

                // Get room
                RoomModel? foundRoom = await unitOfWork.Room.FindById(
                    roomId,
                    "Hotel.Rooms.Bookings"
                );

                // Check room status
                if (foundRoom == null)
                    return Problem(statusCode: 404, title: "Room not found");

                if (foundRoom.Status == RoomStatus.Booked)
                    return Problem(statusCode: 400, title: "Room already booked");

                // Check user dont have booking with status booking now
                if (bookingService.CustomerHasBook(foundRoom, customerId))
                {
                    return Problem(
                        statusCode: 400,
                        title: "User already has booking confirmed with this room"
                    );
                }

                // Check if another customer has same booking date
                if (
                    bookingService.CheckConfictRange(foundRoom, body.CheckInDate, body.CheckOutDate)
                )
                {
                    return Problem(statusCode: 400, title: "This date is booked by another");
                }

                // Check the capacity and guests user
                if (body.NumberOfGuests > foundRoom.Capacity)
                    return Problem(
                        statusCode: 400,
                        title: $"Number guests must be less than {foundRoom.Capacity}"
                    );

                BookingModel createdBooking = mapper.Map<BookingModel>(
                    body,
                    opt =>
                    {
                        opt.AfterMap(
                            (src, dest) =>
                            {
                                dest.Room = foundRoom;
                                dest.Customer = owner!;
                            }
                        );
                    }
                );

                await unitOfWork.Booking.Create(createdBooking);

                await unitOfWork.SaveChanges();

                // Create Jobs
                string CheckinJobId = BackgroundJob.Schedule(
                    () => bookingService.UpdateStatus(createdBooking.Id, BookingStatus.Confirmed),
                    createdBooking.CheckInDate
                );

                string CheckOutJobId = BackgroundJob.Schedule(
                    () => bookingService.UpdateStatus(createdBooking.Id, BookingStatus.CheckedOut),
                    createdBooking.CheckOutDate
                );

                createdBooking.Job = new BookingJobModel
                {
                    IdCheckInJob = CheckinJobId,
                    IdCheckOutJob = CheckOutJobId,
                };

                await unitOfWork.Booking.Update(createdBooking);

                await unitOfWork.SaveChanges();

                await transication.CommitAsync();

                return Ok(mapper.Map<BookingDetailsResponse>(createdBooking));
            }
            catch (Exception Exception)
            {
                await transication.RollbackAsync();
                throw new Exception(Exception.Message);
            }
        }

        [HttpGet("customer")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> FindAll(
            [FromQuery] PaginationQuery pagination,
            [FromQuery] BookingFilterParams? filterParams = null,
            [FromQuery] string? hotelName = null
        )
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                Expression<Func<BookingModel, bool>>? filterExpression = bookingService.CheckParams(
                    true,
                    userId,
                    filterParams,
                    hotelName
                );

                return await ReponseFindAll<BookingCustomerResponse>(pagination, filterExpression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("owner")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> FindAll(
            [FromQuery] PaginationQuery pagination,
            [FromQuery] BookingFilterParams? filterParams = null
        )
        {
            try
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                Expression<Func<BookingModel, bool>>? filterExpression = bookingService.CheckParams(
                    false,
                    userId,
                    filterParams
                );

                return await ReponseFindAll<BookingOwnerResponse>(pagination, filterExpression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Customer,Owner")]
        public async Task<IActionResult> Find(Guid id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var role = User.FindFirstValue(ClaimTypes.Role);
                BookingModel? foundBooking = await unitOfWork.Booking.FindById(
                    id,
                    "Room,Customer,Room.Hotel.Location,Room.Hotel.Owner"
                );

                if (foundBooking == null)
                    return Problem(statusCode: 404, title: "No booking found");

                // TODO test
                if (role == UserTypes.Customer.ToString() && foundBooking.CustomerId != userId)
                    return Problem(statusCode: 403, title: "Not allowed");

                return Ok(mapper.Map<BookingDetailsResponse>(foundBooking));
            }
            catch (Exception Exception)
            {
                throw new Exception(Exception.Message);
            }
        }

        [HttpPatch("{id}/cancel")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            try
            {
                string customerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                BookingModel? booking = await unitOfWork.Booking.FindById(id, "Customer,Job,Room");

                if (booking == null)
                    return Problem(statusCode: 404, title: "Booking not found");

                if (booking.CustomerId != customerId)
                    return Problem(statusCode: 403, title: "Not Allowed");

                if (
                    booking.Status != BookingStatus.Pending
                    && booking.Status != BookingStatus.Confirmed
                )
                {
                    return Problem(statusCode: 400, title: "Cant cancel because status changed");
                }

                // TODO when booking status change from confirmed  to (candel or checkedOut)
                // change room status from booked to available

                booking.Status = BookingStatus.Cancelled;

                await unitOfWork.Booking.Update(booking);

                await unitOfWork.SaveChanges();

                await bookingService.UpdateScaduleRoom(booking);

                return Ok(new { isSuccess = true });
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                string ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                BookingModel? booking = await unitOfWork.Booking.FindById(
                    id,
                    "Job,Room,Room.Hotel.Owner"
                );

                if (booking == null)
                    return Problem(statusCode: 404, title: "Booking not found");

                if (booking.Room.Hotel.OwnerId != ownerId)
                    return Problem(statusCode: 403, title: "Not Allowed");

                if (booking.Status == BookingStatus.Confirmed)
                {
                    return Problem(statusCode: 400, title: "Cant delete booking working now");
                }

                await unitOfWork.Booking.Delete(booking);

                await unitOfWork.SaveChanges();

                await bookingService.UpdateScaduleRoom(booking);

                return Ok(new { isSuccess = true });
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private async Task<IActionResult> ReponseFindAll<T>(
            PaginationQuery pagination,
            Expression<Func<BookingModel, bool>>? filterExpression
        )
        {
            FindAllParams<BookingModel> allParams =
                new(pagination.PageNumber, pagination.PageSize, filterExpression);

            var bookings = await unitOfWork
                .Booking.FindAll(allParams, "Customer,Room.Hotel.Owner")
                .Select(item => mapper.Map<T>(item))
                .ToListAsync();

            return Ok(
                new ListPaginationResponse<T>(
                    bookings,
                    await unitOfWork.Booking.FindSize(filterExpression),
                    (int)allParams.PageNumber!,
                    (int)allParams.PageSize!
                )
            );
        }
    }
}
