namespace MattDowning.EquityOrder.Domain.OrderEvents
{
    public interface IOrderPlaced
    {
        event OrderPlacedEventHandler OrderPlaced;
    }
}