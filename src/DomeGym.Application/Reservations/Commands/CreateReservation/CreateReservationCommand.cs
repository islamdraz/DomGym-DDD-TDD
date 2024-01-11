using ErrorOr;

using MediatR;

namespace DomeGym.Application.Reservations.Commands.CreateReservation;

public record CreateReservationCommand(
    Guid SessionId,
    Guid ParticipantId) : IRequest<ErrorOr<Success>>;