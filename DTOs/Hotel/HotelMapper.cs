using api.DTOs.Hotel.Requests;
using api.DTOs.Hotel.Responses;
using api.DTOs.Room.Responses;
using api.Models.Hotel;
using api.Models.Room;
using api.Models.User;
using AutoMapper;

namespace api.DTOs.Hotel
{
    public class HotelMapper : Profile
    {
        public HotelMapper()
        {
            CreateMap<CreateHotelRequest, HotelModel>()
                .ForMember(des => des.Owner, opt => opt.Ignore())
                .ForMember(des => des.Gellary, opt => opt.Ignore());

            CreateMap<CreateLocationRequest, LocationModel>();

            CreateMap<HotelModel, HotelResponse>();

            CreateMap<HotelModel, HotelBriefResponse>();

            CreateMap<HotelModel, HotelListResponse>()
                .ForMember(
                    dest => dest.Description,
                    opt =>
                        opt.MapFrom(src =>
                            src.Description.Length > 50
                                ? src.Description.Substring(0, 50) + "..."
                                : src.Description
                        )
                );

            CreateMap<RoomModel, RoomResponse>();

            CreateMap<GalleryModel, GellaryResponse>();

            CreateMap<LocationModel, LocationResponse>();

            CreateMap<AppUserModel, OwnerResponse>()
                .ForMember(
                    dest => dest.FullName,
                    opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}")
                );

            CreateMap<TopHotel, TopHotelResponse>();

            MapUpdates();
        }

        private void MapUpdates()
        {
            CreateMap<UpdateHotelRequset, HotelModel>()
                .ForMember(dest => dest.Gellary, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateLocationRequest, LocationModel>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateGeoCoordinatesRequest, GeoCoordinates>();

            CreateMap<UpdateHotelServicesRequest, HotelServices>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMem) => srcMem != null));
        }
    }
}
