using System;
using FakeItEasy;
using MattDowning.EquityOrder.Domain;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Serialization;
using NUnit.Framework;

namespace MattDowning.EquityOrder.Tests.Domain
{
    public class EquityOrderTests
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
        public void EquityOrder_WhenPriceBelowThreshold_OrderPlaced()
        {
            IEquityOrder sut = new EquityOrderFactory(orderServiceFake, loggerFake).Create(OrderType.Buy, equityCode, quantity, threshold);
            sut.ReceiveTick(equityCode, 4);
            A.CallTo(() => orderServiceFake.Buy(equityCode, quantity, 4)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void EquityOrder_WhenPriceAboveThreshold_NoOrder()
        {
            IEquityOrder sut = new EquityOrderFactory(orderServiceFake, loggerFake).Create(OrderType.Buy, equityCode, quantity, threshold);
            sut.ReceiveTick(equityCode, 6);
            A.CallTo(() => orderServiceFake.Buy(equityCode, quantity, 4)).MustNotHaveHappened();
        }

        [Test]
        public void EquityOrder_WhenTwoCallsBelowThreshold_OnlyOneOrderPlaced()
        {
            IEquityOrder sut = new EquityOrderFactory(orderServiceFake, loggerFake).Create(OrderType.Buy, equityCode, quantity, threshold);
            sut.ReceiveTick(equityCode, 4);
            sut.ReceiveTick(equityCode, 3);
            A.CallTo(() => orderServiceFake.Buy(equityCode, quantity, 4)).MustHaveHappenedOnceExactly();
        }
    }
}
