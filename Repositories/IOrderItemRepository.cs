using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderItemDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// Defines the contract for operations related to order items in the VirtualShoppingStore application.
    /// </summary>
    public interface IOrderItemRepository
    {

        /// <summary>
        /// Retrieves a list of order items associated with a specified order ID.
        /// </summary>
        /// <param name="id">The ID of the order for which to retrieve order items.</param>
        /// <returns>A list of order items that belong to the specified order ID.</returns>
        /// <response code="200">Returns the list of order items for the specified order ID.</response>
        /// <response code="404">If no order items are found for the specified order ID.</response>
        public List<Orderitem> GetOrderItemsByOrderId(int id);

        /// <summary>
        /// Adds a new order item to the system based on the provided details.
        /// </summary>
        /// <param name="addOrderItemDto">The data transfer object containing the details of the order item to be added.</param>
        /// <returns>Asynchronous operation indicating the result of the add operation.</returns>
        /// <response code="200">If the order item was added successfully.</response>
        /// <response code="400">If there is a problem with the input data, such as an invalid order ID or product ID.</response>
        public void AddOrderItem(AddOrderItemDto addOrderItemDto);

        /// <summary>
        /// Retrieves a list of all order items in the system.
        /// </summary>
        /// <returns>A list of all order items in the system.</returns>
        /// <response code="200">Returns the list of all order items.</response>
        /// <response code="404">If no order items are found in the system.</response>
        public List<Orderitem> ShowAllOrderItem();

        public List<Orderitem> GetOrderItemsByUserId(int userId);
    }

}

