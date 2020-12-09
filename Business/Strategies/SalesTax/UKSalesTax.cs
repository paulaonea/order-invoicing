using Strategy_Pattern_First_Look.Business.Strategies.SalesTax;

namespace Strategy_Pattern_First_Look.Business.Models
{
    public class UKSalesTax : ISalesTax
    {
        public decimal GetTaxFor(Order order)
        {
            var totalTax = 0m;
            if (order.ShippingDetails.DestinationCountry == "uk")
            {
                foreach (var (item, quantity) in order.LineItems)
                {
                    totalTax += item.ItemType switch
                    {
                        ItemType.Food => (item.Price * 0.05m) * quantity,
                        _ => (item.Price * 0.2m) * quantity
                    };
                }
            }
            return totalTax;
        }
    }
}