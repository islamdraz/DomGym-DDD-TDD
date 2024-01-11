using System.Reflection.Metadata.Ecma335;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Rooms.Queries;

public class GetRoomQueryHandler : IRequestHandler<GetRoomQuery, ErrorOr<Room>>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly IGymRepository _gymRepository;

    public GetRoomQueryHandler(IRoomsRepository roomsRepository, IGymRepository gymRepository)
    {
        _roomsRepository = roomsRepository;
        _gymRepository = gymRepository;
    }

    public async Task<ErrorOr<Room>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetByIdAsync(request.GymId);
        if (gym is null)
            return Error.NotFound("Gym is not Found ");

        if (!gym.HasRoom(request.ID))
            return Error.NotFound("Room is not exists in the gym ");

        if (await _roomsRepository.GetByIdAsync(request.ID) is not Room room)
        {
            return Error.NotFound("Room not found");
        }

        return room;
    }
}
