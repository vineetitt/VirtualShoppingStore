using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderItemDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// class OrderItemRepository
    /// </summary>
    
    public class OrderItemRepository:IOrderItemRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStore;

        /// <summary>
        /// OrderItemRepository constructor
        /// </summary>
        /// <param name="virtualShoppingStore"></param>
        public OrderItemRepository(VirtualShoppingStoreDbContext virtualShoppingStore)
        {
            this.virtualShoppingStore = virtualShoppingStore;
        }

        /// <summary>
        /// GetOrderitemsByOrderId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public List<Orderitem> GetOrderitemsByOrderId(int id)
        {
            var orderExists= virtualShoppingStore.Orderitems.Any(x=>x.OrderItemId == id);
            if (!orderExists) {
                throw new Exception("order item not  found");
            }
            var orderitems= virtualShoppingStore.Orderitems.Where(y=>y.OrderItemId==id).ToList();
            return orderitems;
        }

        /// <summary>
        /// AddOrderitem
        /// </summary>
        /// <param name="addOrderItemDto"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddOrderitem(AddOrderItemDto addOrderItemDto)
        {
            var orderExists = virtualShoppingStore.Orders.Any(o => o.OrderId == addOrderItemDto.OrderID);
            if (!orderExists)
            {
                throw new Exception("Order not found.");
            }

            var productExists = virtualShoppingStore.Products.Any(p => p.ProductId == addOrderItemDto.ProductID);
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
        /// UpdateOrderItem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateOrderItem"></param>
        /// <exception cref="Exception"></exception>
        public Orderitem UpdateOrderItem(int id, UpdateOrderItemDto updateOrderItem )
        {

            var data = virtualShoppingStore.Orderitems.FirstOrDefault(oi => oi.OrderItemId == id);

            if (data == null)
            {
                throw new CustomException("order item not found",400);
            }

            data.Quantity = updateOrderItem.Quantity ?? data.Quantity;
            data.Price = updateOrderItem.Price ?? data.Price;
            data.ProductId = updateOrderItem.ProductID ?? data.ProductId;

            virtualShoppingStore.SaveChanges();

            return data;
        }

        /// <summary>
        /// DeleteOrderitem
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteOrderitem(int id)
        {

            var orderitem = virtualShoppingStore.Orderitems.FirstOrDefault(oi => oi.OrderItemId == id);

            if (orderitem == null)
            {
                throw new CustomException("Order item not found.",400);
            }
            else
            {
                virtualShoppingStore.Orderitems.Remove(orderitem);
                virtualShoppingStore.SaveChanges();
            }
        }

        /// <summary>
        /// ShowAllOrderItem
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Orderitem> ShowAllOrderItem()
        {

            var orderitems= virtualShoppingStore.Orderitems.ToList();

            if(orderitems.Count == 0)
            {
                throw new CustomException("No order items",400);
            }
            return orderitems;

        }

    }

}
