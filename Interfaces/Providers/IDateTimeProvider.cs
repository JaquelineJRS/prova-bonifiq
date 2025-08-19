namespace ProvaPub.Interfaces.Providers
{
    public interface IDateTimeProvider
    {
        DateTime UtcNow { get; }
    }
}
