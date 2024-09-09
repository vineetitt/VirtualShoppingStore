using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CartItemDto;
using VirtualShoppingStore.Repositories;

namespace VirtualShoppingStore.Controllers
{

    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class CartItemController : ControllerBase
    {
        private readonly ICartItemRepository cartItemRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartItemRepository"></param>
        public CartItemController(ICartItemRepository cartItemRepository)
        {
            this.cartItemRepository = cartItemRepository;
        }

        ///// <summary>
        ///// Get All Cart 
        ///// </summary>
        ///// <returns></returns>

        //[HttpGet]

        //public IActionResult GetAllCartItems()
        //{

        //    try
        //    {
        //        var allcartitems = cartItemRepository.GetAllCartItems();
        //        return Ok(allcartitems);
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
        /// Get non placed CartItem By User id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        
        [HttpGet]

        [Route("{userId}")]

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
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Add product to cart By user id 
        /// </summary>
        /// <param name="addToCartDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        
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
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
            
        }

        /// <summary>
        /// Delete From Cart By CartItemId
        /// </summary>
        /// <param name="cartitemId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

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
                return StatusCode(500 ,$"An unexpected error occurred: {ex.Message}");
            }

        }

        /// <summary>
        /// Update CartItem Quantity by cartitemId
        /// </summary>
        /// <param name="CartItemId"></param>
        /// <param name="updateCartDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        
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
