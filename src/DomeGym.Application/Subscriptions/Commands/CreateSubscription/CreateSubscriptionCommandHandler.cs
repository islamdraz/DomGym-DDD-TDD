using DomeGym.Application.Common.Interfaces;
using DomeGym.Domain.SubscriptionAggregate;

using ErrorOr;

using MediatR;

namespace DomeGym.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly IAdminRepository _adminsRepository;

    public CreateSubscriptionCommandHandler(IAdminRepository adminsRepository)
    {
        _adminsRepository = adminsRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand command, CancellationToken cancellationToken)
    {
        var admin = await _adminsRepository.GetByIdAsync(command.AdminId);

        if (admin is null)
        {
            return Error.NotFound(description: "Admin not found");
        }

        if (admin.SubscriptionId is not null)
        {
            return Error.Conflict(description: "Admin already has active subscription");
        }

        var subscription = new Subscription(command.SubscriptionType, command.AdminId);
        admin.SetSubscription(subscription);

        await _adminsRepository.UpdateAsync(admin);

        return subscription;
    }
}
