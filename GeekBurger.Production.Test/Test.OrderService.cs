using System;

using Moq;
using Xunit;

using GeekBurger.Production.Application.Interfaces;

namespace GeekBurger.Production.Test
{
    public class TestOrderService
    {
        [Fact]
        public void ProductionAreaChanged()
        {
            var mock = new Mock<IOrderService>();

            mock.Setup(s => s.ProductionAreaChanged());
        }

        [Fact]
        public void NewOrder()
        {
            var mock = new Mock<IOrderService>();

            mock.Setup(s => s.NewOrder());
        }


        [Fact]
        public void OrderChanged()
        {
            var mock = new Mock<IOrderService>();

            mock.Setup(s => s.OrderChanged());
        }

        [Fact]
        public void WaitOrderChanged()
        {
            var mock = new Mock<IOrderService>();

            mock.Setup(s => s.WaitOrderChanged());
        }
    }
}
