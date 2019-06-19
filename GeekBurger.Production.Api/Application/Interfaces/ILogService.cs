namespace GeekBurger.Production.Application.Interfaces
{
    /// <summary>
    /// Log Service Interface
    /// </summary>
    public interface ILogService
    {   /// <summary>
        /// Send messages asynchronously
        /// </summary>
        /// <param name="message">message</param>
        void SendMessagesAsync(string message);
    }
}
