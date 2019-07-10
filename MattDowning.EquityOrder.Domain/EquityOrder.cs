using System;
using Microsoft.Extensions.Logging;

namespace MattDowning.EquityOrder.Domain
{
    public abstract class EquityOrder : IEquityOrder
    {
        protected readonly ILogger logger;
        protected readonly IOrderService orderService;
        protected readonly string orderEquity;
        protected readonly decimal thresholdPrice;
        protected readonly int quantity;
        protected bool orderPlaced = false;
        public event OrderPlacedEventHandler OrderPlaced;
        public event OrderErroredEventHandler OrderErrored;

        protected abstract bool CanExecute(decimal price);
        protected abstract void Execute(decimal price);

        protected EquityOrder(IOrderService orderService, string orderEquity, int quantity, decimal thresholdPrice, ILogger logger)
        {
            this.orderService = orderService;
            this.orderEquity = orderEquity;
            this.thresholdPrice = thresholdPrice;
            this.logger = logger;
            this.quantity = quantity;

            logger.Log(LogLevel.Information, $"{DateTime.Now} EquityOrder created for {quantity} of {orderEquity} at thresholdPrice {thresholdPrice}");
        }

        public void ReceiveTick(string equityCode, decimal price)
        {
            logger.Log(LogLevel.Debug, $"{DateTime.Now} ReceiveTick for {equityCode} at {price}");

            if (!orderPlaced && (orderEquity == equityCode) && CanExecute(price))
            {
                logger.Log(LogLevel.Information, $"{DateTime.Now} Order placed for {equityCode} at {price}");
                orderPlaced = true;
                try
                {
                    Execute(price);
                    OrderPlaced?.Invoke(new OrderPlacedEventArgs(equityCode, price));
                    logger.Log(LogLevel.Information, $"{DateTime.Now} Order placed success.");
                }
                catch (Exception ex)
                {
                    OrderErrored?.Invoke(new OrderErroredEventArgs(equityCode, price, ex));
                    logger.Log(LogLevel.Error, $"{DateTime.Now} Order placed errored: {ex.Message}");
                }
            }
        }
    }
}