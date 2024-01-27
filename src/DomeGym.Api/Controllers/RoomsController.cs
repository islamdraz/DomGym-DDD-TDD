using DomeGym.Application.Rooms.Commands;
using DomeGym.Application.Rooms.Commands.CreateRoom;
using DomeGym.Application.Rooms.Queries;
using DomeGym.Contracts.Rooms;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DomeGym.Api.Controllers;

[Route("gyms/{gymId:guid}/rooms")]
public class RoomsController : ApiController
{
    private readonly ISender _sender;

    public RoomsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{roomId:guid}")]
    public async Task<IActionResult> GetRoom(Guid gymId, Guid roomId)
    {
        var result = await _sender.Send(new GetRoomQuery(GymId: gymId, ID: roomId));

        return result.Match(room => Ok(new RoomResponse(room.Id, room.Name)), Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListRooms(Guid gymId)
    {
        var result = await _sender.Send(new ListRoomQuery(GymId: gymId));

        return result.Match(rooms => Ok(rooms.Select(room => new RoomResponse(room.Id, room.Name))), Problem);
    }
    [HttpPost]
    public async Task<IActionResult> CreateRoom(CreateRoomRequest roomRequest, Guid gymId)
    {
        var command = new CreateRoomCommand(
            gymId,
            roomRequest.Name
        );
        var result = await _sender.Send(command);

        return result.Match(room => CreatedAtAction(nameof(GetRoom), new { gymId = gymId, roomId = room.Id }, new RoomResponse(room.Id, room.Name)), Problem);
    }

    [HttpDelete("{roomId:guid}")]
    public async Task<IActionResult> DeleteRoom(Guid gymId, Guid roomId)
    {
        var command = new DeleteRoomCommand(
            gymId,
            roomId
        );
        var result = await _sender.Send(command);

        return result.Match(_ => NoContent(), Problem);
    }




}