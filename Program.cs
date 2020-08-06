using System;
using Strategy_Pattern_First_Look.Business.Models;
using Strategy_Pattern_First_Look.Business.Strategies.Invoice;
using Strategy_Pattern_First_Look.Business.Strategies.SalesTax;

namespace Strategy_Pattern_First_Look
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new Order
            {
                ShippingDetails = new ShippingDetails 
                { 
                    OriginCountry = "Sweden",
                    DestinationCountry = "Sweden"
                }
            };
            var destination = order.ShippingDetails.DestinationCountry.ToLowerInvariant();
            switch (destination)
            {
                case "sweden":
                    order.SalesTaxStrategy = new SwedenSalesTax();
                    break;
                case "us":
                    order.SalesTaxStrategy = new USSalesTax();
                    break;
            }
            order.InvoiceService = new FileInvoiceService();
            
            order.LineItems.Add( new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m, ItemType.Literature), 1);
            order.LineItems.Add( new Item("CONSULTING","Building a website",100m, ItemType.Service), 1);
            
            order.SelectedPayments.Add(new Payment
            {
                PaymentProvider = PaymentProvider.Invoice
            });

            order.FinaliseOrder();
            
        }
    }
}
