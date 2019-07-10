using System;
using System.Collections.Generic;
using System.Text;

namespace MattDowning.EquityOrder.Domain
{
    public interface IOrderService
    {
        void Buy(string equityCode, int quantity, decimal price);
        void Sell(string equityCode, int quantity, decimal price);
    }
}
