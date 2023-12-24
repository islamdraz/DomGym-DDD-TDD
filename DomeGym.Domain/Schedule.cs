using ErrorOr;

namespace DomeGym.Domain;

public class Schedule
{
    private readonly Guid _id;

    private readonly Dictionary<DateOnly, List<TimeRange>> _calendar = new();

    public Schedule(Dictionary<DateOnly, List<TimeRange>> calendar = null, Guid? id = null)
    {
        _calendar = calendar ?? new();
        _id = id ?? Guid.NewGuid();
    }

    internal bool CanBookTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out List<TimeRange> timeSlots))
        {
            return true;
        }

        return !timeSlots.Any(timeSlot => timeSlot.OverLapsWith(time));
    }

    public ErrorOr<Success> BookTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots))
        {
            _calendar[date] = new() { time };
            return Result.Success;
        }

        if (timeSlots.Any(timeSlot => timeSlot.OverLapsWith(time)))
        {
            return Error.Conflict();
        }

        timeSlots.Add(time);
        return Result.Success;

    }

    public ErrorOr<Success> RemoveTimeSlot(DateOnly date, TimeRange time)
    {
        if (!_calendar.TryGetValue(date, out var timeSlots))
        {
            return Error.NotFound("Time range not found");

        }
        if (!timeSlots.Remove(time))
        {
            return Error.Unexpected();
        }
        return Result.Success;
    }
    public static Schedule Empty()
    {
        return new Schedule();
    }

}