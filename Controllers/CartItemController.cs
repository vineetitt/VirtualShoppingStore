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



        /// <summary>
        /// Get All Users CartItems
        /// </summary>
        /// <returns></returns>
        [HttpGet]

        public IActionResult GetAllUsersCartItems()
        {
            try
            {
                var allcartitems = cartItemRepository.GetAllUsersCartItems();
                return Ok(allcartitems);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }

            }
        }


        /// <summary>
        /// Get Cart Item By User id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId}")]
        public IActionResult GetCartItemsByUserId(int userId)
        {
            try
            {
                var cartitem = cartItemRepository.GetCartItemsByUserId(userId);
                return Ok(cartitem);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }        
        }



        /// <summary>
        /// Add to cart By user id
        /// </summary>
        /// <param name="addToCartDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]

        public IActionResult AddItemToCart(AddToCartDto addToCartDto)
        {
            try
            {
                cartItemRepository.AddItemToCart(addToCartDto);
                return Ok("Added");
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
            
        }


        /// <summary>
        /// Decrease quantity by cartitem id
        /// </summary>
        /// <param name="cartitemId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        [HttpDelete]

        public IActionResult DeleteProductQuantityFromCartByCartItemId(int cartitemId)
        {
            try
            {
                cartItemRepository.DeleteProductQuantityFromCartByCartItemId(cartitemId);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }


        /// <summary>
        /// Update Cart By ProductId
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPatch]
        [Route("{int productId}")]
        public IActionResult UpdateCartByProductId(int productId, UpdateCartDto updateCartDto)
        {
            try
            {
                var updatedcartitem = cartItemRepository.UpdateCartByProductId(productId, updateCartDto);
                return Ok(updatedcartitem);
            }
            catch (Exception ex)
            {
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
