namespace DomeGym.Contracts.Sessions;

public record SessionResponse(
    Guid Id,
    string Name,
    string Description,
    int NumParticipants,
    int MaxParticipants,
    DateTime StartDateTime,
    DateTime EndDateTime,
    List<string> Categories);