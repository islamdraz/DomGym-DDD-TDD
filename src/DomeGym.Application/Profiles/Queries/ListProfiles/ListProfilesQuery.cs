using DomeGym.Application.Profiles.Common;
using ErrorOr;
using MediatR;

namespace DomeGym.Application.Profiles.Queries.ListProfilesQuery;

public record ListProfilesQuery(Guid UserId) : IRequest<ErrorOr<List<Profile>>>;