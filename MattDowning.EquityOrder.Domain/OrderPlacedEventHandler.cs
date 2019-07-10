namespace MattDowning.EquityOrder.Domain
{
    // Can put handler and event args in separate files, for maintenance I find it easier to have both in same file.

    public delegate void OrderPlacedEventHandler(OrderPlacedEventArgs e);

    public class OrderPlacedEventArgs
    {
        public OrderPlacedEventArgs(string equityCode, decimal price)
        {
            EquityCode = equityCode;
            Price = price;
        }
        public string EquityCode { get; }
        public decimal Price { get; }
    }
}