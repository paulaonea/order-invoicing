using System;
using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.SalesTax
{
    public class SwedenSalesTax : ISalesTax
    {
        public decimal GetTaxFor(Order order)
        {
            var origin = order.ShippingDetails.OriginCountry.ToLowerInvariant();
            var destination = order.ShippingDetails.DestinationCountry.ToLowerInvariant();
            if (origin == destination)
            {
                var totalTax = 0m;
                foreach (var (item, units) in order.LineItems)
                {
                    switch (item.ItemType)
                    {
                        case ItemType.Food:
                            totalTax += (item.Price * 0.06m) * units;
                            break;

                        case ItemType.Literature:
                            totalTax += (item.Price * 0.08m) * units;
                            break;

                        case ItemType.Service:
                        case ItemType.Hardware:
                            totalTax += (item.Price * 0.25m) * units;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }

                return totalTax;
            }
            else
            {
                return 0m;
            }
        }
    }
}