using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Repositories;
using VirtualShoppingStore.Models.DTO.OrderItemDto;
namespace VirtualShoppingStore.Controllers
{

    /// <summary>
    /// Handles HTTP requests related to order items in the VirtualShoppingStore application.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]

    public class OrderItemController : ControllerBase
    {

        private readonly IOrderItemRepository orderItemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderItemController"/> class.
        /// </summary>
        /// <param name="orderItemRepository">The repository used for managing order items.</param>

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
            this.orderItemRepository = orderItemRepository;
        }

        /// <summary>
        /// Retrieves a list of order items for a specified order ID.
        /// </summary>
        /// <param name="id">The ID of the order for which to retrieve order items.</param>
        /// <returns>A list of order items for the specified order ID.</returns>
        /// <response code="200">Returns the list of order items.</response>
        /// <response code="404">If no order items are found for the specified order ID.</response>

        [HttpGet]
        [Route("{id}")]

        public IActionResult GetOrderItemsByOrderId(int id)
        {
            try
            {

                var data = orderItemRepository.GetOrderItemsByOrderId(id);
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
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Adds a new order item to the system.
        /// </summary>
        /// <param name="addOrderItemDto">The data transfer object containing the details of the order item to add.</param>
        /// <returns>HTTP response indicating the result of the add operation.</returns>
        /// <response code="200">If the order item was added successfully.</response>
        /// <response code="400">If there is a problem with the input data.</response>

        [HttpPost]

        public IActionResult AddOrderItem(AddOrderItemDto addOrderItemDto)
        {

            try
            {
                orderItemRepository.AddOrderItem(addOrderItemDto);
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

        /// <summary>
        /// Retrieves a list of all order items in the system.
        /// </summary>
        /// <returns>A list of all order items.</returns>
        /// <response code="200">Returns the list of all order items.</response>
        
        [HttpGet("all")]

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
                return BadRequest(ex.Message);
            }

        }

    }

}
