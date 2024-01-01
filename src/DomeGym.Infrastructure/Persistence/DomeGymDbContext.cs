using System.Reflection;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.GymAggregate;
using DomeGym.Domain.ParticipantAggregate;
using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.SessionAggregate;
using DomeGym.Domain.SubscriptionAggregate;
using DomeGym.Domain.TrainerAggregate;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Persistance;

public class DomeGymDbContext : DbContext
{
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Gym> Gyms { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<Participant> Participants { get; set; }
    public DbSet<Admin> Admins { get; set; }

    public DomeGymDbContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}