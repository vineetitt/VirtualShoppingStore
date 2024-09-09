using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Repositories;
using VirtualShoppingStore.Models.DTO.OrderItemDto;
namespace VirtualShoppingStore.Controllers
{

    /// <summary>
    /// 
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]

    public class OrderItemController : ControllerBase
    {

        private readonly IOrderItemRepository orderItemRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderItemRepository"></param>

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }

        /// <summary>
        /// Get Order Item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        
        [HttpGet]
        [Route("{id}")]

        public IActionResult GetOrderitemsByOrderId(int id)
        {
            try
            {
                var data = orderItemRepository.GetOrderitemsByOrderId(id);
                if (data == null || !data.Any())
                {
                    return NotFound($"No order items found for order ID {id}.");
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
        /// Add order Item
        /// </summary>
        /// <param name="addOrderItemDto"></param>
        /// <returns></returns>
        [HttpPost]

        public IActionResult AddOrderitem(AddOrderItemDto addOrderItemDto)
        {
            try
            {
                orderItemRepository.AddOrderitem(addOrderItemDto);
                return Ok();
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
        /// Update Order Item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateOrderItem"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdateOrderitem(int id, UpdateOrderItemDto updateOrderItem)
        {
            try
            {
                var updatedorderitem = orderItemRepository.UpdateOrderItem(id, updateOrderItem);
                return Ok(updatedorderitem);
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
        /// Delete Order Item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteOrderitem(int id)
        {
            try
            {
                orderItemRepository.DeleteOrderitem(id);
                return Ok();
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
        /// Show All order itemsx
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ShowAllOrderItems()
        {
            try
            {
                var orderitems = orderItemRepository.ShowAllOrderItem();
                return Ok(orderitems);
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
    }
}
