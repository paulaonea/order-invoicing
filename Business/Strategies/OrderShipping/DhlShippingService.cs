using System;
using System.Net.Http;
using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.OrderShipping
{
    public class DhlShippingService : IShippingService
    {
        public void Ship(Order order)
        {
            // to be implemented
            Console.WriteLine("The order was shipped with DHL");
         }
    }
}