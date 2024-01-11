using ErrorOr;
using MediatR;

namespace DomeGym.Application.Rooms.Commands;


public record DeleteRoomCommand(Guid gymId, Guid roomId) : IRequest<ErrorOr<Deleted>>;