using DomeGym.Domain;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.TrainerAggregate;
using DomeGym.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Infrastructure.Persistance.Configurations;

public class TrainerConfigurations : IEntityTypeConfiguration<Trainer>
{
    public void Configure(EntityTypeBuilder<Trainer> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property<List<Guid>>("_sessionIds")
               .HasListOfIdsConverter()
               .HasColumnName("SessionIds");

        builder.Property(t => t.UserId);

        builder.OwnsOne<Schedule>("_schedule", sb =>
        {
            sb.Property<Dictionary<DateOnly, List<TimeRange>>>("_calendar")
                .HasColumnName("ScheduleCalendar")
                .HasValueJsonConverter();

            sb.Property(s => s.Id)
                .HasColumnName("ScheduleId");
        });
    }
}