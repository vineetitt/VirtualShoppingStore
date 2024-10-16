using Microsoft.AspNetCore.Mvc;
using VirtualShoppingStore.Models;
using VirtualShoppingStore.Models.DTO.CartItemDto;

namespace VirtualShoppingStore.Repositories
{
    /// <summary>
    /// Interface for managing cart items in the virtual shopping store.
    /// </summary>

    public interface ICartItemRepository
    {

        /// <summary>
        /// Retrieves cart items for a specific user that are not yet placed.
        /// </summary>
        /// <param name="id">The ID of the user whose cart items are to be retrieved.</param>
        /// <returns>An <see cref="IEnumerable{Cartitem}"/> collection of non-placed cart items for the specified user.</returns>
        /// <exception cref="CustomException">Thrown if no non-placed cart items are found for the user.</exception>

        public IEnumerable<Cartitem> GetCartByUserId(int id);

        /// <summary>
        /// Adds a product to the cart for a specific user.
        /// </summary>
        /// <param name="addToCartDto">The DTO containing the details of the product to be added to the cart.</param>
        /// <exception cref="CustomException">Thrown if the cart item data is null, the user ID or product ID is invalid, or if stock is insufficient.</exception>

        public void AddToCart(AddToCartDto addToCartDto);

        /// <summary>
        /// Deletes a cart item by its ID.
        /// </summary>
        /// <param name="cartitemId">The ID of the cart item to be deleted.</param>
        /// <exception cref="CustomException">Thrown if the cart item or its associated product is not found.</exception>

        public void DeleteFromCart(int cartitemId);

        /// <summary>
        /// Updates the quantity of a cart item by its ID.
        /// </summary>
        /// <param name="cartitemId">The ID of the cart item to be updated.</param>
        /// <param name="updateCartDto">The DTO containing updated quantity and other details.</param>
        /// <returns>The updated <see cref="Cartitem"/>.</returns>
        /// <exception cref="CustomException">Thrown if the cart item or its associated product is not found.</exception>
        public Cartitem UpdateCart(int cartitemId, UpdateCartDto updateCartDto);

    }

}
