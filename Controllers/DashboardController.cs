using System.Security.Claims;
using api.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    // TODO test
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class DashboardController(IUnitOfWork unitOfWork) : ControllerBase
    {
        [HttpGet("last-months-hotels")]
        public async Task<IActionResult> FindLastMonthsHotel([FromQuery] int? lastMonthCount = 3)
        {
            try
            {
                return Ok(await unitOfWork.Hotel.LastMonths(lastMonthCount));
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
                return Ok(await unitOfWork.Booking.GetTopLocations(lastMonthCount));
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
                return Ok(await unitOfWork.Booking.GetFinanceStates());
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
                    await unitOfWork.Users.GetLastMonths(
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
