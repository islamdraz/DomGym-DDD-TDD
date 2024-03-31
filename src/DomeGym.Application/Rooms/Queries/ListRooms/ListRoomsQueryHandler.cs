using System.Reflection.Metadata.Ecma335;
using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Rooms.Queries;

public class ListRoomsQueryHandler : IRequestHandler<ListRoomQuery, ErrorOr<List<Room>>>
{
    private readonly IRoomsRepository _roomsRepository;
    private readonly IGymsRepository _gymRepository;

    public ListRoomsQueryHandler(IRoomsRepository roomsRepository, IGymsRepository gymRepository)
    {
        _roomsRepository = roomsRepository;
        _gymRepository = gymRepository;
    }

    public async Task<ErrorOr<List<Room>>> Handle(ListRoomQuery query, CancellationToken cancellationToken)
    {
        if (!await _gymRepository.ExistsAsync(query.GymId))
            return Error.NotFound("gym not found ");

        return await _roomsRepository.ListByGymIdAsync(query.GymId);

    }
}
