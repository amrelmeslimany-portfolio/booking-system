using System;
using api.Data.Repository.Booking;
using api.Data.Repository.Hotel;
using api.Data.Repository.Room;
using api.Data.Repository.Users;
using api.Services.Authentication;
using api.Services.Booking;
using api.Services.Common;
using api.Services.Hotel;

namespace api.Config
{
    public static class AddRepos
    {
        public static IServiceCollection AddRepositorys(this IServiceCollection services)
        {
            services.AddSingleton<IUploadFile, UploadFile>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IHotelRepository, HotelRepository>().AddSingleton<HotelService>();

            services.AddScoped<IRoomRepository, RoomRepository>();

            services
                .AddScoped<IBookingRepository, BookingRepository>()
                .AddSingleton<BookingService>();

            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
