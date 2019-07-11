using Microsoft.Extensions.Logging;

namespace MattDowning.EquityOrder.Domain.Orders
{
    public class EquityOrderBuy : EquityOrder
    {
        public EquityOrderBuy(IOrderService orderService, string orderEquity, int quantity, decimal thresholdPrice, ILogger logger) 
            : base(orderService, orderEquity, quantity, thresholdPrice, logger)
        {
        }

        protected override bool CanExecute(decimal price)
        {
            return price < thresholdPrice;
        }

        protected override void Execute(decimal price)
        {
            orderService.Buy(orderEquity, quantity, price);
        }
    }
}