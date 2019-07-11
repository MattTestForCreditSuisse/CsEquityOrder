using MattDowning.EquityOrder.Domain.OrderEvents;

namespace MattDowning.EquityOrder.Domain.Orders
{
    public interface IEquityOrder : IOrderPlaced, IOrderErrored
    {
        void ReceiveTick(string equityCode, decimal price);
    }
}
