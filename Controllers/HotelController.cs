using System.Linq.Expressions;
using System.Security.Claims;
using api.Config.Enums;
using api.Config.Utils.Common;
using api.Data.Repository.Hotel;
using api.DTOs.Hotel.Requests;
using api.DTOs.Hotel.Responses;
using api.Models.Hotel;
using api.Models.User;
using api.Services.Hotel;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    [Authorize(Roles = "Owner")]
    public class HotelController(
        UserManager<AppUserModel> userManager,
        IHotelRepository hotelRepository,
        IMapper mapper,
        HotelService hotelService
    ) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateHotelRequest body)
        {
            try
            {
                var user = await userManager.GetUserAsync(User);

                if (await hotelRepository.FindWhere(c => c!.OwnerId == user!.Id) != null)
                {
                    return BadRequest(new ProblemDetails { Title = "Owner has Hotel already" });
                }

                List<GalleryModel> gallery = await hotelService.UploadImageHandling(body.Gellary);

                HotelModel hotel = mapper.Map<HotelModel>(body);
                hotel.Owner = user!;
                hotel.Gellary = gallery;

                await hotelRepository.Create(hotel);

                return Ok(mapper.Map<HotelResponse>(hotel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> FindAll(
            [FromQuery] PaginationQuery pagination,
            HotelStatus? status = null,
            string? country = null,
            string? city = null
        )
        {
            try
            {
                Expression<Func<HotelModel, bool>>? filterExpression = (hotel) =>
                    (status == null || status == hotel.Status)
                    && (country == null || hotel.Location!.Country.ToLower() == country.ToLower())
                    && (city == null || hotel.Location!.City.ToLower() == city.ToLower());

                FindAllParams<HotelModel> allParams =
                    new(pagination.PageNumber, pagination.PageSize, filterExpression);

                var hotels = await hotelRepository
                    .FindAll(allParams)
                    .Select(item => mapper.Map<HotelListResponse>(item))
                    .ToListAsync();

                return Ok(
                    new ListPaginationResponse<HotelListResponse>(
                        hotels,
                        await hotelRepository.FindSize(filterExpression),
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

        [HttpGet("top")]
        [AllowAnonymous]
        public async Task<IActionResult> FindTop([FromQuery] int? count = 10)
        {
            try
            {
                var hotels = await hotelRepository.GetTop(
                    count > 20 ? 20
                    : count <= 0 ? 10
                    : count
                );
                return Ok(mapper.Map<List<TopHotelResponse>>(hotels));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("myhotel")]
        public async Task<IActionResult> FindMyHotel()
        {
            try
            {
                Guid ownerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
                HotelModel? foundHotel = await hotelRepository.FindById(ownerId);

                if (foundHotel == null)
                {
                    return NotFound(new ProblemDetails { Title = "You dont have hotel" });
                }

                return Ok(mapper.Map<HotelResponse>(foundHotel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Find(Guid id)
        {
            try
            {
                HotelModel? foundHotel = await hotelRepository.FindById(id);

                if (foundHotel == null)
                {
                    return NotFound(new ProblemDetails { Title = "Hotel not found" });
                }

                return Ok(mapper.Map<HotelResponse>(foundHotel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateHotelRequset body)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                HotelModel? currentHotel = await hotelRepository.FindById(
                    Guid.Parse(userId!),
                    true
                );

                if (currentHotel == null)
                {
                    return NotFound(new ProblemDetails { Title = "You dont have hotel" });
                }

                List<GalleryModel> gallery = await hotelService.UploadImageHandling(body.Gellary);

                currentHotel.Gellary.AddRange(gallery);

                currentHotel = mapper.Map(body, currentHotel);

                hotelService.DeleteGellaryItem(currentHotel, body.RemoveGellary);

                await hotelRepository.Update(currentHotel!);

                return Ok(mapper.Map<HotelResponse>(currentHotel));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                HotelModel? currentHotel = await hotelRepository.FindById(
                    Guid.Parse(userId!),
                    true
                );

                if (currentHotel == null)
                {
                    return NotFound(new ProblemDetails { Title = "You dont have hotel" });
                }

                if (!currentHotel.Rooms.IsNullOrEmpty())
                {
                    return BadRequest(
                        new ProblemDetails { Title = "cant remove hotel include Rooms" }
                    );
                }

                await hotelRepository.Delete(currentHotel);

                return Ok(new { isDeleted = true, currentHotel.Name });
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
