using DomeGym.Domain.SessionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomeGym.Infrastructure.Persistance.Configurations;

public class SessionConfigurations : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();


        builder.Property(x => x.Name);
        builder.Property(x => x.Description);
        builder.Property(x => x.Date);
        builder.OwnsOne(x => x.Time);
        builder.Property(x => x.RoomId);
        builder.Property(x => x.MaxParticipants);
        builder.Property(x => x.TrainerId);

        builder.OwnsMany<Reservation>("_reservations", rb =>
        {
            rb.ToTable("SessionReservations");
            rb.HasKey(r => r.Id);
            rb.Property(x => x.Id).ValueGeneratedNever();

            rb.WithOwner().HasForeignKey("SessionId");
            rb.Property(r => r.ParticipantId);

        });

    }
}