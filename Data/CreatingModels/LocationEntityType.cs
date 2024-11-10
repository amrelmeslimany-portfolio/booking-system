using System.Text.Json;
using api.Models.Hotel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.CreatingModels
{
    public class LocationEntityType : IEntityTypeConfiguration<LocationModel>
    {
        public void Configure(EntityTypeBuilder<LocationModel> builder)
        {
            builder
                .Property(x => x.GeoCoordinates)
                .HasConversion(
                    e => JsonSerializer.Serialize(e, (JsonSerializerOptions)null!),
                    e => JsonSerializer.Deserialize<GeoCoordinates>(e, (JsonSerializerOptions)null!)
                );

            builder.HasIndex(x => new { x.Country, x.City });
        }
    }
}
