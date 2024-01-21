using DomeGym.Application.Gyms.Commands;
using DomeGym.Application.Gyms.Commands.AddTrainer;
using DomeGym.Application.Gyms.Queries.GetGym;
using DomeGym.Application.Gyms.Queries.ListGyms;
using DomeGym.Application.Gyms.Queries.ListSessions;
using DomeGym.Contracts.Gyms;
using DomeGym.Contracts.Sessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;

[Route("subscriptions/{subscriptionId:guid}/gyms")]
public class GymsController : ApiController
{
    private readonly ISender sender;

    public GymsController(ISender sender)
    {
        this.sender = sender;
    }


    [HttpPost]
    public async Task<IActionResult> Post(CreateGymRequest gymRequest, Guid subscriptionId)
    {
        var command = new CreateGymCommand(gymRequest.Name, SubscriptionId: subscriptionId);
        var result = await sender.Send(command);

        return result.Match(gym => CreatedAtAction(nameof(GetGym),
                                                   new { subscriptionId, gymId = gym.Id },
                                                   new GymResponse(gym.Id, gym.Name)), Problem);
    }


    [HttpGet("{gymId:guid}")]
    public async Task<IActionResult> GetGym(Guid subscriptionId, Guid gymId)
    {
        var result = await sender.Send(new GetGymQuery(SubscriptionId: subscriptionId, GymId: gymId));

        return result.Match(gym => Ok(new GymResponse(gym.Id, gym.Name)), Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListGyms(Guid subscriptionId)
    {
        var result = await sender.Send(new ListGymsQuery(SubscriptionId: subscriptionId));

        return result.Match(gyms => Ok(gyms.Select(gym => new GymResponse(gym.Id, gym.Name))), Problem);
    }


    [HttpPost("{gymId:guid}/trainers")]
    public async Task<IActionResult> Post(Guid subscriptionId, Guid gymId, AddTrainerRequest trainerRequest)
    {
        var command = new AddTrainerCommand(SubscriptionId: subscriptionId, GymId: gymId, TrainerId: trainerRequest.TrainerId);
        var result = await sender.Send(command);

        return result.Match(trainer => Ok(), Problem);
    }

    [HttpGet("{gymId:guid}/sessions")]
    public async Task<IActionResult> ListSessions(Guid subscriptionId, Guid gymId, DateTime? startDateTime, DateTime? endDateTime, [FromQuery] List<string>? categories)
    {
        var categoriesList = SessionCategoryUtils.ConvertToDomain(categories);
        if (categoriesList.IsError)
            return Problem(categoriesList.Errors);

        var result = await sender.Send(new ListSessionsQuery(SubscriptionId: subscriptionId, GymId: gymId, StartDateTime: startDateTime, EndDateTime: endDateTime, Categories: categoriesList.Value));

        return result.Match(
            sessions => Ok(sessions.ConvertAll(
                session => new SessionResponse(
                    session.Id,
                    session.Name,
                    session.Description,
                    session.NumParticipants,
                    session.MaxParticipants,
                    session.Date.ToDateTime(session.Time.Start),
                    session.Date.ToDateTime(session.Time.End),
                    session.Categories.Select(category => category.Name).ToList()
        ))),
        Problem);
    }
}