using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.SubscriptionAggregate;
using DomeGym.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Infrastructure.Persistance.Configurations;

public class SubscriptionConfigurations : IEntityTypeConfiguration<Subscription>
{

    public void Configure(EntityTypeBuilder<Subscription> builder)
    {

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();


        builder.Property("_adminId").HasColumnName("AdminId");
        builder.Property("_maxGyms").HasColumnName("MaxGyms");
        builder.Property(a => a.SubscriptionType).HasConversion(subscriptionType => subscriptionType.Value,
        value => SubscriptionType.FromValue(value));

        builder.Property<List<Guid>>("_gymIds").HasColumnName("GymIds").HasListOfIdsConverter();
    }
}