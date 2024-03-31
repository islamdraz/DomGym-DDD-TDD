using DomeGym.Application.Common.Interfaces;
using DomeGym.Application.Profiles.Common;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Queries.ListProfilesQuery;

public class ListProfilesQueryHandler : IRequestHandler<ListProfilesQuery, ErrorOr<List<Profile>>>
{
    private readonly ITrainersRepository _trainersRepository;
    private readonly IParticipantsRepository _participantsRepository;
    private readonly IAdminsRepository _adminsRepository;

    public ListProfilesQueryHandler(ITrainersRepository trainersRepository, IParticipantsRepository participantsRepository, IAdminsRepository adminsRepository)
    {
        _trainersRepository = trainersRepository;
        _participantsRepository = participantsRepository;
        _adminsRepository = adminsRepository;
    }

    public async Task<ErrorOr<List<Profile>>> Handle(ListProfilesQuery request, CancellationToken cancellationToken)
    {
        var trainerProfile = await _trainersRepository.GetProfileByUserIdAsync(request.UserId);
        var adminProfile = await _adminsRepository.GetProfileByUserIdAsync(request.UserId);
        var participantProfile = await _participantsRepository.GetProfileByUserIdAsync(request.UserId);

        var profiles = new[]
        {
            trainerProfile,
            adminProfile,
            participantProfile
        }
        .Where(profile => profile is not null)
        .ToList();

        if (profiles.Count == 0)
        {
            return Error.NotFound(description: "User not found");
        }

        return profiles!;
    }
}