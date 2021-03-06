using System;
using System.IO;
using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.Invoice
{
    public class FileInvoiceService : InvoiceService
    {
        public override void GenerateInvoice(Order order)
        {
            var textInvoice = GenerateTextInvoice(order);
            using (var stream = new StreamWriter($"invoice_{Guid.NewGuid()}.txt"))    
            {
                stream.Write(textInvoice);
                stream.Flush();
            }
        }
    }
}