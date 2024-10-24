using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.OrderDto;
using VirtualShoppingStore.Repositories;

namespace VirtualShoppingStore.Controllers
{

    /// <summary>
    /// Handles HTTP requests related to orders in the VirtualShoppingStore application.
    /// </summary>
    
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {

        private readonly IOrderRepository orderRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderController"/> class.
        /// </summary>
        /// <param name="orderRepository">The repository used for managing orders.</param>

        public OrderController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        /// <summary>
        /// Retrieves a list of all orders.
        /// </summary>
        /// <returns>A list of all orders.</returns>
        /// <response code="200">Returns the list of orders.</response>
        /// <response code="404">If no orders are found.</response>
        /// <response code="500">If an internal server error occurs.</response>

        [HttpGet]

        public IActionResult GetOrder()
        {

            try
            {
                var data = orderRepository.GetOrders();
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
        /// Retrieves a list of orders for a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve orders for.</param>
        /// <returns>A list of orders for the specified user.</returns>
        /// <response code="200">Returns the list of orders for the specified user.</response>
        /// <response code="404">If no orders are found for the user.</response>
        /// <response code="500">If an internal server error occurs.</response>

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
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Deletes an order by its ID.
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        /// <returns>HTTP response indicating the result of the delete operation.</returns>
        /// <response code="200">If the order was successfully deleted.</response>
        /// <response code="400">If the delete operation failed due to a bad request.</response>
        /// <response code="500">If an internal server error occurs.</response>

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

        /// <summary>
        /// Places an order for a specific user by their user ID.
        /// </summary>
        /// <param name="userId">The ID of the user placing the order.</param>
        /// <returns>HTTP response indicating the result of the place order operation.</returns>
        /// <response code="200">If the order was successfully placed.</response>
        /// <response code="500">If an internal server error occurs.</response>
        /// <exception cref="Exception">Throws an exception if an error occurs.</exception>

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
                return BadRequest(ex.Message);
            }

            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }

}
