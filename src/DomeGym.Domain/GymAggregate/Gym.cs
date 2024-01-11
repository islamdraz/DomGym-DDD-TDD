using DomeGym.Domain.Common;
using DomeGym.Domain.RoomAggregate;
using DomeGym.Domain.TrainerAggregate;
using ErrorOr;

namespace DomeGym.Domain.GymAggregate;

public class Gym : AggregateRoot
{
    private List<Guid> _roomIds { get; } = new();
    private List<Guid> _trainerIds { get; } = new();
    private int _maxRooms { get; }
    public string Name { get; } = null;
    public Guid SubscriptionId { get; }


    public Gym(string name, int maxRooms,
               Guid subscriptionId,
               Guid? id = null) : base(id ?? Guid.NewGuid())
    {
        Name = name;
        _maxRooms = maxRooms;
        SubscriptionId = subscriptionId;

    }


    public bool HasRoom(Guid roomId)
    {
        return _roomIds.Contains(roomId);
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (_roomIds.Count >= _maxRooms)
            return GymErrors.CannotHaveRoomMoreThanSubscription;

        if (_roomIds.Contains(room.Id))
            return Error.Conflict(description: "Room already exists");

        _roomIds.Add(room.Id);

        return Result.Success;
    }

    public ErrorOr<Success> AddTrainer(Trainer trainer)
    {

        if (_trainerIds.Contains(trainer.Id))
            return Error.Conflict(description: "Trainer already exists");

        _trainerIds.Add(trainer.Id);

        return Result.Success;
    }

    public ErrorOr<Success> RemoveRoom(Room room)
    {
        if (!_roomIds.Contains(room.Id))
        {
            return Error.NotFound("Room is not found");
        }

        _roomIds.Remove(room.Id);
        return Result.Success;
    }

    public bool HasTrainer(Guid trainerId)
    {
        return _trainerIds.Contains(trainerId);
    }

    private Gym() { }
}
