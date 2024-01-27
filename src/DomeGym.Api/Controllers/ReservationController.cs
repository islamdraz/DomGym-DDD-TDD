using DomeGym.Application.Reservations.Commands.CreateReservation;
using DomeGym.Contracts.Reservations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;

[Route("sessions/{sessionId:guid}/reservations")]
public class ReservationController : ApiController
{
    private readonly ISender _sender;

    public ReservationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Guid sessionId, CreateReservationRequest reservationRequest)
    {
        var command = new CreateReservationCommand(
            SessionId: sessionId,
            reservationRequest.ParticipantId
        );
        var result = await _sender.Send(command);

        return result.Match(_ => NoContent(), Problem);
    }
}