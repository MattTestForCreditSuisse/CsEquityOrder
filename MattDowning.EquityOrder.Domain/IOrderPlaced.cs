namespace MattDowning.EquityOrder.Domain
{
    public interface IOrderPlaced
    {
        event OrderPlacedEventHandler OrderPlaced;
    }
}