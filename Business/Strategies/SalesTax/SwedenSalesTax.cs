using System;
using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.SalesTax
{
    public class SwedenSalesTax : ISalesTax
    {
        public decimal GetTaxFor(Order order)
        {
            var totalTax = 0m;
            if (string.Equals(order.ShippingDetails.OriginCountry,
                order.ShippingDetails.DestinationCountry,
                StringComparison.InvariantCultureIgnoreCase))
            {
                foreach (var (item, quantity) in order.LineItems)
                {
                    totalTax += item.ItemType switch
                    {
                        ItemType.Food => (item.Price * 0.06m) * quantity,
                        ItemType.Literature => (item.Price * 0.08m) * quantity,
                        ItemType.Service => (item.Price * 0.2m) * quantity,
                        ItemType.Hardware => (item.Price * 0.25m) * quantity,
                        _ => (item.Price * 0.1m) * quantity
                    };
                }
            }
            return totalTax;
        }
    }
}