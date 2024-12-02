using api.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.CreatingModels;

public class UserEntityType : IEntityTypeConfiguration<AppUserModel>
{
    public void Configure(EntityTypeBuilder<AppUserModel> builder)
    {
        // builder
        //     .HasMany(e => e.Roles)
        //     .WithOne()
        //     .HasForeignKey(e => e.UserId)
        //     .IsRequired()
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}
