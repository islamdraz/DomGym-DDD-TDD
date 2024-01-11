using DomeGym.Application.Common.Interfaces;
using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.ParticipantAggregate;
using DomeGym.Domain.Profiles;
using DomeGym.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace DomeGym.Infrastructure.Persistence.Repositories;

public class ParticipantsRepository : IParticipantRepository
{
    private readonly DomeGymDbContext _dbContext;

    public ParticipantsRepository(DomeGymDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddParticipantAsync(Participant participant)
    {
        await _dbContext.Participants.AddAsync(participant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Participant?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Participants.FirstOrDefaultAsync(participant => participant.Id == id);
    }

    public async Task<Profile?> GetProfileByUserIdAsync(Guid userId)
    {
        var participant = await _dbContext.Participants
            .AsNoTracking()
            .FirstOrDefaultAsync(participant => participant.UserId == userId);

        return participant is null ? null : new Profile(participant.Id, ProfileType.Participant);
    }

    public async Task<List<Participant>> ListByIdsAsync(List<Guid> ids)
    {
        return await _dbContext.Participants
            .Where(participant => ids.Contains(participant.Id))
            .ToListAsync();
    }

    public async Task UpdateAsync(Participant participant)
    {
        _dbContext.Update(participant);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(List<Participant> participants)
    {
        _dbContext.UpdateRange(participants);
        await _dbContext.SaveChangesAsync();
    }
}