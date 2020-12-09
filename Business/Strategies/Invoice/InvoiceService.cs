using System;
using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.Invoice
{
    public abstract class InvoiceService : IInvoiceService
    {
        public abstract void GenerateInvoice(Order order);

        protected static string GenerateTextInvoice(Order order)
        {
            var invoiceText = $"INVOICE DATE: {DateTimeOffset.Now}{Environment.NewLine}";
            invoiceText += $"ID | NAME | PRICE | Quantity{Environment.NewLine}";
            foreach (var (item, quantity) in order.LineItems)
            {
                invoiceText += $"{item.Id} | {item.Name} | {item.Price} | {quantity}{Environment.NewLine}";
            }

            invoiceText += Environment.NewLine + Environment.NewLine;
            invoiceText += $"TOTAL NET: {order.TotalPrice}{Environment.NewLine}";
            invoiceText += $"TAX TOTAL: {order.GetTax()}{Environment.NewLine}";
            invoiceText += $"TOTAL: {order.TotalPrice + order.GetTax()}{Environment.NewLine}";

            return invoiceText;
        }
    }
}