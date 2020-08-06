using System;
using System.Net.Http;
using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.OrderShipping
{
    public class UpsShippingService : IShippingService
    {
        public void Ship(Order order)
        {
            using (var client = new HttpClient())
            {
                // TODO implement DHL Shipping strategy
                Console.WriteLine("The order was shipped with UPS");
            }
        }
    }
}