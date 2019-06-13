namespace GeekBurger.Production.Application.Interfaces
{
    /// <summary>
    /// Log Service Interface
    /// </summary>
    public interface ILogService
    {
        void SendMessagesAsync(string message);
    }
}
