using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.GymAggregate;
using DomeGym.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


namespace DomeGym.Infrastructure.Persistence.Repositories;

public class GymsRepository : IGymsRepository
{
    private readonly DomeGymDbContext _dbcontext;

    public GymsRepository(DomeGymDbContext context)
    {
        _dbcontext = context;
    }
    public async Task AddGymAsync(Gym gym)
    {

        await _dbcontext.Gyms.AddAsync(gym);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbcontext.Gyms.AnyAsync(x => x.Id == id);
    }

    public async Task<Gym?> GetByIdAsync(Guid id)
    {
        return await _dbcontext.Gyms.FindAsync(id);
    }
    public async Task<List<Gym>> ListSubscriptionGymsAsync(Guid subscriptionId)
    {
        return await _dbcontext.Gyms.Where(x => x.SubscriptionId == subscriptionId).ToListAsync();

    }
    public async Task RemoveAsync(Guid id)
    {
        var gym = await GetByIdAsync(id);
        _dbcontext.Gyms.Remove(gym);
        await _dbcontext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Gym gym)
    {
        _dbcontext.Update(gym);
        await _dbcontext.SaveChangesAsync();
    }

}

