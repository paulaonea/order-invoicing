using System;
using System.Collections.Generic;
using Strategy_Pattern_First_Look.Business.Models;
using Strategy_Pattern_First_Look.Business.Strategies.Invoice;
using Strategy_Pattern_First_Look.Business.Strategies.OrderShipping;
using Strategy_Pattern_First_Look.Business.Strategies.SalesTax;

namespace Strategy_Pattern_First_Look
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Select the country of origin: ");
            var origin = Console.ReadLine().Trim();
            
            Console.WriteLine("Select the destination cpuntry:  ");
            var destination = Console.ReadLine().Trim();


            var order = new Order
            {
                ShippingDetails = new ShippingDetails 
                { 
                    OriginCountry = origin,
                    DestinationCountry = destination
                },
                InvoiceService = GetInvoiceService(),
                ShippingService = GetShippingService(),
                SelectedPayments = new List<Payment>
                { new Payment
                    {
                        PaymentProvider = PaymentProvider.Invoice
                    } 
                }
            };

            order.LineItems.Add( new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m, ItemType.Literature), 1);
            order.LineItems.Add( new Item("CONSULTING","Building a website",100m, ItemType.Service), 1);


            try
            {
                order.FinaliseOrder();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            order.ShipOrder();
            
        }

        private static IShippingService GetShippingService()
        {
            while (true)
            {
                Console.WriteLine("Chose one of the following shipping providers:  ");
                Console.WriteLine("1. DHL");
                Console.WriteLine("2. FedEx");
                Console.WriteLine("3. UPS");
                var shippingProvider = int.Parse(Console.ReadLine().Trim());
                switch (shippingProvider)    
                {
                    case 1:
                        return new DhlShippingService();
                    case 2:
                        return new FedExShippingService();
                    case 3:
                        return new UpsShippingService();
                    default:
                        break;
                }
            }
            
        }

        private static InvoiceService GetInvoiceService()
        {
            while (true)
            {
                Console.WriteLine("Chose one of the following invoice delivery options: ");
                Console.WriteLine("1. Email");
                Console.WriteLine("2. File");
                Console.WriteLine("3. Print");
                var invoiceOption = int.Parse(Console.ReadLine().Trim());
                
                switch (invoiceOption)
                {
                    case 1:
                        return new EmailInvoiceService();
                    case 2:
                        return new FileInvoiceService();
                    case 3:
                        return new PrintInvoiceService();
                    default:
                        break;
                }
            }
        }
    }
}
