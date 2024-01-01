// using DomeGym.Application.Profiles.Common;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.Profile;
using DomeGym.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Persistence.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly DomeGymDbContext _dbcontext;

    public AdminRepository(DomeGymDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task AddAdminAsync(Admin participant)
    {
        await _dbcontext.Admins.AddAsync(participant);
        _dbcontext.SaveChanges();
    }
    public async Task<Profile?> GetProfileByUserIdAsync(Guid userId)
    {
        var admin = await _dbcontext.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(admin => admin.UserId == userId);

        return admin is null ? null : new Profile(admin.Id, ProfileType.Admin);
    }
    public async Task<Admin?> GetByIdAsync(Guid adminId)
    {
        return await _dbcontext.Admins.FindAsync(adminId);
    }
    public async Task UpdateAsync(Admin admin)
    {
        await _dbcontext.Admins.AddAsync(admin);
        await _dbcontext.SaveChangesAsync();
    }
}