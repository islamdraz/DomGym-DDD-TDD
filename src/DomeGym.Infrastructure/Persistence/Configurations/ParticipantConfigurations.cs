using DomeGym.Domain;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.Common.ValueObjects;
using DomeGym.Domain.GymAggregate;
using DomeGym.Domain.ParticipantAggregate;
using DomeGym.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Infrastructure.Persistance.Configurations;

public class ParticipantConfigurations : IEntityTypeConfiguration<Participant>
{
    public void Configure(EntityTypeBuilder<Participant> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.OwnsOne<Schedule>("_schedule", sb =>
          {
              sb.Property<Dictionary<DateOnly, List<TimeRange>>>("_calendar")
                  .HasColumnName("ScheduleCalendar")
                  .HasValueJsonConverter();

              sb.Property(s => s.Id)
                  .HasColumnName("ScheduleId");
          });

        builder.Property<List<Guid>>("_sessionIds")
            .HasColumnName("SessionIds")
            .HasListOfIdsConverter();

        builder.Property(g => g.UserId);

    }
}