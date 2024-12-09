using System.Linq.Expressions;
using System.Security.Claims;
using api.Config.Enums;
using api.Config.Utils.Common;
using api.Data.Repository;
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
        IUnitOfWork unitOfWork,
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

                if (await unitOfWork.Hotel.FindWhere(c => c!.OwnerId == user!.Id) != null)
                {
                    return BadRequest(new ProblemDetails { Title = "Owner has Hotel already" });
                }

                List<GalleryModel> gallery = await hotelService.UploadImageHandling(body.Gellary);

                HotelModel hotel = mapper.Map<HotelModel>(body);
                hotel.Owner = user!;
                hotel.Gellary = gallery;

                await unitOfWork.Hotel.Create(hotel);

                await unitOfWork.SaveChanges();

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
                    && (
                        country == null
                        || hotel.Location!.Country.ToLower().Contains(country.ToLower())
                    )
                    && (city == null || hotel.Location!.City.ToLower().Contains(city.ToLower()));

                FindAllParams<HotelModel> allParams =
                    new(pagination.PageNumber, pagination.PageSize, filterExpression);

                var hotels = await unitOfWork
                    .Hotel.FindAll(allParams, "Location,Gellary,Owner")
                    .Select(item => mapper.Map<HotelListResponse>(item))
                    .ToListAsync();

                return Ok(
                    new ListPaginationResponse<HotelListResponse>(
                        hotels,
                        await unitOfWork.Hotel.FindSize(filterExpression),
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
                var hotels = await unitOfWork.Hotel.GetTop(
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
                string ownerId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
                HotelModel? foundHotel = await unitOfWork.Hotel.GetHotelByOwner(ownerId);

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
                HotelModel? foundHotel = await unitOfWork.Hotel.FindById(
                    id,
                    "Location,Gellary,Rooms,Owner"
                );

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
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                HotelModel? currentHotel = await unitOfWork.Hotel.GetHotelByOwner(userId);

                if (currentHotel == null)
                {
                    return NotFound(new ProblemDetails { Title = "You dont have hotel" });
                }

                List<GalleryModel> gallery = await hotelService.UploadImageHandling(body.Gellary);

                currentHotel.Gellary.AddRange(gallery);

                currentHotel = mapper.Map(body, currentHotel);

                hotelService.DeleteGellaryItem(currentHotel, body.RemoveGellary);

                await unitOfWork.Hotel.Update(currentHotel!);

                await unitOfWork.SaveChanges();

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
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

                HotelModel? currentHotel = await unitOfWork.Hotel.GetHotelByOwner(userId);

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

                await unitOfWork.Hotel.Delete(currentHotel);

                await unitOfWork.SaveChanges();

                return Ok(new { isDeleted = true, currentHotel.Name });
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
