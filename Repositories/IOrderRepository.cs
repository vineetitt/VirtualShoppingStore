using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrders();



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order ShowOrderById(int id);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="addOrderDto"></param>

        public Order AddOrder(AddOrderDto addOrderDto);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void DeleteOrder(int id);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="patchOrderDto"></param>
        public void PatchOrder(int orderId, PatchOrderDto patchOrderDto);



       


    }


    
}
