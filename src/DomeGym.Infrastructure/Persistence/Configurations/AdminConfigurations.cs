using DomeGym.Domain.AdminAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Infrastructure.Persistance.Configurations;

public class AdminConfigurations : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property(a => a.UserId);
        builder.Property(a => a.SubscriptionId);
    }
}