using DomeGym.Application.Profiles.Common;
using DomeGym.Domain.Profiles;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Queries.GetProfile;

public record GetProfileQuery(Guid UserId, ProfileType ProfileType) : IRequest<ErrorOr<Profile?>>;