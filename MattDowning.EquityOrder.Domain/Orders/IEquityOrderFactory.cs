namespace MattDowning.EquityOrder.Domain.Orders
{
    public interface IEquityOrderFactory
    {
        IEquityOrder Create(OrderType orderType, string orderEquity, int quantity, decimal thresholdPrice);
    }
}