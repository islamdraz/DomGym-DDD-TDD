namespace DomeGym.Domain.UnitTests.TestUtils;

public static class TimeRangeFactory
{
    public static TimeRange CreateFromHours(int start, int end)
    {
        return new TimeRange(start: TimeOnly.MinValue.AddHours(start), end: TimeOnly.MinValue.AddHours(end));
    }
}