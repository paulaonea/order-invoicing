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
            var order = new Order
            {
                ShippingDetails = new ShippingDetails
                {
                    OriginCountry = GetOriginCountry(),
                    DestinationCountry = GetDestinationCountry()
                },
                InvoiceService = GetInvoiceService(),
                ShippingService = GetShippingService(),
                SelectedPayments = new List<Payment>
                {
                    new Payment
                    {
                        PaymentProvider = PaymentProvider.Invoice
                    }
                }
            };

            order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m, ItemType.Literature), 1);
            order.LineItems.Add(new Item("CONSULTING", "Building a website", 100m, ItemType.Service), 1);


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

        private static string GetDestinationCountry()
        {
            Console.WriteLine("Select the destination country:  ");
            var destination = Console.ReadLine().Trim();
            return destination;
        }

        private static string GetOriginCountry()
        {
            Console.WriteLine("Select the country of origin: ");
            var origin = Console.ReadLine().Trim();
            return origin;
        }

        private static DeliveryServiceOptions GetDeliveryService()
        {
            Console.WriteLine("Chose one of the following shipping providers: \n 1. DHL\n 2. FedEx\n 3. UPS ");
            while (true)
            {
                if (int.TryParse(Console.ReadLine().Trim(), out var deliveryChoice) &
                    Enum.IsDefined(typeof(DeliveryServiceOptions), deliveryChoice))
                {
                    return (DeliveryServiceOptions) deliveryChoice;
                }

                Console.WriteLine("Not an acceptable option, choose again.");
            }
        }

        private static IShippingService GetShippingService()
        {
            return GetDeliveryService() switch
            {
                DeliveryServiceOptions.DHL => new DhlShippingService(),
                DeliveryServiceOptions.FedEx => new FedExShippingService(),
                DeliveryServiceOptions.Ups => new UpsShippingService(),
                _ => throw new NotImplementedException()
            };
        }

        private static InvoiceService GetInvoiceService()
        {
            return ReadDeliveryType() switch
            {
                InvoiceServiceOptions.Email => new EmailInvoiceService(),
                InvoiceServiceOptions.File => new FileInvoiceService(),
                InvoiceServiceOptions.Print => new PrintInvoiceService(),
                _ => throw new NotImplementedException()
            };
        }

        private static InvoiceServiceOptions ReadDeliveryType()
        {
            Console.WriteLine("Chose one of the following invoice delivery options: " +
                              "\n 1. Email \n 2. File \n 3. Print");
            while (true)
            {
                if (int.TryParse(Console.ReadLine().Trim(), out var invoiceOption) &
                    Enum.IsDefined(typeof(InvoiceServiceOptions), invoiceOption))
                {
                    return (InvoiceServiceOptions)invoiceOption;
                }
                Console.WriteLine("Not an acceptable option, choose again.");
            }
        }
    }
}
