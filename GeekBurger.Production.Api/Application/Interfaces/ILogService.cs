namespace GeekBurger.Production.Application.Interfaces
{
    public interface ILogService
    {
        void SendMessagesAsync(string message);
    }
}
