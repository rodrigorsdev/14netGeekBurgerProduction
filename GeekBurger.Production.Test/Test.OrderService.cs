using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using Moq;
using Xunit;

using GeekBurger.Production.Application.Interfaces;
using GeekBurger.Production.Contract;

namespace GeekBurger.Production.Test
{
    public class TestOrderService
    {
        [Fact]
        public void ProductionAreaChanged()
        {
            var mock = new Mock<IOrderService>();

            mock.Setup(s => s.SendMessagesAsync());
        }

        [Fact]
        public void NewOrder()
        {
            var mock = new Mock<IOrderService>();
            var payload = new List<EntityEntry<Order>>();

            mock.Setup(s => s.AddToMessageList(payload));
        }
     
    }
}
