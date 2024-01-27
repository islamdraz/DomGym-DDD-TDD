using DomeGym.Application.Profiles.Queries.GetProfile;
using DomeGym.Contracts.Profiles;
using ProfileDomain = DomeGym.Domain.Profiles;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomeGym.Application.Profiles.Queries.ListProfilesQuery;
using DomeGym.Application.Profiles.Commands.CreateProfile;

namespace DomeGym.Api.Controllers;


[Route("users/{userId:guid}/profiles")]
public class ProfileController : ApiController
{
    private readonly ISender sender;

    public ProfileController(ISender sender)
    {
        this.sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile(Guid userId, string profileTypeString)
    {
        var profileType = ProfileDomain.ProfileType.FromName(profileTypeString);
        var query = new GetProfileQuery(UserId: userId, profileType);
        var result = await sender.Send(query);

        return result.Match(profile => Ok(new ProfileResponse(profile.Id, ToDto(profileType))), Problem);
    }

    [HttpGet]
    public async Task<IActionResult> ListProfiles(Guid userId)
    {
        var result = await sender.Send(new ListProfilesQuery(UserId: userId));
        return result.Match(profiles => Ok(profiles.Select(profile => new ProfileResponse(profile.Id, ToDto(profile.ProfileType)))), Problem);
    }


    public async Task<IActionResult> CreateProfile(Guid userId, CreateProfileRequest profileRequest)
    {
        var profileType = ProfileDomain.ProfileType.FromName(profileRequest.ProfileType.ToString());
        var command = new CreateProfileCommand(userId, profileType);
        var result = await sender.Send(command);
        return result.Match(profile => CreatedAtAction(nameof(GetProfile),
                                                   new { userId, profileTypeString = profile.ProfileType.Name },
                                                   new ProfileResponse(profile.Id, ToDto(profile.ProfileType))), Problem);
    }
    private Contracts.Profiles.ProfileType ToDto(ProfileDomain.ProfileType profileType)
    {
        return profileType.Name switch
        {
            nameof(ProfileDomain.ProfileType.Admin) => Contracts.Profiles.ProfileType.Admin,
            nameof(ProfileDomain.ProfileType.Participant) => Contracts.Profiles.ProfileType.Participant,
            nameof(ProfileDomain.ProfileType.Trainer) => Contracts.Profiles.ProfileType.Trainer,
            _ => throw new ArgumentOutOfRangeException(nameof(profileType), profileType, null)
        };
    }
}