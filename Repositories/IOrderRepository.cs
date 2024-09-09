using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// IOrderRepository
    /// </summary>
    public interface IOrderRepository
    {

        /// <summary>
        /// GetOrders
        /// </summary>
        /// <returns></returns>

        public List<Order> GetOrders();

        /// <summary>
        /// GetOrder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Order GetOrderById(int id);

        /// <summary>
        /// AddOrder
        /// </summary>
        /// <param name="addOrderDto"></param>

        public Order AddOrder(AddOrderDto addOrderDto);

        /// <summary>
        /// DeleteOrder
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrder(int id);

        /// <summary>
        /// UpdateOrder
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="UpdateOrderDto"></param>

        public void UpdateOrder(int orderId, UpdateOrderDto UpdateOrderDto);

    }

}
