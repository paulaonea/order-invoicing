using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Strategy_Pattern_First_Look.Business.Strategies.Invoice;
using Strategy_Pattern_First_Look.Business.Strategies.OrderShipping;
using Strategy_Pattern_First_Look.Business.Strategies.SalesTax;

namespace Strategy_Pattern_First_Look.Business.Models
{
    public class Order
    {
        public Dictionary<Item, int> LineItems { get; } = new Dictionary<Item, int>();

        public IList<Payment> SelectedPayments { get; set; } = new List<Payment>();

        private IEnumerable<Payment> FinalizedPayments { get; } = new List<Payment>();

        private decimal AmountDue => TotalPrice - FinalizedPayments.Sum(payment => payment.Amount);

        public decimal TotalPrice => LineItems.Sum(item => item.Key.Price * item.Value);

        private ShippingStatus ShippingStatus { get; set; } = ShippingStatus.WaitingForPayment;

        public ShippingDetails ShippingDetails { get; set; }
        public Client Client { get; set; }

        private ISalesTax SalesTaxStrategy => GetSalesTaxStrategy();

        public InvoiceService InvoiceService { get; set; }
        public IShippingService ShippingService { get; set; }

        public decimal GetTax()
        {
            return SalesTaxStrategy?.GetTaxFor(this) ?? 
                   throw new Exception($"Invoice cannot be generated. Tax details are not set for tax region {this.ShippingDetails.DestinationCountry}");
        }

        public void FinaliseOrder()
        {
            if (SelectedPayments.Any(x => x.PaymentProvider == PaymentProvider.Invoice) && 
                AmountDue > 0 &&
                ShippingStatus == ShippingStatus.WaitingForPayment)
            {
                InvoiceService.GenerateInvoice(this);
                ShippingStatus = ShippingStatus.ReadyForShippment;
            }
            else if (AmountDue > 0 )
            {
                throw new Exception("Unable to finalise order");
            }
        }

        public void ShipOrder()
        {
            if (AmountDue != 0) return;
            ShippingService.Ship(this);
            ShippingStatus = ShippingStatus.Shipped;
        }
        
        private ISalesTax GetSalesTaxStrategy()
        {
            return ShippingDetails.DestinationCountry.ToLowerInvariant() switch
            {
                "sweden" => new SwedenSalesTax(),
                "us" => new USSalesTax(),
                "uk" => new UKSalesTax(),
                _ => throw new NotImplementedException()
            };
        }
    }

    public class Payment
    {
        public decimal Amount { get; set; }
        public PaymentProvider PaymentProvider { get; set; }
    }
    
}