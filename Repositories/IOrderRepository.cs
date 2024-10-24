using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// Defines the contract for order management operations.
    /// </summary>

    public interface IOrderRepository
    {

        /// <summary>
        /// Retrieves a list of all orders.
        /// </summary>
        /// <returns>A list of <see cref="Order"/> objects representing all orders.</returns>

        public List<Order> GetOrders();

        /// <summary>
        /// Retrieves a specific order by its user ID.
        /// </summary>
        /// <param name="UserId">The ID of the user whose order is to be retrieved.</param>
        /// <returns>An <see cref="Order"/> object representing the order for the specified user.</returns>

        public List<Order> GetOrderById(int UserId);

        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to be deleted.</param>

        public void DeleteOrder(int id);

        /// <summary>
        /// Places a new order for a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user placing the order.</param>
        /// <returns>An <see cref="OrderDT0"/> object representing the placed order.</returns>

        OrderDT0 PlaceOrderByUserId(int userId);

    }

}
