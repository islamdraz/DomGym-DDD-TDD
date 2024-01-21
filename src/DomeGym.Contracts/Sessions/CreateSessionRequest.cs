namespace DomeGym.Contracts.Sessions;

public record CreateSessionRequest(
    string Name,
    string Description,
    int MaxParticipants,
    DateTime StartDateTime,
    DateTime EndDateTime,
    Guid TrainerId,
    List<string> Categories);