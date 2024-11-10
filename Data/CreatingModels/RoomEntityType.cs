using api.Config.Enums;
using api.Models.Room;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.CreatingModels
{
    public class RoomEntityType : IEntityTypeConfiguration<RoomModel>
    {
        public void Configure(EntityTypeBuilder<RoomModel> builder)
        {
            builder
                .Property(x => x.Status)
                .HasConversion(
                    e => e.ToString(),
                    e => (RoomStatus)Enum.Parse(typeof(RoomStatus), e!)
                );

            builder
                .Property(x => x.Type)
                .HasConversion(
                    e => e.ToString(),
                    e => (RoomTypes)Enum.Parse(typeof(RoomTypes), e!)
                );

            builder
                .HasOne(x => x.Hotel)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.HotelId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
