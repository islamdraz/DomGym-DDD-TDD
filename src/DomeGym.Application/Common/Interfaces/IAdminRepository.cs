// using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.AdminAggregate;

namespace DomeGym.Application.Common.Interfaces;

public interface IAdminRepository
{
    Task AddAdminAsync(Admin participant);
    // Task<Domain.Profile?> GetProfileByUserIdAsync(Guid userId);
    Task<Admin?> GetByIdAsync(Guid adminId);
    Task UpdateAsync(Admin admin);
}