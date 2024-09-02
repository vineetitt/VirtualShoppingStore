using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderDto;
using VirtualShoppingStore.Repositories;

namespace VirtualShoppingStore.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;


        /// <summary>
        /// 
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

                if (data != null)
                {
                    return Ok(data);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        /// <summary>
        /// Get Order By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public IActionResult ShowOrderById(int id)
        {
            try
            {
                var orderlist = orderRepository.ShowOrderById(id);
                return Ok(orderlist);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }


        }




        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="addOrderDto"></param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult AddOrders(AddOrderDto addOrderDto)
        {
            try
            {
                var order= orderRepository.AddOrder(addOrderDto);
                return Ok(order);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }

        }



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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Update Order
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="patchOrderDto"></param>
        /// <returns></returns>
        [HttpPatch("{orderId}")]
        public IActionResult PatchOrder(int orderId, PatchOrderDto patchOrderDto)
        {
            try
            {
                orderRepository.PatchOrder(orderId, patchOrderDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        
    }



}
