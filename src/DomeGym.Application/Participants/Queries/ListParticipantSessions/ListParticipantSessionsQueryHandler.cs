using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.SessionAggregate;

using ErrorOr;

using MediatR;

namespace DomeGym.Application.Participants.Queries.ListParticipantSessions;

public class ListParticipantSessionsQueryHandler : IRequestHandler<ListParticipantSessionsQuery, ErrorOr<List<Session>>>
{
    private readonly ISessionRepository _sessionsRepository;
    private readonly IParticipantRepository _participantsRepository;

    public ListParticipantSessionsQueryHandler(ISessionRepository sessionsRepository, IParticipantRepository participantsRepository)
    {
        _sessionsRepository = sessionsRepository;
        _participantsRepository = participantsRepository;
    }

    public async Task<ErrorOr<List<Session>>> Handle(ListParticipantSessionsQuery query, CancellationToken cancellationToken)
    {
        var participant = await _participantsRepository.GetByIdAsync(query.ParticipantId);

        if (participant is null)
        {
            return Error.NotFound(description: "Participant not found");
        }

        return await _sessionsRepository.ListByIdsAsync(participant.SessionIds, query.StartDateTime, query.EndDateTime);
    }
}
