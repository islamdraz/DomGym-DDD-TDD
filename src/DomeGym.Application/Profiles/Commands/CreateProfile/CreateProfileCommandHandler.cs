using DomeGym.Application.Common.Interfaces;
using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.ParticipantAggregate;
using DomeGym.Domain.Profiles;
using DomeGym.Domain.TrainerAggregate;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Commands.CreateProfile;

public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, ErrorOr<Profile>>
{
    private readonly IAdminRepository _adminRepository;
    private readonly ITrainerRepository _trainerRepository;
    private readonly IParticipantRepository _participantRepository;
    public CreateProfileCommandHandler(IAdminRepository adminRepository, IParticipantRepository participantRepository, ITrainerRepository trainerRepository)
    {
        _adminRepository = adminRepository;
        _participantRepository = participantRepository;
        _trainerRepository = trainerRepository;
    }

    public async Task<ErrorOr<Profile>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        return request.ProfileType.Name switch
        {
            nameof(ProfileType.Admin) => await CreatAdminProfile(request.UserId),
            nameof(ProfileType.Trainer) => await CreatTrainerProfile(request.UserId),
            nameof(ProfileType.Participant) => await CreatParticipantProfile(request.UserId),
            _ => throw new InvalidOperationException()
        };
    }

    private async Task<ErrorOr<Profile>> CreatParticipantProfile(Guid userId)
    {
        if (await _participantRepository.GetProfileByUserIdAsync(userId) is not null)
        {
            return Error.Conflict("user already has participant profile");
        }

        var participant = new Participant(userId: userId);
        await _participantRepository.AddParticipantAsync(participant);

        return new Profile(userId, ProfileType.Trainer);

    }

    private async Task<ErrorOr<Profile>> CreatTrainerProfile(Guid userId)
    {

        if (await _trainerRepository.GetProfileByUserIdAsync(userId) is not null)
        {
            return Error.Conflict("user already has trainer profile");
        }

        var trainer = new Trainer(userId: userId);
        await _trainerRepository.AddTrainerAsync(trainer);

        return new Profile(userId, ProfileType.Trainer);
    }

    private async Task<ErrorOr<Profile>> CreatAdminProfile(Guid userId)
    {
        if (await _adminRepository.GetProfileByUserIdAsync(userId) is not null)
        {
            return Error.Conflict("user already has admin profile");
        }

        var admin = new Admin(userId: userId);
        await _adminRepository.AddAdminAsync(admin);

        return new Profile(userId, ProfileType.Admin);
    }
}
