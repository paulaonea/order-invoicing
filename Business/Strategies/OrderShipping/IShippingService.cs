using Strategy_Pattern_First_Look.Business.Models;

namespace Strategy_Pattern_First_Look.Business.Strategies.OrderShipping
{
    public interface IShippingService
    {
        void Ship(Order order);
    }
}