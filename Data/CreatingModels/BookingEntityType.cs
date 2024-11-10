using api.Config.Enums;
using api.Models.Booking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.CreatingModels
{
    public class BookingEntityType : IEntityTypeConfiguration<BookingModel>
    {
        public void Configure(EntityTypeBuilder<BookingModel> builder)
        {
            builder.HasIndex(x => new { x.CheckInDate, x.CheckOutDate });

            builder
                .Property(x => x.Status)
                .HasConversion(
                    e => e.ToString(),
                    e => (BookingStatus)Enum.Parse(typeof(BookingStatus), e!)
                );

            builder
                .HasOne(x => x.Customer)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(x => x.Job)
                .WithOne(x => x.Booking)
                .HasForeignKey<BookingJobModel>(x => x.BookingId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.Room)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.RoomId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
