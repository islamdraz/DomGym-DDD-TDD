using DomeGym.Domain.Common.Interfaces;

namespace DomeGym.Infrastructure;

public class SystemDatetimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.Now;
}
