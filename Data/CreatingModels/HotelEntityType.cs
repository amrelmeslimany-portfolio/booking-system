using System;
using System.Text.Json;
using api.Config.Enums;
using api.Models.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.CreatingModels
{
    public class HotelEntityType : IEntityTypeConfiguration<HotelModel>
    {
        public void Configure(EntityTypeBuilder<HotelModel> builder)
        {
            builder.HasIndex(e => e.Name).IsUnique();

            builder
                .Property(e => e.Services)
                .HasConversion(
                    e => JsonSerializer.Serialize(e, (JsonSerializerOptions)null!),
                    e => JsonSerializer.Deserialize<HotelServices>(e, (JsonSerializerOptions)null!)!
                );

            builder
                .Property(e => e.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (HotelStatus)Enum.Parse(typeof(HotelStatus), v)
                );

            builder
                .HasMany(e => e.Gellary)
                .WithOne(e => e.Hotel)
                .HasForeignKey(e => e.HotelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(e => e.Location)
                .WithOne(e => e.Hotel)
                .HasForeignKey<LocationModel>(e => e.HotelId);

            builder
                .HasOne(e => e.Owner)
                .WithOne(e => e.Hotel)
                .HasForeignKey<HotelModel>(e => e.OwnerId)
                .IsRequired();
        }
    }
}
