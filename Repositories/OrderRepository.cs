﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// SQLOrderRepository
    /// </summary>

    public class OrderRepository:IOrderRepository
    {

        private readonly VirtualShoppingStoreDbContext virtualShoppingStore;

        /// <summary>
        /// SQLOrderRepository constructor
        /// </summary>
        /// <param name="virtualShoppingStore"></param>

        public OrderRepository(VirtualShoppingStoreDbContext virtualShoppingStore)
        {
            this.virtualShoppingStore = virtualShoppingStore;
        }

        /// <summary>
        /// GetOrders
        /// </summary>
        /// <returns></returns>
        
        public List<Order> GetOrders()
        {
            var data = virtualShoppingStore.Orders.ToList();

            if (data.Count==0)
            {
                throw new CustomException("No order founds", 400);
                
            }

            return data;

        }

        /// <summary>
        /// GetOrder
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        
        public Order GetOrderById(int id)
        {

            var orderlist = virtualShoppingStore.Orders.FirstOrDefault(x => x.OrderId == id);

            if (orderlist==null)
            {
                throw new CustomException("No such data found with the given id",400);
            }
           
            return orderlist;
            
        }

        /// <summary>
        /// AddOrder
        /// </summary>
        /// <param name="addOrderDto"></param>
        /// <exception cref="Exception"></exception>
        public Order AddOrder(AddOrderDto addOrderDto)
        {

            var userExists = virtualShoppingStore.Users.Any(x => x.UserId == addOrderDto.UserId);

            if (!userExists)
            {
                throw new CustomException("User does not exist.",400);
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
                    throw new CustomException($"Product with ID {item.ProductID} does not exist.",400);
                }

                if (item.Quantity > product.StockQuantity)
                {
                    throw new CustomException($"Insufficient stock for product {product.ProductName}.",400);
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
        /// DeleteOrder
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="Exception"></exception>
        
        public void DeleteOrder(int id)
        {

            var order = virtualShoppingStore.Orders.FirstOrDefault(o => o.OrderId == id);

            if (order == null)
            {
                throw new CustomException("Order not found.", 400);
            }

            else
            {
                virtualShoppingStore.Orders.Remove(order);
                virtualShoppingStore.SaveChanges();
            }

        }

        /// <summary>
        /// UpdateOrder
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="UpdateOrderDto"></param>

        public void UpdateOrder(int orderId, UpdateOrderDto UpdateOrderDto)
        {
            
            var existingOrder = virtualShoppingStore.Orders.Include(o => o.Orderitems).FirstOrDefault(o => o.OrderId == orderId);

            if (existingOrder == null)
            {
                throw new CustomException("Order does not exist.",400);
            }

            if (UpdateOrderDto.OrderDate.HasValue)
            {
                existingOrder.OrderDate = UpdateOrderDto.OrderDate.Value;
            }

            if (UpdateOrderDto.StatusId.HasValue)
            {
                existingOrder.StatusId = UpdateOrderDto.StatusId.Value;
            }

            if (UpdateOrderDto.Orderitems != null)
            {

                foreach (var itemDto in UpdateOrderDto.Orderitems)
                {

                    var existingOrderItem = existingOrder.Orderitems.FirstOrDefault(oi => oi.OrderItemId == itemDto.OrderItemId);

                    if (existingOrderItem == null)
                    {
                        throw new CustomException($"Order item with ID {itemDto.OrderItemId} does not exist.",400);
                    }

                    if (itemDto.ProductID.HasValue)
                    {

                        var product = virtualShoppingStore.Products.FirstOrDefault(p => p.ProductId == itemDto.ProductID.Value);

                        if (product == null)
                        {
                            throw new CustomException($"Product with ID {itemDto.ProductID.Value} does not exist.",400);
                        }

                        existingOrderItem.ProductId = itemDto.ProductID.Value;
                        existingOrderItem.Price = product.Price;

                    }

                    if (itemDto.Quantity.HasValue)
                    {

                        if (existingOrderItem.Quantity != itemDto.Quantity.Value)
                        {

                            var difference = itemDto.Quantity.Value - existingOrderItem.Quantity;

                            var product = virtualShoppingStore.Products.FirstOrDefault(p => p.ProductId == existingOrderItem.ProductId);

                            if (product == null || difference > product.StockQuantity)
                            {
                                throw new CustomException($"Insufficient stock for product {product?.ProductName}.",400);
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