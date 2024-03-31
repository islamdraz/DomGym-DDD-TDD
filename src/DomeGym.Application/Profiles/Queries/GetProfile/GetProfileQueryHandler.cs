using DomeGym.Application.Common.Interfaces;
using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.Profiles;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ErrorOr<Profile?>>
{
    private readonly ITrainersRepository _trainersRepository;
    private readonly IParticipantsRepository _participantsRepository;
    private readonly IAdminsRepository _adminsRepository;

    public GetProfileQueryHandler(ITrainersRepository trainersRepository, IParticipantsRepository participantsRepository, IAdminsRepository adminsRepository)
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
