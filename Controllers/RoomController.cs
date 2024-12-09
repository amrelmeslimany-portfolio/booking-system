using System.Linq.Expressions;
using System.Security.Claims;
using api.Config.Enums;
using api.Config.Utils.Common;
using api.Data.Repository;
using api.DTOs.Room.Requests;
using api.DTOs.Room.Responses;
using api.Models.Hotel;
using api.Models.Room;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(Roles = "Owner")]
    public class RoomController(IMapper mapper, IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoomRequest body)
        {
            try
            {
                string ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                HotelModel? foundHotel = await unitOfWork.Hotel.FindWhere(h =>
                    h!.OwnerId == ownerId
                );

                if (foundHotel == null)
                {
                    return NotFound(new ProblemDetails { Title = "Hotel not found" });
                }

                RoomModel room = mapper.Map(body, new RoomModel { Hotel = foundHotel });

                await unitOfWork.Room.Create(room);

                await unitOfWork.SaveChanges();

                return Ok(mapper.Map<RoomDetailsResponse>(room));
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        [HttpGet("hotel/{hotelId}")]
        [AllowAnonymous]
        public async Task<IActionResult> FindAll(
            Guid hotelId,
            [FromQuery] PaginationQuery pagination,
            RoomStatus? roomStatus = null,
            RoomTypes? roomType = null
        )
        {
            try
            {
                Expression<Func<RoomModel, bool>>? filterExpression = (room) =>
                    (room.HotelId == hotelId)
                    && (roomStatus == null || room.Status == roomStatus)
                    && (roomType == null || room.Type == roomType);

                FindAllParams<RoomModel> allParams =
                    new(pagination.PageNumber, pagination.PageSize, filterExpression);

                var rooms = await unitOfWork
                    .Room.FindAll(allParams)
                    .Select(item => mapper.Map<RoomResponse>(item))
                    .ToListAsync();

                return Ok(
                    new ListPaginationResponse<RoomResponse>(
                        rooms,
                        await unitOfWork.Room.FindSize(filterExpression),
                        (int)allParams.PageNumber!,
                        (int)allParams.PageSize!
                    )
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var foundRoom = await unitOfWork.Room.FindById(id, "Hotel.Rooms.Bookings");
                if (foundRoom == null)
                    return NotFound(new ProblemDetails { Title = "Room not found" });
                return Ok(mapper.Map<RoomDetailsResponse>(foundRoom));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoomRequest body)
        {
            try
            {
                string ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                RoomModel? foundRoom = await unitOfWork.Room.FindById(id, "Hotel.Owner");

                if (foundRoom == null)
                {
                    return NotFound(new ProblemDetails { Title = "Hotel not found" });
                }

                if (foundRoom.Hotel == null || foundRoom.Hotel.OwnerId != ownerId)
                {
                    return StatusCode(
                        403,
                        new ProblemDetails { Title = "Dont allowed delete this room" }
                    );
                }

                foundRoom = mapper.Map(body, foundRoom);

                await unitOfWork.Room.Update(foundRoom);

                await unitOfWork.SaveChanges();

                return Ok(mapper.Map<RoomDetailsResponse>(foundRoom));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                string ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                RoomModel? foundRoom = await unitOfWork.Room.FindById(id, "Hotel.Owner,Bookings");

                if (foundRoom == null)
                {
                    return NotFound(new ProblemDetails { Title = "Hotel not found" });
                }

                if (foundRoom.Hotel == null || foundRoom.Hotel?.OwnerId != ownerId)
                {
                    return StatusCode(
                        403,
                        new ProblemDetails { Title = "Dont allowed delete this room" }
                    );
                }

                // TODO check this when creating bookings
                bool hasBookingNow = foundRoom.Bookings.Any(x =>
                    x.Status == BookingStatus.Pending || x.Status == BookingStatus.Confirmed
                );

                if (foundRoom.Status == RoomStatus.Booked || hasBookingNow)
                {
                    return BadRequest(
                        new ProblemDetails
                        {
                            Title = "This room is booked now or has at least booking",
                        }
                    );
                }

                await unitOfWork.Room.Delete(foundRoom);

                await unitOfWork.SaveChanges();

                return Ok(new { isDeleted = true });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
