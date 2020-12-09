using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.SalesTax
{
    public class USSalesTax : ISalesTax
    {
        public decimal GetTaxFor(Order order)
        {
            return order.ShippingDetails.DestinationState.ToLowerInvariant() switch
            {
                "la" => order.TotalPrice * 0.095m,
                "ny" => order.TotalPrice * 0.04m,
                "nyc" => order.TotalPrice * 0.045m,
                _ => 0m
            };
        }
    }
}