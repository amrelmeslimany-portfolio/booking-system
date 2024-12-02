using api.Data.CreatingModels;
using api.Models.Booking;
using api.Models.Common;
using api.Models.Hotel;
using api.Models.Room;
using api.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class DataAppContext(DbContextOptions<DataAppContext> options)
        : IdentityDbContext<AppUserModel>(options)
    {
        public DbSet<HotelModel> Hotels { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
        public DbSet<GalleryModel> Gellarys { get; set; }
        public DbSet<BookingModel> Bookings { get; set; }
        public DbSet<BookingJobModel> BookingJobs { get; set; }
        public DbSet<RoomModel> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(RoleEntityType).Assembly);
            base.OnModelCreating(builder);
        }
    }
}
