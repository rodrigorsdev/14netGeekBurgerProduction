using GeekBurger.Production.Api.Controllers;
using GeekBurger.Production.Application.Interfaces;
using GeekBurger.Production.Interface;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace GeekBurger.Production.Test
{
    /// <summary>
    /// Test class for the production controller
    /// </summary>
    public class TestProductionController
    {
        [Fact]
        public void GetAreas()
        {
            var mock = GetController();

            mock.Setup(m => m.GetAreas());
        }

        [Fact]
        public void AddArea()
        {
            var mock = GetController();

            var payload = new Contract.Production
            {
                ProductionId = Guid.NewGuid(),
                Restrictions = new List<string> { "Leite", "Soja" }
            };

            mock.Setup(m => m.AddArea(payload));
        }

        [Fact]
        public void UpdateArea()
        {
            var mock = GetController();

            var payload = new Contract.Production
            {
                ProductionId = Guid.NewGuid(),
                Restrictions = new List<string> { "Leite", "Soja" }
            };

            mock.Setup(m => m.UpdateArea(payload));
        }

        [Fact]
        public void NewOrder()
        {
            var mock = GetController();
        }

        [Fact]
        public void UpdateOrder()
        {
            var mock = GetController();
        }

        /// <summary>
        /// Get the mocked production controller
        /// </summary>
        /// <returns></returns>
        private Mock<ProductionController> GetController()
        {
            // Mock the dependency injection items
            var productRepository = new Mock<IProductionRepository>();
            var logService = new Mock<ILogService>();

            var mock = new Mock<ProductionController>(productRepository.Object, logService.Object);

            return mock;
        }
    }
}
