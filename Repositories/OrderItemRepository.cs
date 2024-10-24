using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderItemDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// Handles HTTP requests related to order items in the VirtualShoppingStore application.
    /// </summary>

    public class OrderItemRepository:IOrderItemRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemRepository"/> class.
        /// </summary>
        /// <param name="virtualShoppingStore">The database context used for accessing the database.</param>

        public OrderItemRepository(VirtualShoppingStoreDbContext virtualShoppingStore)
        {
            this.virtualShoppingStore = virtualShoppingStore;
        }

        /// <summary>
        /// Retrieves a list of order items for a specified order ID.
        /// </summary>
        /// <param name="id">The ID of the order for which to retrieve order items.</param>
        /// <returns>A list of order items for the specified order ID.</returns>
        /// <response code="200">Returns the list of order items.</response>
        /// <response code="404">If no order items are found for the specified order ID.</response>

        public List<Orderitem> GetOrderItemsByOrderId(int id)
        {

            var orderExists= virtualShoppingStore.Orderitems.Any(orderitem=>orderitem.OrderId == id);

            if (!orderExists)
            {
                throw new CustomException("order item not  found",404);
            }

            var orderitems= virtualShoppingStore.Orderitems.Where(orderitem=>orderitem.OrderId==id).ToList();
            return orderitems;

        }

        /// <summary>
        /// Adds a new order item to the system.
        /// </summary>
        /// <param name="addOrderItemDto">The data transfer object containing the details of the order item to add.</param>
        /// <returns>HTTP response indicating the result of the add operation.</returns>
        /// <response code="200">If the order item was added successfully.</response>
        /// <response code="400">If there is a problem with the input data.</response>
        
        public void AddOrderItem(AddOrderItemDto addOrderItemDto)
        {

            var orderExists = virtualShoppingStore.Orders.Any(order => order.OrderId == addOrderItemDto.OrderID);

            if (!orderExists)
            {
                throw new Exception("Order not found.");
            }

            var productExists = virtualShoppingStore.Products.Any(product => product.ProductId == addOrderItemDto.ProductID);

            if (!productExists)
            {
                throw new CustomException("Product not found.",400);
            }

            var orderItem = new Orderitem()
            {
                OrderId = addOrderItemDto.OrderID,
                ProductId = addOrderItemDto.ProductID,
                Quantity = addOrderItemDto.Quantity,
                Price = addOrderItemDto.Price
            };

            virtualShoppingStore.Orderitems.Add(orderItem);
            virtualShoppingStore.SaveChanges();

        }

        /// <summary>
        /// Retrieves a list of all order items in the system.
        /// </summary>
        /// <returns>A list of all order items.</returns>
        /// <response code="200">Returns the list of all order items.</response>

        public List<Orderitem> ShowAllOrderItem()
        {

            var orderitems= virtualShoppingStore.Orderitems.ToList();

            if(orderitems.Count == 0)
            {
                throw new CustomException("No order items found",404);
            }
            return orderitems;

        }

        public List<Orderitem> GetOrderItemsByUserId(int userId)
        {

            var orderItems = virtualShoppingStore.Orderitems.Include(o => o.Order).Include(o=>o.Order.Status).Where(orderitem => orderitem.Order.UserId == userId).ToList();

            if (orderItems.Count() == 0)
            {
                throw new CustomException("order item not  found", 404);
            }

            return orderItems;

        }
    }

}
