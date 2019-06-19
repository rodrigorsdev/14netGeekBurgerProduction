using System;

using Moq;
using Xunit;

using GeekBurger.Production.Application.Interfaces;

namespace GeekBurger.Production.Test
{
    /// <summary>
    /// Test class for the log service
    /// </summary>
    public class TestLogService
    {
        [Fact]
        public void SendMessagesAsync()
        {
            var mock = new Mock<ILogService>();

            mock.Setup(s => s.SendMessagesAsync("Sample message"));
            //Assert.Equal("Jignesh", "");
        }
    }
}
