using System;
using System.IO;

namespace MattDowning.EquityOrder.Domain.OrderEvents
{
    // Can put handler and event args in separate files, for maintenance I find it easier to have both in same file.

    public delegate void OrderErroredEventHandler(OrderErroredEventArgs e);

    public class OrderErroredEventArgs : ErrorEventArgs
    {
        public OrderErroredEventArgs(string equityCode, decimal price, Exception
            ex) : base(ex)
        {
            EquityCode = equityCode;
            Price = price;
        }
        public string EquityCode { get; }
        public decimal Price { get; }
    }
}