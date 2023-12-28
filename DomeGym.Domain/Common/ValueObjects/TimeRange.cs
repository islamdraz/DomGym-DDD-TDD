
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using DomeGym.Domain.Common;
using ErrorOr;
using Throw;

namespace DomeGym.Domain.Common.ValueObjects;

public class TimeRange : ValueObject
{
    public TimeOnly Start { get; init; }
    public TimeOnly End { get; init; }

    public TimeRange(TimeOnly start, TimeOnly end)
    {
        Start = start.Throw().IfGreaterThanOrEqualTo(end);
        End = end;
    }

    public static ErrorOr<TimeRange> FromDateTimes(DateTime start, DateTime end)
    {
        if (start.Date != end.Date)
        {
            return Error.Validation(description: "Start and end time should be in same day");
        }

        if (start >= end)
        {
            return Error.Validation(description: "end time should be greater than the start time ");
        }

        return new TimeRange(TimeOnly.FromDateTime(start), TimeOnly.FromDateTime(end));
    }

    public bool OverLapsWith(TimeRange other)
    {
        if (Start >= other.End) return false;
        if (other.Start >= End) return false;

        return true;

    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Start;
        yield return End;
    }
}