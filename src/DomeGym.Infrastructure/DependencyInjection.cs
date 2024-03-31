using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Common.Interfaces;
using DomeGym.Infrastructure.Persistence.Repositories;
using DomeGym.Infrastructure.Persistance;
using DomeGym.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DomeGym.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddServices().AddPersistence();
        return services;
    }


    static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, SystemDatetimeProvider>();
        return services;
    }


    static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<DomeGymDbContext>(options =>
            options.UseSqlite("Data Source = DomeGym.db"));

        services.AddScoped<IAdminsRepository, AdminsRepository>();
        services.AddScoped<IGymsRepository, GymsRepository>();
        services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
        services.AddScoped<IRoomsRepository, RoomsRepository>();
        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<ISubscriptionsRepository, SubscriptionsRepository>();
        services.AddScoped<ITrainersRepository, TrainersRepository>();
        return services;
    }



}