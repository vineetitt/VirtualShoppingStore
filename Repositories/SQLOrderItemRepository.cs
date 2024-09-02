using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderItemDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class SQLOrderItemRepository:IOrderItemRepository
    {
        private readonly VirtualShoppingStoreDbContext virtualShoppingStore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualShoppingStore"></param>
        public SQLOrderItemRepository(VirtualShoppingStoreDbContext virtualShoppingStore)
        {
            this.virtualShoppingStore = virtualShoppingStore;
        }

 


        /// <summary>
        /// 
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
        /// 
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
                throw new Exception("Product not found.");
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
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateOrderItem"></param>
        /// <exception cref="Exception"></exception>
        public Orderitem UpdateOrderItem(int id, PatchOrderItemDto updateOrderItem )
        {
           

            var data = virtualShoppingStore.Orderitems.FirstOrDefault(oi => oi.OrderItemId == id);

            if (data == null)
            {
                throw new Exception("order item not found");
            }

            data.Quantity = updateOrderItem.Quantity ?? data.Quantity;
            data.Price = updateOrderItem.Price ?? data.Price;
            data.ProductId = updateOrderItem.ProductID ?? data.ProductId;

            virtualShoppingStore.SaveChanges();

            return data;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteOrderitem(int id)
        {
            

            var orderitem = virtualShoppingStore.Orderitems.FirstOrDefault(oi => oi.OrderItemId == id);

            if (orderitem == null)
            {
                throw new Exception("Order item not found.");
            }
            else
            {
                virtualShoppingStore.Orderitems.Remove(orderitem);
                virtualShoppingStore.SaveChanges();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<Orderitem> ShowAllOrderItem()
        {
           

            var orderitems= virtualShoppingStore.Orderitems.ToList();

            if(orderitems.Count == 0)
            {
                throw new Exception("No order items");
            }
            return orderitems;
            
            
            
        }
    }
}
