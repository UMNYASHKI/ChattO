using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.DataAccess.EntityTypeConfigurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasMany(org => org.AppUsers)
            .WithOne(user => user.Organization)
            .HasForeignKey(user => user.OrganizationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
