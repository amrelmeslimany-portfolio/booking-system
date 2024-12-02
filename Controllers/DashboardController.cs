using System.Security.Claims;
using api.Data.Repository.Booking;
using api.Data.Repository.Hotel;
using api.Data.Repository.Users;
using api.DTOs.Hotel.Responses;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController(
        IHotelRepository hotelRepository,
        IBookingRepository bookingRepository,
        IUsersRepository usersRepository
    ) : ControllerBase
    {
        [HttpGet("last-months-hotels")]
        public async Task<IActionResult> FindLastMonthsHotel([FromQuery] int? lastMonthCount = 3)
        {
            try
            {
                return Ok(await hotelRepository.LastMonths(lastMonthCount));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("top-booking-location")]
        public async Task<IActionResult> FindTopBookingLocation([FromQuery] int? lastMonthCount = 3)
        {
            try
            {
                return Ok(await bookingRepository.GetTopLocations(lastMonthCount));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("finance-states")]
        public async Task<IActionResult> FindFinanceStates()
        {
            try
            {
                return Ok(await bookingRepository.GetFinanceStates());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("last-months-users")]
        public async Task<IActionResult> FindLastMonthsUsers()
        {
            try
            {
                // Test
                return Ok(
                    await usersRepository.GetLastMonths(
                        User.FindFirstValue(ClaimTypes.NameIdentifier)!
                    )
                );
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
