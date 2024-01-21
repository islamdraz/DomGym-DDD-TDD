
using DomeGym.Domain.SessionAggregate;
using ErrorOr;

public static class SessionCategoryUtils
{
    public static ErrorOr<List<SessionCategory>> ConvertToDomain(List<string>? contractSessionCategory)
    {

        if (contractSessionCategory == null)
        {
            return new List<SessionCategory>();
        }
        List<SessionCategory?> domainSessionCategory = contractSessionCategory
                        .Select(category => SessionCategory.TryFromName(category, out var parsedCategory) ? parsedCategory : null)
                        .Where(category => category is not null)
                        .ToList();

        if (domainSessionCategory.Count != contractSessionCategory.Count)
        {
            return contractSessionCategory.Except(domainSessionCategory.Select(category => category!.Name))
            .Select(invalidCategory => Error.Validation("Categories.InvalidCategory", $"invalid category {invalidCategory}")).ToList();

        }
        return domainSessionCategory.Where(category => category is not null).Select(category => category!).ToList();

    }
}
