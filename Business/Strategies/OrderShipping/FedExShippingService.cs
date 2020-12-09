using System;
using System.Net.Http;
using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.OrderShipping
{
    public class FedExShippingService : IShippingService
    {
        public void Ship(Order order)
        {
            using var client = new HttpClient();
            Console.WriteLine("The order was shipped with FedEx");
        }
    }
}