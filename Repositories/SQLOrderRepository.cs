using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    public class SQLOrderRepository:IOrderRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStore"></param>
        public SQLOrderRepository(VirtualShoppingStoreDbContext virtualShoppingStore)
        {
            this.virtualShoppingStore = virtualShoppingStore;
        }

       
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Order> GetOrders()
        {
            var data = virtualShoppingStore.Orders.ToList();

            if (data != null)
            {
                return data;
            }

            throw new Exception("No order founds");
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Order ShowOrderById(int id)
        {
            var orderlist = virtualShoppingStore.Orders.FirstOrDefault(x => x.OrderId == id);

            if (orderlist==null){
                throw new Exception("No such data found with the given id");
            }
           
            return orderlist;
            
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="addOrderDto"></param>
        /// <exception cref="Exception"></exception>
        public Order AddOrder(AddOrderDto addOrderDto)

        {
            

            var userExists = virtualShoppingStore.Users.Any(x => x.UserId == addOrderDto.UserId);
            if (!userExists)
            {
                throw new Exception("User does not exist.");
            }

            var order = new Order
            {
                UserId = addOrderDto.UserId,
                OrderDate = addOrderDto.OrderDate,
                StatusId = 1, 
                Orderitems = new List<Orderitem>(),
                TotalAmount = 0 
            };

            foreach (var item in addOrderDto.Orderitems)
            {
                var product = virtualShoppingStore.Products.FirstOrDefault(p => p.ProductId == item.ProductID);
                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductID} does not exist.");
                }
                if (item.Quantity > product.StockQuantity)
                {
                    throw new Exception($"Insufficient stock for product {product.ProductName}.");
                }

                product.StockQuantity -= item.Quantity;
                item.Price = product.Price;

                var orderItem = new Orderitem
                {
                    ProductId = item.ProductID,
                    Quantity = item.Quantity,
                    Price = product.Price
                };

                order.Orderitems.Add(orderItem);
                order.TotalAmount += item.Price * item.Quantity; 
            }

            virtualShoppingStore.Orders.Add(order);
            virtualShoppingStore.SaveChanges();
            return order;
           

        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteOrder(int id)
        {
            var order = virtualShoppingStore.Orders.FirstOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                throw new Exception("Order not found.");
            }
            else
            {
                virtualShoppingStore.Orders.Remove(order);
                virtualShoppingStore.SaveChanges();
            }
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="patchOrderDto"></param>
        public void PatchOrder(int orderId, PatchOrderDto patchOrderDto)
        {
            
            var existingOrder = virtualShoppingStore.Orders
                .Include(o => o.Orderitems)
                .FirstOrDefault(o => o.OrderId == orderId);

            if (existingOrder == null)
            {
                throw new Exception("Order does not exist.");
            }

            
            if (patchOrderDto.OrderDate.HasValue)
            {
                existingOrder.OrderDate = patchOrderDto.OrderDate.Value;
            }

            if (patchOrderDto.StatusId.HasValue)
            {
                existingOrder.StatusId = patchOrderDto.StatusId.Value;
            }

            
            if (patchOrderDto.Orderitems != null)
            {
                foreach (var itemDto in patchOrderDto.Orderitems)
                {
                    var existingOrderItem = existingOrder.Orderitems
                        .FirstOrDefault(oi => oi.OrderItemId == itemDto.OrderItemId);

                    if (existingOrderItem == null)
                    {
                        throw new Exception($"Order item with ID {itemDto.OrderItemId} does not exist.");
                    }

                    if (itemDto.ProductID.HasValue)
                    {
                        var product = virtualShoppingStore.Products
                            .FirstOrDefault(p => p.ProductId == itemDto.ProductID.Value);

                        if (product == null)
                        {
                            throw new Exception($"Product with ID {itemDto.ProductID.Value} does not exist.");
                        }

                        existingOrderItem.ProductId = itemDto.ProductID.Value;
                        existingOrderItem.Price = product.Price;
                    }

                    if (itemDto.Quantity.HasValue)
                    {
                        if (existingOrderItem.Quantity != itemDto.Quantity.Value)
                        {
                            var difference = itemDto.Quantity.Value - existingOrderItem.Quantity;

                            var product = virtualShoppingStore.Products
                                .FirstOrDefault(p => p.ProductId == existingOrderItem.ProductId);

                            if (product == null || difference > product.StockQuantity)
                            {
                                throw new Exception($"Insufficient stock for product {product.ProductName}.");
                            }

                           
                            product.StockQuantity -= difference;
                            existingOrderItem.Quantity = itemDto.Quantity.Value;
                        }
                    }

                    if (itemDto.Price.HasValue)
                    {
                        existingOrderItem.Price = itemDto.Price.Value;
                    }
                }
            }

            
            virtualShoppingStore.SaveChanges();
        }


    }
}
