using System;
using FakeItEasy;
using MattDowning.EquityOrder.Domain;
using MattDowning.EquityOrder.Domain.Orders;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace MattDowning.EquityOrder.Tests.Domain
{
    public class EquityOrderBuyTests
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
        public void EquityOrderBuy_WhenPriceBelowThreshold_OrderPlaced()
        {
            IEquityOrder sut = new EquityOrderBuy(orderServiceFake, equityCode, quantity, threshold, loggerFake);
            sut.ReceiveTick(equityCode, 4);
            A.CallTo(() => orderServiceFake.Buy(equityCode, quantity, 4)).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void EquityOrderBuy_WhenPriceAboveThreshold_NoOrder()
        {
            IEquityOrder sut = new EquityOrderBuy(orderServiceFake, equityCode, quantity, threshold, loggerFake);
            sut.ReceiveTick(equityCode, 6);
            A.CallTo(() => orderServiceFake.Buy(equityCode, quantity, 4)).MustNotHaveHappened();
        }

        [Test]
        public void EquityOrderBuy_WhenTwoCallsBelowThreshold_OnlyOneOrderPlaced()
        {
            IEquityOrder sut = new EquityOrderBuy(orderServiceFake, equityCode, quantity, threshold, loggerFake);
            sut.ReceiveTick(equityCode, 4);
            sut.ReceiveTick(equityCode, 3);
            A.CallTo(() => orderServiceFake.Buy(equityCode, quantity, 4)).MustHaveHappenedOnceExactly();
        }
    }
}
