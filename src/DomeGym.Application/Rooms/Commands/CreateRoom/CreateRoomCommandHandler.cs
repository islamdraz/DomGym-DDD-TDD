using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.RoomAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, ErrorOr<Room>>
{
    private readonly IGymsRepository _gymRepository;
    private readonly ISubscriptionsRepository _subscriptionRepository;


    public CreateRoomCommandHandler(IGymsRepository gymRepository, ISubscriptionsRepository subscriptionRepository)
    {
        _gymRepository = gymRepository;
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<ErrorOr<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var gym = await _gymRepository.GetByIdAsync(request.gymId);
        if (gym == null)
            return Error.NotFound(description: "Gym not found");

        var subscription = await _subscriptionRepository.GetByIdAsync(gym.SubscriptionId);
        if (subscription is null)
            return Error.Unexpected(description: "Subscription not found");


        var room = new Room(name: request.roomName, maxDailySession: subscription.GetMaxDailySessions(), gymId: gym.Id);

        gym.AddRoom(room);
        return room;
    }
}