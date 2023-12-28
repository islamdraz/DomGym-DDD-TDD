namespace DomeGym.Domain.Common;

public abstract class ValueObject
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null || obj.GetType() != GetType())
        {
            return false;
        }

        return ((ValueObject)obj).GetEqualityComponents()
                                 .SequenceEqual(GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        // Binary representation of 3: 00000011
        // Binary representation of 5: 00000101
        // 00000011
        // XOR 00000101
        // -----------
        // 00000110

        return GetEqualityComponents().Select(x => x?.GetHashCode() ?? 0)
                                      .Aggregate((x, y) => x ^ y);
    }
}