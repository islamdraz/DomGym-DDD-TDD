using DomeGym.Application.Common.Interfaces;
using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.Profiles;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ErrorOr<Profile?>>
{
    private readonly ITrainerRepository _trainersRepository;
    private readonly IParticipantRepository _participantsRepository;
    private readonly IAdminRepository _adminsRepository;

    public GetProfileQueryHandler(ITrainerRepository trainersRepository, IParticipantRepository participantsRepository, IAdminRepository adminsRepository)
    {
        _trainersRepository = trainersRepository;
        _participantsRepository = participantsRepository;
        _adminsRepository = adminsRepository;
    }

    public async Task<ErrorOr<Profile?>> Handle(GetProfileQuery query, CancellationToken cancellationToken)
    {
        return query.ProfileType.Name switch
        {
            nameof(ProfileType.Admin) => await _adminsRepository.GetProfileByUserIdAsync(query.UserId),
            nameof(ProfileType.Participant) => await _participantsRepository.GetProfileByUserIdAsync(query.UserId),
            nameof(ProfileType.Trainer) => await _trainersRepository.GetProfileByUserIdAsync(query.UserId),
            _ => Error.Unexpected(description: "Unexpected profile type")
        };
    }
}
