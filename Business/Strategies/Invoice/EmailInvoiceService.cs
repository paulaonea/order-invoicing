using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.Invoice
{
    public class EmailInvoiceService : InvoiceService
    {
        public override void GenerateInvoice(Order order)
        {
            var body = GenerateTextInvoice(order);
            const string user = "**";
            const string pass = "**";
            using (SmtpClient client = new SmtpClient("smtp.sendgrid.net", 587))            
            {
                var credentials = new NetworkCredential(user, pass);
                client.Credentials = credentials;
                
                var email = new MailMessage("accounts@gmail.com", order.Client.Email)
                {
                    Subject = "We have created an invoice for your order",
                    Body = body
                };
                
                client.Send(email);
            }
        }
    }
}