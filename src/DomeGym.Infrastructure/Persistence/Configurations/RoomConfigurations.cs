using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.RoomAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Infrastructure.Persistance.Configurations;

public class RoomConfigurations : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property("_maxDailySessions").HasColumnName("MaxDailySessions");
        builder.Property(a => a.Name);
        builder.Property(a => a.GymId);
    }
}