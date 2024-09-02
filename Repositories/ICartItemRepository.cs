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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Cartitem> GetAllUsersCartItems();




        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Cartitem GetCartItemsByUserId(int id);



        /// <summary>
        /// 
        /// </summary>
        /// <param name="addToCartDto"></param>
        public void AddItemToCart(AddToCartDto addToCartDto);




        /// <summary>
        /// 
        /// </summary>
        /// <param name="cartitemId"></param>
        public void DeleteProductQuantityFromCartByCartItemId(int cartitemId);


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Cartitem UpdateCartByProductId(int productId, UpdateCartDto updateCartDto);


    }
}
