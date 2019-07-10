namespace MattDowning.EquityOrder.Domain
{
    public interface IOrderErrored
    {
        event OrderErroredEventHandler OrderErrored;
    }
}