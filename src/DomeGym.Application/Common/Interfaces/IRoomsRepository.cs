
using DomeGym.Domain.RoomAggregate;

namespace DomeGym.Application.Common.Interfaces;

public interface IRoomsRepository
{
    Task AddRoomAsync(Room room);
    Task<Room?> GetByIdAsync(Guid id);
    Task<List<Room>> ListByGymIdAsync(Guid gymId);
    Task RemoveAsync(Room room);
    Task UpdateAsync(Room room);

}