using api.DTOs.Booking.Requests;
using api.DTOs.Booking.Responses;
using api.DTOs.Hotel.Responses;
using api.Models.Booking;
using api.Models.Hotel;
using AutoMapper;

namespace api.DTOs.Booking;

public class BookingMapper : Profile
{
    public BookingMapper()
    {
        CreateRequests();
        CreateResponses();
    }

    private void CreateRequests()
    {
        CreateMap<CreateBookingRequest, BookingModel>();
    }

    private void CreateResponses()
    {
        CreateMap<BookingModel, BookingDetailsResponse>()
            .ForPath(dest => dest.Hotel, opt => opt.MapFrom(src => src.Room.Hotel));

        CreateMap<HotelModel, HotelBookingResponse>()
            .ForMember(dest => dest.Gellary, opt => opt.Ignore());

        CreateMap<BookingModel, BookingResponse>();
        CreateMap<BookingModel, BookingCustomerResponse>()
            .ForMember(dest => dest.HotelName, opt => opt.MapFrom(src => src.Room.Hotel.Name));

        CreateMap<BookingModel, BookingOwnerResponse>();
    }
}
