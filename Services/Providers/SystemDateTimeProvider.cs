using ProvaPub.Interfaces.Providers;

namespace ProvaPub.Services.Providers
{
    public class SystemDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
