using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.Invoice
{
    public interface IInvoice
    {
        public void GenerateInvoice(Order order);
    }
}