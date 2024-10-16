using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderDto;
using VirtualShoppingStore.Models.DTO.OrderItemDto;
using VirtualShoppingStore.Models.DTO.StatusDto;
using VirtualShoppingStore.Enum;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// Repository class responsible for managing orders in the database.
    /// </summary>

    public class OrderRepository:IOrderRepository
    {

        private readonly VirtualShoppingStoreDbContext virtualShoppingStore;
        private readonly ICartItemRepository cartItemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderRepository"/> class.
        /// </summary>
        /// <param name="virtualShoppingStore">The database context to interact with the database.</param>
        /// <param name="cartItemRepository">The repository to manage cart items.</param>

        public OrderRepository(VirtualShoppingStoreDbContext virtualShoppingStore, ICartItemRepository cartItemRepository)
        {
            this.virtualShoppingStore = virtualShoppingStore;
            this.cartItemRepository = cartItemRepository;
        }

        /// <summary>
        /// Retrieves all orders from the database.
        /// </summary>
        /// <returns>A list of <see cref="Order"/> objects.</returns>
        /// <exception cref="CustomException">Thrown if no orders are found.</exception>

        public List<Order> GetOrders()
        {

            var data = virtualShoppingStore.Orders.ToList();

            if (data.Count==0)
            {
                throw new CustomException("Order not found", 404);
                
            }

            return data;

        }

        /// <summary>
        /// Retrieves the order details for a specific user based on the user's ID.
        /// </summary>
        /// <param name="UserId">The ID of the user whose orders are being retrieved.</param>
        /// <returns>The order associated with the given user.</returns>
        /// <exception cref="CustomException">Thrown if no order is found for the provided user ID.</exception>

        public Order GetOrderById(int UserId)
        {

            var orderlist = virtualShoppingStore.Orders
                //.Include(id=>id.StatusId)
                .FirstOrDefault(order => order.UserId == UserId);

            if (orderlist==null)
            {
                throw new CustomException("Order not found",404);
            }
           
            return orderlist;
            
        }

        /// <summary>
        /// Deletes an order and its associated order items from the database.
        /// Updates the stock quantity for each product in the order.
        /// </summary>
        /// <param name="id">The ID of the order to be deleted.</param>
        /// <exception cref="CustomException">Thrown if the order or associated items are not found.</exception>

        public void DeleteOrder(int id)
        {

            var order = virtualShoppingStore.Orders.FirstOrDefault(order => order.OrderId == id);

            if (order==null)
            {
                throw new CustomException("Order not found", 404);
            }

            var orderitem = virtualShoppingStore.Orderitems.Where(orderitem => orderitem.OrderId == id).ToList();

            foreach (var orderItem in orderitem)
            {
               
                var product = virtualShoppingStore.Products.FirstOrDefault(product => product.ProductId == orderItem.ProductId);

                if (product != null)
                {
                    
                    product.StockQuantity += orderItem.Quantity;
                }

                virtualShoppingStore.Orderitems.Remove(orderItem);
                virtualShoppingStore.SaveChanges();

            }

        }

        /// <summary>
        /// Places an order for a user based on their cart items and user ID.
        /// Deducts the stock for the ordered products and clears the cart items.
        /// </summary>
        /// <param name="userId">The ID of the user placing the order.</param>
        /// <returns>An <see cref="OrderDT0"/> object containing the order details.</returns>
        /// <exception cref="CustomException">Thrown if the user or products are invalid, or if there is insufficient stock.</exception>
        /// <exception cref="Exception">Thrown if the cart is empty or another general error occurs.</exception>

        public OrderDT0 PlaceOrderByUserId(int userId)
        {

            var isUserValid = virtualShoppingStore.Users.Any(user => user.UserId == userId);

            if (!isUserValid)
            {
                throw new CustomException("User with this userId not found", 404);
            }

            var cartItems = cartItemRepository.GetCartByUserId(userId);

            if (cartItems == null || !cartItems.Any())
            {
                throw new CustomException("No items in the cart",404);
            }

            foreach (var cartEntry in cartItems)
            {

                var product = virtualShoppingStore.Products.FirstOrDefault(product => product.ProductId == cartEntry.ProductId);

                if (product == null)
                {
                    throw new CustomException($"Product with ID {cartEntry.ProductId} not found", 404);
                }

                if (product.StockQuantity < cartEntry.Quantity)
                {
                    throw new CustomException($"Not enough stock for product {product.ProductName}. Available: {product.StockQuantity}, Requested: {cartEntry.Quantity}", 400);
                }

            }

            var totalAmount = cartItems.Sum(cartitem => cartitem.TotalAmount ?? 0);

            var order = new Order()
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = totalAmount,
                StatusId = (int)Enum.OrderStatus.Pending
            };

            virtualShoppingStore.Orders.Add(order);
            virtualShoppingStore.SaveChanges();

            foreach (var cartEntry in cartItems)
            {
                var product = virtualShoppingStore.Products.FirstOrDefault(product => product.ProductId == cartEntry.ProductId);

                product!.StockQuantity -= cartEntry.Quantity;

                var orderItem = new Orderitem
                {
                    OrderId = order.OrderId,
                    Quantity = cartEntry.Quantity,
                    ProductId = cartEntry.ProductId,
                    Price = cartEntry.TotalAmount ?? 0
                };

                virtualShoppingStore.Orderitems.Add(orderItem);
            }

            virtualShoppingStore.SaveChanges();

            var orderDTO = virtualShoppingStore.Orders
                .Where(o => o.OrderId == order.OrderId)
                .Include(o => o.Status)
                .Include(o => o.Orderitems)
                .ThenInclude(oi => oi.Product)
                .Select(o => new OrderDT0
                {
                    OrderId = o.OrderId,
                    UserId = o.UserId ?? 0,
                    OrderDate = o.OrderDate,
                    TotalAmount = o.TotalAmount,
                    Status = new StatusDTO
                    {
                        StatusId = o.Status!.StatusId,
                        StatusName = o.Status.StatusName
                    },
                    OrderItems = o.Orderitems.Select(oi => new OrderItemDTO
                    {
                        OrderItemId = oi.OrderItemId,
                        ProductId = oi.Product!.ProductId,
                        ProductName = oi.Product.ProductName,
                        Quantity = oi.Quantity,
                        Price = oi.Price
                    }).ToList()
                }).FirstOrDefault();

            virtualShoppingStore.Cartitems.RemoveRange(cartItems);
            virtualShoppingStore.SaveChanges();

            return orderDTO!;

        }

    }

}
