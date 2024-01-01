using System.Security.Cryptography.X509Certificates;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.GymAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Infrastructure.Persistance.Configurations;

public class GymConfigurations : IEntityTypeConfiguration<Gym>
{
    public void Configure(EntityTypeBuilder<Gym> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();
        builder.Property(x => x.Name).HasColumnName("Name");
        builder.Property(a => a.SubscriptionId);

        builder.Property<int>("_maxRooms").HasColumnName("MaxRooms");
        builder.Property<List<Guid>>("_roomIds").HasColumnName("RoomIds");
        builder.Property<List<Guid>>("_trainerIds").HasColumnName("TrainerIds");
    }
}