using api.Data.Repository;
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

            services.AddScoped<HotelService>();

            services.AddScoped<BookingService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
