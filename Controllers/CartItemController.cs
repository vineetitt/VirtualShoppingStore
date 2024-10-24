using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CartItemDto;
using VirtualShoppingStore.Repositories;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VirtualShoppingStore.Controllers
{

    /// <summary>
    /// Controller for managing cart items in the virtual shopping store.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]

    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository cartItemRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartItemController"/> class.
        /// </summary>
        /// <param name="cartItemRepository">The repository used to manage cart items.</param>
        public CartItemController(ICartItemRepository cartItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
        }

        /// <summary>
        /// Retrieves non-placed cart items for a specified user.
        /// </summary>
        /// <param name="userId">The ID of the user whose cart items are to be retrieved.</param>
        /// <returns>A list of non-placed cart items for the specified user.</returns>

        [HttpGet]
        [Route("{userId}")]
        //[Authorize]
        public IActionResult GetCartByUserId(int userId)
        {

            try
            {
                var cartitem = cartItemRepository.GetCartByUserId(userId);
                return Ok(cartitem);

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
        /// Adds a product to the cart for a specified user.
        /// </summary>
        /// <param name="addToCartDto">The DTO containing details of the product to be added to the cart.</param>
        /// <returns>A status indicating the success or failure of the operation.</returns>



        [Authorize]
        [HttpPost]
        
        public IActionResult AddToCart(AddToCartDto addToCartDto)
        {

            try
            {
                cartItemRepository.AddToCart(addToCartDto);
                return Ok("Added");
            }

            catch(CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }

            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Deletes a cart item by its ID.
        /// </summary>
        /// <param name="cartitemId">The ID of the cart item to be deleted.</param>
        /// <returns>A status indicating the success or failure of the operation.</returns>

        [HttpDelete]

        public IActionResult DeleteFromCart(int cartitemId)
        {
            try
            {
                cartItemRepository.DeleteFromCart(cartitemId);
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
        /// Updates the quantity of a cart item by its ID.
        /// </summary>
        /// <param name="CartItemId">The ID of the cart item to be updated.</param>
        /// <param name="updateCartDto">The DTO containing the updated quantity and other details.</param>
        /// <returns>The updated cart item.</returns>

        [HttpPatch]
        [Route("{CartItemId:int}")]

        public IActionResult UpdateCart(int CartItemId, UpdateCartDto updateCartDto)
        {

            try
            {
                var updatedcart = cartItemRepository.UpdateCart(CartItemId, updateCartDto);
                return Ok(updatedcart);
            }

            catch (CustomException ex) 
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


