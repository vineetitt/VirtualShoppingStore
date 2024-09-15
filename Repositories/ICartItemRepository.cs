using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CartItemDto;

namespace VirtualShoppingStore.Repositories
{

    /// <summary>
    /// 
    /// </summary>
    
    public interface ICartItemRepository
    {

        ///// <summary>
        ///// Get All Cart Items
        ///// </summary>
        ///// <returns></returns>
        
        //public List<Cartitem> GetAllCartItems();

        /// <summary>
        /// Get CartItems By UserId
        /// </summary>
        /// <returns></returns>
        
        public IEnumerable<Cartitem> GetCartByUserId(int id);

        /// <summary>
        /// Add Item To Cart
        /// </summary>
        /// <param name="addToCartDto"></param>
        
        public void AddToCart(AddToCartDto addToCartDto);

        /// <summary>
        /// Delete From Cart By CartitemId
        /// </summary>
        /// <param name="cartitemId"></param>
        
        public void DeleteFromCart(int cartitemId);

        ///// <summary>
        ///// Update Cart
        ///// </summary>
        ///// <returns></returns>

        //public Cartitem UpdateCart(int cartItemId, UpdateCartDto updateCartDto);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartitemId"></param>
        /// <param name="updateCartDto"></param>
        /// <returns></returns>
        public Cartitem UpdateCart(int cartitemId, UpdateCartDto updateCartDto);





    }

}
