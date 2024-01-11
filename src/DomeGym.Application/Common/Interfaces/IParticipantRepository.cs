using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.ParticipantAggregate;

namespace DomeGym.Application.Common.Interfaces;

public interface IParticipantRepository
{
    Task AddParticipantAsync(Participant participant);
    Task<Profile?> GetProfileByUserIdAsync(Guid userId);
    Task<Participant?> GetByIdAsync(Guid id);
    Task UpdateAsync(Participant participant);
}