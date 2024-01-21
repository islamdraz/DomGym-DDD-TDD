using DomeGym.Application.Participants.Queries.ListParticipantSessions;
using DomeGym.Contracts.Sessions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;


[Route("participants")]
public class ParticipantsController : ApiController
{
    private readonly ISender sender;

    public ParticipantsController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet("{participantId:guid}/sessions")]
    public async Task<IActionResult> ListParticipantSessions(Guid participantId, DateTime? startDate, DateTime? endDate)
    {
        var result = await sender.Send(new ListParticipantSessionsQuery(ParticipantId: participantId));

        return result.Match(sessions =>
            Ok(sessions.ConvertAll(
                session =>
                    new SessionResponse(session.Id,
                                    session.Name,
                                    session.Description,
                                    session.NumParticipants,
                                    session.MaxParticipants,
                                    session.Date.ToDateTime(session.Time.Start),
                                    session.Date.ToDateTime(session.Time.End),
                                    session.Categories.Select(cat => cat.Name).ToList())))
                        , Problem);
    }



}