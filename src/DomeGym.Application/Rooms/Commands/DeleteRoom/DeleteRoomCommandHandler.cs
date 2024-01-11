using DomeGym.Application.Common.Interfaces;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Rooms.Commands;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, ErrorOr<Deleted>>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly IGymRepository _gymRepository;
    public DeleteRoomCommandHandler(IRoomsRepository roomsRepository, IGymRepository gymRepository)
    {
        _roomsRepository = roomsRepository;
        _gymRepository = gymRepository;
    }


    public async Task<ErrorOr<Deleted>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetByIdAsync(request.gymId);

        if (gym is null)
        {
            return Error.NotFound("Gym not found");
        }
        if (gym.HasRoom(request.roomId))
        {
            return Error.NotFound("Room not found");

        }
        var room = await _roomsRepository.GetByIdAsync(request.roomId);
        if (room is null)
        {
            return Error.NotFound("Room not found");
        }

        var removeRoomResult = gym.RemoveRoom(room);
        if (removeRoomResult.IsError)
        {
            return removeRoomResult.Errors;
        }

        await _gymRepository.UpdateAsync(gym);

        return Result.Deleted;

    }
}
