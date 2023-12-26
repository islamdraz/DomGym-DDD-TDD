
using System.Security.Cryptography.X509Certificates;
using ErrorOr;
using Throw;

namespace DomeGym.Domain;

public class TimeRange
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
        if (start.Date != end.Date || start >= end)
        {
            return Error.Validation();
        }
        return new TimeRange(TimeOnly.FromDateTime(start), TimeOnly.FromDateTime(end));
    }

    public bool OverLapsWith(TimeRange other)
    {
        if (Start >= other.End) return false;
        if (other.Start >= End) return false;

        return true;

    }

}