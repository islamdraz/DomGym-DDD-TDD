using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.Common.Interfaces;

using ErrorOr;

using MediatR;

namespace DomeGym.Application.Participants.Commands.CancelReservation;

public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, ErrorOr<Deleted>>
{
    private readonly ISessionRepository _sessionsRepository;
    private readonly IParticipantRepository _participantsRepository;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CancelReservationCommandHandler(IParticipantRepository participantsRepository, ISessionRepository sessionsRepository, IDateTimeProvider dateTimeProvider)
    {
        _participantsRepository = participantsRepository;
        _sessionsRepository = sessionsRepository;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<Deleted>> Handle(CancelReservationCommand command, CancellationToken cancellationToken)
    {
        var session = await _sessionsRepository.GetByIdAsync(command.SessionId);

        if (session is null)
        {
            return Error.NotFound(description: "Session not found");
        }

        if (!session.HasReservationForParticipant(command.ParticipantId))
        {
            return Error.NotFound(description: "User doesn't have a reservation for the given session");
        }

        var participant = await _participantsRepository.GetByIdAsync(command.ParticipantId);

        if (participant is null)
        {
            return Error.NotFound(description: "Participant not found");
        }

        if (!participant.HasReservationForSession(session.Id))
        {
            return Error.Unexpected(description: "Participant expected to have reservation to session");
        }

        var cancelReservationResult = session.CancelReservation(participant, _dateTimeProvider);

        if (cancelReservationResult.IsError)
        {
            return cancelReservationResult.Errors;
        }

        await _sessionsRepository.UpdateAsync(session);

        return Result.Deleted;
    }
}
