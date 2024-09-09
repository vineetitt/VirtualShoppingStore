using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderItemDto;

namespace VirtualShoppingStore.Repositories
{


    /// <summary>
    /// 
    /// </summary>
    public interface IOrderItemRepository
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Orderitem> GetOrderitemsByOrderId(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="addOrderItemDto"></param>
        public void AddOrderitem(AddOrderItemDto addOrderItemDto);

        /// <summary>
        /// UpdateOrderItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateOrderItem"></param>
        public Orderitem UpdateOrderItem(int id,UpdateOrderItemDto updateOrderItem );

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrderitem(int id);

        /// <summary>
        /// ShowAllOrderItem
        /// </summary>
        /// <returns></returns>
        
        public List<Orderitem> ShowAllOrderItem();
        
    }

}
