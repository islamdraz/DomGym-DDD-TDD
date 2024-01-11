using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.AdminAggregate;
using DomeGym.Domain.Profiles;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Commands.CreateProfile;

public record CreateProfileCommand(Guid UserId, ProfileType ProfileType) : IRequest<ErrorOr<Profile>>;