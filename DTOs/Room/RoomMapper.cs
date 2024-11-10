using api.DTOs.Room.Requests;
using api.DTOs.Room.Responses;
using api.Models.Room;
using AutoMapper;

namespace api.DTOs.Room
{
    public class RoomMapper : Profile
    {
        public RoomMapper()
        {
            CreateRequets();
            CreateResponses();
        }

        private void CreateRequets()
        {
            CreateMap<CreateRoomRequest, RoomModel>();

            CreateMap<UpdateRoomRequest, RoomModel>()
                .ForAllMembers(opt =>
                {
                    opt.Condition((src, dest, srcMam) => srcMam != null);
                });
        }

        private void CreateResponses()
        {
            CreateMap<RoomModel, RoomDetailsResponse>()
                .ForMember(
                    dest => dest.TotalBookings,
                    (opt) => opt.MapFrom(src => src.Bookings.Count)
                );
        }
    }
}
