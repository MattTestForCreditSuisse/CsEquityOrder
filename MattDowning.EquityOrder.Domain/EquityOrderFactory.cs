using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace MattDowning.EquityOrder.Domain
{
    public class EquityOrderFactory
    {
        private readonly IOrderService orderService;
        private readonly ILogger logger;

        public EquityOrderFactory(IOrderService orderService, ILogger logger)
        {
            this.orderService = orderService;
            this.logger = logger;
        }
        public IEquityOrder Create(OrderType orderType, string orderEquity, int quantity, decimal thresholdPrice)
        {
            // KISS: the requirement only has one order type.
            return new EquityOrderBuy(orderService, orderEquity, quantity, thresholdPrice, logger);
        }
    }
}
