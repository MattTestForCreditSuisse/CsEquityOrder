namespace MattDowning.EquityOrder.Domain.OrderEvents
{
    public interface IOrderErrored
    {
        event OrderErroredEventHandler OrderErrored;
    }
}