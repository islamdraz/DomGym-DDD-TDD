﻿using DomeGym.Domain.Common;
using DomeGym.Domain.RoomAggregate;
using ErrorOr;

namespace DomeGym.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private readonly Guid _id;
    private readonly Guid _subscriptionId;
    private readonly List<Guid> _roomIds = new();
    private string _name;
    private int _maxRoom;


    public Gym(int maxRooms,
               Guid subscriptionId,
               Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        _maxRoom = maxRooms;
        _subscriptionId = subscriptionId;

    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (_roomIds.Count >= _maxRoom)
            return GymErrors.CannotHaveRoomMoreThanSubscription;

        if (_roomIds.Contains(room.Id))
            return Error.Conflict(description: "Room already exists");

        _roomIds.Add(room.Id);

        return Result.Success;
    }
}
