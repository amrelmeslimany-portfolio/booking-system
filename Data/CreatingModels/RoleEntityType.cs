using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.CreatingModels
{
    public class RoleEntityType : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                [
                    new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole() { Name = "Customer", NormalizedName = "CUSTOMER" },
                    new IdentityRole() { Name = "Owner", NormalizedName = "OWNER" },
                ]
            );
        }
    }
}
