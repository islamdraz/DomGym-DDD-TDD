using DomeGym.Domain;

namespace DomeGym.UnitTests.TestUtils.Services
{

    public class TestDateTimeProvider : IDateTimeProvider
    {
        private readonly DateTime? _fixedDateTime;

        public TestDateTimeProvider(DateTime? fixedDateTime = null)
        {
            this._fixedDateTime = fixedDateTime;
        }
        public DateTime UtcNow => _fixedDateTime ?? DateTime.UtcNow;
    }

}
