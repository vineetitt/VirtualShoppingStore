﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderDto;
using VirtualShoppingStore.Repositories;

namespace VirtualShoppingStore.Controllers
{

    /// <summary>
    /// OrderController class
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository orderRepository;

        /// <summary>
        /// OrderController constructor
        /// </summary>
        /// <param name="orderRepository"></param>

        public OrderController(IOrderRepository orderRepository)
        {

            this.orderRepository = orderRepository;

        }

        /// <summary>
        /// Get order details
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetOrder()
        {

            try
            {
                var data = orderRepository.GetOrders();

                if (data == null)
                {
                    return NotFound("No orders found");
                }

                return Ok(data);

            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Get Order By UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>

        [HttpGet]

        [Route("{userId:int}")]

        public IActionResult GetOrderById([FromRoute] int userId)
        {
            try
            {
                var orderlist = orderRepository.GetOrderById(userId);
                return Ok(orderlist);
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }

        }

        ///// <summary>
        ///// Add Order
        ///// </summary>
        ///// <param name="addOrderDto"></param>
        ///// <returns></returns>
        
        //[HttpPost]

        //public IActionResult AddOrder(AddOrderDto addOrderDto)
        //{
        //    try
        //    {
        //        var order= orderRepository.AddOrder(addOrderDto);
        //        return Ok(order);
        //    }

        //    catch (CustomException ex)
        //    {
        //        return StatusCode(ex.StatusCode, ex.Message);
        //    }

        //    catch (Exception ex)
        //    { 
        //        return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
        //    }

        //}

        /// <summary>
        /// Delete order by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete]

        [Route("{id}")]

        public IActionResult DeleteOrder(int id)
        {

            try
            {
                orderRepository.DeleteOrder(id);
                return Ok();
            }

            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        ///// <summary>
        ///// Update Order
        ///// </summary>
        ///// <param name="orderId"></param>
        ///// <param name="UpdateOrderDto"></param>
        ///// <returns></returns>
        
        //[HttpPatch("{orderId}")]

        //public IActionResult UpdateOrder(int orderId, UpdateOrderDto UpdateOrderDto)
        //{

        //    try
        //    {
        //        orderRepository.UpdateOrder(orderId, UpdateOrderDto);
        //        return Ok();
        //    }

        //    catch (CustomException ex)
        //    {
        //        return StatusCode(ex.StatusCode, ex.Message);
        //    }

        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}

        /// <summary>
        /// Place Order By User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [HttpPost]
        [Route("{userId:int}")]
        public IActionResult PlaceOrderByUserId(int userId)
        {
            try
            {
                var placeorder = orderRepository.PlaceOrderByUserId(userId);
                return Ok(placeorder);
            }
            catch(CustomException ex)
            {
                throw new Exception(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }

}
