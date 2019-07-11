using System;
using System.Collections.Generic;
using System.Text;
using FakeItEasy;
using MattDowning.EquityOrder.Domain;
using MattDowning.EquityOrder.Domain.Orders;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace MattDowning.EquityOrder.Tests.Domain
{
    public class EquityOrderFactoryTests
    {
        private readonly string equityCode = "MSFT";
        private readonly decimal threshold = 5;
        private readonly int quantity = 100;
        private readonly ILogger loggerFake = A.Fake<ILogger>();
        private IOrderService orderServiceFake;

        [SetUp]
        public void SetUp()
        {
            orderServiceFake = A.Fake<IOrderService>();
        }

        [Test]
        public void EquityOrderFactory_CreatesBuyOrder()
        {
            EquityOrderFactory sut = new EquityOrderFactory(orderServiceFake, loggerFake);
            IEquityOrder equityOrder = sut.Create(OrderType.Buy, equityCode, quantity, threshold);
            Assert.IsInstanceOf<EquityOrderBuy>(equityOrder);
        }
    }
}
