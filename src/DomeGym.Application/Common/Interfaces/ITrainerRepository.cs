// using DomeGym.Application.Profiles.Common;
using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.TrainerAggregate;

namespace DomeGym.Application.Common.Interfaces;

public interface ITrainerRepository
{
    Task AddTrainerAsync(Trainer trainer);
    Task<Profile?> GetProfileByUserIdAsync(Guid userId);
    Task<Trainer?> GetByIdAsync(Guid TrainerId);
    Task UpdateAsync(Trainer trainer);
}