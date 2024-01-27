using DomeGym.Application.Gyms.Queries.ListSessions;
using DomeGym.Application.Sessions.Commands.CreateSession;
using DomeGym.Application.Sessions.Queries.GetSession;
using DomeGym.Contracts.Sessions;
using DomeGym.Domain.SessionAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;

[Route("rooms/{roomId:guid}/sessions")]
public class SessionController : ApiController
{
    private readonly ISender _sender;

    public SessionController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{sessionId:guid}")]
    public async Task<IActionResult> GetSession(Guid roomId, Guid sessionId)
    {
        var result = await _sender.Send(new GetSessionQuery(RoomId: roomId, SessionId: sessionId));

        return result.Match(session => Ok(new SessionResponse(session.Id,
                                    session.Name,
                                    session.Description,
                                    session.NumParticipants,
                                    session.MaxParticipants,
                                    session.Date.ToDateTime(session.Time.Start),
                                    session.Date.ToDateTime(session.Time.End),
                                    session.Categories.Select(cat => cat.Name).ToList())), Problem);
    }



    [HttpPost]
    public async Task<IActionResult> CreateSession(CreateSessionRequest sessionRequest, Guid roomId)
    {
        var categoriesResult = SessionCategoryUtils.ConvertToDomain(sessionRequest.Categories);

        if (categoriesResult.IsError)
        {
            return Problem(categoriesResult.Errors);
        }

        var command = new CreateSessionCommand(
            RoomId: roomId,
            sessionRequest.Name,
            sessionRequest.Description,
            sessionRequest.MaxParticipants,
            sessionRequest.StartDateTime,
            sessionRequest.EndDateTime,
            sessionRequest.TrainerId,
            categoriesResult.Value
        );

        var result = await _sender.Send(command);

        return result.Match(session => CreatedAtAction(nameof(GetSession), new { sessionId = session.Id }, new SessionResponse(session.Id,
                                    session.Name,
                                    session.Description,
                                    session.NumParticipants,
                                    session.MaxParticipants,
                                    session.Date.ToDateTime(session.Time.Start),
                                    session.Date.ToDateTime(session.Time.End),
                                    session.Categories.Select(cat => cat.Name).ToList())), Problem);
    }




}