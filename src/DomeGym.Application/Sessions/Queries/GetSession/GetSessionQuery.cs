using DomeGym.Domain.SessionAggregate;

using ErrorOr;

using MediatR;

namespace DomeGym.Application.Sessions.Queries.GetSession;

public record GetSessionQuery(Guid RoomId, Guid SessionId)
    : IRequest<ErrorOr<Session>>;